using DataDictionary.Resource.Enumerations;
using System.Xml.Schema;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record XmlTypeCodeList
    {
        public XmlTypeCode ValueMember { get; init; } = XmlTypeCode.None;
        public String DisplayMember { get { return ValueMember.GetName(); } }

        public static RenderValueAsList Empty { get; } = new RenderValueAsList();
        public static XmlTypeCode NullValue { get; } = XmlTypeCode.None;

        static IReadOnlyList<XmlTypeCodeList> data = Enum.GetValues<XmlTypeCode>().
            Where(w => w.TryGetValue(out XmlTypeCodeEnumeration? value) && value.IsSupported).
            Select(s => new XmlTypeCodeList(s)).
            ToList();

        XmlTypeCodeList(XmlTypeCode value) : base()
        { ValueMember = value; }


        public static void Load(ComboBoxData control)
        {
            control.ValueMember = nameof(ValueMember);
            control.DisplayMember = nameof(DisplayMember);
            control.DataSource = data;
        }
    }
}
