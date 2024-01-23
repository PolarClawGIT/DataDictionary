using DataDictionary.DataLayer.DomainData.Attribute;
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
    partial class DomainAttribute_New : ApplicationBase, IApplicationDataForm
    {
        public bool IsOpenItem(object? item)
        {
            return (
                bindingSource.Current is DomainAttributeItem current
                && new DomainAttributeKey(current).Equals(item));
        }

        public DomainAttribute_New() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Attribute;
        }

        public DomainAttribute_New(DomainAttributeItem data) : this()
        {
            if (Program.Data.DomainAttributes.Contains(data))
            {
                DomainAttributeKey key = new DomainAttributeKey(data);
                bindingSource.DataSource = new BindingView<DomainAttributeItem>(Program.Data.DomainAttributes, w => key.Equals(w));
                bindingSource.Position = 0;

            }
            else
            {
                bindingSource.DataSource = new BindingList<DomainAttributeItem>() { data };
                bindingSource.Position = 0;
            }

            RowState = data.RowState();
            data.RowStateChanged += Data_RowStateChanged;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            IDomainAttributeItem nameOfValues;
            this.DataBindings.Add(new Binding(nameof(this.Text), bindingSource, nameof(nameOfValues.AttributeTitle)));
            titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingSource, nameof(nameOfValues.AttributeTitle)));
            descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingSource, nameof(nameOfValues.AttributeDescription)));

            isSingleValueData.DataBindings.Add(new Binding(nameof(isSingleValueData.Checked), bindingSource, nameof(nameOfValues.IsSingleValue), false, DataSourceUpdateMode.OnPropertyChanged));
            isMultiValuedData.DataBindings.Add(new Binding(nameof(isMultiValuedData.Checked), bindingSource, nameof(nameOfValues.IsMultiValue), false, DataSourceUpdateMode.OnPropertyChanged));
            isSimpleTypeData.DataBindings.Add(new Binding(nameof(isSimpleTypeData.Checked), bindingSource, nameof(nameOfValues.IsSimpleType), false, DataSourceUpdateMode.OnPropertyChanged));
            isCompositeTypeData.DataBindings.Add(new Binding(nameof(isCompositeTypeData.Checked), bindingSource, nameof(nameOfValues.IsCompositeType), false, DataSourceUpdateMode.OnPropertyChanged));
            isIntegralData.DataBindings.Add(new Binding(nameof(isIntegralData.Checked), bindingSource, nameof(nameOfValues.IsIntegral), false, DataSourceUpdateMode.OnPropertyChanged));
            isDerivedData.DataBindings.Add(new Binding(nameof(isDerivedData.Checked), bindingSource, nameof(nameOfValues.IsDerived), false, DataSourceUpdateMode.OnPropertyChanged));
            isValuedData.DataBindings.Add(new Binding(nameof(isValuedData.Checked), bindingSource, nameof(nameOfValues.IsValued), false, DataSourceUpdateMode.OnPropertyChanged));
            isNullableData.DataBindings.Add(new Binding(nameof(isNullableData.Checked), bindingSource, nameof(nameOfValues.IsNullable), false, DataSourceUpdateMode.OnPropertyChanged));
            isNonKeyData.DataBindings.Add(new Binding(nameof(isNonKeyData.Checked), bindingSource, nameof(nameOfValues.IsNonKey), false, DataSourceUpdateMode.OnPropertyChanged));
            isKeyData.DataBindings.Add(new Binding(nameof(isKeyData.Checked), bindingSource, nameof(nameOfValues.IsKey), false, DataSourceUpdateMode.OnPropertyChanged));

            // TODO: Could this be stored in Help Docs for easy retrieval? Otherwise this text is maintained in muliple locations.
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


    }
}
