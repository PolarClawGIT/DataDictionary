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
using System.Linq.Expressions;

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
        /// Function called to determine if a given Button has authorization.
        /// </summary>
        protected Func<Enumerations.ButtonType, Boolean> GetAuthorization { get; private set; } = (button) => true;

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
            InitCommands();

            helpCommand.Image = ScopeType.ApplicationHelp.GetImage(ButtonType.Default);
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
                item.BindingComplete += Item_BindingComplete;
                item.DataError += Item_DataError;
                item.Disposed += Item_Disposed;

                void Item_CurrentItemChanged(Object? sender, EventArgs e)
                {
                    rowStateCommand.Image = GetToolImage();
                    rowStateCommand.ToolTipText = GetToolTip();
                }

                void Item_CurrentChanged(Object? sender, EventArgs e)
                { // Update the RowState of the form to reflect the RowState of the first binding.
                    if (sender is BindingSource binding
                        && ReferenceEquals(bindings.FirstOrDefault(), binding))
                    {
                        BindingRowState rowState = binding.GetRowState();
                        RowState = rowState.AsDataRowState();

                        if (rowState is BindingRowState.Null or BindingRowState.Detached or BindingRowState.Deleted)
                        { IsLocked(true); }
                        else { IsLocked(false); }
                    }
                }

                void Item_DataSourceChanged(Object? sender, EventArgs e)
                {
                    item.CurrentItemChanged -= Item_CurrentItemChanged;
                    item.CurrentChanged -= Item_CurrentChanged;
                    item.DataSourceChanged -= Item_DataSourceChanged;
                    item.Disposed -= Item_Disposed;

                    item.CurrentItemChanged += Item_CurrentItemChanged;
                    item.CurrentChanged += Item_CurrentChanged;
                    item.DataSourceChanged += Item_DataSourceChanged;
                    item.Disposed += Item_Disposed;
                }

                void Item_DataError(Object? sender, BindingManagerDataErrorEventArgs e)
                {   // This is only a basic trap to try to get more information when binding errors occur.
                    // Currently this is not providing useful information.
                    e.Exception.Data.Add(nameof(item), item.GetType().Name);
                }

                void Item_BindingComplete(Object? sender, BindingCompleteEventArgs e)
                {   // This is only a basic trap to try to get more information when binding errors occur.
                    // Currently this is not providing useful information.
                    if (e.Exception is not null)
                    { e.Exception.Data.Add(nameof(item), item.GetType().Name); }
                }

                void Item_Disposed(Object? sender, EventArgs e)
                {
                    item.CurrentItemChanged -= Item_CurrentItemChanged;
                    item.CurrentChanged -= Item_CurrentChanged;
                    item.Disposed -= Item_Disposed;
                    item.DataSourceChanged -= Item_DataSourceChanged;
                    item.BindingComplete -= Item_BindingComplete;
                    item.DataError -= Item_DataError;
                }
            }

            rowStateCommand.Visible = true;
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

                if (String.IsNullOrWhiteSpace(result.ToString()))
                { return "empty dataset"; }
                else
                {
                    result.AppendLine(lastTemporal);
                    return result.ToString();
                }
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

                if (result is BindingRowState.Null)
                { return Resources.Icon_Row.MergeImage(Resources.StatusInvalid, default, System.Drawing.Drawing2D.CompositingMode.SourceOver); }
                else { return result.GetImage(); }
            }






        }

        /// <summary>
        /// Sets the Title based on the BindingSource provided.
        /// Allows for the Title to be changed if the Title of the object changes.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="defaultTitle"></param>
        /// <remarks>
        /// If the BindingSource is a IDataValue, the Title attribute is used. Otherwise the ToString is used.
        /// </remarks>
        protected void SetTitle(BindingSource data, String? defaultTitle = null)
        {
            //Data_CurrentChanged(data, EventArgs.Empty);
            data.CurrentChanged += Data_CurrentChanged;
            data.Disposed += Data_Disposed;
            data.DataSourceChanged += Data_DataSourceChanged;

            SetTitle(defaultTitle ?? String.Empty);

            void Data_DataSourceChanged(Object? sender, EventArgs e)
            { Data_CurrentChanged(data, EventArgs.Empty); }

            void Data_CurrentChanged(Object? sender, EventArgs e)
            {
                if (data.Position >= 0)
                {
                    if (data.Current is IDataValue dataValue)
                    { SetTitle(dataValue.Title); }
                    else if (data.Current is not null
                        && data.Current.ToString() is String objectValue
                        && !String.IsNullOrWhiteSpace(objectValue))
                    { SetTitle(objectValue); }
                    else { SetTitle(defaultTitle ?? String.Empty); }
                }
            }

            void Data_Disposed(Object? sender, EventArgs e)
            {
                data.CurrentChanged -= Data_CurrentChanged;
                data.Disposed -= Data_Disposed;
            }
        }

        /// <summary>
        /// /// Sets the Title text to the string provided.
        /// </summary>
        /// <param name="value"></param>
        protected void SetTitle(String value)
        { Text = value; }

        /// <summary>
        /// Sets the Icon based on the BindingSource provided.
        /// Allows for the Icon to be changed if the Scope of the object changes.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="defaultScope"></param>
        /// <remarks>
        /// The data in the BindingSource must be an IScopeType.
        /// </remarks>
        protected void SetIcon(BindingSource data, ScopeType defaultScope = ScopeType.Null)
        {
            data.CurrentChanged += Data_CurrentChanged;
            data.Disposed += Data_Disposed;
            data.DataSourceChanged += Data_DataSourceChanged;
            SetIcon(defaultScope);

            void Data_DataSourceChanged(Object? sender, EventArgs e)
            { Data_CurrentChanged(data, EventArgs.Empty); }

            void Data_CurrentChanged(Object? sender, EventArgs e)
            {
                if (data.Position >= 0 && data.Current is IScopeType dataValue)
                { SetIcon(dataValue.Scope); }
                else { SetIcon(defaultScope); }
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
        /// <remarks>This applies icon that is expected to be static.</remarks>
        protected void SetIcon(ScopeType scope)
        {
            Icon = scope.GetIcon();

            foreach (var item in CommandButtons.Values)
            {
                if (item.Scope is ScopeType.Null)
                { item.Scope = scope; }
            }
        }

        /// <summary>
        /// Sets the Icon and Command Button Images. 
        /// Sets the buttons to visible and enabled.
        /// </summary>
        /// <param name="commands"></param>
        protected void SetCommand(params ButtonType[]? commands)
        {
            if (commands is not null)
            {
                foreach (ButtonType item in commands)
                {
                    if (CommandButtons.TryGetValue(item, out ToolStripCommand? value))
                    {
                        value.Visible = true;
                        value.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Add a ToolStrip to the main form ToolStrip.
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="displayStyle">default is Image and Text</param>
        /// <param name="positionOf">Position the toolstrip to the Left of the command specified</param>
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
                item.ToolTipText = item.Text;
            }

            ToolStripManager.Merge(commands, toolStrip);
        }

        /// <summary>
        /// Set the IsEnabled based on security function.
        /// </summary>
        /// <param name="getAuthorization"></param>
        public virtual void SetAuthorization(Func<Enumerations.ButtonType, Boolean> getAuthorization)
        {
            GetAuthorization = getAuthorization;
            foreach (var item in CommandButtons)
            { item.Value.Refresh(); }
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

        [Obsolete("Don't think this will be needed. Use DataBinding.AddBinding instead.")]
        public virtual String NavigationPath<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var members = new Stack<string>();
            var memberExpr = expression.Body as MemberExpression;

            while (memberExpr != null)
            {
                members.Push(memberExpr.Member.Name);
                memberExpr = memberExpr.Expression as MemberExpression;
            }

            return string.Join(".", members);
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

        protected virtual void SelectCommand_Click(object? sender, EventArgs e)
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
            CommandButtons[Enumerations.ButtonType.OpenDatabase].Refresh();
            CommandButtons[Enumerations.ButtonType.SaveDatabase].Refresh();
            CommandButtons[Enumerations.ButtonType.DeleteDatabase].Refresh();
        }


    }
}
