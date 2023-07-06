using DataDictionary.Main.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Mediator;

namespace DataDictionary.Main.Forms
{
    class ApplicationFormBase : Form, IColleague
    {

        public ApplicationFormBase() : base()
        {
            Program.Messenger.AddColleague(this);
            SendMessage(new FormAddMdiChild() { ChildForm = this });
        }

        #region IColleague
        public event EventHandler<MessageEventArgs>? OnSendMessage;

        public virtual void ReceiveMessage(object? sender, MessageEventArgs message)
        { HandleMessage((dynamic)message); }

        protected virtual void  SendMessage(MessageEventArgs message)
        {
            if (OnSendMessage is EventHandler<MessageEventArgs> handler)
            { handler(this, message); }
        }

        /// <summary>
        /// Handles a Message Event.
        /// </summary>
        /// <param name="message"></param>
        /// <remarks>
        /// Override the specific method to handle the event.
        /// This is called by RecieveMessage with Dynamic typing.
        /// </remarks>
        protected virtual void HandleMessage(MessageEventArgs message) { }

        protected virtual void HandleMessage(FormAddMdiChild message) { }

        protected virtual void HandleMessage(DbDataBatchStarting message) { }

        protected virtual void HandleMessage(DbDataBatchCompleted message) { }
        #endregion
    }
}
