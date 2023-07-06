using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Mediator
{
    /// <summary>
    /// This implements the Mediator pattern to pass message to 0 or more Colleagues.
    /// A colleague can send or receive messages.
    /// </summary>
    public class Mediator : IDisposable
    {
        private Collection<IColleague> colleagues = new Collection<IColleague>();

        /// <summary>
        /// Adds a new Colleague to the Mediator.
        /// </summary>
        /// <param name="colleague"></param>
        public virtual void AddColleague(IColleague colleague)
        {
            if (colleague is null) { throw new ArgumentNullException(nameof(colleague)); }
            if (disposedValue) { throw new ObjectDisposedException("Mediator"); }

            if (colleagues.Contains(colleague)) { }
            else
            {
                colleagues.Add(colleague);

                colleague.OnSendMessage += Colleague_SendMessage;
                colleague.Disposed += Colleague_Disposed;
            }
        }

        /// <summary>
        /// Handles the Send Message event from the Colleague.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        protected void Colleague_SendMessage(object? sender, MessageEventArgs message)
        {
            if (sender is null) { throw new ArgumentNullException(nameof(sender)); }
            if (sender is IColleague sentBy
                && colleagues.Contains(sentBy))
            {
                /* TODO: Parallel is causing cross threading issues. Could not figure out how to fix.
                 * Parallel.ForEach(colleagues.Where(sentTo => sentTo != sentBy), item =>  item.RecieveMessage(sender, message));*/
                foreach (IColleague? item in colleagues.Where(sentTo => sentTo != sentBy))
                { item.ReceiveMessage(sender, message); }
            }
        }

        /// <summary>
        /// Handles the Dispose event of the Colleague and remove them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Colleague_Disposed(object? sender, EventArgs e)
        {
            if (sender is null) { throw new ArgumentNullException(nameof(sender)); }
            if (sender is IColleague colleague
                && colleagues.Contains(colleague))
            { RemoveColleague(colleague); }
        }

        #region IDisposable
        /// <summary>
        /// Event that is fired at the end of the Depose method.
        /// </summary>
        public event EventHandler? Disposed;

        private bool disposedValue;

        /// <summary>
        /// Removes a Colleague to the Mediator.
        /// </summary>
        /// <param name="colleague"></param>
        public virtual void RemoveColleague(IColleague colleague)
        {
            if (colleague is null) { throw new ArgumentNullException(nameof(colleague)); }
            if (disposedValue) { throw new ObjectDisposedException("Mediator"); }

            if (colleagues.Contains(colleague))
            {
                colleague.OnSendMessage -= Colleague_SendMessage;
                colleague.Disposed -= Colleague_Disposed;

                colleagues.Remove(colleague);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    while (colleagues.Count > 0)
                    { colleagues.Remove(colleagues.First()); }

                    colleagues.Clear();
                }

                disposedValue = true;
                if (Disposed is EventHandler handler) { handler(this, EventArgs.Empty); }
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
