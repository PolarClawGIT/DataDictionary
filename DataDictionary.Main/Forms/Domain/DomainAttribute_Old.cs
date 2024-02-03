using DataDictionary.BusinessLayer.NameSpace;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.Collections;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Domain
{
    [Obsolete]
    partial class DomainAttribute_Old : ApplicationBase, IApplicationDataForm<DomainAttributeKey>
    {
        public DomainAttributeKey DataKey { get; private set; }

        public bool IsOpenItem(object? item)
        { return DataKey.Equals(item); }

        public DomainAttribute_Old() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Attribute;

            newItemCommand.Enabled = true;
            newItemCommand.Image = Resources.NewProperty;
            newItemCommand.ToolTipText = "add Property";
            newItemCommand.Click += NewItemCommand_Click;

            deleteItemCommand.Click += DeleteItemCommand_Click;
            deleteItemCommand.Image = Resources.DeleteAttribute;
            deleteItemCommand.ToolTipText = "Remove the Attribute";

            DomainAttributeItem data = new DomainAttributeItem();
            DataKey = new DomainAttributeKey(data);
            bindingAttribute.DataSource = new BindingList<DomainAttributeItem>() { data };

            bindingProperties.DataSource =
                   new BindingView<DomainAttributePropertyItem>(
                       Program.Data.DomainAttributeProperties,
                       w => DataKey.Equals(w));

            bindingAlias.DataSource =
                    new BindingView<DomainAttributeAliasItem>(
                        Program.Data.DomainAttributeAliases,
                        w => DataKey.Equals(w));
        }

        public DomainAttribute_Old(DomainAttributeItem data) : this()
        {
            DataKey = new DomainAttributeKey(data);

            if (Program.Data.DomainAttributes.Contains(data))
            {
                bindingAttribute.DataSource = new BindingView<DomainAttributeItem>(Program.Data.DomainAttributes, w => DataKey.Equals(w));
                bindingAttribute.Position = 0;
            }

            if (bindingAttribute.Current is not DomainAttributeItem)
            {
                bindingAttribute.DataSource = new BindingList<DomainAttributeItem>() { data };
                bindingAttribute.Position = 0;
            }

            bindingProperties.DataSource =
                   new BindingView<DomainAttributePropertyItem>(
                       Program.Data.DomainAttributeProperties,
                       w => DataKey.Equals(w));

            bindingAlias.DataSource =
                    new BindingView<DomainAttributeAliasItem>(
                        Program.Data.DomainAttributeAliases,
                        w => DataKey.Equals(w));
        }

        private void DomainAttribute_Load(object sender, EventArgs e)
        {
            // one time bindings
            PropertyNameItem.Load(propertyTypeData);
            PropertyNameItem.Load(propertyTypeColumn);

            (this as IApplicationDataBind).BindData();
        }

        public bool BindDataCore()
        {
            bindingAttribute.CurrentItemChanged += DataChanged;
            UpdateRowState();

            if (bindingAttribute.Current is DomainAttributeItem data)
            {
                this.DataBindings.Add(new Binding(nameof(this.Text), bindingAttribute, nameof(data.AttributeTitle)));

                attributeTitleData.DataBindings.Add(new Binding(nameof(attributeTitleData.Text), bindingAttribute, nameof(data.AttributeTitle)));
                attributeDescriptionData.DataBindings.Add(new Binding(nameof(attributeDescriptionData.Text), bindingAttribute, nameof(data.AttributeDescription)));

                propertyNavigation.AutoGenerateColumns = false;
                propertyNavigation.DataSource = bindingProperties;
                bindingProperties.CurrentItemChanged += DataChanged;

                DomainAttributePropertyItem propertyMembers = new DomainAttributePropertyItem();
                propertyTypeData.DataBindings.Add(new Binding(nameof(propertyTypeData.SelectedValue), bindingProperties, nameof(propertyMembers.PropertyId), true));
                propertyValueData.DataBindings.Add(new Binding(nameof(propertyValueData.Text), bindingProperties, nameof(propertyMembers.PropertyValue), true));
                //propertyChoiceData.DataBindings.Add(new Binding(nameof(propertyChoiceData.SelectedValue, bindingProperties, nameof(propertyNames.))));
                propertyDefinitionData.DataBindings.Add(new Binding(nameof(propertyDefinitionData.Rtf), bindingProperties, nameof(propertyMembers.DefinitionText), true));

                propertyChoiceData.Enabled = false;
                propertyValueData.Enabled = false;
                propertyDefinitionData.Enabled = false;

                if (bindingProperties.Current is DomainAttributePropertyItem propItem
                    && Program.Data.Properties.FirstOrDefault(w => w.PropertyId == propItem.PropertyId) is PropertyItem property)
                { BindChoiceData(property, propItem); }

                aliasData.AutoGenerateColumns = false;
                aliasData.DataSource = bindingAlias;
                bindingAlias.CurrentItemChanged += DataChanged;

                deleteItemCommand.Enabled = true;
                return true;
            }
            else
            {
                deleteItemCommand.Enabled = false;
                return false;
            }
        }

        private void DataChanged(object? sender, EventArgs e)
        {
            if (bindingAttribute.Current is DomainAttributeItem data)
            { UpdateRowState(); }
        }

        public void UnbindDataCore()
        {
            bindingAttribute.DataMemberChanged -= DataChanged;

            this.DataBindings.Clear();
            attributeTitleData.DataBindings.Clear();
            attributeDescriptionData.DataBindings.Clear();

            propertyNavigation.DataSource = null;
            propertyTypeData.DataBindings.Clear();
            propertyValueData.DataBindings.Clear();
            propertyDefinitionData.DataBindings.Clear();

            aliasData.DataSource = null;
        }

        void BindChoiceData(PropertyItem property, DomainAttributePropertyItem data)
        {
            propertyChoiceData.Enabled = (property.IsChoice == true);
            propertyValueData.Enabled = (property.IsExtendedProperty == true || property.IsFrameworkSummary == true);
            propertyDefinitionData.Enabled = (property.IsDefinition == true);

            propertyChoiceData.Items.Clear();

            List<String> selectedChoices = new List<String>();
            if (data.PropertyValue is not null)
            { selectedChoices.AddRange(data.PropertyValue.Split(",")); }

            foreach (PropertyItem.ChoiceItem choice in property.Choices)
            {
                String newItem = choice.Choice;
                propertyChoiceData.Items.Add(newItem);
                Int32 index = propertyChoiceData.Items.IndexOf(newItem);
                Boolean isChecked = selectedChoices.Contains(newItem);
                propertyChoiceData.SetItemChecked(index, isChecked);
            }
        }

        #region IColleague
        protected override void HandleMessage(DbApplicationBatchStarting message)
        { (this as IApplicationDataBind).UnbindData(); }

        protected override void HandleMessage(DbApplicationBatchCompleted message)
        { (this as IApplicationDataBind).BindData(); }
        #endregion

        private void bindingProperties_AddingNew(object sender, AddingNewEventArgs e)
        {
            DomainAttributePropertyItem newItem = new DomainAttributePropertyItem(DataKey);

            if (bindingProperties.Current is null || propertyNavigation.CurrentRow is null)
            { // First Row scenario
                if (propertyTypeData.SelectedValue is Guid propertyId)
                { newItem.PropertyId = propertyId; }
            }

            e.NewObject = newItem;
        }

        private void BindingComplete(object sender, BindingCompleteEventArgs e)
        { // Helps with debugging code.
            if (e.Exception is not null)
            {

            }
        }

        private void propertyNavigation_Leave(object sender, EventArgs e)
        {
            if (propertyNavigation.CurrentRow is not null && propertyNavigation.CurrentRow.IsNewRow)
            {
                propertyNavigation.NotifyCurrentCellDirty(true); // Marks the row as dirty so when end-edit the row is retained.
                propertyNavigation.EndEdit(); // Completes the edit so a binding error does not occur when focus changes.
            }
        }

        private void bindingProperties_CurrentChanged(object sender, EventArgs e)
        { }

        private void propertyTypeData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bindingProperties.Current is null)
            {
                if (bindingProperties.AddNew() is DomainAttributePropertyItem newItem
                    && bindingProperties.DataSource is BindingView<DomainAttributePropertyItem> data)
                { bindingProperties.Position = data.IndexOf(newItem); }
            }

            if (propertyTypeData.SelectedItem is PropertyNameItem selected
                && bindingProperties.Current is DomainAttributePropertyItem currentRow
                && Program.Data.Properties.FirstOrDefault(w => w.PropertyId == selected.PropertyId) is PropertyItem property)
            { BindChoiceData(property, currentRow); }
        }

        private void propertyChoiceData_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            List<String> values = new List<String>();

            foreach (var item in propertyChoiceData.CheckedItems)
            {
                if (item is String value)
                { values.Add(value); }
            }

            if (e.NewValue == CheckState.Checked)
            {
                if (propertyChoiceData.Items[e.Index] is String addValue)
                { values.Add(addValue); }
            }
            else
            {
                if (propertyChoiceData.Items[e.Index] is String deletValue && values.Contains(deletValue))
                { values.Remove(deletValue); }
            }

            if (bindingProperties.Current is DomainAttributePropertyItem current)
            { current.PropertyValue = String.Join(", ", values); }
        }

        private void propertyChoiceData_EnabledChanged(object sender, EventArgs e)
        {
            if (propertyChoiceData.Enabled)
            { propertyChoiceData.ResetBackColor(); }
            else { propertyChoiceData.BackColor = SystemColors.Control; }
        }

        private void bindingDatabaseAlias_CurrentChanged(object sender, EventArgs e)
        { }

        private void propertyDefinitionData_Validated(object sender, EventArgs e)
        {
            String value = propertyDefinitionData.Text.Trim();
            Int32 maxCutOff = value.Length;
            if (maxCutOff > 4000) { maxCutOff = 4000; }

            Int32 firstBreak = value.Substring(0, maxCutOff).IndexOf("\n\n");
            Int32 lastReturn = value.Substring(0, maxCutOff).LastIndexOf("\n");
            Int32 lastSpace = value.Substring(0, maxCutOff).LastIndexOf(" ");

            if (firstBreak > 0) { value = value.Substring(0, firstBreak); }
            else if (lastReturn > 0) { value = value.Substring(0, lastReturn); }
            else if (lastSpace > 0) { value = value.Substring(0, lastSpace); }
            else { value = value.Substring(0, maxCutOff); }

            value = value.Replace(Environment.NewLine, " ");
            value = value.Replace("\t", " ");
            value = value.Replace("\n", " ");

            if (bindingProperties.Current is DomainAttributePropertyItem current)
            { current.PropertyValue = value.Trim(); }
        }

        private void attributeTitleData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(attributeTitleData.Text))
            { errorProvider.SetError(attributeTitleData.ErrorControl, "Attribute Title is required"); }
            else
            { errorProvider.SetError(attributeTitleData.ErrorControl, String.Empty); }
        }

        private void NewItemCommand_Click(object? sender, EventArgs e)
        {
            if (attributeTabLayout.SelectedIndex == 0) { bindingProperties.AddNew(); }
            else if (attributeTabLayout.SelectedIndex == 1) { bindingAlias.AddNew(); }
            else { }
        }

        private void bindingAlias_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (modelAliasNavigation.SelectedAlias is ModelNameSpaceItem selected)
            {
                DomainAttributeAliasItem newItem = new DomainAttributeAliasItem(DataKey);
                newItem.AliasName = modelAliasNavigation.SelectedAlias.MemberFullName;
                newItem.ScopeName = modelAliasNavigation.SelectedAlias.Scope.ToScopeName();

                e.NewObject = newItem;
            }
        }

        private void attributeTabLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (attributeTabLayout.SelectedIndex == 0)
            {
                newItemCommand.Enabled = true;
                newItemCommand.Image = Resources.NewProperty;
                newItemCommand.ToolTipText = "add Property";
            }
            else if (attributeTabLayout.SelectedIndex == 1)
            {
                newItemCommand.Enabled = true;
                newItemCommand.Image = Resources.NewSynonym;
                newItemCommand.ToolTipText = "add Alias";
            }
            else
            {
                newItemCommand.Enabled = false;
                newItemCommand.Image = Resources.NewDocument;
            }
        }

        private void DeleteItemCommand_Click(object? sender, EventArgs e)
        {
            if (bindingAttribute.Current is DomainAttributeItem data)
            {
                this.IsLocked(true);
                DomainAttributeKey key = new DomainAttributeKey(data);

                Program.Data.DomainAttributeAliases.Remove(key);
                Program.Data.DomainAttributeProperties.Remove(key);
                Program.Data.DomainAttributes.Remove(data);
                UpdateRowState();
            }
        }

        void UpdateRowState()
        {
            if (bindingAttribute.Current is DomainAttributeItem data)
            {
                RowState = data.RowState();

                if (RowState == DataRowState.Unchanged
                    && bindingProperties.DataSource is IEnumerable<DomainAttributePropertyItem> properties
                    && properties.Count(w => w.RowState() != DataRowState.Unchanged) > 0)
                { RowState = DataRowState.Modified; }

                if (RowState == DataRowState.Unchanged
                    && bindingAlias.DataSource is IEnumerable<DomainAttributeAliasItem> alias
                    && alias.Count(w => w.RowState() != DataRowState.Unchanged) > 0)
                { RowState = DataRowState.Modified; }
            }
        }
    }
}
