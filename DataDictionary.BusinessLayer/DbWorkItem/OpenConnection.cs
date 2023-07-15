using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.DbContext;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    interface IDbWorkItem: IWorkItem
    { }

    class OpenConnection : WorkItem
    {
        public IConnection Connection { get; private set; }
        Dictionary<IDbWorkItem, (Boolean IsComplete, Exception? Ex)> children = new Dictionary<IDbWorkItem, (bool IsComplete, Exception? Ex)>();
        public override string WorkName { get; init; } = "Open Connection";

        public OpenConnection(IContext context) : base()
        {   Connection = context.CreateConnection(); }

        public void Dependency(IDbWorkItem item)
        {
            children.Add(item, (false, null));
            item.Completing += WorkItem_Completing;
        }

        private void WorkItem_Completing(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (sender is IDbWorkItem item && children.ContainsKey(item))
            {
                (Boolean IsComplete, Exception? Ex) child = children[item];
                child.IsComplete = true;
                child.Ex = e.Error;
                item.Completing -= WorkItem_Completing;
            }

            if (children.Count(w => w.Value.IsComplete) == children.Count)
            {
                if (Connection.HasException) { Connection.Rollback(); }
                else { Connection.Commit(); }
            }
        }
        
        protected override void Work()
        { Connection.Open(); }
    }
}
