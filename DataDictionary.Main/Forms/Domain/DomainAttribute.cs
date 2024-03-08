using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Data;
using System.Xml.Linq;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainAttribute : ApplicationBase, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingAttribute.Current is IDomainAttributeItem current && ReferenceEquals(current, item); }

        public DomainAttribute() : base()
        {
            InitializeComponent();
            newItemCommand.Click += NewItemCommand_Click;

            deleteItemCommand.Enabled = true;
            deleteItemCommand.Image = Resources.DeleteAttribute;
            deleteItemCommand.Click += DeleteItemCommand_Click;
            deleteItemCommand.ToolTipText = "Delete Attribute";
        }

        public DomainAttribute(IDomainAttributeItem attributeItem) : this()
        {
            DomainAttributeKey key = new DomainAttributeKey(attributeItem);
            bindingAttribute.DataSource = new BindingView<DomainAttributeItem>(BusinessData.DomainModel.Attributes, w => key.Equals(w));
            bindingAttribute.Position = 0;

            if (bindingAttribute.Current is IDomainAttributeItem current)
            {
                this.Icon = new ScopeKey(ScopeType.ModelAttribute).Scope.ToIcon();
                RowState = current.RowState();
                current.RowStateChanged += RowStateChanged;
                this.Text = current.ToString();

                bindingProperty.DataSource = new BindingView<DomainAttributePropertyItem>(BusinessData.DomainModel.Attributes.Properties, w => key.Equals(w));
                bindingAlias.DataSource = new BindingView<DomainAttributeAliasItem>(BusinessData.DomainModel.Attributes.Aliases, w => key.Equals(w));
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            IDomainAttributeItem nameOfValues;
            PropertyNameItem.Load(propertyIdColumn);
            ScopeNameItem.Load(aliaseScopeColumn);

            this.DataBindings.Add(new Binding(nameof(this.Text), bindingAttribute, nameof(nameOfValues.AttributeTitle), false, DataSourceUpdateMode.OnPropertyChanged));
            titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingAttribute, nameof(nameOfValues.AttributeTitle), false, DataSourceUpdateMode.OnPropertyChanged));
            descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingAttribute, nameof(nameOfValues.AttributeDescription), false, DataSourceUpdateMode.OnPropertyChanged));

            AttributeNameItem.Load(typeOfAttributeData, BusinessData.DomainModel.Attributes);
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

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingAttribute.Current is not IDomainAttributeItem);
        }

        private void NewItemCommand_Click(object? sender, EventArgs e)
        {
            if (detailTabLayout.TabPages[detailTabLayout.SelectedIndex] == propertyTab)
            {
                bindingProperty.AddNew();
                domainProperty.RefreshControls();
            }
            else if (detailTabLayout.TabPages[detailTabLayout.SelectedIndex] == aliasTab)
            {
                bindingAlias.AddNew();
            }
            else { }
        }

        private void DeleteItemCommand_Click(object? sender, EventArgs e)
        {
            if (bindingAttribute.Current is IDomainAttributeItem current)
            {
                BusinessData.DomainModel.Attributes.Remove(current);
                BusinessData.NameScope.Remove(new NamedScopeKey(current));
            }
        }

        private void DetailTabLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (detailTabLayout.TabPages[detailTabLayout.SelectedIndex] == propertyTab)
            {
                newItemCommand.Enabled = true;
                newItemCommand.Image = Resources.NewProperty;
                newItemCommand.ToolTipText = "add Property";
            }
            else if (detailTabLayout.TabPages[detailTabLayout.SelectedIndex] == aliasTab)
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

        private void BindingProperty_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingAttribute.Current is DomainAttributeItem current)
            {
                DomainAttributePropertyItem newItem = new DomainAttributePropertyItem(current);
                e.NewObject = newItem;

            }
        }

        private void BindingAlias_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingAttribute.Current is DomainAttributeItem current)
            {
                DomainAttributeAliasItem newItem = new DomainAttributeAliasItem(current);
                e.NewObject = newItem;

                newItem.AliasName = domainAlias.SelectedAlias.MemberFullName;
                newItem.Scope = domainAlias.SelectedAlias.Scope;
            }
        }
    }
}
