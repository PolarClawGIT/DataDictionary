using DataDictionary.Resource.Enumerations;
using System.Xml.Schema;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record XmlTypeCodeList
    {
        public XmlTypeCode ValueMember { get; init; }
        public String DisplayMember { get; init; }

        public static XmlTypeCode NullValue { get; } = XmlTypeCode.None;

        static IReadOnlyList<XmlTypeCodeList> data = Enum.GetValues<XmlTypeCode>().
            Where(w => w.TryGetValue(out XmlTypeCodeEnumeration? value) && value.IsSupported).
            Select(s => new XmlTypeCodeList(s)).
            ToList();

        XmlTypeCodeList(XmlTypeCode xmlType) : base()
        {
            ValueMember = xmlType;

            if (xmlType.TryGetValue(out XmlTypeCodeEnumeration? value))
            { DisplayMember = value.DisplayName; }
            else
            { DisplayMember = Enum.GetName<XmlTypeCode>(xmlType) ?? String.Empty; }
        }

        public static void Load(ComboBoxData control)
        {
            control.ValueMember = nameof(ValueMember);
            control.DisplayMember = nameof(DisplayMember);
            control.DataSource = data;
        }
    }
}
