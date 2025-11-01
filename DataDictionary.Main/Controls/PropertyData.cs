using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Main.Controls.ComboBoxList;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls
{
    partial class PropertyData : UserControl
    {
        BindingSource? dataBinding; // Pointer to the BindingSource.
        Func<IPropertySubType>? onAddProperty; // Constructor for the Property

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingView<PropertyValue> Properties { get; private set; } =
            new BindingView<PropertyValue>(BusinessData.Model.Properties)
            { AllowEdit = false, AllowNew = false, AllowRemove = false };

        public PropertyData()
        {
            InitializeComponent();
        }

        public void BindTo(BindingSource binding, Func<IPropertySubType> newProperty)
        {
            dataBinding = binding;
            onAddProperty = newProperty;
            PropertyNameList.Load(propertyTypeData, Properties);
            PropertyNameList.Load(propertyIdColumn, Properties);

            propertyTypeData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedValue), binding, nameof(IPropertySubType.PropertyId), false, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));
            propertyValueData.DataBindings.Add(new Binding(nameof(TextBox.Text), binding, nameof(IPropertySubType.PropertyValue), false, DataSourceUpdateMode.OnPropertyChanged));

            propertyGrid.AutoGenerateColumns = false;
            propertyGrid.DataSource = dataBinding;

            RebuildChoices();

            dataBinding.AddingNew += DataBinding_AddingNew;
            dataBinding.CurrentChanged += DataBinding_CurrentChanged;

            void DataBinding_AddingNew(Object? sender, AddingNewEventArgs e)
            { e.NewObject = onAddProperty(); }

            void DataBinding_CurrentChanged(Object? sender, EventArgs e)
            { RebuildChoices(); }
        }

        private void PropertyTypeData_SelectedIndexChanged(object sender, EventArgs e)
        { } // This can be called multiple times

        private void PropertyTypeData_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (propertyTypeData.SelectedValue is Guid value && value != Guid.Empty)
            {
                // Handling of a new or existing item.
                if (dataBinding is not null &&
                    propertyTypeData.SelectedItem is IPropertyIndex selectedValue)
                {
                    PropertyIndex key = new PropertyIndex(selectedValue);
                    var currentValues = dataBinding.List.OfType<IPropertySubType>().ToList();

                    if (currentValues.FirstOrDefault(w => key.Equals(w)) is IPropertySubType currentValue)
                    { dataBinding.Position = currentValues.IndexOf(currentValue); } // set focus to existing item
                    else
                    {   // create new item

                        //TODO: adding a new items appears to be the source.

                        if (dataBinding.AddNew() is IPropertySubType newItem)
                        { newItem.PropertyId = value; }
                        else { throw new InvalidOperationException("AddNew did not create a IPropertySubType"); }
                    }
                }

                RebuildChoices();
            }
            else
            {   // Nothing selected option
                propertyValueData.Enabled = false;
                propertyChoiceData.Enabled = false;
                propertyTabs.SelectedTab = propertyValueTab;
            }
        }

        private void PropertyChoiceData_EnabledChanged(object sender, EventArgs e)
        {
            if (propertyChoiceData.Enabled)
            { propertyChoiceData.ResetBackColor(); }
            else { propertyChoiceData.BackColor = SystemColors.Control; }
        }

        private void PropertyChoiceData_ItemCheck(object sender, ItemCheckEventArgs e)
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

            if (dataBinding is not null && dataBinding.Current is IPropertySubType current)
            { current.PropertyValue = String.Join(", ", values); }
        }

        private void RebuildChoices()
        {
            if (dataBinding is not null
                && dataBinding.Current is IPropertySubType current)
            {
                PropertyIndex key = new PropertyIndex(current);

                if (Properties.FirstOrDefault(w => key.Equals(w)) is PropertyValue property)
                {
                    propertyChoiceData.Items.Clear();

                    if (property.PropertyType is Resource.Enumerations.DomainPropertyType.List)
                    {
                        List<String> selected = new List<String>();
                        if (current.PropertyValue is String)
                        { selected.AddRange(current.PropertyValue.Split(',', StringSplitOptions.TrimEntries).Select(s => s.Trim())); }

                        foreach (String choice in property.Choices)
                        {
                            if (selected.Any(w => w.Equals(choice, StringComparison.CurrentCultureIgnoreCase)))
                            { propertyChoiceData.Items.Add(choice, true); }
                            else { propertyChoiceData.Items.Add(choice, false); }
                        }

                        propertyValueData.Enabled = false;
                        propertyChoiceData.Enabled = true;
                        propertyTabs.SelectedTab = propertyChoiceTab;
                    }
                    else
                    {
                        propertyChoiceData.Items.Clear();

                        propertyChoiceData.Enabled = false;
                        propertyValueData.Enabled = true;
                        propertyTabs.SelectedTab = propertyValueTab;
                    }
                }
            }
        }
    }
}
