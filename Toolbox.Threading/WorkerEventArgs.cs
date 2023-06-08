using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Threading
{
    public class WorkerEventArgs : EventArgs
    {
        public WorkItem Item { get; private set; }
        public WorkerEventArgs(WorkItem item) : base()
        { Item = item; }
    }
}
