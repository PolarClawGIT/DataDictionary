using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Scripting.ComboBoxList
{
    record SupportedTypeItem
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

        protected SupportedTypeItem() : base() { }

        public static void Load(ComboBoxData control)
        {
            List<SupportedTypeItem> values = new List<SupportedTypeItem>();

            values.AddRange(
                Enum.GetValues(typeof(XmlDataType)).
                    Cast<XmlDataType>().
                    Where(w =>
                    {
                        var item = w.ToCrossReference();
                        return item.HasValue && item.Value.IsSupported;
                    }).
                    Select(s => new SupportedTypeItem() { DataType = s }));

            SupportedTypeItem names;
            control.DisplayMember = nameof(names.DataName);
            control.DataSource = values;
        }
    }
}
