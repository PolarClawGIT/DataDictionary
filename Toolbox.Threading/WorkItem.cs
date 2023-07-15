using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Threading
{
    public interface IWorkItem
    {
        String WorkName { get; }
        event EventHandler<RunWorkerCompletedEventArgs>? Completing;
    }

    /// <summary>
    /// Represents a unit of work to be performed
    /// </summary>
    /// <remarks>
    /// Using Action Or Func instead of Events because I am having problems with Events not working across threads.
    /// Method calls do not seem to have this issue.
    /// </remarks>
    public class WorkItem : IWorkItem
    {
        /// <summary>
        /// Name for the unit of work.
        /// </summary>
        public virtual String WorkName { get; init; } = "Work Item";

        /// <summary>
        /// The action/Work to be performed.
        /// </summary>
        public virtual Action DoWork { get; init; }

        /// <summary>
        /// Function to return if this work item should be canceled.
        /// </summary>
        /// <remarks>
        /// The intent here is to have a way of having one WorkItem fail and
        /// cancel all the dependent WorkItems. When this evaluates true,
        /// the DoWork is not executed and the Completing is marked as canceled.
        /// By default, the function always returns false.
        /// </remarks>
        public virtual Func<Boolean> IsCanceling { get; init; } = () => false;

        public WorkItem() : base()
        { DoWork = Work; }

        protected virtual void Work() { }

        /// <summary>
        /// This triggers after the DoWork method is complete.
        /// </summary>
        /// <remarks>
        /// The event fires within the scope of a Background Worker thread of the WorkerQueue.
        /// As such, it is possible to get cross thread exceptions.
        /// </remarks>
        public event EventHandler<RunWorkerCompletedEventArgs>? Completing;
        internal virtual void OnCompleting(Exception? error, Boolean canceled)
        {
            if (Completing is EventHandler<RunWorkerCompletedEventArgs> handler)
            { handler(this, new RunWorkerCompletedEventArgs(this, error, canceled)); }
        }

        /// <summary>
        /// This event is for trigging progress within the DoWork method.
        /// </summary>
        /// <remarks>
        /// Progress should be for this task only.
        /// Overall progress is computed by WorkerQueue.ProgressChanged
        /// </remarks>
        public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;
        public virtual void OnProgressChanged(Int32 progress)
        {
            if (ProgressChanged is EventHandler<ProgressChangedEventArgs> handler)
            { handler(this, new ProgressChangedEventArgs(progress, null)); }
        }

    }
}
