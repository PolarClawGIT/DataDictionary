﻿using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.Collections;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainAttribute : ApplicationBase, IApplicationDataForm<DomainAttributeKey>
    {
        public required DomainAttributeKey DataKey { get; init; }

        public bool IsOpenItem(object? item)
        { return DataKey.Equals(item); }

        public DomainAttribute() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Attribute;
        }

        private void DomainAttribute_Load(object sender, EventArgs e)
        {
            // one time bindings
            PropertyNameItem.Bind(propertyTypeData);
            PropertyNameItem.Bind(propertyTypeColumn);

            (this as IApplicationDataBind).BindData();
        }

        public bool BindDataCore()
        {
            if (Program.Data.DomainAttributes.FirstOrDefault(w => DataKey.Equals(w)) is DomainAttributeItem data)
            {
                this.Text = data.AttributeTitle;

                attributeTitleData.DataBindings.Add(new Binding(nameof(attributeTitleData.Text), data, nameof(data.AttributeTitle)));
                attributeDescriptionData.DataBindings.Add(new Binding(nameof(attributeDescriptionData.Text), data, nameof(data.AttributeDescription)));
                
                SubjectAreaNameItem.Bind(subjectAreaData);
                subjectAreaData.ReadOnly = (subjectAreaData.DataSource is IList subjectAreaItems && subjectAreaItems.Count == 0);
                subjectAreaData.DataBindings.Add(new Binding(nameof(subjectAreaData.SelectedValue), data, nameof(data.SubjectAreaId), true, DataSourceUpdateMode.OnValidation, Guid.Empty));

                bindingProperties.DataSource =
                    new BindingView<DomainAttributePropertyItem>(
                        Program.Data.DomainAttributeProperties,
                        w => DataKey.Equals(w));
                propertyNavigation.AutoGenerateColumns = false;
                propertyNavigation.DataSource = bindingProperties;

                DomainAttributePropertyItem propertyMembers = new DomainAttributePropertyItem();
                propertyTypeData.DataBindings.Add(new Binding(nameof(propertyTypeData.SelectedValue), bindingProperties, nameof(propertyMembers.PropertyId), true));
                propertyValueData.DataBindings.Add(new Binding(nameof(propertyValueData.Text), bindingProperties, nameof(propertyMembers.PropertyValue), true));
                //propertyChoiceData.DataBindings.Add(new Binding(nameof(propertyChoiceData.SelectedValue, bindingProperties, nameof(propertyNames.))));
                propertyDefinitionData.DataBindings.Add(new Binding(nameof(propertyDefinitionData.Rtf), bindingProperties, nameof(propertyMembers.DefinitionText), true));

                propertyChoiceData.Enabled = false;
                propertyValueData.Enabled = false;
                propertyDefinitionData.Enabled = false;

                bindingDatabaseAlias.DataSource =
                    new BindingView<DomainAttributeAliasItem>(
                        Program.Data.DomainAttributeAliases,
                        w => DataKey.Equals(w));

                attributeAlaisData.AutoGenerateColumns = false;
                attributeAlaisData.DataSource = bindingDatabaseAlias;

                DomainAttributeAliasItem aliasMembers = new DomainAttributeAliasItem();
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), bindingDatabaseAlias, nameof(aliasMembers.DatabaseName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), bindingDatabaseAlias, nameof(aliasMembers.SchemaName)));
                objectNameData.DataBindings.Add(new Binding(nameof(objectNameData.Text), bindingDatabaseAlias, nameof(aliasMembers.ObjectName)));
                elementNameData.DataBindings.Add(new Binding(nameof(elementNameData.Text), bindingDatabaseAlias, nameof(aliasMembers.ElementName)));

                if (bindingDatabaseAlias.Current is DomainAttributeAliasItem aliasItem)
                {
                    CatalogNameItem.Bind(catalogNameData);
                    SchemaNameItem.Bind(schemaNameData, aliasItem);
                    ObjectNameItem.Bind(objectNameData, aliasItem);
                    ElementNameItem.Bind(elementNameData, aliasItem);
                }

                entityData.AutoGenerateColumns = false;
                entityData.DataSource = Program.Data.GetEntities(data).OrderBy(o => o.EntityTitle).ToList();

                if (bindingProperties.Current is DomainAttributePropertyItem propItem
                    && Program.Data.Properties.FirstOrDefault(w => w.PropertyId == propItem.PropertyId) is PropertyItem property)
                { BindChoiceData(property, propItem); }

                return true;
            }
            else { return false; }
        }
        public void UnbindDataCore()
        {
            attributeTitleData.DataBindings.Clear();
            attributeDescriptionData.DataBindings.Clear();
            subjectAreaData.DataBindings.Clear();

            propertyNavigation.DataSource = null;
            propertyTypeData.DataBindings.Clear();
            propertyValueData.DataBindings.Clear();
            propertyDefinitionData.DataBindings.Clear();

            attributeAlaisData.DataSource = null;
            catalogNameData.DataBindings.Clear();
            schemaNameData.DataBindings.Clear();
            objectNameData.DataBindings.Clear();

            entityData.DataSource = null;
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

        private void catalogNameData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(catalogNameData.Text))
            { errorProvider.SetError(catalogNameData.ErrorControl, "Catalog Name is required"); }
            else { errorProvider.SetError(catalogNameData.ErrorControl, String.Empty); }
        }

        private void catalogNameData_Validated(object sender, EventArgs e)
        {
            if (bindingDatabaseAlias.Current is DomainAttributeAliasItem aliasItem)
            {
                SchemaNameItem.Bind(schemaNameData, aliasItem);
                ObjectNameItem.Bind(objectNameData, aliasItem);
                ElementNameItem.Bind(elementNameData, aliasItem);
            }
        }

        private void schemaNameData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(schemaNameData.Text))
            { errorProvider.SetError(schemaNameData.ErrorControl, "Schema Name is required"); }
            else
            { errorProvider.SetError(schemaNameData.ErrorControl, String.Empty); }
        }

        private void schemaNameData_Validated(object sender, EventArgs e)
        {
            if (bindingDatabaseAlias.Current is DomainAttributeAliasItem aliasItem)
            {
                ObjectNameItem.Bind(objectNameData, aliasItem);
                ElementNameItem.Bind(elementNameData, aliasItem);
            }
        }

        private void objectNameData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(objectNameData.Text))
            { errorProvider.SetError(objectNameData.ErrorControl, "Object Name is required"); }
            else
            { errorProvider.SetError(objectNameData.ErrorControl, String.Empty); }
        }

        private void objectNameData_Validated(object sender, EventArgs e)
        {
            if (bindingDatabaseAlias.Current is DomainAttributeAliasItem aliasItem)
            {
                ElementNameItem.Bind(elementNameData, aliasItem);
            }
        }

        private void elementNameData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(elementNameData.Text))
            { errorProvider.SetError(elementNameData.ErrorControl, "Element Name is required"); }
            else
            { errorProvider.SetError(elementNameData.ErrorControl, String.Empty); }
        }

        private void elementNameData_Validated(object sender, EventArgs e)
        {
            if (bindingDatabaseAlias.Current is DomainAttributeAliasItem aliasItem)
            { }
        }

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

        private void bindingDatabaseAlias_AddingNew(object sender, AddingNewEventArgs e)
        {
            DomainAttributeAliasItem newItem = new DomainAttributeAliasItem(DataKey);

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

        private void attributeAlaisData_Leave(object sender, EventArgs e)
        {
            if (attributeAlaisData.CurrentRow is not null && attributeAlaisData.CurrentRow.IsNewRow)
            {
                attributeAlaisData.NotifyCurrentCellDirty(true); // Marks the row as dirty so when end-edit the row is retained.
                attributeAlaisData.EndEdit(); // Completes the edit so a binding error does not occur when focus changes.
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
    }
}
