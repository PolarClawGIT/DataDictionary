using DataDictionary.DataLayer.DomainData;
using DataDictionary.DataLayer.DomainData.Attribute;
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
        public bool IsOpenItem(object? item)
        {
            return (
                mainBinding.Current is DomainAttributeItem current
                && new DomainAttributeKey(current).Equals(item));
        }

        public DomainAttribute() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Attribute;

            newItemCommand.Click += NewItemCommand_Click;
        }

        public DomainAttribute(DomainAttributeItem data) : this()
        {
            if (Program.Data.DomainAttributes.Contains(data))
            {
                DomainAttributeKey key = new DomainAttributeKey(data);
                mainBinding.DataSource = new BindingView<DomainAttributeItem>(Program.Data.DomainAttributes, w => key.Equals(w));
                mainBinding.Position = 0;

                propertyBinding.DataSource = new BindingView<DomainAttributePropertyItem>(Program.Data.DomainAttributeProperties, w => key.Equals(w));
            }
            else
            {
                mainBinding.DataSource = new BindingList<DomainAttributeItem>() { data };
                mainBinding.Position = 0;
            }

            RowState = data.RowState();
            data.RowStateChanged += Data_RowStateChanged;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            IDomainAttributeItem nameOfValues;
            PropertyNameItem.Load(propertyIdColumn);

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

            // TODO: Could this be stored in Help Docs for easy retrieval? Otherwise this text is maintained in multiple locations.
            toolTip.SetToolTip(isSingleValueData, DomainAttribute_Resource.IsSingleValue);
            toolTip.SetToolTip(isMultiValuedData, DomainAttribute_Resource.IsMultiValue);
            toolTip.SetToolTip(isSimpleTypeData, DomainAttribute_Resource.IsSimpleType);
            toolTip.SetToolTip(isCompositeTypeData, DomainAttribute_Resource.IsCompositeType);
            toolTip.SetToolTip(isIntegralData, DomainAttribute_Resource.IsIntegral);
            toolTip.SetToolTip(isDerivedData, DomainAttribute_Resource.IsDerived);
            toolTip.SetToolTip(isValuedData, DomainAttribute_Resource.IsValued);
            toolTip.SetToolTip(isNullableData, DomainAttribute_Resource.IsNullable);
            toolTip.SetToolTip(isNonKeyData, DomainAttribute_Resource.IsNonKey);
            toolTip.SetToolTip(isKeyData, DomainAttribute_Resource.IsKey);

        }

        private void Data_RowStateChanged(object? sender, EventArgs e)
        {
            if (sender is IBindingTableRow data)
            {
                this.Invoke(() =>
                {
                    RowState = data.RowState();
                    this.IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted);
                });
            }
        }

        private void NewItemCommand_Click(object? sender, EventArgs e)
        {
            if (detailTabLayout.TabPages[detailTabLayout.SelectedIndex] == propertyTab)
            {
                propertyBinding.AddNew();
                domainProperty.RefreshControls();
            }
            else if (detailTabLayout.TabPages[detailTabLayout.SelectedIndex] == aliasTab) { }
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
    }
}
