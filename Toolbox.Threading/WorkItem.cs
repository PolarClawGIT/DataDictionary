using System.ComponentModel;

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

        public Action OnDoWork { get; init; } = () => { };
        public virtual void DoWork() { OnDoWork(); }

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

        public Action OnDoWork { get; init; } = () => { };
        public virtual void DoWork() { OnDoWork(); }
    }
}
