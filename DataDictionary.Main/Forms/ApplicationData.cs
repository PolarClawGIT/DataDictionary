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
using System.ComponentModel;

namespace DataDictionary.Main.Forms
{
    partial class ApplicationData : ApplicationBase
    {
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
        protected IReadOnlyDictionary<Enumerations.CommandType, CommandState> CommandButtons { get { return commandButtons; } }
        Dictionary<Enumerations.CommandType, CommandState> commandButtons = new Dictionary<Enumerations.CommandType, CommandState>();

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

            openFromDatabaseCommand.Image = ScopeType.Database.GetImage(Enumerations.CommandType.OpenDatabase);
            saveToDatabaseCommand.Image = ScopeType.Database.GetImage(Enumerations.CommandType.SaveDatabase);
            deleteFromDatabaseCommand.Image = ScopeType.Database.GetImage(Enumerations.CommandType.DeleteDatabase);

            commandButtons.Add(Enumerations.CommandType.Browse, new CommandState(browseCommand) { IsVisible = false });
            commandButtons.Add(Enumerations.CommandType.Select, new CommandState(selectCommand) { IsVisible = false });
            commandButtons.Add(Enumerations.CommandType.Add, new CommandState(newCommand) { IsVisible = false });
            commandButtons.Add(Enumerations.CommandType.Delete, new CommandState(deleteCommand) { IsVisible = false });
            commandButtons.Add(Enumerations.CommandType.Save, new CommandState(saveCommand) { IsVisible = false });
            commandButtons.Add(Enumerations.CommandType.Open, new CommandState(openCommand) { IsVisible = false });
            commandButtons.Add(Enumerations.CommandType.Import, new CommandState(importCommand) { IsVisible = false });
            commandButtons.Add(Enumerations.CommandType.Export, new CommandState(exportCommand) { IsVisible = false });
            toolStripSeparator.Visible = false;
            commandButtons.Add(Enumerations.CommandType.OpenDatabase, new CommandState(openFromDatabaseCommand) { IsVisible = true, AllowEnabled = () => Settings.Default.IsOnLineMode });
            commandButtons.Add(Enumerations.CommandType.SaveDatabase, new CommandState(saveToDatabaseCommand) { IsVisible = true, AllowEnabled = () => Settings.Default.IsOnLineMode });
            commandButtons.Add(Enumerations.CommandType.DeleteDatabase, new CommandState(deleteFromDatabaseCommand) { IsVisible = true, AllowEnabled = () => Settings.Default.IsOnLineMode });
            commandButtons.Add(Enumerations.CommandType.SecurityDatabase, new CommandState(securityCommand) { IsVisible = false, AllowEnabled = () => Settings.Default.IsOnLineMode });
            commandButtons.Add(Enumerations.CommandType.HistoryDatabase, new CommandState(historyCommand) { IsVisible = false, AllowEnabled = () => Settings.Default.IsOnLineMode });
        }

        private void ApplicationData_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            { // Avoids issues where the Load event fires in Design Mode
                LoadToolTips(this);
            }
        }

        /// <summary>
        /// Returns the DataRowState of the first BindingSource of SetRowState.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataRowState RowState { get; private set; } = DataRowState.Unchanged;

        /// <summary>
        /// Sets/Binds the RowState.
        /// </summary>
        /// <param name="bindings"></param>
        /// <remarks>The first BindingSource is considered the parent and RowState reflects that value</remarks>
        protected void SetRowState(params BindingSource[] bindings)
        {
            rowStateCommand.Enabled = true;

            foreach (BindingSource item in bindings)
            {
                item.CurrentItemChanged += Item_CurrentItemChanged;
                item.CurrentChanged += Item_CurrentChanged;
                item.DataSourceChanged += Item_DataSourceChanged;
                item.Disposed += Item_Disposed;
            }

            rowStateCommand.Image = GetToolImage();
            rowStateCommand.ToolTipText = GetToolTip();

            String GetToolTip()
            {
                StringBuilder result = new StringBuilder();
                DateTime lastChange = DateTime.MinValue;
                String? lastTemporal = String.Empty;

                foreach (BindingSource binding in bindings)
                {
                    if (binding.Position >= 0
                        && binding.Current is not null
                        && binding.GetRowState().TryGetValue(out IRowStateEnumeration? rowState))
                    {
                        if (binding.Current is IScopeType scopeType)
                        { result.AppendLine(String.Format("{0}: {1}", scopeType.Scope.GetEnumeration().DisplayName, rowState.DisplayName)); }
                        else
                        { result.AppendLine(String.Format("{0}: {1}", binding.Current.GetType().Name, rowState.DisplayName)); }

                        if (binding.Current is ITemporal temporal
                            && temporal.Temporal.AsOfUtcDate > lastChange
                            && temporal.Temporal.Modification is
                                DbModificationType.Inserted or
                                DbModificationType.Updated or
                                DbModificationType.Deleted)
                        {
                            lastChange = temporal.Temporal.AsOfUtcDate;

                            if (String.IsNullOrEmpty(lastTemporal))
                            { lastTemporal = temporal.Temporal.ToString(); }
                            else
                            {
                                StringBuilder value = new StringBuilder();
                                value.Append(DbModificationType.Updated.GetEnumeration().DisplayName);

                                if (temporal.Temporal.CreatedOn is DateTime createOn)
                                { value.Append(String.Format("on {0}", createOn)); }

                                if (temporal.Temporal.CreatedBy is String createBy)
                                { value.Append(String.Format("by {0}", createBy)); }
                            }
                        }
                    }
                }

                result.AppendLine(lastTemporal);
                return result.ToString();
            }

            Image GetToolImage()
            {
                BindingRowState result = BindingRowState.Null;

                foreach (BindingSource binding in bindings)
                {
                    if (binding.Position >= 0
                        && binding.Current is not null
                        && binding.GetRowState().TryGetValue(out IRowStateEnumeration? rowState))
                    {
                        if (result is BindingRowState.Null)
                        { result = rowState.Value; }
                        else if (result is BindingRowState.Unchanged
                            && rowState.Value is
                                BindingRowState.Added or
                                BindingRowState.Modified or
                                BindingRowState.Deleted or
                                BindingRowState.Detached)
                        { result = BindingRowState.Modified; }
                    }
                }

                return result.GetImage();
            }

            void Item_CurrentItemChanged(Object? sender, EventArgs e)
            {
                rowStateCommand.Image = GetToolImage();
                rowStateCommand.ToolTipText = GetToolTip();
            }

            void Item_CurrentChanged(Object? sender, EventArgs e)
            {   // Item Changed occures each time Current Change occurs. Don't need to do anything.
                //rowStateCommand.Image = GetToolImage();
                //rowStateCommand.ToolTipText = GetToolTip();
            }

            void Item_DataSourceChanged(Object? sender, EventArgs e)
            {
                if (sender is BindingSource binding)
                {
                    binding.CurrentItemChanged -= Item_CurrentItemChanged;
                    binding.CurrentChanged -= Item_CurrentChanged;
                    binding.DataSourceChanged -= Item_DataSourceChanged;
                    binding.Disposed -= Item_Disposed;

                    binding.CurrentItemChanged += Item_CurrentItemChanged;
                    binding.CurrentChanged += Item_CurrentChanged;
                    binding.DataSourceChanged += Item_DataSourceChanged;
                    binding.Disposed += Item_Disposed;
                }
            }

            void Item_Disposed(Object? sender, EventArgs e)
            {
                if (sender is BindingSource binding)
                {
                    binding.CurrentItemChanged -= Item_CurrentItemChanged;
                    binding.CurrentChanged -= Item_CurrentChanged;
                    binding.Disposed -= Item_Disposed;
                    binding.DataSourceChanged -= Item_DataSourceChanged;
                }
            }
        }


        /// <summary>
        /// Sets the Title text and Icon based on the BindingSource provided.
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>
        /// This sets up the CurrentChanged and Disposed events.
        /// Title and Icon are updated based on what record is being viewed.
        /// Override behavior of SetIcon.
        /// </remarks>
        protected void SetTitle(BindingSource data)
        {
            Data_CurrentChanged(data, EventArgs.Empty);
            data.CurrentChanged += Data_CurrentChanged;
            data.Disposed += Data_Disposed;
            data.DataSourceChanged += Data_DataSourceChanged;

            void Data_DataSourceChanged(Object? sender, EventArgs e)
            { Data_CurrentChanged(data, EventArgs.Empty); }

            void Data_CurrentChanged(Object? sender, EventArgs e)
            {
                if (data.Position >= 0 && data.Current is IDataValue dataValue)
                {
                    Text = dataValue.Title;
                    SetIcon(dataValue.Scope);
                }
            }

            void Data_Disposed(Object? sender, EventArgs e)
            {
                data.CurrentChanged -= Data_CurrentChanged;
                data.Disposed -= Data_Disposed;
            }
        }

        /// <summary>
        /// Sets the Icon based on Scope Provided
        /// </summary>
        /// <param name="scope"></param>
        /// <remarks>Icon is static unless SetTitle is used.</remarks>
        protected void SetIcon(ScopeType scope)
        { Icon = scope.GetIcon(); }

        /// <summary>
        /// Sets the Icon and Command Button Images. 
        /// Sets the buttons to visible and enabled.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="commands"></param>
        protected void SetCommand(ScopeType scope, params Enumerations.CommandType[]? commands)
        {
            foreach (KeyValuePair<Enumerations.CommandType, CommandState> item in commandButtons)
            {
                if (scope.TryGetImage(item.Key, out Image? image))
                { commandButtons[item.Key].Image = image; }
                // Else leave the image as is

                if (commands is not null && commands.Any(w => item.Key.Equals(w)))
                {
                    CommandButtons[item.Key].IsVisible = true;
                    CommandButtons[item.Key].IsEnabled = true;
                }
            }
        }

        /// <summary>
        /// Add a ToolStrip to the main form ToolStrip.
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="displayStyle">default is Image and Text</param>
        protected void AddCommands(ToolStrip commands,
            ToolStripItemDisplayStyle displayStyle = ToolStripItemDisplayStyle.ImageAndText)
        {
            Int32 mergeIndex = -1;
            if (toolStrip.Items.OfType<ToolStripSeparator>().FirstOrDefault() is ToolStripSeparator separator)
            {
                if (commands.Items.OfType<ToolStripSeparator>().FirstOrDefault() is ToolStripSeparator incoming)
                {
                    if (commands.Items.IndexOf(incoming) == 0)
                    { // Place the items before the Database Commands
                        mergeIndex = toolStrip.Items.IndexOf(separator);
                        separator.Visible = true;
                    }
                    else { mergeIndex = 0; } // Place the items first
                }
                else
                { mergeIndex = toolStrip.Items.IndexOf(separator); } // Place the items before the Database Commands

            }

            foreach (ToolStripItem item in commands.Items.OfType<ToolStripItem>())
            {
                item.DisplayStyle = displayStyle;
                item.MergeIndex = mergeIndex++;
                item.MergeAction = MergeAction.Insert;
            }

            ToolStripManager.Merge(commands, toolStrip);
        }

        /// <summary>
        /// Set the IsEnabled based on security function.
        /// </summary>
        /// <param name="getAuthorization"></param>
        public virtual void SetAuthorization(Func<Enumerations.CommandType, Boolean> getAuthorization)
        {
            foreach (var item in CommandButtons)
            { item.Value.IsEnabled = getAuthorization(item.Key); }
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
        {
            General.HelpContent helpForm = Activate(() => new General.HelpContent());
            helpForm.OpenSubject(this);
        }

        protected virtual void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        { if (!ValidateChildren()) { return; } }

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

        protected virtual void SecurityCommand_Click(object sender, EventArgs e)
        { }

        protected override void HandleMessage(OnlineStatusChanged message)
        {
            base.HandleMessage(message);
            commandButtons[Enumerations.CommandType.OpenDatabase].Refresh();
            commandButtons[Enumerations.CommandType.SaveDatabase].Refresh();
            commandButtons[Enumerations.CommandType.DeleteDatabase].Refresh();
        }


    }
}
