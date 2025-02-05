using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Main.Forms.Model.ComboBoxList;
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

namespace DataDictionary.Main.Forms.Model.Controls
{
    partial class Property : UserControl
    {
        BindingSource? dataBinding; // Pointer to the BindingSource.

        public Property()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets up the Control.
        /// </summary>
        public void LoadControl(BindingSource binding, IEnumerable<IPropertyValue> values)
        {
            dataBinding = binding;
            PropertyNameList.Load(propertyTypeData, values);

            propertyTypeData.DataBindings.Add(new Binding(nameof(propertyTypeData.SelectedValue), binding, nameof(IPropertySubType.PropertyId), false, DataSourceUpdateMode.OnPropertyChanged));
            propertyValueData.DataBindings.Add(new Binding(nameof(propertyValueData.Text), binding, nameof(IPropertySubType.PropertyValue), false, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void PropertyTypeData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (propertyTypeData.SelectedValue is Guid value && value != Guid.Empty)
            {
                propertyChoiceData.Items.Clear();

                // Handling of a new or existing item.
                if (dataBinding is not null &&
                    dataBinding.DataSource is IList data &&
                    propertyTypeData.SelectedItem is IPropertyIndex selectedValue)
                {
                    PropertyIndex key = new PropertyIndex(selectedValue);
                    var currentValues = data.OfType<IPropertySubType>().ToList();

                    if (currentValues.FirstOrDefault(w => key.Equals(w)) is IPropertySubType currentValue)
                    { dataBinding.Position = currentValues.IndexOf(currentValue); } // set focus to existing item
                    else
                    {   // create new item
                        if (dataBinding.AddNew() is IPropertySubType newItem)
                        { newItem.PropertyId = value; }
                    }
                }

                // Rebuild Choice list
                if (propertyTypeData.SelectedItem is PropertyNameList item && item.IsChoice)
                {
                    propertyValueData.Enabled = false;
                    propertyChoiceData.Enabled = true;
                    propertyTabs.SelectedTab = propertyChoiceTab;

                    if (dataBinding is not null)
                    {
                        List<String> selected = propertyValueData.Text.Split(",", StringSplitOptions.TrimEntries).ToList();

                        foreach (String choice in item.PropertyChoice)
                        {
                            if (selected.Any(w => w.Equals(choice, StringComparison.CurrentCultureIgnoreCase)))
                            { propertyChoiceData.Items.Add(choice, true); }
                            else { propertyChoiceData.Items.Add(choice, false); }
                        }
                    }
                }
                else
                {   // Not a Choice Property
                    propertyChoiceData.Enabled = false;
                    propertyValueData.Enabled = true;
                    propertyTabs.SelectedTab = propertyValueTab;
                }
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


    }
}
