using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using DataDictionary.Main.Properties;
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

        public DomainAttribute(IAttributeValue attributeItem) : this()
        {
            AttributeIndex key = new AttributeIndex(attributeItem);
            bindingAttribute.DataSource = new BindingView<AttributeValue>(BusinessData.DomainModel.Attributes, w => key.Equals(w));
            bindingAttribute.Position = 0;

            Setup(bindingAttribute);

            if (bindingAttribute.Current is IAttributeValue current)
            {
                bindingProperty.DataSource = new BindingView<AttributePropertyValue>(BusinessData.DomainModel.Attributes.Properties, w => key.Equals(w));
                bindingAlias.DataSource = new BindingView<AttributeAliasValue>(BusinessData.DomainModel.Attributes.Aliases, w => key.Equals(w));
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            IAttributeValue nameOfValues;
            PropertyNameMember.Load(propertyIdColumn);
            ScopeNameItem.Load(aliaseScopeColumn);

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
            domainProperty.BindData(bindingProperty);

            aliasesData.AutoGenerateColumns = false;
            aliasesData.DataSource = bindingAlias;
            domainAlias.BindData(bindingAlias);

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingAttribute.Current is not IAttributeValue);
        }


        private void DeleteItemCommand_Click(object? sender, EventArgs e)
        {
            if (bindingAttribute.Current is IAttributeValue current)
            {
                BusinessData.DomainModel.Attributes.Remove(current);
                BusinessData.NamedScope.Remove(new NamedScopeKey(current));
            }
        }

        private void BindingProperty_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingAttribute.Current is AttributeValue current)
            {
                AttributePropertyValue newItem = new AttributePropertyValue(current);

                e.NewObject = newItem;
            }
        }

        private void BindingAlias_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingAttribute.Current is AttributeValue current)
            {
                AttributeAliasValue newItem = new AttributeAliasValue(current);
                e.NewObject = newItem;

                newItem.AliasName = domainAlias.SelectedAlias.MemberFullName;
                newItem.Scope = domainAlias.SelectedAlias.Scope;
            }
        }

        private void AddPropertyCommand_Click(object sender, EventArgs e)
        {
            if (detailTabLayout.SelectedTab == propertyTab)
            {
                bindingProperty.AddNew();
                domainProperty.RefreshControls();
            }
            else { detailTabLayout.SelectedTab = propertyTab; }
        }

        private void AddAliasCommand_Click(object sender, EventArgs e)
        {
            if (detailTabLayout.SelectedTab == aliasTab)
            { bindingAlias.AddNew(); }
            else
            { detailTabLayout.SelectedTab = aliasTab; }
        }
    }
}
