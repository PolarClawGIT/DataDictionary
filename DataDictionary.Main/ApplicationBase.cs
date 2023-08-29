using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;
using Toolbox.Mediator;
using Toolbox.Threading;

namespace DataDictionary.Main
{
    interface IApplicationDataForm
    {
        public object? OpenItem { get; }
    }

    partial class ApplicationBase : Form, IColleague
    {
        public ApplicationBase() : base()
        {
            InitializeComponent();
        }

        #region Open Form
        /// <summary>
        /// Looks for the Target Form already open. If it is open, just activate it. Otherwise, show/activate the form.
        /// </summary>
        /// <typeparam name="TForm"></typeparam>
        /// <param name="constructor"></param>
        /// <returns></returns>
        protected virtual TForm Activate<TForm>(Func<TForm> constructor)
            where TForm : ApplicationBase
        {
            Form parent = MdiParent ?? this;

            if (parent.MdiChildren.FirstOrDefault(w => w.GetType() == typeof(TForm)) is TForm existingForm)
            {
                existingForm.Activate();
                return existingForm;
            }
            else
            {
                TForm newForm = constructor();
                newForm.MdiParent = parent;
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
            where TForm : ApplicationBase
        {
            Form parent = MdiParent ?? this;

            if (parent.MdiChildren.FirstOrDefault(w => w.GetType() == typeof(TForm)) is TForm existingForm)
            {
                if (existingForm is IApplicationDataForm existingData && ReferenceEquals(existingData.OpenItem, data))
                { existingForm.Activate(); }
                else
                {
                    TForm newForm = constructor(data);
                    newForm.MdiParent = parent;
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
            where TForm : ApplicationBase
        {
            Form parent = MdiParent ?? this;

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
                ActiveControl is Control activeControl &&
                !message.IsHandled)
            { HandleControl(activeControl); }

            void HandleControl(Control control)
            {
                if (ActiveControl is TextBoxBase controlBase)
                {
                    controlBase.Copy();
                    message.IsHandled = true;
                }
                else if (control is ComboBox combo)
                {
                    if (!string.IsNullOrEmpty(combo.SelectedText))
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
                ActiveControl is Control activeControl &&
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
                    if (!string.IsNullOrEmpty(combo.SelectedText))
                    {
                        Clipboard.SetText(combo.SelectedText);
                        combo.SelectedText = string.Empty;
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
                ActiveControl is Control activeControl &&
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
                    if (Clipboard.GetText() is string newText)
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
                ActiveControl is Control activeControl &&
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
                ActiveControl is Control activeControl &&
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

        private void ApplicationBase_ControlAdded(object sender, ControlEventArgs e)
        {
            // Adjust the padding on child forms to allow for the shared tool strip
            // This only effects controls added to the base form.
            // It is expected that all child forms will have a single TablePanelControl as a base control.
            // Child controls of the expected base control do not fire this event.
            if (e.Control is not null &&
                e.Control.Parent is ApplicationBase && // Top level controls only
                toolStrip.Visible && // Caution, changing visibility dynamically could results in incorrect layout.
                e.Control != toolStrip) // ignore the control being accounted for
            {
                e.Control.Padding = new Padding(
                e.Control.Padding.Left,
                toolStrip.Height + e.Control.Padding.Top,
                e.Control.Padding.Right,
                e.Control.Padding.Bottom);
            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            Dialogs.HelpSubject x = Activate(() => new Dialogs.HelpSubject());
            { x.NavigateTo(this); }
        }
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
        public static Dictionary<Control, string> GetAllErrors(this ErrorProvider provider, Control rootControl)
        {
            Dictionary<Control, string> errors = new Dictionary<Control, string>();
            string errorText = provider.GetError(rootControl);

            if (!string.IsNullOrWhiteSpace(errorText))
            { errors.Add(rootControl, errorText); }

            foreach (Control item in rootControl.Controls)
            {
                Dictionary<Control, string> child = provider.GetAllErrors(item);
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
