using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Text;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms
{
    partial class ApplicationData : ApplicationBase
    {
        Dictionary<DataRowState, (Image icon, String tooltip)> rowStateSettings = new Dictionary<DataRowState, (Image icon, string tooltip)>()
        {
            { DataRowState.Added, new (Resources.RowAdded, "Added")},
            { DataRowState.Deleted, new (Resources.RowDeleted, "Deleted")},
            { DataRowState.Detached, new (Resources.RowDetached, "Detached")},
            { DataRowState.Modified, new (Resources.RowModified, "Modified")},
            { DataRowState.Unchanged, new (Resources.Row, "Unchanged")},
        };

        protected class CommandState
        {
            readonly ToolStripItem Control;

            /// <summary>
            /// Is the Command Button Enabled
            /// </summary>
            public Boolean IsEnabled
            {
                get { return Control.Enabled; }
                set
                {
                    Control.Enabled = value && AllowEnabled();
                    isEnabled = value;
                }
            }
            Boolean isEnabled = false; // Intended State
            public Func<Boolean> AllowEnabled { get; init; } = () => { return true; };

            public void Refresh()
            { Control.Enabled = isEnabled && AllowEnabled(); }

            /// <summary>
            /// Is the Command Button Visible
            /// </summary>
            public Boolean IsVisible
            {
                get { return Control.Visible; }
                set { Control.Visible = value; }
            }

            /// <summary>
            /// Image for the Command Button
            /// </summary>
            public Image? Image
            {
                get { return Control.Image; }
                set { Control.Image = value; }
            }

            public CommandState(ToolStripItem control)
            {
                Control = control;
                control.VisibleChanged += Control_VisibleChanged;
            }

            /// <summary>
            /// Text for the Control
            /// </summary>
            public String Text
            {
                get { return Control.Text ?? String.Empty; }
                set { Control.Text = value; }
            }

            public ToolStripDropDown? DropDown
            {
                get
                {
                    if (Control is ToolStripDropDownButton dropButton)
                    { return dropButton.DropDown; }
                    else if (Control is ToolStripSplitButton splitButton)
                    { return splitButton.DropDown; }
                    else { return null; }
                }
                set
                {
                    if (Control is ToolStripDropDownButton dropButton)
                    {
                        if (value is null)
                        { dropButton.ShowDropDownArrow = false; }
                        else { dropButton.ShowDropDownArrow = true; }

                        dropButton.DropDown = value;
                    }
                    else if (Control is ToolStripSplitButton splitButton)
                    { splitButton.DropDown = value; }
                }
            }

            void Test()
            {
                if (Control is ToolStripDropDownButton x) { x.DropDown = null; }
                if (Control is ToolStripSplitButton y) { y.DropDown = null; }
            }

            private void Control_VisibleChanged(Object? sender, EventArgs e)
            {
                // Detects if there is anything before the separator and if not, do not show the separator.
                if (sender is ToolStripItem caller && caller.Owner is ToolStrip tools)
                {
                    Int32 before = 0;

                    foreach (ToolStripItem item in tools.Items)
                    {
                        if (item is ToolStripSeparator)
                        {
                            if (before > 0) { item.Visible = true; }
                            else { item.Visible = false; }
                            before = 0;
                        } // Caller has not yet set the Visible flag
                        else if (item.Visible || (item == caller && !item.Visible))
                        { before++; }
                    }

                }
            }
        }

        /// <summary>
        /// The set of Command Buttons
        /// </summary>
        protected IReadOnlyDictionary<CommandImageType, CommandState> CommandButtons { get { return commandButtons; } }
        Dictionary<CommandImageType, CommandState> commandButtons = new Dictionary<CommandImageType, CommandState>();

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
        /// <remarks>
        /// WARNING: When a child form is designed, this method executes.
        /// The "DesignMode" property IS NOT SET.
        /// Any reference to instances of Objects defined outside of this code
        /// will throw errors when a child form is designed.
        /// This causes lots of issue with Visual Studio.
        /// </remarks>
        public ApplicationData() : base()
        {
            InitializeComponent();

            commandButtons.Add(CommandImageType.Browse, new CommandState(browseCommand) { IsVisible = false });
            commandButtons.Add(CommandImageType.Select, new CommandState(selectCommand) { IsVisible = false });
            commandButtons.Add(CommandImageType.Add, new CommandState(newCommand) { IsVisible = false });
            commandButtons.Add(CommandImageType.Delete, new CommandState(deleteCommand) { IsVisible = false });
            commandButtons.Add(CommandImageType.Save, new CommandState(saveCommand) { IsVisible = false });
            commandButtons.Add(CommandImageType.Open, new CommandState(openCommand) { IsVisible = false });
            commandButtons.Add(CommandImageType.Import, new CommandState(importCommand) { IsVisible = false });
            commandButtons.Add(CommandImageType.Export, new CommandState(exportCommand) { IsVisible = false });
            toolStripSeparator.Visible = false;
            commandButtons.Add(CommandImageType.OpenDatabase, new CommandState(openFromDatabaseCommand) { AllowEnabled = () => Settings.Default.IsOnLineMode });
            commandButtons.Add(CommandImageType.SaveDatabase, new CommandState(saveToDatabaseCommand) { AllowEnabled = () => Settings.Default.IsOnLineMode });
            commandButtons.Add(CommandImageType.DeleteDatabase, new CommandState(deleteFromDatabaseCommand) { AllowEnabled = () => Settings.Default.IsOnLineMode });
            commandButtons.Add(CommandImageType.HistoryDatabase, new CommandState(historyCommand) { AllowEnabled = () => Settings.Default.IsOnLineMode });
        }

        private void ApplicationData_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            { // Avoids issues where the Load event fires in Design Mode
                LoadToolTips(this);
            }
        }

        /// <summary>
        /// Sets RowState, default Window Text, Icon and Command Buttons based on the BindingSource data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="commands"></param>
        /// <remarks>Calls Setup by Scope, if possible.</remarks>
        protected void Setup(BindingSource data, params CommandImageType[] commands)
        {
            data.CurrentChanged += Data_CurrentChanged;

            if (data.Current is not null)
            { Data_CurrentChanged(data, EventArgs.Empty); }

            void Data_CurrentChanged(Object? sender, EventArgs e)
            {
                if (data.Current is IScopeType scopeValue)
                { Setup(scopeValue.Scope, commands); }

                if (data.Current is IBindingRowState binding)
                {
                    rowStateCommand.Enabled = true;
                    RowState = binding.RowState();
                    binding.RowStateChanged += RowStateChanged;
                    RowStateChanged(binding, EventArgs.Empty);
                }

                if (data.Current is IDataValue dataValue)
                {
                    Text = dataValue.Title;
                    Icon = ImageEnumeration.GetIcon(dataValue.Scope);
                }
            }
        }

        /// <summary>
        /// Sets the Icon and Command Button Images
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="commands"></param>
        protected void Setup(ScopeType scope, params CommandImageType[]? commands)
        {
            Icon = ImageEnumeration.GetIcon(scope);
            rowStateCommand.Enabled = false;

            foreach (KeyValuePair<CommandImageType, CommandState> item in commandButtons)
            {
                if (ImageEnumeration.Members.ContainsKey(scope) && ImageEnumeration.Members[scope].Images.ContainsKey(item.Key))
                { item.Value.Image = ImageEnumeration.GetImage(scope, item.Key); }
                // Else leave the image as is
            }

            if (commands is not null)
            {
                foreach (CommandImageType item in commands)
                {
                    CommandButtons[item].IsVisible = true;
                    CommandButtons[item].IsEnabled = true;
                }
            }
        }

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

            if (sender is ITemporalValue temporal && RowState is DataRowState.Unchanged)
            {
                if (temporal.IsDeleted == true)
                { rowStateCommand.Image = Resources.RowDeleted; }
                else if (temporal.IsCurrent == false)
                { rowStateCommand.Image = Resources.RowHistory; }

                StringBuilder toolTip = new StringBuilder();
                toolTip.AppendLine(DbModificationEnumeration.Cast(temporal.Modification).DisplayName);
                if (temporal.ModifiedOn is DateTime)
                { toolTip.AppendLine(String.Format("{0}: {1}", nameof(temporal.ModifiedOn), temporal.ModifiedOn)); }
                if (temporal.ModifiedBy is String modifiedBy)
                { toolTip.AppendLine(String.Format("{0}: {1}", nameof(temporal.ModifiedBy), temporal.ModifiedBy)); }
                rowStateCommand.ToolTipText = toolTip.ToString();
            }

        }

        private void ToolStrip_VisibleChanged(object? sender, EventArgs e)
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
                toolStrip.VisibleChanged -= ToolStrip_VisibleChanged;
            }
        }

        protected virtual void helpToolStripButton_Click(object sender, EventArgs e)
        { Activate(() => new General.HelpContent(this)); }

        protected virtual void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void BrowseCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void AddCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void SelectCommand_Click(object sender, EventArgs e)
        { }

        protected virtual void DeleteCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void ImportCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void ExportCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void OpenCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void SaveCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void HistoryCommand_Click(object sender, EventArgs e)
        { }

        protected override void HandleMessage(OnlineStatusChanged message)
        {
            base.HandleMessage(message);
            commandButtons[CommandImageType.OpenDatabase].Refresh();
            commandButtons[CommandImageType.SaveDatabase].Refresh();
            commandButtons[CommandImageType.DeleteDatabase].Refresh();
        }


    }
}
