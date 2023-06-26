using System.ComponentModel;

namespace Toolbox.Threading
{
    namespace WorkItem
    {

        public interface IWorkItem
        {
            String WorkName { get; init; }

            event EventHandler? WorkStarting;
            event EventHandler? WorkCompleting;
            event EventHandler<Exception>? WorkException;

            void OnStarting();
            void OnCompleting();
            void OnException(Exception ex);

            void DoWork();
        }


        public abstract class WorkBase : IWorkItem
        {
            public required String WorkName { get; init; }
            public Action OnDoWork { get; init; } = () => { };

            public event EventHandler? WorkStarting;
            public event EventHandler? WorkCompleting;
            public event EventHandler<Exception>? WorkException;

            public WorkBase() : base() { }

            public virtual void DoWork() { OnDoWork(); }

            public virtual void OnStarting()
            { if (WorkStarting is EventHandler onEvent) { onEvent(this, EventArgs.Empty); } }

            public virtual void OnCompleting()
            { if (WorkCompleting is EventHandler onEvent) { onEvent(this, EventArgs.Empty); } }

            public virtual void OnException(Exception ex)
            { if (WorkException is EventHandler<Exception> onEvent) { onEvent(this, ex); } }
        }

        public abstract class SynchronousWork : WorkBase
        {

        }

        public class BackgroundWork : SynchronousWork
        {
            public RunWorkerCompletedEventArgs? CompletedResult { get; set; }
        }

        public class ForegroundWork : SynchronousWork
        { }

        public class BatchWork : SynchronousWork
        {
            public required IEnumerable<IWorkItem> WorkItems { get; init; }
        }

        public abstract class AsynchronousWork : WorkBase { }

        public class ParellelWork : AsynchronousWork
        {
            public required IEnumerable<IWorkItem> WorkItems { get; init; }
            public Int32 MaxDegreeOfParallelism { get; init; } = -1;
        }

    }

}
