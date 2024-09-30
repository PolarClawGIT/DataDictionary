using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.Application;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
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

        /// <summary>
        /// Constructor called when in Form Design mode
        /// </summary>
        /// <remarks>
        /// WARNING: When a child form is designed, this method executes.
        /// The "DesignMode" property IS NOT SET.
        /// Any reference to instances of Objects defined outside of this code
        /// will throw errors when a child form is designed.
        /// This causes lots of issue with Visual Studio.
        /// </remarks>
        public ApplicationBase() : base()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Load event for the Application Base
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// WARNING: When a child form is designed, this method executes.
        /// The "DesignMode" property IS Set.
        /// Any reference to instances of Objects defined outside of this code
        /// will throw errors when a child form is designed.
        /// This causes lots of issue with Visual Studio.
        /// </remarks>
        private void ApplicationBase_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            { // Avoids issues where the Load event fires in Design Mode
                Messenger.AddColleague(this);
                SendMessage(new FormAddMdiChild() { ChildForm = this });
                LoadToolTips(this);
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
        protected virtual TForm Activate<TForm>(Func<IBindingTableRow, TForm> constructor, IBindingTableRow data)
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

        protected virtual TForm Activate<TForm>(Func<IBindingList, TForm> constructor, IBindingList data)
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
        /// Used to Load the ToolTips into the Form. 
        /// </summary>
        /// <param name="source"></param>
        /// <remarks>
        /// This can load tool tips for any control in the Controls structure.
        /// That includes controls nested inside User Controls.
        /// This works because the Form.Controls structure includes all controls that appear in the form (built in and User Controls).
        /// Not all control types display Tool Tips. The key is that they are the top-most visible control.
        /// Otherwise, the hidden control does not receive the event.
        /// </remarks>
        /// <example>
        /// Run during Form Load event.
        /// LoadToolTips(this); // where this is a Form
        /// </example>
        protected virtual void LoadToolTips(Control source)
        {
            if (ToToolTipText(source) is String value && !String.IsNullOrWhiteSpace(value))
            { toolTip.SetToolTip(source, value); }

            foreach (Control child in source.Controls)
            { LoadToolTips(child); }

            String ToToolTipText(Control source)
            {
                HelpSubjectIndexPath key = source.ToNameSpaceKey();
                if (BusinessData.ApplicationData.HelpSubjects.FirstOrDefault(w => key.Equals(new HelpSubjectIndexPath(w))) is HelpSubjectValue item
                    && item.HelpToolTip is String toolTip)
                { return toolTip; }
                else { return String.Empty; }
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
        /// Performs a single item of work on a background thread.
        /// </summary>
        /// <param name="work"></param>
        /// <param name="onCompleting"></param>
        protected void DoWork(WorkItem work, Action<RunWorkerCompletedEventArgs>? onCompleting = null)
        {
            Worker.Enqueue(work, completing);

            void completing(RunWorkerCompletedEventArgs result)
            {
                if (result.Error is not null) { Program.ShowException(result.Error); }
                if (onCompleting is not null) { onCompleting(result); }
            }
        }

        /// <summary>
        /// Set and returns the Locked state of the form.
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

        /// <summary>
        /// Set and returns the Wait Cursor state of the form
        /// </summary>
        /// <param name="newState"></param>
        /// <returns></returns>
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

        protected virtual void HandleMessage(RefreshNavigation message) { }

        #endregion



    }
}
