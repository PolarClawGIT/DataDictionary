using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.Main.Controls;

namespace DataDictionary.Main.Forms.Scripting.ComboBoxList
{
    record XmlDataTypeList
    {
        public XmlDataType DataType { get; set; } = XmlDataType.xs_string;
        public String DataName
        {
            get
            {
                var value = DataType.ToCrossReference();
                if (value.HasValue) { return value.Value.Name; }
                else { return String.Empty; }
            }
        }

        protected XmlDataTypeList() : base() { }

        public static void Load(ComboBoxData control)
        {
            List<XmlDataTypeList> values = new List<XmlDataTypeList>();

            values.AddRange(
                Enum.GetValues(typeof(XmlDataType)).
                    Cast<XmlDataType>().
                    Where(w =>
                    {
                        var item = w.ToCrossReference();
                        return item.HasValue && item.Value.IsSupported;
                    }).
                    Select(s => new XmlDataTypeList() { DataType = s }));

            XmlDataTypeList names;
            control.DisplayMember = nameof(names.DataName);
            control.DataSource = values;
        }
    }
}
