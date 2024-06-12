using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.Main.Controls;

namespace DataDictionary.Main.Forms.Scripting.ComboBoxList
{
    record SupportedTypeList
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

        protected SupportedTypeList() : base() { }

        public static void Load(ComboBoxData control)
        {
            List<SupportedTypeList> values = new List<SupportedTypeList>();

            values.AddRange(
                Enum.GetValues(typeof(XmlDataType)).
                    Cast<XmlDataType>().
                    Where(w =>
                    {
                        var item = w.ToCrossReference();
                        return item.HasValue && item.Value.IsSupported;
                    }).
                    Select(s => new SupportedTypeList() { DataType = s }));

            SupportedTypeList names;
            control.DisplayMember = nameof(names.DataName);
            control.DataSource = values;
        }
    }
}
