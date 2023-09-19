using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Domain.ComboBoxList
{
    record PropertyNameItem
    {
        public Guid PropertyId { get; set; } = Guid.Empty;
        public String PropertyTitle { get; set; } = String.Empty;

        public static void Bind(ComboBoxData control)
        {
            PropertyNameItem propertyNameDataItem = new PropertyNameItem();
            BindingList<PropertyNameItem> list = new BindingList<PropertyNameItem>();
            list.Add(new PropertyNameItem() { PropertyId = Guid.Empty, PropertyTitle = "(select property Type)" });

            foreach (PropertyItem item in Program.Data.Properties)
            {
                if (item.PropertyId is Guid propertyId && item.PropertyTitle is String propertyTitle)
                { list.Add(new PropertyNameItem() { PropertyId = propertyId, PropertyTitle = propertyTitle }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(propertyNameDataItem.PropertyId);
            control.DisplayMember = nameof(propertyNameDataItem.PropertyTitle);
        }

        public static void Bind(DataGridViewComboBoxColumn control)
        {
            PropertyNameItem propertyNameDataItem = new PropertyNameItem();
            BindingList<PropertyNameItem> list = new BindingList<PropertyNameItem>();
            list.Add(new PropertyNameItem() { PropertyId = Guid.Empty, PropertyTitle = "(select property Type)" });

            foreach (PropertyItem item in Program.Data.Properties)
            {
                if (item.PropertyId is Guid propertyId && item.PropertyTitle is String propertyTitle)
                { list.Add(new PropertyNameItem() { PropertyId = propertyId, PropertyTitle = propertyTitle }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(propertyNameDataItem.PropertyId);
            control.DisplayMember = nameof(propertyNameDataItem.PropertyTitle);
        }
    }
}
