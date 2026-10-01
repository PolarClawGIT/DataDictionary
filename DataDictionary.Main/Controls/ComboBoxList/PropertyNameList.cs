using DataDictionary.BusinessLayer.AppModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record PropertyNameList : IPropertyIndex, IPropertyIndexName, IBindingPropertyChanged
    {
        /// <inheritdoc/>
        public Guid? PropertyId { get; set; } = Guid.Empty;

        /// <inheritdoc/>
        public String PropertyTitle { get; set; } = String.Empty;

        public IEnumerable<String> Choices { get; init; } = new List<String>();

        public Boolean IsChoice { get; init; } = false;

        public static Guid NullValue { get; } = Guid.Empty;

        PropertyNameList(IPropertyValue value)
        {
            PropertyId = value.PropertyId;
            PropertyTitle = value.PropertyTitle ?? String.Empty;

            List<String> choices = new List<string>();

            if (value.PropertyType is Resource.Enumerations.DomainPropertyType.List
                && value.PropertyData is String)
            { choices.AddRange(value.PropertyData.Split(",", StringSplitOptions.TrimEntries)); }

            Choices = choices;
            IsChoice = choices.Count > 0;
        }

        PropertyNameList(String? emptyText = "(n/a)")
        { PropertyTitle = emptyText ?? "(n/a)"; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public static void Load(ComboBoxData control, String? emptyText = null)
        {
            BindingComboList<PropertyNameList> comboList = BuildList(emptyText);
            comboList.BindTo(control, () => nameof(PropertyId), () => nameof(PropertyTitle));
        }

        public static void Load(DataGridViewComboBoxColumn control, String? emptyText = null)
        {
            BindingComboList<PropertyNameList> comboList = BuildList(emptyText);
            comboList.BindTo(control, () => nameof(PropertyId), () => nameof(PropertyTitle));
        }

        static BindingComboList<PropertyNameList> BuildList(String? emptyText = null)
        {
            BindingComboList<PropertyNameList> comboList = new BindingComboList<PropertyNameList>();

            comboList.BuildList(
                source: BusinessData.Model.Properties,
                constructor: (c) => new PropertyNameList(c),
                onItemChanged: (s, t) =>
                {
                    t.PropertyTitle = s.PropertyTitle ?? String.Empty;
                    t.OnPropertyChanged(t.PropertyChanged, nameof(t.PropertyTitle));
                },
                orderBy: (o) => o.PropertyTitle,
                areEqual: (a, b) => new PropertyIndex(a).Equals(b),
                emptyValue: () => new PropertyNameList(emptyText));

            return comboList;
        }
    }
}
