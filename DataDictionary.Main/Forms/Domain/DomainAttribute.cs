using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using DataDictionary.Main.Enumerations;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainAttribute : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingAttribute.Current is IAttributeValue current && ReferenceEquals(current, item); }

        protected DomainAttribute() : base()
        { InitializeComponent(); }

        public DomainAttribute(IAttributeValue? attributeItem) : this()
        {
            if (attributeItem is null)
            {
                attributeItem = new AttributeValue();
                BusinessData.DomainModel.Attributes.Add(attributeItem);
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

            this.DataBindings.Add(new Binding(nameof(this.Text), bindingAttribute, nameof(IAttributeValue.AttributeTitle), false, DataSourceUpdateMode.OnPropertyChanged));

            titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingAttribute, nameof(IAttributeValue.AttributeTitle), false, DataSourceUpdateMode.OnPropertyChanged));
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

            aliasesData.AutoGenerateColumns = false;
            aliasesData.DataSource = bindingAlias;

            subjectArea.BindTo(bindingSubjectArea);

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingAttribute.Current is not IAttributeValue);
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            if (bindingAttribute.Current is IAttributeValue current)
            { DoWork(BusinessData.DomainModel.Attributes.Delete(current)); }
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
                newItem.AliasName = namedScopeData.ScopePath.MemberFullPath;
                newItem.AliasScope = namedScopeData.Scope;
                e.NewObject = newItem;
            }
        }

        private void BindingAlias_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingAlias.Current is IAliasValue current)
            {
                NamedScopePath path = new NamedScopePath(NamedScopePath.Parse(current.AliasName).ToArray());

                namedScopeData.ScopePath = path;
                namedScopeData.Scope = current.AliasScope;
            }
        }

        private void NamedScopeData_OnApply(object sender, EventArgs e)
        {
            if (bindingAlias.DataSource is IList<IAliasValue> aliases
                && aliases.FirstOrDefault(
                    w => w.AliasScope == namedScopeData.Scope
                    && new NamedScopePath(NamedScopePath.Parse(w.AliasName).ToArray()) == namedScopeData.ScopePath)
                is IAliasValue value)
            { bindingAlias.Position = aliases.IndexOf(value); }
            else { bindingAlias.AddNew(); }
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
    }
}
