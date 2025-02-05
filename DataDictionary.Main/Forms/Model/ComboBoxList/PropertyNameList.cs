using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Main.Controls;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Model.ComboBoxList
{
    record PropertyNameList : IPropertyIndex, IPropertyIndexName
    {
        /// <inheritdoc/>
        public Guid? PropertyId { get; set; } = Guid.Empty;

        /// <inheritdoc/>
        public String PropertyTitle { get; set; } = String.Empty;

        public IEnumerable<String> PropertyChoice { get; init; } = new List<String>();

        public Boolean IsChoice { get; init; } = false;

        public static void Load(ComboBoxData control, IEnumerable<IPropertyValue> values)
        {

            BindingList<PropertyNameList> list = new BindingList<PropertyNameList>();
            list.Add(new PropertyNameList()
            {
                PropertyId = Guid.Empty,
                PropertyTitle = "(select property Type)"
            });

            foreach (IPropertyValue item in values)
            {
                if (item.PropertyId is Guid propertyId && item.PropertyTitle is String propertyTitle)
                {
                    List<String> choices = new List<string>();

                    if (item.PropertyType is Resource.Enumerations.DomainPropertyType.List
                        && item.PropertyData is String)
                    { choices.AddRange(item.PropertyData.Split(",", StringSplitOptions.TrimEntries)); }

                    list.Add(new PropertyNameList()
                    {
                        PropertyId = propertyId,
                        PropertyTitle = propertyTitle,
                        PropertyChoice = choices,
                        IsChoice = choices.Count > 0
                    });
                }
            }

            control.ValueMember = nameof(PropertyNameList.PropertyId);
            control.DisplayMember = nameof(PropertyNameList.PropertyTitle);
            control.DataSource = list;
        }

        public static void Load(ComboBoxData control)
        { Load(control, BusinessData.Model.Properties); }

        public static void Load(DataGridViewComboBoxColumn control, IEnumerable<IPropertyValue> values)
        {
            PropertyNameList propertyNameDataItem = new PropertyNameList();
            BindingList<PropertyNameList> list = new BindingList<PropertyNameList>();
            list.Add(new PropertyNameList() { PropertyId = Guid.Empty, PropertyTitle = "(select property Type)" });

            foreach (IPropertyValue item in values)
            {
                if (item.PropertyId is Guid propertyId && item.PropertyTitle is String propertyTitle)
                {
                    List<String> choices = new List<string>();

                    if (item.PropertyType is Resource.Enumerations.DomainPropertyType.List
                        && item.PropertyData is String)
                    { choices.AddRange(item.PropertyData.Split(",", StringSplitOptions.TrimEntries)); }

                    list.Add(new PropertyNameList()
                    {
                        PropertyId = propertyId,
                        PropertyTitle = propertyTitle,
                        PropertyChoice = choices,
                        IsChoice = choices.Count > 0
                    });
                }
            }

            control.ValueMember = nameof(propertyNameDataItem.PropertyId);
            control.DisplayMember = nameof(propertyNameDataItem.PropertyTitle);
            control.DataSource = list;
        }

        public static void Load(DataGridViewComboBoxColumn control)
        { Load(control, BusinessData.Model.Properties); }
    }
}
