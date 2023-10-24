using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    /// <summary>
    /// Interface for creating Database Work
    /// </summary>
    public interface IDatabaseWork
    {
        /// <summary>
        /// Creates a WorkItem that executes ExecuteNonQuery using the connection.
        /// </summary>
        /// <param name="workName">name to display while working</param>
        /// <param name="command">the function that returns the command to execute</param>
        /// <returns>WorkItem with the database connection handing wired up</returns>
        WorkItem CreateWork(String workName, Func<IConnection, Command> command);

        /// <summary>
        /// Creates a WorkItem that executes ExecuteReader using the connection.
        /// </summary>
        /// <param name="workName">name to display while working</param>
        /// <param name="target">target object to load the result to</param>
        /// <param name="command">the function that returns the command to execute</param>
        /// <returns>WorkItem with the database connection handing wired up</returns>
        WorkItem CreateWork(String workName, IBindingTable target, Func<IConnection, Command> command);

        /// <summary>
        /// Creates the WorkItem that opens the Connection to the Database.
        /// </summary>
        /// <returns></returns>
        WorkItem OpenConnection();

        /// <summary>
        /// Returns if the Work Items are bing canceled.
        /// </summary>
        Boolean IsCanceling { get; }

    }

    /// <summary>
    /// Manages the Database Work Items
    /// </summary>
    public class DatabaseWork : IDatabaseWork
    {
        class WorkState
        {
            public Boolean IsComplete { get; set; }
            public Exception? Ex { get; set; }
        }

        /// <summary>
        /// The Connection used by the work items (Set by OpenConnection)
        /// </summary>
        public IConnection Connection { get; internal set; }

        Dictionary<IWorkItem, WorkState> workItems = new Dictionary<IWorkItem, WorkState>();

        /// <summary>
        /// Work item that opens the connection on the ModelData context.
        /// </summary>
        public DatabaseWork() : base()
        { Connection = ModelData.ModelContext.CreateConnection(); }

        /// <summary>
        /// Work item that opens the connection on the specified context.
        /// </summary>
        /// <param name="context"></param>
        public DatabaseWork(IContext context) : base()
        { Connection = context.CreateConnection(); }

        /// <inheritdoc/>
        public Boolean IsCanceling { get { return !Connection.HasException; } }

        /// <inheritdoc/>
        public WorkItem CreateWork(String workName, Func<IConnection, Command> command)
        {
            WorkItem result = new WorkItem()
            {
                WorkName = workName,
                DoWork = ExecuteNonQuery,
                IsCanceling = () => Connection.HasException
            };

            workItems.Add(result, new WorkState() { IsComplete = false, Ex = null }); ;
            result.Completing += WorkItem_Completing;

            return result;

            void ExecuteNonQuery()
            {
                Command execute = command(Connection);

                try { Connection.ExecuteNonQuery(execute); }
                catch (Exception ex) { ex.Data.Add("Command", execute.CommandText); throw; }
            }
        }

        /// <inheritdoc/>
        public WorkItem CreateWork(String workName, IBindingTable target, Func<IConnection, Command> command)
        {
            WorkItem result = new WorkItem()
            {
                WorkName = workName,
                DoWork = ExecuteReader,
                IsCanceling = () => Connection.HasException
            };

            workItems.Add(result, new WorkState() { IsComplete = false, Ex = null }); ;
            result.Completing += WorkItem_Completing;

            return result;

            void ExecuteReader()
            {
                Command execute = command(Connection);

                try { target.Load(Connection.ExecuteReader(execute)); }
                catch (Exception ex) { ex.Data.Add("Command", execute.CommandText); throw; }
            }
        }

        /// <inheritdoc/>
        public WorkItem OpenConnection()
        {
            WorkItem result = new WorkItem()
            {
                WorkName = "Open Connection",
                DoWork = ExecuteOpen,
                IsCanceling = () => Connection.HasException
            };

            workItems.Add(result, new WorkState() { IsComplete = false, Ex = null }); ;
            result.Completing += WorkItem_Completing;

            return result;
        }

        private void WorkItem_Completing(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (sender is IWorkItem item && workItems.ContainsKey(item))
            {
                workItems[item].IsComplete = true;
                workItems[item].Ex = e.Error;
                item.Completing -= WorkItem_Completing;
            }

            if (workItems.Count(w => w.Value.IsComplete) == workItems.Count)
            {
                if (Connection is IConnection)
                {
                    if (Connection.HasException) { Connection.Rollback(); }
                    else { Connection.Commit(); }
                }
                else { throw new ArgumentNullException(nameof(Connection)); }
            }
        }

        void ExecuteOpen()
        {
            if (Connection is IConnection)
            { Connection.Open(); }
            else { throw new ArgumentNullException(nameof(Connection)); }
        }
    }
}
