using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Threading
{
    public interface IWorkerQueue : IDisposable
    {
        void Enqueue(WorkItem work, Action<RunWorkerCompletedEventArgs>? onCompleting);
        void Enqueue(IEnumerable<WorkItem> work, Action<RunWorkerCompletedEventArgs>? onCompleting);
    }

    /// <summary>
    /// Performs work on a Background thread.
    /// </summary>
    /// <remarks>
    /// This is attempting to address issues with original.
    /// </remarks>
    public class WorkerQueue : IWorkerQueue
    {
        protected ConcurrentQueue<WorkItem> WorkQueue { get; } = new ConcurrentQueue<WorkItem>();
        public Int32 WorkAdded { get; protected set; } = 0;
        public Int32 WorkComplete { get; protected set; } = 0;
        public Int32 WorkPending { get { return WorkQueue.Count; } }
        public Boolean IsBusy { get { return backgroundWorker.IsBusy; } }

        /// <summary>
        /// Used to pass the Invoke method from a Control on the main UI thread
        /// </summary>
        /// <remarks>
        /// This really problematic, but it gets around the Cross Threading problem.
        /// By passing Invoke method of any control on the UI thread, the call then can be made on the UI thread.
        /// Why can't I find out what thread created this instance and invoke events on that thread?
        /// </remarks>
        public Action<Action>? InvokeUsing { get; set; }

        // Because I don't know how to setup Async correctly
        protected BackgroundWorker backgroundWorker { get; } = new BackgroundWorker()
        { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

        public WorkerQueue() : base()
        {
            backgroundWorker.DoWork += RunWorker;
            backgroundWorker.RunWorkerCompleted += RunWorkerCompleted;
            backgroundWorker.ProgressChanged += OnProgressChanged;
        }

        /// <summary>
        /// Adds a WorkItem to the queue. Starts the queue if not running.
        /// </summary>
        /// <param name="work">The Work Item to add</param>
        /// <param name="onCompleting">Optional: the Action to perform when this work items is completed.</param>
        public virtual void Enqueue(WorkItem work, Action<RunWorkerCompletedEventArgs>? onCompleting = null)
        {
            if (onCompleting is not null) { work.Completing += Work_Completing; }

            WorkQueue.Enqueue(work);
            WorkAdded++;

            if (!backgroundWorker.IsBusy) { backgroundWorker.RunWorkerAsync(); }

            void Work_Completing(object? sender, RunWorkerCompletedEventArgs e)
            {
                work.Completing -= Work_Completing;
                onCompleting(e);
            }
        }

        /// <summary>
        /// Adds a set of items to the queue. Starts the queue if not running.
        /// </summary>
        /// <param name="work"></param>
        public virtual void Enqueue(IEnumerable<WorkItem> work, Action<RunWorkerCompletedEventArgs>? onCompleting = null)
        {
            WorkItem? lastWork = work.LastOrDefault();
            Exception? firstException = null;
            WorkItem? firstExceptionItem = null;

            foreach (WorkItem item in work)
            {
                if (onCompleting is not null) { item.Completing += Item_Completing; }
                WorkQueue.Enqueue(item);
                WorkAdded++;
            }

            if (!backgroundWorker.IsBusy) { backgroundWorker.RunWorkerAsync(); }

            void Item_Completing(object? sender, RunWorkerCompletedEventArgs e)
            {
                if (sender is WorkItem item)
                {
                    item.Completing -= Item_Completing;
                    if (firstException is null && e.Error is not null)
                    {
                        firstException = e.Error;
                        firstExceptionItem = item;
                    }

                    if (ReferenceEquals(item, lastWork) && onCompleting is not null)
                    { onCompleting(new RunWorkerCompletedEventArgs(lastWork, firstException, e.Cancelled)); }
                }
            }
        }

        /// <summary>
        /// Triggers a Cancel on the Background worker.
        /// </summary>
        /// <remarks>
        /// The current item being worked will continue.
        /// All other items in the queue will be canceled.
        /// </remarks>
        public virtual void Cancel()
        { backgroundWorker.CancelAsync(); }

        /// <summary>
        /// Primary Worker Loop of the Queue.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RunWorker(object? sender, DoWorkEventArgs e)
        {
            WorkItem? workItem;

            while (WorkQueue.TryDequeue(out workItem) && workItem is not null)
            {
                try
                {
                    if (backgroundWorker.CancellationPending || workItem.IsCanceling())
                    {
                        workItem.OnCompleting(null, true, InvokeUsing);
                        backgroundWorker.ReportProgress(100, workItem);
                    }
                    else
                    {
                        workItem.ProgressChanged += WorkItem_OnProgress;
                        backgroundWorker.ReportProgress(0, workItem);

                        workItem.DoWork();
                        workItem.OnCompleting(null, false, InvokeUsing);
                    }
                }
                catch (Exception ex)
                {
                    workItem.OnCompleting(ex, false, InvokeUsing);
                    backgroundWorker.ReportProgress(100, workItem);
                }
                finally
                {
                    workItem.ProgressChanged -= WorkItem_OnProgress;
                    WorkComplete++;
                    backgroundWorker.ReportProgress(0, workItem);
                }
            }

            void WorkItem_OnProgress(object? sender, ProgressChangedEventArgs e)
            { backgroundWorker.ReportProgress(e.ProgressPercentage, workItem); }
        }

        /// <summary>
        /// Progress has changed.
        /// </summary>
        /// <remarks>
        /// Overall progress is computed by treating each WorkItem as 100 points.
        /// Calculations of completed is computed from the last time the queue was empty.
        /// Total points is the number items added to the queue * 100.
        /// Completed points is the number of items completed * 100
        /// If the WorkItem support ProgressChanged, then the progress is added to the Completed.
        /// </remarks>
        public event EventHandler<WorkerProgressChangedEventArgs>? ProgressChanged;

        /// <summary>
        /// Responds to the Background Worker Progress changed and throws the event up to the next level.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// This receives the progress of the current WorkItem.
        /// The overall progress is reported up based on the computation described in ProgressChanged.
        /// </remarks>
        protected void OnProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            if (ProgressChanged is EventHandler<WorkerProgressChangedEventArgs> handler)
            {
                if (e.UserState is WorkItem workItem)
                {
                    Double progress = 0;

                    if (WorkAdded == 0) { progress = 0; }
                    else { progress = ((Double)WorkComplete * 100.0 + (Double)e.ProgressPercentage) / ((Double)WorkAdded * 100); }

                    if (InvokeUsing is not null)
                    { InvokeUsing(() => handler(sender, new WorkerProgressChangedEventArgs(workItem.WorkName, (int)(progress * 100.0)))); }
                    else { handler(sender, new WorkerProgressChangedEventArgs(workItem.WorkName, (int)(progress * 100.0))); }
                }
            }
        }

        /// <summary>
        /// Background worker has completed and the queue is expected to be empty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (WorkQueue.Count == 0)
            {
                WorkAdded = 0;
                WorkComplete = 0;
            }

            if (e.Error is not null) { throw e.Error; }
        }

        public event EventHandler<WorkerExceptionEventArgs>? WorkException;
        protected void OnWorkException(Exception ex)
        {
            if (WorkException is EventHandler<WorkerExceptionEventArgs> handler)
            {
                if (InvokeUsing is not null)
                { InvokeUsing(() => handler(this, new WorkerExceptionEventArgs() { Exception = ex })); }
                else { handler(this, new WorkerExceptionEventArgs() { Exception = ex }); }
            }
        }

        #region IDisposable
        private Boolean disposedValue;
        protected virtual void Dispose(Boolean disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    WorkQueue.Clear();
                    backgroundWorker.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
