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
        /// <summary>
        /// Name for the unit of work.
        /// </summary>
        String WorkName { get; init; }

        /// <summary>
        /// Function to return if this work item should be canceled.
        /// </summary>
        /// <remarks>
        /// The intent here is to have a way of having one WorkItem fail and
        /// cancel all the dependent WorkItems. When this evaluates true,
        /// the DoWork is not executed and the Completing is marked as canceled.
        /// By default, the function always returns false.
        /// </remarks>
        Func<Boolean> IsCanceling { get; init; }

        /// <summary>
        /// This triggers after the DoWork method is complete.
        /// </summary>
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
        /// <inheritdoc/>
        public virtual String WorkName { get; init; } = "Work Item";

        /// <summary>
        /// The action/Work to be performed.
        /// </summary>
        public virtual Action DoWork { get; init; } = () => { };

        /// <inheritdoc/>
        public virtual Func<Boolean> IsCanceling { get; init; } = () => false;

        public WorkItem() : base()
        { }

        /// <summary>
        /// This triggers after the DoWork method is complete.
        /// </summary>
        public event EventHandler<RunWorkerCompletedEventArgs>? Completing;


        /// <summary>
        /// Fires the Completing event.
        /// </summary>
        /// <param name="error"></param>
        /// <param name="canceled"></param>
        /// <remarks>
        /// This is called by the Background Worker of the WorkerQueue.
        /// If the InvokeUsing value is set, the process uses the method passed to execute the statement.
        /// The goal here is to execute on the UI thread so the InvokeUsing is expected to have the Invoke method of a Control on the UI thread.
        /// If the InvokeUsing is not set, then the event occurs on the Background Worker thread and can cause a cross thread exception.
        /// </remarks>
        internal virtual void OnCompleting(Exception? error, Boolean canceled, Action<Action>? InvokeUsing = null)
        {
            if (Completing is EventHandler<RunWorkerCompletedEventArgs> handler)
            {
                //handler.BeginInvoke(this, new RunWorkerCompletedEventArgs(this, error, canceled), null, null); // Throws error, not available on platform
                //handler(this, new RunWorkerCompletedEventArgs(this, error, canceled)); // Invokes the event on the same thread that the method was called on not the thread that created the object. Throws Cross threading exception.
                //Completing.Invoke(this, new RunWorkerCompletedEventArgs(this, error, canceled)) // Does the same as above but clearly not thread safe.

                if (InvokeUsing is not null)
                { InvokeUsing(() => handler(this, new RunWorkerCompletedEventArgs(this, error, canceled))); }
                else { handler(this, new RunWorkerCompletedEventArgs(this, error, canceled)); }
            }
        }

        /// <summary>
        /// This event is for trigging progress within the DoWork method.
        /// </summary>
        /// <remarks>
        /// Progress should be for this task only.
        /// Overall progress is computed by WorkerQueue.ProgressChanged
        /// </remarks>
        public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;

        /// <summary>
        /// Standard OnProgressChanged method
        /// </summary>
        /// <param name="progress"></param>
        public virtual void OnProgressChanged(Int32 progress)
        {
            if (ProgressChanged is EventHandler<ProgressChangedEventArgs> handler)
            { handler(this, new ProgressChangedEventArgs(progress, null)); }
        }

        /// <summary>
        /// OnProgressChanged that accepts two integers and converts it to a standard call.
        /// </summary>
        /// <param name="itemsComplete"></param>
        /// <param name="totalItems"></param>
        public virtual void OnProgressChanged(Int32 itemsComplete, Int32 totalItems)
        {
            if (totalItems > 0 && itemsComplete >= 0)
            {
                Double progress = (((Double)itemsComplete / (Double)totalItems)) * 100.0D;
                OnProgressChanged(Convert.ToInt32(progress));
            }
        }

    }
}
