using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainEntity : ApplicationBase, IApplicationDataForm<DomainEntityKey>
    {
        public required DomainEntityKey DataKey { get; init; }

        public bool IsOpenItem(object? item)
        { return DataKey.Equals(item); }

        public DomainEntity() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_ClassPublic;

            newItemCommand.Enabled = true;
            newItemCommand.Image = Resources.NewProperty;
            newItemCommand.ToolTipText = "add Property";
            newItemCommand.Click += NewItemCommand_Click;
        }



        private void DomainEntity_Load(object sender, EventArgs e)
        {
            // one time bindings
            PropertyNameItem.Load(propertyTypeData);
            PropertyNameItem.Load(propertyTypeColumn);

            (this as IApplicationDataBind).BindData();
        }

        public bool BindDataCore()
        {
            if (Program.Data.DomainEntities.FirstOrDefault(w => DataKey.Equals(w)) is DomainEntityItem data)
            {
                this.Text = data.EntityTitle;

                entityTitleData.DataBindings.Add(new Binding(nameof(entityTitleData.Text), data, nameof(data.EntityTitle)));
                entityDescriptionData.DataBindings.Add(new Binding(nameof(entityDescriptionData.Text), data, nameof(data.EntityDescription)));

                SubjectAreaNameItem.Load(subjectAreaData);
                subjectAreaData.ReadOnly = (subjectAreaData.DataSource is IList subjectAreaItems && subjectAreaItems.Count == 0);
                subjectAreaData.DataBindings.Add(new Binding(nameof(subjectAreaData.SelectedValue), data, nameof(data.SubjectAreaId), true, DataSourceUpdateMode.OnValidation, Guid.Empty));

                bindingProperties.DataSource =
                    new BindingView<DomainEntityPropertyItem>(
                        Program.Data.DomainEntityProperties,
                        w => DataKey.Equals(w));
                propertyNavigation.AutoGenerateColumns = false;
                propertyNavigation.DataSource = bindingProperties;

                DomainEntityPropertyItem propertyNameOf;
                propertyTypeData.DataBindings.Add(new Binding(nameof(propertyTypeData.SelectedValue), bindingProperties, nameof(propertyNameOf.PropertyId), true));
                propertyValueData.DataBindings.Add(new Binding(nameof(propertyValueData.Text), bindingProperties, nameof(propertyNameOf.PropertyValue), true));
                //propertyChoiceData.DataBindings.Add(new Binding(nameof(propertyChoiceData.SelectedValue, bindingProperties, nameof(propertyNames.))));
                propertyDefinitionData.DataBindings.Add(new Binding(nameof(propertyDefinitionData.Rtf), bindingProperties, nameof(propertyNameOf.DefinitionText), true));

                propertyChoiceData.Enabled = false;
                propertyValueData.Enabled = false;
                propertyDefinitionData.Enabled = false;

                if (bindingProperties.Current is DomainEntityPropertyItem propItem
                    && Program.Data.Properties.FirstOrDefault(w => w.PropertyId == propItem.PropertyId) is PropertyItem property)
                { BindChoiceData(property, propItem); }

                bindingAlias.DataSource = new BindingView<DomainEntityAliasItem>(Program.Data.DomainEntityAliases, w => DataKey.Equals(w));
                entityAliasData.AutoGenerateColumns = false;
                entityAliasData.DataSource = bindingAlias;

                SourceNameItem.Load(sourceNameData);
                ScopeNameItem.LoadEntity(scopeNameData);

                DomainEntityAliasItem alaisNameOf;
                sourceNameData.DataBindings.Add(new Binding(nameof(sourceNameData.SelectedValue), bindingAlias, nameof(alaisNameOf.SourceName), true, DataSourceUpdateMode.OnPropertyChanged, SourceNameItem.Empty));
                scopeNameData.DataBindings.Add(new Binding(nameof(scopeNameData.SelectedValue), bindingAlias, nameof(alaisNameOf.ScopeName), true, DataSourceUpdateMode.OnPropertyChanged, ScopeNameItem.Empty));
                aliasNameData.DataBindings.Add(new Binding(nameof(aliasNameData.SelectedValue), bindingAlias, nameof(alaisNameOf.AliasName), true, DataSourceUpdateMode.OnPropertyChanged, AliasNameItem.Empty));
                sourceNameData.Enabled = false;
                scopeNameData.Enabled = false;
                aliasNameData.Enabled = false;

                return true;
            }
            else { return false; }
        }

        public void UnbindDataCore()
        {
            entityTitleData.DataBindings.Clear();
            entityDescriptionData.DataBindings.Clear();
            subjectAreaData.DataBindings.Clear();

            propertyNavigation.DataSource = null;
            propertyTypeData.DataBindings.Clear();
            propertyValueData.DataBindings.Clear();
            propertyDefinitionData.DataBindings.Clear();

            sourceNameData.DataBindings.Clear();
            scopeNameData.DataBindings.Clear();
            aliasNameData.DataBindings.Clear();
            entityAliasData.DataSource = null;
            bindingAlias.DataSource = null;
        }

        void BindChoiceData(PropertyItem property, DomainEntityPropertyItem data)
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


        private void BindingComplete(object sender, BindingCompleteEventArgs e)
        { if (sender is BindingSource binding) { binding.BindComplete(sender, e); } }

        private void bindingProperties_AddingNew(object sender, AddingNewEventArgs e)
        {
            DomainEntityPropertyItem newItem = new DomainEntityPropertyItem(DataKey);

            if (bindingProperties.Current is null || propertyNavigation.CurrentRow is null)
            { // First Row scenario
                if (propertyTypeData.SelectedValue is Guid propertyId)
                { newItem.PropertyId = propertyId; }
            }

            e.NewObject = newItem;
        }


        private void bindingAlias_AddingNew(object sender, AddingNewEventArgs e)
        {
            DomainEntityAliasItem newItem = new DomainEntityAliasItem(DataKey);

            e.NewObject = newItem;
        }

        private void bindingAlias_CurrentChanged(object sender, EventArgs e)
        {
            sourceNameData.Enabled = true;
            scopeNameData.Enabled = true;
            aliasNameData.Enabled = true;

            if (sourceNameData.SelectedItem is SourceNameItem source &&
                scopeNameData.SelectedItem is ScopeNameItem scope)
            { AliasNameItem.LoadEntity(aliasNameData, source, scope); }
        }

        private void propertyNavigation_Leave(object sender, EventArgs e)
        {
            if (propertyNavigation.CurrentRow is not null && propertyNavigation.CurrentRow.IsNewRow)
            {
                propertyNavigation.NotifyCurrentCellDirty(true); // Marks the row as dirty so when end-edit the row is retained.
                propertyNavigation.EndEdit(); // Completes the edit so a binding error does not occur when focus changes.
            }
        }

        private void propertyTypeData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bindingProperties.Current is null)
            {
                if (bindingProperties.AddNew() is DomainEntityPropertyItem newItem
                    && bindingProperties.DataSource is BindingView<DomainEntityPropertyItem> data)
                { bindingProperties.Position = data.IndexOf(newItem); }
            }

            if (propertyTypeData.SelectedItem is PropertyNameItem selected
                && bindingProperties.Current is DomainEntityPropertyItem currentRow
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

            if (bindingProperties.Current is DomainEntityPropertyItem current)
            { current.PropertyValue = String.Join(", ", values); }
        }

        private void propertyChoiceData_EnabledChanged(object sender, EventArgs e)
        {
            if (propertyChoiceData.Enabled)
            { propertyChoiceData.ResetBackColor(); }
            else { propertyChoiceData.BackColor = SystemColors.Control; }
        }

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

            if (bindingProperties.Current is DomainEntityPropertyItem current)
            { current.PropertyValue = value.Trim(); }
        }

        private void EntityTitleData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(entityTitleData.Text))
            { errorProvider.SetError(entityTitleData.ErrorControl, "Entity Title is required"); }
            else
            { errorProvider.SetError(entityTitleData.ErrorControl, String.Empty); }
        }

        private void EntityTabLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (entityTabLayout.SelectedIndex == 0)
            {
                newItemCommand.Enabled = true;
                newItemCommand.Image = Resources.NewProperty;
                newItemCommand.ToolTipText = "add Property";
            }
            else if (entityTabLayout.SelectedIndex == 1)
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

        private void NewItemCommand_Click(object? sender, EventArgs e)
        {
            if (entityTabLayout.SelectedIndex == 0) { bindingProperties.AddNew(); }
            else if (entityTabLayout.SelectedIndex == 1) { bindingAlias.AddNew(); }
            else { }
        }

        private void SourceNameData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sourceNameData.SelectedItem is SourceNameItem source &&
                scopeNameData.SelectedItem is ScopeNameItem scope)
            { AliasNameItem.LoadEntity(aliasNameData, source, scope); }
        }

        private void ScopeNameData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sourceNameData.SelectedItem is SourceNameItem source &&
                scopeNameData.SelectedItem is ScopeNameItem scope)
            { AliasNameItem.LoadEntity(aliasNameData, source, scope); }
        }

    }
}
