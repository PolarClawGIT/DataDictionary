using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Threading
{
    public class WorkerQueue : IDisposable
    {
        protected ConcurrentQueue<WorkItem> WorkQueue { get; } = new ConcurrentQueue<WorkItem>();
        protected BackgroundWorker BackgroundWorker { get; } = new BackgroundWorker();
        public Int32 WorkAdded { get; protected set; } = 0;
        public Int32 WorkComplete { get; protected set; } = 0;
        public Int32 WorkQueued { get { return WorkQueue.Count; } }
        protected WorkItem? CurrentWork { get; private set; } = null;

        public WorkerQueue() : base()
        {
            WorkStarting += WorkerQueue_WorkStarting;
            WorkCompleted += WorkerQueue_WorkCompleted;
        }

        public virtual void Enqueue(WorkItem item)
        {
            Boolean isWaiting = WorkQueue.IsEmpty;

            WorkQueue.Enqueue(item);
            WorkAdded++;
            if (isWaiting) { WorkStarting(this, new EventArgs()); }
        }

        protected event EventHandler WorkStarting;
        protected event EventHandler<WorkerEventArgs> WorkCompleted;

        void WorkerQueue_WorkStarting(object? sender, EventArgs e)
        {
            if (WorkQueue.TryDequeue(out WorkItem? item) && item is not null)
            {
                CurrentWork = item;
                DoWork((dynamic)item);
            }

            OnProgressChanged();
        }

        void WorkerQueue_WorkCompleted(object? sender, WorkerEventArgs e)
        {
            WorkComplete++;
            if (WorkQueue.IsEmpty) { WorkAdded = 0; WorkComplete = 0; CurrentWork = null; }
            else { WorkStarting(this, new EventArgs()); }

            OnProgressChanged();
        }

        protected virtual void DoWork(WorkItem item) { throw new InvalidOperationException("Specfied DoWork has not been coded"); }

        protected virtual void DoWork(WorkBackgroundItem item)
        {
            WorkerEventArgs eventArgs = new WorkerEventArgs(item);

            BackgroundWorker.DoWork += BackgroundWorker_DoWork;
            BackgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            BackgroundWorker.RunWorkerAsync(item);

            void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
            {
                item.OnStarting();
                item.DoWork();
            }

            void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
            {
                item.OnCompleting(e);
                WorkCompleted(this, eventArgs);
            }
        }

        protected virtual void DoWork<T>(WorkParellelItem<T> item)
        {
            Parallel.ForEach(
                item.Tasks,
                new ParallelOptions() { MaxDegreeOfParallelism = item.MaxDegreeOfParallelism },
                item.DoWork);
            WorkCompleted(this, new WorkerEventArgs(item));
        }

        protected virtual void DoWork(WorkForegroundItem item)
        {
            item.DoWork();

            WorkCompleted(this, new WorkerEventArgs(item));
        }

        public event EventHandler<WorkerProgressChangedEventArgs>? ProgressChanged;
        protected void OnProgressChanged()
        {
            if (ProgressChanged is EventHandler<WorkerProgressChangedEventArgs> progress)
            {
                Int32 workProgress = ((Int32)Math.Truncate(((((decimal)WorkComplete / (decimal)WorkAdded) * (decimal)100.0))));

                if (CurrentWork is WorkItem)
                { progress(this, new WorkerProgressChangedEventArgs(CurrentWork, workProgress)); }
                else { progress(this, new WorkerProgressChangedEventArgs(workProgress)); }
            }
        }

        #region IDisposable
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    WorkStarting -= WorkerQueue_WorkStarting;
                    BackgroundWorker.Dispose();
                    WorkQueue.Clear();
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
