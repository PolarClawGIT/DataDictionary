using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainAttribute : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingAttribute.Current is IAttributeValue current && ReferenceEquals(current, item); }

        public DomainAttribute() : base()
        {
            InitializeComponent();
            toolStrip.TransferItems(attributeToolStrip, 0);

        }

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

            Setup(bindingAttribute);

            if (bindingAttribute.Current is IAttributeValue current)
            {
                bindingProperty.DataSource = new BindingView<AttributePropertyValue>(BusinessData.DomainModel.Attributes.Properties, w => key.Equals(w));
                bindingAlias.DataSource = new BindingView<AttributeAliasValue>(BusinessData.DomainModel.Attributes.Aliases, w => key.Equals(w));
                bindingSubjectArea.DataSource = new BindingView<AttributeSubjectAreaValue>(BusinessData.DomainModel.Attributes.SubjectArea, w => key.Equals(w));
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            IAttributeValue nameOfValues;
            PropertyNameMember.Load(propertyIdColumn);
            ScopeNameMember.Load(aliaseScopeColumn);

            this.DataBindings.Add(new Binding(nameof(this.Text), bindingAttribute, nameof(nameOfValues.AttributeTitle), false, DataSourceUpdateMode.OnPropertyChanged));
            titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingAttribute, nameof(nameOfValues.AttributeTitle), false, DataSourceUpdateMode.OnPropertyChanged));
            descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingAttribute, nameof(nameOfValues.AttributeDescription), false, DataSourceUpdateMode.OnPropertyChanged));

            AttributeNameMember.Load(typeOfAttributeData, BusinessData.DomainModel.Attributes);
            typeOfAttributeData.DataBindings.Add(new Binding(nameof(typeOfAttributeData.SelectedValue), bindingAttribute, nameof(nameOfValues.TypeOfAttributeId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));

            isSingleValueData.DataBindings.Add(new Binding(nameof(isSingleValueData.Checked), bindingAttribute, nameof(nameOfValues.IsSingleValue), false, DataSourceUpdateMode.OnPropertyChanged));
            isMultiValuedData.DataBindings.Add(new Binding(nameof(isMultiValuedData.Checked), bindingAttribute, nameof(nameOfValues.IsMultiValue), false, DataSourceUpdateMode.OnPropertyChanged));
            isSimpleTypeData.DataBindings.Add(new Binding(nameof(isSimpleTypeData.Checked), bindingAttribute, nameof(nameOfValues.IsSimpleType), false, DataSourceUpdateMode.OnPropertyChanged));
            isCompositeTypeData.DataBindings.Add(new Binding(nameof(isCompositeTypeData.Checked), bindingAttribute, nameof(nameOfValues.IsCompositeType), false, DataSourceUpdateMode.OnPropertyChanged));
            isIntegralData.DataBindings.Add(new Binding(nameof(isIntegralData.Checked), bindingAttribute, nameof(nameOfValues.IsIntegral), false, DataSourceUpdateMode.OnPropertyChanged));
            isDerivedData.DataBindings.Add(new Binding(nameof(isDerivedData.Checked), bindingAttribute, nameof(nameOfValues.IsDerived), false, DataSourceUpdateMode.OnPropertyChanged));
            isValuedData.DataBindings.Add(new Binding(nameof(isValuedData.Checked), bindingAttribute, nameof(nameOfValues.IsValued), false, DataSourceUpdateMode.OnPropertyChanged));
            isNullableData.DataBindings.Add(new Binding(nameof(isNullableData.Checked), bindingAttribute, nameof(nameOfValues.IsNullable), false, DataSourceUpdateMode.OnPropertyChanged));
            isNonKeyData.DataBindings.Add(new Binding(nameof(isNonKeyData.Checked), bindingAttribute, nameof(nameOfValues.IsNonKey), false, DataSourceUpdateMode.OnPropertyChanged));
            isKeyData.DataBindings.Add(new Binding(nameof(isKeyData.Checked), bindingAttribute, nameof(nameOfValues.IsKey), false, DataSourceUpdateMode.OnPropertyChanged));

            propertiesData.AutoGenerateColumns = false;
            propertiesData.DataSource = bindingProperty;

            aliasesData.AutoGenerateColumns = false;
            aliasesData.DataSource = bindingAlias;

            subjectArea.BindTo(bindingSubjectArea);

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingAttribute.Current is not IAttributeValue);
        }


        private void DeleteItemCommand_Click(object? sender, EventArgs e)
        {
            if (bindingAttribute.Current is IAttributeValue current)
            { BusinessData.DomainModel.Attributes.Remove(current); }
        }

        private void BindingProperty_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingAttribute.Current is AttributeValue current)
            {
                AttributePropertyValue newItem = new AttributePropertyValue(current);
                newItem.PropertyId = domainProperty.PropertyId;
                newItem.PropertyValue = domainProperty.PropertyValue;
                newItem.DefinitionText = domainProperty.DefinitionText;
                e.NewObject = newItem;
            }
        }

        private void BindingAlias_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingAttribute.Current is AttributeValue current)
            {
                AttributeAliasValue newItem = new AttributeAliasValue(current);
                newItem.AliasName = namedScopeData.ScopePath.MemberFullPath;
                newItem.Scope = namedScopeData.Scope;
                e.NewObject = newItem;
            }
        }

        private void BindingAlias_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingAlias.Current is IAliasValue current)
            {
                NamedScopePath path = new NamedScopePath(current.AliasName);

                namedScopeData.ScopePath = path;
                namedScopeData.Scope = current.Scope;
            }
        }

        private void NamedScopeData_OnApply(object sender, EventArgs e)
        {
            if (bindingAlias.DataSource is IList<IAliasValue> aliases
                && aliases.FirstOrDefault(
                    w => w.Scope == namedScopeData.Scope
                    && new NamedScopePath(w.AliasName) == namedScopeData.ScopePath)
                is IAliasValue value)
            { bindingAlias.Position = aliases.IndexOf(value); }
            else { bindingAlias.AddNew(); }
        }

        private void BindingProperty_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingProperty.Current is IPropertyValue current)
            {
                domainProperty.PropertyId = current.PropertyId ?? Guid.Empty;
                domainProperty.PropertyValue = current.PropertyValue ?? String.Empty; ;
                domainProperty.DefinitionText = current.DefinitionText ?? String.Empty;
            }
        }

        private void DomainProperty_OnApply(object sender, EventArgs e)
        {
            if (bindingProperty.DataSource is IList<IPropertyValue> properties
                && properties.FirstOrDefault(
                    w => w.PropertyId == domainProperty.PropertyId)
                is IPropertyValue value)
            {
                value.PropertyValue = domainProperty.PropertyValue;
                value.DefinitionText = domainProperty.DefinitionText;
                bindingProperty.Position = properties.IndexOf(value);
            }
            else { bindingProperty.AddNew(); }
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
        private void subjectArea_OnSubjectAdd(object sender, SubjectAreaValue e)
        {
            addingSubject = e;
            bindingSubjectArea.AddNew();
        }

        private void subjectArea_OnSubjectRemove(object sender, SubjectAreaValue e)
        {
            SubjectAreaIndex key = new SubjectAreaIndex(e);

            if (bindingSubjectArea.DataSource is IEnumerable<ISubjectAreaIndex> data
                && data.FirstOrDefault(w => key.Equals(w)) is AttributeSubjectAreaValue target)
            { bindingSubjectArea.Remove(target); }
        }
    }
}
