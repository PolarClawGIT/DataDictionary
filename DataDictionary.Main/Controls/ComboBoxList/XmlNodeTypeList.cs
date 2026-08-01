using DataDictionary.Resource.Enumerations;
using System.Xml;

namespace DataDictionary.Main.Controls.ComboBoxList
{

    record XmlNodeTypeList
    {
        public XmlNodeType ValueMember { get; init; }
        public String DisplayMember { get; init; }

        public static XmlNodeType NullValue { get; } = XmlNodeType.None;

        static IReadOnlyList<XmlNodeTypeList> data = Enum.GetValues<XmlNodeType>().
            Where(w => w.TryGetValue(out XmlNodeTypeEnumeration? value) && value.IsSupported).
            Select(s => new XmlNodeTypeList(s)).
            ToList();

        XmlNodeTypeList(XmlNodeType xmlType) : base()
        {
            ValueMember = xmlType;

            if (xmlType.TryGetValue(out XmlNodeTypeEnumeration? value))
            { DisplayMember = value.DisplayName; }
            else
            { DisplayMember = Enum.GetName<XmlNodeType>(xmlType) ?? String.Empty; }
        }

        public static void Load(ComboBoxData control)
        {
            control.ValueMember = nameof(ValueMember);
            control.DisplayMember = nameof(DisplayMember);
            control.DataSource = data;
        }
    }
}
