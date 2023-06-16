using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading.WorkItem;

namespace Toolbox.Threading
{
    public class WorkerQueue : IDisposable
    {
        protected ConcurrentQueue<WorkBase> WorkQueue { get; } = new ConcurrentQueue<WorkBase>();
        protected BackgroundWorker BackgroundWorker { get; } = new BackgroundWorker();
        public Int32 WorkAdded { get; protected set; } = 0;
        public Int32 WorkComplete { get; protected set; } = 0;
        public Int32 WorkProgress
        {
            get
            {
                if (WorkAdded == 0) { return 0; }
                else { return ((Int32)Math.Truncate(((((decimal)WorkComplete / (decimal)WorkAdded) * (decimal)100.0)))); }
            }
        }

        public Int32 WorkQueued { get { return WorkQueue.Count; } }
        protected WorkItem.WorkBase? CurrentWork { get; private set; } = null;

        public WorkerQueue() : base()
        {
            WorkStarting += WorkerQueue_WorkStarting;
            WorkCompleted += WorkerQueue_WorkCompleted;
        }

        public virtual void Enqueue(WorkBase item)
        {
            WorkQueue.Enqueue(item);
            WorkAdded++;
            if (CurrentWork is null) { WorkStarting(this, new EventArgs()); }
        }

        protected event EventHandler WorkStarting;
        protected event EventHandler<WorkerEventArgs> WorkCompleted;

        protected virtual void WorkerQueue_WorkStarting(object? sender, EventArgs e)
        {
            if (WorkQueue.TryDequeue(out WorkBase? item) && item is not null)
            {
                CurrentWork = item;
                item.OnStarting();
                DoWork((dynamic)item);
            }

            OnProgressChanged();
        }

        protected virtual void WorkerQueue_WorkCompleted(object? sender, WorkerEventArgs e)
        {
            if (CurrentWork is WorkBase)
            {
                CurrentWork = null;
                WorkComplete++;
            }

            if (WorkQueue.IsEmpty) { WorkAdded = 0; WorkComplete = 0; }
            else { WorkStarting(this, new EventArgs()); }

            OnProgressChanged();
        }

        protected virtual void DoWork(WorkBase item)
        { throw new NotImplementedException(); }

        protected virtual void DoWork(BackgroundWork item)
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
                BackgroundWorker.DoWork -= BackgroundWorker_DoWork;
                BackgroundWorker.RunWorkerCompleted -= BackgroundWorker_RunWorkerCompleted;
                if (e.Error is Exception) { item.OnException(e.Error); }

                item.OnCompleting();

                WorkCompleted(this, eventArgs);
            }
        }

        protected virtual void DoWork(BatchWork item)
        {
            item.OnStarting();

            WorkBase? lastItem = item.WorkItems.LastOrDefault(); // So the list of items to be worked with becomes static.
            if (lastItem is WorkBase workItem) { workItem.WorkCompleting += WorkItem_WorkCompleting; }

            foreach (WorkBase work in item.WorkItems)
            { this.Enqueue(work); }

            WorkCompleted(this, new WorkerEventArgs(item));

            void WorkItem_WorkCompleting(object? sender, EventArgs e)
            {
                if (lastItem is WorkItem.WorkBase workItem) { workItem.WorkCompleting -= WorkItem_WorkCompleting; }
                item.OnCompleting();

            }
        }

        protected virtual void DoWork(ParellelWork item)
        {
            item.OnStarting();
            List<Action> work = item.Tasks.ToList(); // So the list of items to be worked with becomes static.
            WorkAdded = WorkAdded + work.Count;

            try
            {
                Parallel.ForEach(
                    work,
                    new ParallelOptions() { MaxDegreeOfParallelism = item.MaxDegreeOfParallelism },
                    DoWork
                    );
            }
            catch (Exception ex)
            { item.OnException(ex); }

            WorkCompleted(this, new WorkerEventArgs(item));

            void DoWork(object source, ParallelLoopState state, long arg3)
            {
                if (source is Action action) { action(); }
                WorkComplete++;
            }
        }

        protected virtual void DoWork(ForegroundWork item)
        {
            try
            { item.DoWork(); }
            catch (Exception ex)
            { item.OnException(ex); }

            WorkCompleted(this, new WorkerEventArgs(item));
        }

        public event EventHandler<WorkerProgressChangedEventArgs>? ProgressChanged;
        protected void OnProgressChanged()
        {
            if (ProgressChanged is EventHandler<WorkerProgressChangedEventArgs> progress)
            {
                if (CurrentWork is WorkItem.WorkBase)
                { progress(this, new WorkerProgressChangedEventArgs(CurrentWork, WorkProgress)); }
                else { progress(this, new WorkerProgressChangedEventArgs(WorkProgress)); }
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
