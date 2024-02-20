using DataDictionary.DataLayer;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
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
        /// Creates a WorkItem for Loading Extended Properties
        /// </summary>
        /// <typeparam name="TDbItem"></typeparam>
        /// <param name="workName"></param>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        WorkItem CreateWork<TDbItem>(String workName, IBindingTable<DbExtendedPropertyItem> target, IBindingTable<TDbItem> source)
            where TDbItem : class, IBindingTableRow, IDbExtendedProperty;

        /// <summary>
        /// Create a WorkItem for loading a Data Object.
        /// </summary>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        WorkItem CreateLoad<TCollection>(TCollection target)
            where TCollection : IBindingTable, IReadData;

        /// <summary>
        /// Create a WorkItem for loading a Data Object by Key.
        /// </summary>
        /// <typeparam name="TCollection"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="target"></param>
        /// <param name="targetKey"></param>
        /// <returns></returns>
        WorkItem CreateLoad<TCollection, TKey>(TCollection target, TKey targetKey)
            where TKey : IKey
            where TCollection : IBindingTable, IReadData<TKey>;

        /// <summary>
        /// Create a WorkItem for loading a Data Object.
        /// </summary>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        WorkItem CreateSave<TCollection>(TCollection target)
            where TCollection : IBindingTable, IWriteData;

        /// <summary>
        /// Create a WorkItem for loading a Data Object by Key.
        /// </summary>
        /// <typeparam name="TCollection"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="target"></param>
        /// <param name="targetKey"></param>
        /// <returns></returns>
        WorkItem CreateSave<TCollection, TKey>(TCollection target, TKey targetKey)
            where TKey : IKey
            where TCollection : IBindingTable, IWriteData<TKey>;

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
        public Boolean IsCanceling { get { return Connection.HasException; } }

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
        public WorkItem CreateWork<TDbItem>(String workName, IBindingTable<DbExtendedPropertyItem> target, IBindingTable<TDbItem> source)
            where TDbItem : class, IBindingTableRow, IDbExtendedProperty
        {
            Action<int, int> progress = (x, y) => { };

            WorkItem result = new WorkItem()
            {
                WorkName = workName,
                DoWork = Work,
                IsCanceling = () => Connection.HasException
            };
            progress = result.OnProgressChanged;

            workItems.Add(result, new WorkState() { IsComplete = false, Ex = null }); ;
            result.Completing += WorkItem_Completing;

            return result;

            void Work()
            {
                Int32 toDo = source.Count();
                Int32 complete = 0;

                if (Connection is IConnection)
                {
                    foreach (TDbItem item in source)
                    {
                        Command command = item.PropertyCommand(Connection);
                        try { target.Load(Connection.ExecuteReader(command)); }
                        catch (Exception ex)
                        {
                            ex.Data.Add("Command", "MSSQL Extended Property");
                            foreach (DbParameter parameter in command.Parameters)
                            {
                                if (parameter.Value is not null)
                                { ex.Data.Add(parameter.ParameterName, parameter.Value.ToString()); }
                                else { ex.Data.Add(parameter.ParameterName, "(Null)"); }
                            }

                            throw;
                        }

                        complete++;
                        progress(complete, toDo);
                    }
                }
                else { throw new ArgumentNullException(nameof(Connection)); }
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

        /// <inheritdoc/>
        public WorkItem CreateLoad<TCollection>(TCollection target)
            where TCollection : IBindingTable, IReadData
        {
            return this.CreateWork(
                workName: String.Format("Load {0}",target.BindingName),
                target: target,
                command: target.LoadCommand);
        }

        /// <inheritdoc/>
        public WorkItem CreateLoad<TCollection, TKey>(TCollection target, TKey targetKey)
            where TKey : IKey
            where TCollection : IBindingTable, IReadData<TKey>
        {
            return this.CreateWork(
                workName: String.Format("Load {0}", target.BindingName),
                target: target,
                command: (conn) => target.LoadCommand(conn, targetKey));
        }

        /// <inheritdoc/>
        public WorkItem CreateSave<TCollection>(TCollection target)
            where TCollection : IBindingTable, IWriteData
        {
            return this.CreateWork(
                workName: String.Format("Save {0}", target.BindingName),
                command: target.SaveCommand);
        }

        /// <inheritdoc/>
        public WorkItem CreateSave<TCollection, TKey>(TCollection target, TKey targetKey)
            where TKey : IKey
            where TCollection : IBindingTable, IWriteData<TKey>
        {
            return this.CreateWork(
                workName: String.Format("Load {0}", target.BindingName),
                command: (conn) => target.SaveCommand(conn, targetKey));
        }

    }

    /// <summary>
    /// Helper Method for DatabaseWork
    /// </summary>
    public static class WorkItem_Extension
    {
        /// <summary>
        /// Creates a List out of a Single Work Item.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> ToList(this WorkItem source)
        { return new List<WorkItem> { source }.AsReadOnly(); }

        /// <summary>
        /// Creates a List out of a Single DataTable
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IReadOnlyList<DataTable> ToList(this DataTable source)
        { return new List<DataTable> { source }.AsReadOnly(); }

        /// <summary>
        /// Create a WorkItem for Clearing a Data Object (remove all).
        /// </summary>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="target"></param>
        /// <param name="workName"></param>
        /// <returns></returns>
        public static WorkItem CreateClear<TCollection>(this TCollection target, String workName)
            where TCollection : IBindingTable
        { return new WorkItem() { WorkName = workName, DoWork = () => target.Clear() }; }
    }
}
