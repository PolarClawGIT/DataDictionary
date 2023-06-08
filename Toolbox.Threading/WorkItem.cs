using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Threading
{
    public class WorkItem
    {
        public required String WorkName { get; init; }

        public WorkItem() : base() { }
    }

    public class WorkBackgroundItem : WorkItem
    {
        public Action OnStarting { get; init; } = () => { };
        public Action<RunWorkerCompletedEventArgs> OnCompleting { get; init; } = (e) => { };

        public virtual void DoWork() { }
        public WorkBackgroundItem() : base() { }
    }

    public class WorkParellelItem<T> : WorkItem
    {
        public required IReadOnlyList<T> Tasks { get; init; }
        public Int32 MaxDegreeOfParallelism { get; init; } = -1;

        public WorkParellelItem() : base() { }

        public virtual void DoWork(T task) { }
    }

    public class WorkForegroundItem : WorkItem
    {
        public WorkForegroundItem() : base() { }
        public virtual void DoWork() { }
    }
}
