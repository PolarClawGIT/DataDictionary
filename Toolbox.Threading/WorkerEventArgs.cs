using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading.WorkItem;

namespace Toolbox.Threading
{
    public class WorkerEventArgs : EventArgs
    {
        public WorkItem.WorkBase Item { get; private set; }
        public WorkerEventArgs(WorkItem.WorkBase item) : base()
        { Item = item; }
    }


    public class WorkerProgressChangedEventArgs : EventArgs
    {
        public String ProgressText { get; private set; } = "(ideal)";
        public Int32 ProgressPercent { get; protected set; } = 0;

        public WorkerProgressChangedEventArgs() : base() { }
        public WorkerProgressChangedEventArgs(String status) : this() { ProgressText = status; }
        public WorkerProgressChangedEventArgs(Int32 progress) : this()
        {
            if (progress <= 0) { ProgressPercent = 0; ProgressText = "(ideal)"; }
            else if (progress > 0 && progress < 100) { ProgressPercent = progress; ProgressText = "(working)"; }
            else if (progress >= 100) { ProgressPercent = 100; ProgressText = "(done)"; }
        }
        public WorkerProgressChangedEventArgs(String status, Int32 progress) : this(status)
        {
            if (progress <= 0) { ProgressPercent = 0; }
            else if (progress > 0 && progress < 100) { ProgressPercent = progress; }
            else if (progress >= 100) { ProgressPercent = 100; }
        }

        public WorkerProgressChangedEventArgs(IWorkItem item) : this(item.WorkName) { }
        public WorkerProgressChangedEventArgs(IWorkItem item, Int32 progress) : this(item.WorkName, progress) { }
    }
}
