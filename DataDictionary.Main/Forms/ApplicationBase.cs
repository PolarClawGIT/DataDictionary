using DataDictionary.DataLayer;
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
using static System.Windows.Forms.Control;

namespace DataDictionary.Main.Forms
{
    interface IApplicationBase
    {
        ControlCollection Controls { get; }

        /// <summary>
        /// Locks (disable) and Unlock (enable) the Form.
        /// </summary>
        /// <remarks>
        /// True disables the top most controls and sets the Wait Cursor.
        /// False enables the top most controls and clears the Wait Cursor.
        /// </remarks>
       Boolean IsLocked
        {
            get { return this.Controls.Cast<Control>().Any(w => w.HasChildren && w.Enabled); }
            set
            {
                foreach (Control item in this.Controls.Cast<Control>().Where(w => w.HasChildren))
                {
                    item.Enabled = !value;
                    item.UseWaitCursor = value;
                }
            }
        }


        /// <summary>
        /// Controls the UseWaitCursor of the top most controls.
        /// </summary>
        /// <remarks>This is effected by the IsLocked but allows the application to override the current state of UseWaitCursor.</remarks>
        Boolean IsWaitCursor
        {
            get { return this.Controls.Cast<Control>().Any(w => w.HasChildren && w.UseWaitCursor); }
            set
            {
                foreach (Control item in this.Controls.Cast<Control>().Where(w => w.HasChildren))
                { item.UseWaitCursor = value; }
            }
        }
    }

    abstract partial class ApplicationBase : Form, IColleague, IApplicationBase
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
                if (existingForm is IApplicationDataForm existingData && existingData.IsOpenItem(data))
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
        protected virtual TForm Activate<TForm>(Func<IDataItem, TForm> constructor, IDataItem data)
            where TForm : ApplicationBase
        {
            Form parent = MdiParent ?? this;

            if (parent.MdiChildren.FirstOrDefault(w => w.GetType() == typeof(TForm)) is TForm existingForm)
            {
                if (existingForm is IApplicationDataForm existingData && existingData.IsOpenItem(data))
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
        #endregion


        /// <summary>
        /// Locks (disable) and Unlock (enable) the Form.
        /// </summary>
        /// <remarks>
        /// True disables the top most controls and sets the Wait Cursor.
        /// False enables the top most controls and clears the Wait Cursor.
        /// </remarks>
        protected virtual Boolean IsLocked
        {
            get { return this.Controls.Cast<Control>().Any(w => w.HasChildren && w.Enabled); }
            set
            {
                foreach (Control item in this.Controls.Cast<Control>().Where(w => w.HasChildren))
                {
                    item.Enabled = !value;
                    item.UseWaitCursor = value;
                }
            }
        }

        /// <summary>
        /// Controls the UseWaitCursor of the top most controls.
        /// </summary>
        /// <remarks>This is effected by the IsLocked but allows the application to override the current state of UseWaitCursor.</remarks>
        protected virtual Boolean IsWaitCursor
        {
            get { return this.Controls.Cast<Control>().Any(w => w.HasChildren && w.UseWaitCursor); }
            set
            {
                foreach (Control item in this.Controls.Cast<Control>().Where(w => w.HasChildren))
                { item.UseWaitCursor = value; }
            }
        }

        /// <summary>
        /// Performs the list of work on a background thread.
        /// </summary>
        /// <param name="work"></param>
        /// <param name="onCompleting"></param>
        /// <remarks>The method calls LockForm and UnlockForm while work is being done.</remarks>
        protected void DoWork(IEnumerable<WorkItem> work, Action<RunWorkerCompletedEventArgs>? onCompleting = null)
        {
            Program.Worker.Enqueue(work, completing);

            void completing(RunWorkerCompletedEventArgs result)
            {
                if (result.Error is not null) { Program.ShowException(result.Error); }
                if (onCompleting is not null) { onCompleting(result); }
            }
        }

        #region IColleague
        public event EventHandler<MessageEventArgs>? OnSendMessage;

        public virtual void ReceiveMessage(object? sender, MessageEventArgs message)
        { HandleMessage((dynamic)message); }

        /// <summary>
        /// Message of a specific class are sent to all forms (except the sending form) that handle that message.
        /// All ApplicationBase forms have a base handler for all known message types
        /// </summary>
        /// <param name="message"></param>
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
        protected void HandleMessage(MessageEventArgs message)
        { throw new InvalidOperationException("Base message handler was called instead of a specific override."); }

        /// <summary>
        /// Message sent by Child Forms to inform the Main Form that a child needs to be added.
        /// </summary>
        /// <param name="message"></param>
        protected virtual void HandleMessage(FormAddMdiChild message) { }

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
                    //Exception ex = new NotSupportedException();
                    //ex.Data.Add("ActiveControl", control.GetType().FullName);
                    //throw ex;
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
        { Activate(() => new Dialogs.HelpSubject(this)); }

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
}
