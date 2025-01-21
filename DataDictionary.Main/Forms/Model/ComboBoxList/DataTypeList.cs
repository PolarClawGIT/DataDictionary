using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Model.ComboBoxList
{
    record DataTypeList
    {
        public required String DataType { get; init; }

        static List<String> defaultValues = new List<String>()
        {   nameof(Int16),
            nameof(Int32),
            nameof(Int64),
            nameof(Byte),
            nameof(String),
            nameof(Char),
            nameof(Boolean),
            nameof(Single),
            nameof(Double),
            nameof(Decimal),
            nameof(DateTime),
            nameof(Guid),

        };

        public static void Load(ComboBoxData control, IEnumerable<String> appendValues)
        {
            BindingList<DataTypeList> list = new BindingList<DataTypeList>();

            foreach (String item in defaultValues.Union(appendValues).Distinct().Order())
            { list.Add(new DataTypeList() { DataType = item }); }

            control.ValueMember = nameof(DataType);
            control.DisplayMember = nameof(DataType);
            control.DataSource = list;
        }
    }
}
