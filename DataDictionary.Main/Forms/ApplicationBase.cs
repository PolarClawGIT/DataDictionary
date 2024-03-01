using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Mediator;
using Toolbox.Threading;
using static System.Windows.Forms.Control;

namespace DataDictionary.Main.Forms
{

    interface IApplicationForm
    {
        /// <summary>
        /// Locks (disable) and Unlock (enable) the Form.
        /// </summary>
        /// <param name="newState">Sets the new Value. If Null, the value is not set, only returned.</param>
        /// <returns>
        /// True disables the top most controls.
        /// False enables the top most controls.
        /// </returns>
        Boolean IsLocked(Boolean? newState = null);

        /// <summary>
        /// Controls the UseWaitCursor of the top most controls.
        /// </summary>
        /// <param name="newState"></param>
        /// <returns></returns>
        Boolean IsWaitCursor(Boolean? newState = null);

        /// <summary>
        ///  Collection of child controls.
        /// </summary>
        /// <remarks>Implemented by the Control classes, including Form.</remarks>
        ControlCollection Controls { get; }
    }

    partial class ApplicationBase : Form, IColleague, IApplicationForm
    {
        Dictionary<DataRowState, (Image icon, String tooltip)> rowStateSettings = new Dictionary<DataRowState, (Image icon, string tooltip)>()
        {
            { DataRowState.Added, new (Resources.RowAdded, "Added")},
            { DataRowState.Deleted, new (Resources.RowDeleted, "Deleted")},
            { DataRowState.Detached, new (Resources.RowDetached, "Detached")},
            { DataRowState.Modified, new (Resources.RowModified, "Modified")},
            { DataRowState.Unchanged, new (Resources.Row, "Unchanged")},
        };

        private DataRowState? rowState;
        public DataRowState? RowState
        {
            get { return rowState; }
            set
            {
                if (value is DataRowState state)
                {
                    rowStateCommand.Enabled = true;
                    rowStateCommand.Visible = true;
                    rowStateCommand.Image = rowStateSettings[state].icon;
                    rowStateCommand.ToolTipText = rowStateSettings[state].tooltip;
                }
                else
                {
                    rowStateCommand.Enabled = false;
                    rowStateCommand.Visible = false;
                }

                rowState = value;
            }
        }

        /// <summary>
        /// Constructor called when in Form Design mode
        /// </summary>
        public ApplicationBase() : base()
        {
            InitializeComponent();
            RowState = null;

            try
            {// Designer might throw errors when this code is executed.
                // TODO: This code should be skipped in design mode. 
                // The property DesignMode does not get set until after the Load event fires.
                // There is no known way to detect if the Form is in design mode within the constructor.
                // This is my work-around.
                Messenger.AddColleague(this);
                SendMessage(new FormAddMdiChild() { ChildForm = this });
            }
            catch (Exception ex)
            {// Cannot do anything when the Designer throws errors.
                // This data will likely not show up in any window.
                // Meaning there is next to no way to debug this.
                System.Diagnostics.Debug.WriteLine("Designer Error ignored");
                System.Diagnostics.Debug.WriteLine(string.Format("  Exception: {0}", ex.Message));

                if (ex.InnerException is Exception innerEx)
                { System.Diagnostics.Debug.WriteLine(string.Format("  InnerException: {0}", innerEx.Message)); }

                System.Diagnostics.Debug.WriteLine(string.Format("  StackTrace: {0}", ex.StackTrace));
            }
        }

        #region Open Form

        /// <summary>
        /// Default Activate, does not open any forms.
        /// </summary>
        /// <param name="data"></param>
        protected void Activate(Object data)
        { }

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

            if (parent.MdiChildren.FirstOrDefault(w =>
                w is TForm targetForm) is ApplicationBase existingForm)
            {
                existingForm.Activate();
                return (TForm)existingForm;
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

            if (parent.MdiChildren.FirstOrDefault(w =>
                w is TForm targetForm &&
                targetForm is IApplicationDataForm dataForm &&
                dataForm.IsOpenItem(data)) is ApplicationBase existingForm)
            {
                existingForm.Activate();
                return (TForm)existingForm;
            }
            else
            {
                TForm newForm = constructor(data);
                newForm.MdiParent = parent;
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

            if (parent.MdiChildren.FirstOrDefault(w =>
                w is TForm targetForm &&
                targetForm is IApplicationDataForm dataForm &&
                dataForm.IsOpenItem(data)) is ApplicationBase existingForm)
            {
                existingForm.Activate();
                return (TForm)existingForm;
            }
            else
            {
                TForm newForm = constructor(data);
                newForm.MdiParent = parent;
                newForm.Show();
                return newForm;
            }
        }

        protected virtual TForm Activate<TForm>(Func<IBindingData, TForm> constructor, IBindingData data)
            where TForm : ApplicationBase
        {
            Form parent = MdiParent ?? this;

            if (parent.MdiChildren.FirstOrDefault(w =>
                w is TForm targetForm &&
                targetForm is IApplicationDataForm dataForm &&
                dataForm.IsOpenItem(data)) is ApplicationBase existingForm)
            {
                existingForm.Activate();
                return (TForm)existingForm;
            }
            else
            {
                TForm newForm = constructor(data);
                newForm.MdiParent = parent;
                newForm.Show();
                return newForm;
            }
        }
        #endregion

        /// <summary>
        /// Delegate for the Event to handle the RowState of the data.
        /// </summary>
        /// <param name="sender">IBindingRowState</param>
        /// <param name="e"></param>
        /// <remarks>This will lock the form is the data is Detached or Deleted.</remarks>
        protected virtual void RowStateChanged(object? sender, EventArgs e)
        {
            if (sender is IBindingRowState data)
            {
                RowState = data.RowState();
                if (IsHandleCreated)
                { this.Invoke(() => { this.IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted); }); }
                else { this.IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted); }
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
            Worker.Enqueue(work, completing);

            void completing(RunWorkerCompletedEventArgs result)
            {
                if (result.Error is not null) { Program.ShowException(result.Error); }
                if (onCompleting is not null) { onCompleting(result); }
            }
        }

        /// <summary>
        /// Set and returns the Locked State of the form.
        /// </summary>
        /// <param name="newState"></param>
        /// <returns></returns>
        /// <remarks>Locked enables or disables the top level controls of the form.</remarks>
        public virtual Boolean IsLocked(Boolean? newState = null)
        {
            if (newState is Boolean value)
            {
                foreach (Control item in this.Controls)
                {
                    if (item is MdiClient) { } // Don't touch the MdiClient control as it will cause child forms to be disabled.
                    else { item.Enabled = !value; }
                }
            }

            return this.Controls.Cast<Control>().Any(w => w.Enabled);
        }

        public virtual Boolean IsWaitCursor(Boolean? newState = null)
        {
            if (newState is Boolean value)
            {
                foreach (Control item in this.Controls)
                {
                    if (item is MdiClient) { } // Don't touch the MdiClient control as it will cause child forms to be disabled.
                    else { item.UseWaitCursor = value; }
                }
            }

            return this.Controls.Cast<Control>().Any(w => w.UseWaitCursor);
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

        /// <summary>
        /// Message sent when all forms should call the UnBindData method.
        /// This method call the UnbindData of all forms EXCEPT the form that sent the message.
        /// </summary>
        /// <param name="message"></param>
        protected virtual void HandleMessage(DoUnbindData message)
        { if (this is IApplicationDataBind form) { form.UnbindData(); } }

        /// <summary>
        /// Message sent when all forms should call the BindData method.
        /// This method calls the BindData of all forms EXCEPT the form that sent the message.
        /// </summary>
        /// <param name="message"></param>
        protected virtual void HandleMessage(DoBindData message)
        { if (this is IApplicationDataBind form) { form.BindData(); } }

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

        protected virtual void HandleMessage(OnlineStatusChanged message) { }

        #endregion

        private void helpToolStripButton_Click(object sender, EventArgs e)
        { Activate(() => new ApplicationWide.HelpSubject(this)); }

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
