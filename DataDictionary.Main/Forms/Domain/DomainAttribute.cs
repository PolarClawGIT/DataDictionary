using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.ApplicationWide;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using DataDictionary.Main.Properties;
using System;
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
    partial class DomainAttribute : ApplicationBase, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return mainBinding.Current is IDomainAttributeItem current && ReferenceEquals(current, item); }

        public DomainAttribute() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Attribute;
            newItemCommand.Click += NewItemCommand_Click;
        }

        public DomainAttribute(IDomainAttributeItem attributeItem) : this()
        {
            DomainAttributeKey key = new DomainAttributeKey(attributeItem);
            mainBinding.DataSource = new BindingView<DomainAttributeItem>(BusinessData.DomainData.DomainAttributes, w => key.Equals(w));
            mainBinding.Position = 0;

            if (mainBinding.Current is IDomainAttributeItem current)
            {
                this.Icon = new ScopeKey(ScopeType.ModelAttribute).Scope.ToIcon();
                RowState = current.RowState();
                current.RowStateChanged += RowStateChanged;
                this.Text = current.ToString();

                propertyBinding.DataSource = new BindingView<DomainAttributePropertyItem>(BusinessData.DomainData.DomainAttributes.DomainAttributeProperties, w => key.Equals(w));
                aliasBinding.DataSource = new BindingView<DomainAttributeAliasItem>(BusinessData.DomainData.DomainAttributes.DomainAttributeAliases, w => key.Equals(w));
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            IDomainAttributeItem nameOfValues;
            PropertyNameItem.Load(propertyIdColumn);
            ScopeNameItem.Load(aliaseScopeColumn);

            this.DataBindings.Add(new Binding(nameof(this.Text), mainBinding, nameof(nameOfValues.AttributeTitle)));
            titleData.DataBindings.Add(new Binding(nameof(titleData.Text), mainBinding, nameof(nameOfValues.AttributeTitle)));
            descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), mainBinding, nameof(nameOfValues.AttributeDescription)));

            isSingleValueData.DataBindings.Add(new Binding(nameof(isSingleValueData.Checked), mainBinding, nameof(nameOfValues.IsSingleValue), false, DataSourceUpdateMode.OnPropertyChanged));
            isMultiValuedData.DataBindings.Add(new Binding(nameof(isMultiValuedData.Checked), mainBinding, nameof(nameOfValues.IsMultiValue), false, DataSourceUpdateMode.OnPropertyChanged));
            isSimpleTypeData.DataBindings.Add(new Binding(nameof(isSimpleTypeData.Checked), mainBinding, nameof(nameOfValues.IsSimpleType), false, DataSourceUpdateMode.OnPropertyChanged));
            isCompositeTypeData.DataBindings.Add(new Binding(nameof(isCompositeTypeData.Checked), mainBinding, nameof(nameOfValues.IsCompositeType), false, DataSourceUpdateMode.OnPropertyChanged));
            isIntegralData.DataBindings.Add(new Binding(nameof(isIntegralData.Checked), mainBinding, nameof(nameOfValues.IsIntegral), false, DataSourceUpdateMode.OnPropertyChanged));
            isDerivedData.DataBindings.Add(new Binding(nameof(isDerivedData.Checked), mainBinding, nameof(nameOfValues.IsDerived), false, DataSourceUpdateMode.OnPropertyChanged));
            isValuedData.DataBindings.Add(new Binding(nameof(isValuedData.Checked), mainBinding, nameof(nameOfValues.IsValued), false, DataSourceUpdateMode.OnPropertyChanged));
            isNullableData.DataBindings.Add(new Binding(nameof(isNullableData.Checked), mainBinding, nameof(nameOfValues.IsNullable), false, DataSourceUpdateMode.OnPropertyChanged));
            isNonKeyData.DataBindings.Add(new Binding(nameof(isNonKeyData.Checked), mainBinding, nameof(nameOfValues.IsNonKey), false, DataSourceUpdateMode.OnPropertyChanged));
            isKeyData.DataBindings.Add(new Binding(nameof(isKeyData.Checked), mainBinding, nameof(nameOfValues.IsKey), false, DataSourceUpdateMode.OnPropertyChanged));

            propertiesData.AutoGenerateColumns = false;
            propertiesData.DataSource = propertyBinding;
            domainProperty.BindData(propertyBinding);

            aliasesData.AutoGenerateColumns = false;
            aliasesData.DataSource = aliasBinding;
            domainAlias.BindData(aliasBinding);

            toolTip.LoadToolTips(this);
            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || mainBinding.Current is not IDomainAttributeItem);
        }

        private void RowStateChanged(object? sender, EventArgs e)
        {
            if (sender is IBindingRowState data)
            {
                RowState = data.RowState();
                if (IsHandleCreated)
                { this.Invoke(() => { this.IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted); }); }
                else { this.IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted); }
            }
        }

        private void NewItemCommand_Click(object? sender, EventArgs e)
        {
            if (detailTabLayout.TabPages[detailTabLayout.SelectedIndex] == propertyTab)
            {
                propertyBinding.AddNew();
                domainProperty.RefreshControls();
            }
            else if (detailTabLayout.TabPages[detailTabLayout.SelectedIndex] == aliasTab)
            {
                aliasBinding.AddNew();
                domainAlias.RefreshControls();
            }
            else { }
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

        private void PropertyBinding_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (mainBinding.Current is DomainAttributeItem current)
            {
                DomainAttributePropertyItem newItem = new DomainAttributePropertyItem(current);
                e.NewObject = newItem;
            }
        }

        private void AliasBinding_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (mainBinding.Current is DomainAttributeItem current)
            {
                DomainAttributeAliasItem newItem = new DomainAttributeAliasItem(current);
                e.NewObject = newItem;
            }
        }
    }
}
