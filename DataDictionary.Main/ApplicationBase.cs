using DataDictionary.DataLayer.ApplicationData;
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

        protected virtual void HandleMessage(DbApplicationBatchStarting message) { }
        protected virtual void HandleMessage(DbApplicationBatchCompleted message) { }

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

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            Dialogs.HelpSubject x = Activate(() => new Dialogs.HelpSubject());
            { x.NavigateTo(this); }
        }

        private void toolStrip_VisibleChanged(object? sender, EventArgs e)
        {
            // Visibility can be set in code.
            // More often it changes based on other controls on the Form and any over lapping controls or form not the top most form.
            if (toolStrip.Visible)
            {
                // Assumes that a TableLayout Control or similar is the only other control on the page.
                // Condition and order is an attempt to prevent issues when multiple controls on the same form.
                if (this.Controls.Cast<Control>().
                    OrderBy(o => o.Top).
                    FirstOrDefault(w => w.HasChildren &&
                        w != toolStrip && // Not the ToolStrip
                        w.Top < toolStrip.Height // Top control does not overlap with ToolStrip
                        ) is Control topControl)
                {
                    topControl.Padding = new Padding(
                       topControl.Padding.Left,
                       toolStrip.Height + topControl.Padding.Top,
                       topControl.Padding.Right,
                       topControl.Padding.Bottom);
                }

                // Don't respond to further changes to Visibility (change only on first time visible only).
                toolStrip.VisibleChanged -= toolStrip_VisibleChanged;
            }
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

        public static void DoWork(this ApplicationBase form, IEnumerable<WorkItem> work, Action<RunWorkerCompletedEventArgs>? onCompleting = null)
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

        public static void DoWork(this ApplicationBase form, WorkItem work, Action<RunWorkerCompletedEventArgs>? onCompleting = null)
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

        public static void LockForm(this ApplicationBase form)
        { // This assumes that all form layouts start with a TablePanel Control. Nothing is expected outside of that control
            foreach (Control item in form.Controls.Cast<Control>().Where(w => w.HasChildren))
            {
                item.Enabled = false;
                item.UseWaitCursor = true;
            }
        }

        public static void UnLockForm(this ApplicationBase form)
        { // This assumes that all form layouts start with a TablePanel Control. Nothing is expected outside of that control.
            foreach (Control item in form.Controls.Cast<Control>().Where(w => w.HasChildren))
            {
                item.Enabled = true;
                item.UseWaitCursor = false;
            }
        }

    }
}
