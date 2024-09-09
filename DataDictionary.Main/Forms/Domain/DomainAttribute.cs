using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using DataDictionary.Main.Enumerations;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using DataDictionary.Main.Dialogs;

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainAttribute : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingAttribute.Current is IAttributeValue current && ReferenceEquals(current, item); }

        Boolean isNew = false; // Flags the item as new to handled deferred Refresh.

        protected DomainAttribute() : base()
        {
            InitializeComponent();

            aliasAddCommand.Image = ImageEnumeration.GetImage(ScopeType.ModelEntityAlias, CommandImageType.Add);
            aliasSelectCommand.Image = ImageEnumeration.GetImage(ScopeType.ModelEntityAlias, CommandImageType.Select);
        }

        public DomainAttribute(IAttributeValue? attributeItem) : this()
        {
            if (attributeItem is null)
            {
                attributeItem = new AttributeValue();
                BusinessData.DomainModel.Attributes.Add(attributeItem);
                isNew = true;
            }

            AttributeIndex key = new AttributeIndex(attributeItem);

            bindingAttribute.DataSource = new BindingView<AttributeValue>(BusinessData.DomainModel.Attributes, w => key.Equals(w));
            bindingAttribute.Position = 0;

            if (bindingAttribute.Current is IAttributeValue current)
            {
                Setup(bindingAttribute, CommandImageType.Delete);

                bindingProperty.DataSource = new BindingView<AttributePropertyValue>(BusinessData.DomainModel.Attributes.Properties, w => key.Equals(w));
                bindingDefinition.DataSource = new BindingView<AttributeDefinitionValue>(BusinessData.DomainModel.Attributes.Definitions, w => key.Equals(w));
                bindingAlias.DataSource = new BindingView<AttributeAliasValue>(BusinessData.DomainModel.Attributes.Aliases, w => key.Equals(w));
                bindingSubjectArea.DataSource = new BindingView<AttributeSubjectAreaValue>(BusinessData.DomainModel.Attributes.SubjectArea, w => key.Equals(w));
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            PropertyNameList.Load(propertyIdColumn);
            DefinitionNameList.Load(definitionColumn);
            ScopeNameList.Load(aliaseScopeColumn);

            if (isNew) { SendMessage(new RefreshNavigation()); }

            this.DataBindings.Add(new Binding(nameof(this.Text), bindingAttribute, nameof(IAttributeValue.AttributeTitle)));

            titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingAttribute, nameof(IAttributeValue.AttributeTitle)));
            descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingAttribute, nameof(IAttributeValue.AttributeDescription), false, DataSourceUpdateMode.OnPropertyChanged));

            memberNameData.DataBindings.Add(new Binding(nameof(memberNameData.Text), bindingAttribute, nameof(IAttributeValue.MemberName), false, DataSourceUpdateMode.OnPropertyChanged));

            isSingleValueData.DataBindings.Add(new Binding(nameof(isSingleValueData.Checked), bindingAttribute, nameof(IAttributeValue.IsSingleValue), false, DataSourceUpdateMode.OnPropertyChanged));
            isMultiValuedData.DataBindings.Add(new Binding(nameof(isMultiValuedData.Checked), bindingAttribute, nameof(IAttributeValue.IsMultiValue), false, DataSourceUpdateMode.OnPropertyChanged));
            isSimpleTypeData.DataBindings.Add(new Binding(nameof(isSimpleTypeData.Checked), bindingAttribute, nameof(IAttributeValue.IsSimpleType), false, DataSourceUpdateMode.OnPropertyChanged));
            isCompositeTypeData.DataBindings.Add(new Binding(nameof(isCompositeTypeData.Checked), bindingAttribute, nameof(IAttributeValue.IsCompositeType), false, DataSourceUpdateMode.OnPropertyChanged));
            isIntegralData.DataBindings.Add(new Binding(nameof(isIntegralData.Checked), bindingAttribute, nameof(IAttributeValue.IsIntegral), false, DataSourceUpdateMode.OnPropertyChanged));
            isDerivedData.DataBindings.Add(new Binding(nameof(isDerivedData.Checked), bindingAttribute, nameof(IAttributeValue.IsDerived), false, DataSourceUpdateMode.OnPropertyChanged));
            isValuedData.DataBindings.Add(new Binding(nameof(isValuedData.Checked), bindingAttribute, nameof(IAttributeValue.IsValued), false, DataSourceUpdateMode.OnPropertyChanged));
            isNullableData.DataBindings.Add(new Binding(nameof(isNullableData.Checked), bindingAttribute, nameof(IAttributeValue.IsNullable), false, DataSourceUpdateMode.OnPropertyChanged));
            isNonKeyData.DataBindings.Add(new Binding(nameof(isNonKeyData.Checked), bindingAttribute, nameof(IAttributeValue.IsNonKey), false, DataSourceUpdateMode.OnPropertyChanged));
            isKeyData.DataBindings.Add(new Binding(nameof(isKeyData.Checked), bindingAttribute, nameof(IAttributeValue.IsKey), false, DataSourceUpdateMode.OnPropertyChanged));

            propertiesData.AutoGenerateColumns = false;
            propertiesData.DataSource = bindingProperty;

            definitionData.AutoGenerateColumns = false;
            definitionData.DataSource = bindingDefinition;

            subjectArea.BindTo(bindingSubjectArea);

            // Alias Handling
            ScopeNameList.Load(aliaseScopeColumn);
            ScopeNameList.Load(aliasScopeData);

            aliasesData.AutoGenerateColumns = false;
            aliasesData.DataSource = bindingAlias;

            aliasScopeData.DataBindings.Add(new Binding(nameof(aliasScopeData.SelectedValue), bindingAlias, nameof(IEntityAliasValue.AliasScope), false, DataSourceUpdateMode.OnPropertyChanged) { DataSourceNullValue = ScopeNameList.NullValue });
            aliasNameData.DataBindings.Add(new Binding(nameof(aliasNameData.Text), bindingAlias, nameof(EntityAliasValue.AliasName), false, DataSourceUpdateMode.OnPropertyChanged));


            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingAttribute.Current is not IAttributeValue);
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            if (bindingAttribute.Current is IAttributeValue current)
            { DoWork(BusinessData.DomainModel.Attributes.Delete(current), Complete); }

            void Complete(RunWorkerCompletedEventArgs args)
            { SendMessage(new RefreshNavigation()); }
        }

        private void BindingProperty_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingAttribute.Current is AttributeValue current)
            {
                AttributePropertyValue newItem = new AttributePropertyValue(current);
                newItem.PropertyId = domainProperty.PropertyId;
                newItem.PropertyValue = domainProperty.PropertyValue;
                e.NewObject = newItem;
            }
        }

        private void BindingAlias_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingAttribute.Current is AttributeValue current)
            {
                AttributeAliasValue newItem = new AttributeAliasValue(current);
                e.NewObject = newItem;
            }
        }

        private void BindingAlias_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingAlias.Current is AttributeAliasValue current)
            {
                Boolean inModel = BusinessData.NamedScope.PathKeys(current.AliasPath).Count > 0;
                isAliasInModelData.Checked = inModel;
                aliasNameData.ReadOnly = inModel;
                aliasScopeData.ReadOnly = inModel;
            }
        }

        private void BindingProperty_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingProperty.Current is AttributePropertyValue current)
            {
                domainProperty.PropertyId = current.PropertyId ?? Guid.Empty;
                domainProperty.PropertyValue = current.PropertyValue ?? String.Empty;
            }
        }

        private void DomainProperty_OnApply(object sender, EventArgs e)
        {
            if (bindingProperty.DataSource is IList<AttributePropertyValue> properties
                && properties.FirstOrDefault(
                    w => w.PropertyId == domainProperty.PropertyId)
                is AttributePropertyValue value)
            {
                value.PropertyValue = domainProperty.PropertyValue;
                bindingProperty.Position = properties.IndexOf(value);
            }
            else { bindingProperty.AddNew(); }
        }

        private void DomainDefinition_OnApply(object sender, EventArgs e)
        {
            if (bindingDefinition.DataSource is IList<AttributeDefinitionValue> definition
                    && definition.FirstOrDefault(
                        w => domainDefinition.Definition is IDefinitionIndex
                        && domainDefinition.Definition.Equals(w))
                    is AttributeDefinitionValue value)
            {
                value.DefinitionSummary = domainDefinition.DefinitionSummary;
                value.DefinitionText = domainDefinition.DefinitionText;
                bindingDefinition.Position = definition.IndexOf(value);
            }
            else { bindingDefinition.AddNew(); }
        }

        private void BindingDefinition_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingDefinition.Current is AttributeDefinitionValue current)
            {
                domainDefinition.DefinitionId = current.DefinitionId ?? Guid.Empty;
                domainDefinition.DefinitionText = current.DefinitionText;
                domainDefinition.DefinitionSummary = current.DefinitionSummary ?? String.Empty;
            }
        }

        private void BindingDefinition_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingAttribute.Current is AttributeValue current)
            {
                AttributeDefinitionValue newItem = new AttributeDefinitionValue(current);
                newItem.DefinitionId = domainDefinition.DefinitionId;
                newItem.DefinitionSummary = domainDefinition.DefinitionSummary;
                newItem.DefinitionText = domainDefinition.DefinitionText;
                e.NewObject = newItem;
            }
        }

        private void BindingSubjectArea_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (addingSubject is SubjectAreaValue subject && bindingAttribute.Current is AttributeValue attribute)
            {
                AttributeSubjectAreaValue newItem = new AttributeSubjectAreaValue(attribute, subject);
                e.NewObject = newItem;
            }
            addingSubject = null;
        }

        SubjectAreaValue? addingSubject = null;
        private void SubjectArea_OnSubjectAdd(object sender, SubjectAreaValue e)
        {
            addingSubject = e;
            bindingSubjectArea.AddNew();
        }

        private void SubjectArea_OnSubjectRemove(object sender, SubjectAreaValue e)
        {
            SubjectAreaIndex key = new SubjectAreaIndex(e);

            if (bindingSubjectArea.DataSource is IEnumerable<ISubjectAreaIndex> data
                && data.FirstOrDefault(w => key.Equals(w)) is AttributeSubjectAreaValue target)
            { bindingSubjectArea.Remove(target); }
        }

        private void MemberNameData_Validating(object sender, CancelEventArgs e)
        {
            NamedScopePath path = new NamedScopePath(NamedScopePath.Parse(memberNameData.Text).ToArray());
            memberNameData.Text = path.MemberFullPath;
        }

        private void AliasSelectCommand_Click(object sender, EventArgs e)
        {
            if (bindingAlias.DataSource is IList<AttributeAliasValue> alias)
            {
                using (var dialog = new SelectionDialog(this))
                {
                    dialog.FilterScopes.Add(ScopeType.ModelAttribute);
                    dialog.FilterScopes.Add(ScopeType.DatabaseTableColumn);
                    dialog.FilterScopes.Add(ScopeType.DatabaseViewColumn);
                    dialog.FilterScopes.Add(ScopeType.LibraryTypeField);
                    dialog.FilterScopes.Add(ScopeType.LibraryTypeProperty);

                    dialog.BuildData(alias.SelectMany(s => BusinessData.NamedScope.PathKeys(s.AliasPath)));

                    if (dialog.ShowDialog(this) is DialogResult.OK)
                    {
                        IEnumerable<INamedScopeValue> selected = dialog.SelectedByNamedScope();
                        IEnumerable<AttributeAliasValue> inModel = alias.Where(w => BusinessData.NamedScope.PathKeys(w.AliasPath).Count() > 0);

                        foreach (AttributeAliasValue removeItem in alias.Where(w => !selected.Select(s => s.Path).Contains(w.AliasPath)).ToList())
                        {
                            if (inModel.Contains(removeItem)) // Only remove items that are in this model
                            { alias.Remove(removeItem); }
                        }

                        foreach (INamedScopeValue addItem in selected.Where(w => !alias.Select(s => s.AliasPath).Contains(w.Path)).ToList())
                        { // Add
                            if (bindingAlias.AddNew() is AttributeAliasValue newValue)
                            {
                                newValue.AliasPath = addItem.Path;
                                newValue.AliasScope = addItem.Scope;
                            }
                        }
                    }
                }
            }
        }

        private void AliasAddCommand_Click(object sender, EventArgs e)
        {
            if (bindingAlias.AddNew() is AttributeAliasValue newValue)
            { }
        }

        private void AliasNameData_Validating(object sender, CancelEventArgs e)
        {
            NamedScopePath path = new NamedScopePath(NamedScopePath.Parse(aliasNameData.Text).ToArray());
            aliasNameData.Text = path.MemberFullPath;
        }
    }
}
