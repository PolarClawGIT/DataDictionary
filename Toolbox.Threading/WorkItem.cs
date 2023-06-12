using System.ComponentModel;

namespace Toolbox.Threading
{
    namespace WorkItem
    {
        public abstract class WorkBase
        {
            public required String WorkName { get; init; }

            public event EventHandler? WorkStarting;
            public event EventHandler? WorkCompleting;
            public event EventHandler<Exception>? WorkException;

            public virtual void OnStarting()
            { if (WorkStarting is EventHandler onEvent) { onEvent(this, EventArgs.Empty); } }

            public virtual void OnCompleting()
            { if (WorkCompleting is EventHandler onEvent) { onEvent(this, EventArgs.Empty); } }

            public virtual void OnException(Exception ex)
            { if (WorkException is EventHandler<Exception> onEvent) { onEvent(this, ex); } }

            public WorkBase() : base() { }
        }

        public abstract class SynchronousWork : WorkBase
        {
            public Action OnDoWork { get; init; } = () => { };
            public virtual void DoWork() { OnDoWork(); }
        }

        public class BackgroundWork : SynchronousWork
        {
            public RunWorkerCompletedEventArgs? CompletedResult { get; set; }
        }

        public class ForegroundWork : SynchronousWork
        { }

        public class BatchWork : SynchronousWork
        {
            public List<SynchronousWork> WorkItems { get; } = new List<SynchronousWork>();
        }

        public abstract class AsynchronousWork : WorkBase { }

        public class ParellelWork<T> : AsynchronousWork
        {
            public required IReadOnlyList<T> Tasks { get; init; }
            public Int32 MaxDegreeOfParallelism { get; init; } = -1;

            public virtual void DoWork(T task) { }
        }

    }

}
