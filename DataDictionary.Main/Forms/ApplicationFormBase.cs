using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Toolbox.BindingTable;
using Toolbox.Mediator;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms
{
    interface IApplicationDataForm
    {
        public Object? OpenItem { get; }
    }

    class ApplicationFormBase : Form, IColleague
    {
        public ApplicationFormBase() : base()
        {
            Program.Messenger.AddColleague(this);
            SendMessage(new FormAddMdiChild() { ChildForm = this });
        }

        #region Open Form
        /// <summary>
        /// Looks for the Target Form already open. If it is open, just activate it. Otherwise, show/activate the form.
        /// </summary>
        /// <typeparam name="TForm"></typeparam>
        /// <param name="constructor"></param>
        /// <returns></returns>
        protected virtual TForm Activate<TForm>(Func<TForm> constructor)
            where TForm : ApplicationFormBase
        {
            Form parent = this.MdiParent ?? this;

            if (parent.MdiChildren.FirstOrDefault(w => w.GetType() == typeof(TForm)) is TForm existingForm)
            {
                existingForm.Activate();
                return existingForm;
            }
            else
            {
                TForm newForm = constructor();
                newForm.Show();
                return newForm;
            }
        }

        /// <summary>
        /// Looks for the Target Form already open with the specified BindingTable.
        /// If it is open, just activate it. Otherwise, show/activate the form.
        /// </summary>
        /// <typeparam name="TForm"></typeparam>
        /// <param name="constructor"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected virtual TForm Activate<TForm>(Func<IBindingTable, TForm> constructor, IBindingTable data)
            where TForm : ApplicationFormBase
        {
            Form parent = this.MdiParent ?? this;

            if (parent.MdiChildren.FirstOrDefault(w => w.GetType() == typeof(TForm)) is TForm existingForm)
            {
                if (existingForm is IApplicationDataForm existingData && ReferenceEquals(existingData.OpenItem, data))
                { existingForm.Activate(); }
                else
                {
                    TForm newForm = constructor(data);
                    newForm.Show();
                    return newForm;
                }

                return existingForm;
            }
            else
            {
                TForm newForm = constructor(data);
                newForm.Show();
                return newForm;
            }
        }

        /// <summary>
        /// Looks for the Target Form already open with the specified BindingTableRow.
        /// If it is open, just activate it. Otherwise, show/activate the form.
        /// </summary>
        /// <typeparam name="TForm"></typeparam>
        /// <param name="constructor"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected virtual TForm Activate<TForm>(Func<IBindingTableRow, TForm> constructor, IBindingTableRow data)
            where TForm : ApplicationFormBase
        {
            Form parent = this.MdiParent ?? this;

            if (parent.MdiChildren.FirstOrDefault(w => w.GetType() == typeof(TForm)) is TForm existingForm)
            {
                if (existingForm is IApplicationDataForm existingData && ReferenceEquals(existingData.OpenItem, data))
                { existingForm.Activate(); }
                else
                {
                    TForm newForm = constructor(data);
                    newForm.Show();
                    return newForm;
                }

                return existingForm;
            }
            else
            {
                TForm newForm = constructor(data);
                newForm.Show();
                return newForm;
            }
        }

        #endregion@endr

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
            if (ReferenceEquals(this, message.HandledBy) &&
                this.ActiveControl is Control activeControl &&
                !message.IsHandled)
            { HandleControl(activeControl); }

            void HandleControl(Control control)
            {
                if (this.ActiveControl is TextBoxBase controlBase)
                {
                    controlBase.Copy();
                    message.IsHandled = true;
                }
                else if (control is ComboBox combo)
                {
                    if (!String.IsNullOrEmpty(combo.SelectedText))
                    {
                        Clipboard.SetText(combo.SelectedText);
                        message.IsHandled = true;
                    }
                }
                else if (control is ISupportEditMenu menuControl)
                {
                    menuControl.Copy();
                    message.IsHandled = true;
                }
                else if (control is ContainerControl Container && Container.ActiveControl is Control containerControl)
                { HandleControl(containerControl); }
                else
                {
                    Exception ex = new NotSupportedException();
                    ex.Data.Add("ActiveControl", control.GetType().FullName);
                    throw ex;
                }

            }
        }

        protected virtual void HandleMessage(WindowsCutCommand message)
        {
            if (ReferenceEquals(this, message.HandledBy) &&
                this.ActiveControl is Control activeControl &&
                !message.IsHandled)
            { HandleControl(activeControl); }

            void HandleControl(Control control)
            {
                if (control is TextBoxBase controlBase)
                {
                    controlBase.Cut();
                    message.IsHandled = true;
                }
                else if (control is ComboBox combo)
                {
                    if (!String.IsNullOrEmpty(combo.SelectedText))
                    {
                        Clipboard.SetText(combo.SelectedText);
                        combo.SelectedText = String.Empty;
                        message.IsHandled = true;
                    }
                }
                else if (control is ISupportEditMenu menuControl)
                {
                    menuControl.Cut();
                    message.IsHandled = true;
                }
                else if (control is ContainerControl Container && Container.ActiveControl is Control containerControl)
                { HandleControl(containerControl); }
                else
                {
                    Exception ex = new NotSupportedException();
                    ex.Data.Add("ActiveControl", control.GetType().FullName);
                    throw ex;
                }
            }
        }

        protected virtual void HandleMessage(WindowsPasteCommand message)
        {
            if (ReferenceEquals(this, message.HandledBy) &&
                this.ActiveControl is Control activeControl &&
                !message.IsHandled)
            { HandleControl(activeControl); }

            void HandleControl(Control control)
            {
                if (control is TextBoxBase controlBase)
                {
                    controlBase.Paste();
                    message.IsHandled = true;
                }
                else if (control is ISupportEditMenu menuControl)
                {
                    menuControl.Paste();
                    message.IsHandled = true;
                }
                else if (control is ComboBox combo)
                {
                    if (Clipboard.GetText() is String newText)
                    {
                        combo.SelectedText = newText;
                        message.IsHandled = true;
                    }
                }
                else if (control is ContainerControl Container && Container.ActiveControl is Control containerControl)
                { HandleControl(containerControl); }
                else
                {
                    Exception ex = new NotSupportedException();
                    ex.Data.Add("ActiveControl", control.GetType().FullName);
                    throw ex;
                }
            }
        }

        protected virtual void HandleMessage(WindowsUndoCommand message)
        {
            if (ReferenceEquals(this, message.HandledBy) &&
                this.ActiveControl is Control activeControl &&
                !message.IsHandled)
            { HandleControl(activeControl); }

            void HandleControl(Control control)
            {
                if (control is TextBoxBase controlBase)
                {
                    controlBase.Undo();
                    message.IsHandled = true;
                }
                else if (control is ISupportEditMenu menuControl)
                {
                    menuControl.Undo();
                    message.IsHandled = true;
                }
                else if (control is ContainerControl Container && Container.ActiveControl is Control containerControl)
                { HandleControl(containerControl); }
                else
                {
                    Exception ex = new NotSupportedException();
                    ex.Data.Add("ActiveControl", control.GetType().FullName);
                    throw ex;
                }
            }
        }

        protected virtual void HandleMessage(WindowsSelectAllCommand message)
        {
            if (ReferenceEquals(this, message.HandledBy) &&
                this.ActiveControl is Control activeControl &&
                !message.IsHandled)
            { HandleControl(activeControl); }

            void HandleControl(Control control)
            {
                if (control is TextBoxBase controlBase)
                {
                    controlBase.SelectAll();
                    message.IsHandled = true;
                }
                else if (control is ISupportEditMenu menuControl)
                {
                    menuControl.SelectAll();
                    message.IsHandled = true;
                }
                else if (control is ContainerControl Container && Container.ActiveControl is Control containerControl)
                { HandleControl(containerControl); }
                else
                {
                    Exception ex = new NotSupportedException();
                    ex.Data.Add("ActiveControl", control.GetType().FullName);
                    throw ex;
                }
            }
        }

        #endregion
    }

    static class ApplicationFormExtension
    {
        /// <summary>
        /// Searches the control passed and all child controls for an error associated with the ErrorProvider and return the text.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="rootControl"></param>
        /// <returns>List of controls and the error text that goes with them.</returns>
        /// <remarks>
        /// The error provider does not contain this function.
        /// Also, for some reason the Error Provider can return HasErrors = true when no control on the form has an error.
        /// This way, I can search for controls within a specific scope looking for errors.
        /// </remarks>
        public static Dictionary<Control, String> GetAllErrors(this ErrorProvider provider, Control rootControl)
        {
            Dictionary<Control, String> errors = new Dictionary<Control, String>();
            String errorText = provider.GetError(rootControl);

            if (!String.IsNullOrWhiteSpace(errorText))
            { errors.Add(rootControl, errorText); }

            foreach (Control item in rootControl.Controls)
            {
                Dictionary<Control, String> child = provider.GetAllErrors(item);
                foreach (var childItem in child)
                { errors.Add(childItem.Key, childItem.Value); }
            }

            return errors;
        }

        public static void DoWork(this Form form, IEnumerable<WorkItem> work, Action<RunWorkerCompletedEventArgs>? onCompleting = null)
        {
            form.LockForm();
            Program.Worker.Enqueue(work, completing);

            void completing(RunWorkerCompletedEventArgs result)
            {
                if (result.Error is not null) { Program.ShowException(result.Error); }
                if (onCompleting is not null) { onCompleting(result); }

                form.UnLockForm();
            }
        }

        public static void DoWork(this Form form, WorkItem work, Action<RunWorkerCompletedEventArgs>? onCompleting = null)
        {
            form.UseWaitCursor = true;
            form.Enabled = false;
            Program.Worker.Enqueue(work, completing);

            void completing(RunWorkerCompletedEventArgs result)
            {
                if (result.Error is not null) { Program.ShowException(result.Error); }
                if (onCompleting is not null) { onCompleting(result); }

                form.UseWaitCursor = false;
                form.Enabled = true;
            }
        }

        public static void LockForm(this Form form)
        { // This assumes that all form layouts start with a TablePanel Control. Nothing is expected outside of that control
            foreach (Control item in form.Controls.Cast<Control>().Where(w => w.HasChildren))
            {
                item.Enabled = false;
                item.UseWaitCursor = true;
            }
        }

        public static void UnLockForm(this Form form)
        { // This assumes that all form layouts start with a TablePanel Control. Nothing is expected outside of that control.
            foreach (Control item in form.Controls.Cast<Control>().Where(w => w.HasChildren))
            {
                item.Enabled = true;
                item.UseWaitCursor = false;
            }
        }
    }
}
