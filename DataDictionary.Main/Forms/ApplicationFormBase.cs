using DataDictionary.Main.Controls;
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

        protected virtual void SendMessage(MessageEventArgs message)
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

        protected virtual void HandleMessage(WindowsCommand message) { }

        protected virtual void HandleMessage(WindowsCopyCommand message)
        {
            if (ReferenceEquals(this, message.HandledBy))
            {
                if (this.ActiveControl is TextBoxBase controlBase)
                {
                    controlBase.Copy();
                    message.IsHandled = true;
                }
                else if (this.ActiveControl is ISupportEditMenu control)
                {
                    control.Copy();
                    message.IsHandled = true;
                }
                else if (this.ActiveControl is ComboBox combo)
                {
                    if (!String.IsNullOrEmpty(combo.SelectedText))
                    {
                        Clipboard.SetText(combo.SelectedText);
                        message.IsHandled = true;
                    }
                }

            }
        }

        protected virtual void HandleMessage(WindowsCutCommand message)
        {
            if (ReferenceEquals(this, message.HandledBy))
            {
                if (this.ActiveControl is TextBoxBase controlBase)
                {
                    controlBase.Cut();
                    message.IsHandled = true;
                }
                else if (this.ActiveControl is ISupportEditMenu control)
                {
                    control.Cut();
                    message.IsHandled = true;
                }
                else if (this.ActiveControl is ComboBox combo)
                {
                    if (!String.IsNullOrEmpty(combo.SelectedText))
                    {
                        Clipboard.SetText(combo.SelectedText);
                        combo.SelectedText = String.Empty;
                        message.IsHandled = true;
                    }
                }
            }
        }

        protected virtual void HandleMessage(WindowsPasteCommand message)
        {
            if (ReferenceEquals(this, message.HandledBy))
            {
                if (this.ActiveControl is TextBoxBase controlBase)
                {
                    controlBase.Paste();
                    message.IsHandled = true;
                }
                else if (this.ActiveControl is ISupportEditMenu control)
                {
                    control.Paste();
                    message.IsHandled = true;
                }
                else if (this.ActiveControl is ComboBox combo)
                {
                    if (Clipboard.GetText() is String newText)
                    {
                        combo.SelectedText = newText;
                        message.IsHandled = true;
                    }
                }
            }
        }

        protected virtual void HandleMessage(WindowsUndoCommand message)
        {
            if (ReferenceEquals(this, message.HandledBy))
            {
                if (this.ActiveControl is TextBoxBase controlBase)
                {
                    controlBase.Undo();
                    message.IsHandled = true;
                }
                else if (this.ActiveControl is ISupportEditMenu control)
                {
                    control.Undo();
                    message.IsHandled = true;
                }
            }
        }

        protected virtual void HandleMessage(WindowsSelectAllCommand message)
        {
            if (ReferenceEquals(this, message.HandledBy))
            {
                if (this.ActiveControl is TextBoxBase controlBase)
                {
                    controlBase.SelectAll();
                    message.IsHandled = true;
                }
                else if (this.ActiveControl is ISupportEditMenu control)
                {
                    control.SelectAll();
                    message.IsHandled = true;
                }
            }
        }

        #endregion
    }
}
