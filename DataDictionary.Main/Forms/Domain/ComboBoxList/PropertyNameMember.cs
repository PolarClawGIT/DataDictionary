using DataDictionary.BusinessLayer.Domain;
using DataDictionary.Main.Controls;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Domain.ComboBoxList
{
    record PropertyNameMember : IPropertyIndex, IPropertyIndexName
    {
        /// <inheritdoc/>
        public Guid? PropertyId { get; set; } = Guid.Empty;

        /// <inheritdoc/>
        public String PropertyTitle { get; set; } = String.Empty;

        public static void Load(ComboBoxData control)
        {
            PropertyNameMember propertyNameDataItem = new PropertyNameMember();
            BindingList<PropertyNameMember> list = new BindingList<PropertyNameMember>();
            list.Add(new PropertyNameMember() { PropertyId = Guid.Empty, PropertyTitle = "(select property Type)" });

            foreach (PropertyValue item in BusinessData.DomainModel.Properties)
            {
                if (item.PropertyId is Guid propertyId && item.PropertyTitle is String propertyTitle)
                { list.Add(new PropertyNameMember() { PropertyId = propertyId, PropertyTitle = propertyTitle }); }
            }

            control.ValueMember = nameof(propertyNameDataItem.PropertyId);
            control.DisplayMember = nameof(propertyNameDataItem.PropertyTitle);
            control.DataSource = list;
        }

        public static void Load(DataGridViewComboBoxColumn control)
        {
            PropertyNameMember propertyNameDataItem = new PropertyNameMember();
            BindingList<PropertyNameMember> list = new BindingList<PropertyNameMember>();
            list.Add(new PropertyNameMember() { PropertyId = Guid.Empty, PropertyTitle = "(select property Type)" });

            foreach (PropertyValue item in BusinessData.DomainModel.Properties)
            {
                if (item.PropertyId is Guid propertyId && item.PropertyTitle is String propertyTitle)
                { list.Add(new PropertyNameMember() { PropertyId = propertyId, PropertyTitle = propertyTitle }); }
            }

            control.ValueMember = nameof(propertyNameDataItem.PropertyId);
            control.DisplayMember = nameof(propertyNameDataItem.PropertyTitle);
            control.DataSource = list;
        }
    }
}
