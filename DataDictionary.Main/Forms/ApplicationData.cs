using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Text;
using System.ComponentModel;
using ButtonType = DataDictionary.Main.Enumerations.ButtonType;

namespace DataDictionary.Main.Forms
{

    partial class ApplicationData : ApplicationBase
    {
        /// <summary>
        /// Is the object passed the item the form is using for data.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <remarks>
        /// By default the function always returns true.
        /// Override this if the from is specific to data item.
        /// </remarks>
        public virtual Boolean IsOpenItem(Object? item) { return true; }

        /// <summary>
        /// The set of Command Buttons
        /// </summary>
        protected IReadOnlyDictionary<ButtonType, CommandState> CommandButtons { get { return commandButtons; } }
        Dictionary<ButtonType, CommandState> commandButtons = new Dictionary<ButtonType, CommandState>();

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


            helpCommand.Image = ScopeType.ApplicationHelp.GetImage(ButtonType.Default);

            new CommandState(browseCommand)
            {
                Scope = ScopeType.ApplicationDocument,
                Command = ButtonType.Browse,
                IsVisible = false,
            }.AddTo(commandButtons);

            new CommandState(selectCommand)
            {
                Scope = ScopeType.ApplicationDocument,
                Command = ButtonType.Select,
                IsVisible = false
            }.AddTo(commandButtons);

            new CommandState(newCommand)
            {
                Scope = ScopeType.ApplicationDocument,
                Command = ButtonType.Add,
                IsVisible = false
            }.AddTo(commandButtons);

            new CommandState(deleteCommand)
            {
                Scope = ScopeType.ApplicationDocument,
                Command = ButtonType.Delete,
                IsVisible = false
            }.AddTo(commandButtons);

            new CommandState(saveCommand)
            {
                Scope = ScopeType.ApplicationDocument,
                Command = ButtonType.Save,
                IsVisible = false
            }.AddTo(commandButtons);

            new CommandState(openCommand)
            {
                Scope = ScopeType.ApplicationDocument,
                Command = ButtonType.Open,
                IsVisible = false
            }.AddTo(commandButtons);

            new CommandState(importCommand)
            {
                Scope = ScopeType.ApplicationDocument,
                Command = ButtonType.Import,
                IsVisible = false
            }.AddTo(commandButtons);

            new CommandState(exportCommand)
            {
                Scope = ScopeType.ApplicationDocument,
                Command = ButtonType.Export,
                IsVisible = false
            }.AddTo(commandButtons);

            toolStripSeparator.Visible = false;

            new CommandState(openFromDatabaseCommand)
            {
                Scope = ScopeType.Database,
                Command = ButtonType.OpenDatabase,
                IsVisible = true,
                AllowEnabled = () => Settings.Default.IsOnLineMode && RowState is not (DataRowState.Added or DataRowState.Detached)
            }.AddTo(commandButtons);

            new CommandState(saveToDatabaseCommand)
            {
                Scope = ScopeType.Database,
                Command = ButtonType.SaveDatabase,
                IsVisible = true,
                AllowEnabled = () => Settings.Default.IsOnLineMode && RowState is not (DataRowState.Added or DataRowState.Detached)
            }.AddTo(commandButtons);

            new CommandState(deleteFromDatabaseCommand)
            {
                Scope = ScopeType.Database,
                Command = ButtonType.DeleteDatabase,
                IsVisible = true,
                AllowEnabled = () => Settings.Default.IsOnLineMode && RowState is not (DataRowState.Added or DataRowState.Detached)
            }.AddTo(commandButtons);

            new CommandState(securityCommand)
            {
                Scope = ScopeType.Security,
                Command = ButtonType.SecurityDatabase,
                IsVisible = false,
                AllowEnabled = () => Settings.Default.IsOnLineMode && RowState is not (DataRowState.Added or DataRowState.Detached)
            }.AddTo(commandButtons);

            new CommandState(historyCommand)
            {
                Scope = ScopeType.ApplicationTimeLine,
                Command = ButtonType.HistoryDatabase,
                IsVisible = false,
                AllowEnabled = () => Settings.Default.IsOnLineMode && RowState is not (DataRowState.Added or DataRowState.Detached)
            }.AddTo(commandButtons);
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
            { // Update the RowState of the form to reflect the RowState of the first binding.
                if (sender is BindingSource binding
                    && ReferenceEquals(bindings.FirstOrDefault(), binding))
                { RowState = binding.GetRowState().AsDataRowState(); }
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
        protected void SetCommand(ScopeType scope, params ButtonType[]? commands)
        {
            if (commands is not null)
            {
                foreach (var item in commands)
                {
                    if (CommandButtons.TryGetValue(item, out CommandState? value))
                    {
                        if (value.Scope is ScopeType.ApplicationDocument)
                        { value.Scope = scope; }

                        value.IsVisible = true;
                        value.IsEnabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Add a ToolStrip to the main form ToolStrip.
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="displayStyle">default is Image and Text</param>
        /// <param name="positionOf">Postion the toolstrip to the Left of the command specfied</param>
        protected void AddCommands(ToolStrip commands,
            ToolStripItemDisplayStyle displayStyle = ToolStripItemDisplayStyle.ImageAndText,
            ButtonType positionOf = ButtonType.Default)
        {
            Int32 mergeIndex = -1;
            if (toolStrip.Items.OfType<ToolStripSeparator>().FirstOrDefault() is ToolStripSeparator separator)
            {
                if (positionOf is ButtonType.Default)
                { // Place the items before the Database Commands
                    mergeIndex = toolStrip.Items.IndexOf(separator);
                    separator.Visible = true;
                }
                else if (CommandButtons.ContainsKey(positionOf))
                { // Position relative to the command passed.
                    mergeIndex = CommandButtons[positionOf].IndexOf();
                    separator.Visible = true;
                }
                else { mergeIndex = 0; } // Place the items first
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
        public virtual void SetAuthorization(Func<Enumerations.ButtonType, Boolean> getAuthorization)
        {
            foreach (var item in CommandButtons)
            { item.Value.IsEnabled = getAuthorization(item.Key); }
        }

        /// <summary>
        /// Used to build a property Navigation Path.
        /// A period delimited names of properties used for Binding.
        /// </summary>
        /// <param name="nameOfs"></param>
        /// <returns></returns>
        /// <remarks>
        /// The nameof function cannot be used directly with String.Format.
        /// This is because String.Format expects a list of Objects, not strings.
        /// This works around that limitation.
        /// This is a String Concatenation function.
        /// </remarks>
        /// <example>
        /// textBoxControl.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSource, NavigationPath(nameof(parentClass.parentProperty),nameof(childClass.childProperty))));
        /// </example>
        public virtual String NavigationPath(params List<String> nameOfs)
        {
            String result = String.Empty;

            foreach (String item in nameOfs)
            {
                if (String.IsNullOrWhiteSpace(result))
                { result = String.Format("{0}", item); }
                else { result = String.Format("{0}.{1}", result, item); }
            }

            return result;
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
            commandButtons[Enumerations.ButtonType.OpenDatabase].Refresh();
            commandButtons[Enumerations.ButtonType.SaveDatabase].Refresh();
            commandButtons[Enumerations.ButtonType.DeleteDatabase].Refresh();
        }


    }
}
