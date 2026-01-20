using DataDictionary.BusinessLayer.AppModel;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record AttributeNameList : IAttributeIndex, IAttributeIndexName, IBindingPropertyChanged
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get; private set; } = Guid.Empty;

        /// <inheritdoc/>
        public String AttributeTitle { get; private set; } = String.Empty;

        AttributeNameList(IAttributeValue value)
        {
            AttributeId = value.AttributeId;
            AttributeTitle = value.AttributeTitle ?? String.Empty;
        }

        AttributeNameList(String? emptyText = "(n/a)")
        { AttributeTitle = emptyText ?? "(n/a)"; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public static void Load(ComboBoxData control, String? emptyText = null)
        {
            BindingComboList<AttributeNameList> comboList = BuildList(emptyText);
            comboList.BindTo(control, () => nameof(AttributeId), () => nameof(AttributeTitle));
        }

        public static void Load(DataGridViewComboBoxColumn control, String? emptyText = null)
        {
            BindingComboList<AttributeNameList> comboList = BuildList(emptyText);
            comboList.BindTo(control, () => nameof(AttributeId), () => nameof(AttributeTitle));
        }

        static BindingComboList<AttributeNameList> BuildList(String? emptyText = null)
        {
            BindingComboList<AttributeNameList> comboList = new BindingComboList<AttributeNameList>();

            comboList.BuildList(
                source: BusinessData.Model.Attribute.Attributes,
                constructor: (c) => new AttributeNameList(c),
                onItemChanged: (s, t) =>
                {
                    t.AttributeTitle = s.AttributeTitle ?? String.Empty;
                    t.OnPropertyChanged(t.PropertyChanged, nameof(t.AttributeTitle));
                },
                orderBy: (o) => o.AttributeTitle,
                areEquel: (a, b) => new AttributeIndex(a).Equals(b),
                emptyValue: () => new AttributeNameList(emptyText));

            return comboList;
        }
    }
}
