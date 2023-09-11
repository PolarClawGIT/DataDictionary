using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms
{
    partial class ClipboardView : ApplicationBase
    {
        public ClipboardView() : base()
        {
            InitializeComponent();
        }

        private void ClipboardView_Load(object sender, EventArgs e)
        {
            List<String> formats = new List<string>();
            if (Clipboard.ContainsAudio()) { formats.Add("Audio"); }
            if (Clipboard.ContainsFileDropList()) { formats.Add("FileDropList"); }
            if (Clipboard.ContainsImage()) { formats.Add("Image"); }
            if (Clipboard.ContainsText()) { formats.Add("Text"); }
            if (Clipboard.GetDataObject() is IDataObject data)
            { formats.AddRange(data.GetFormats()); }

            clipboardDataType.DataSource = formats;
        }

        private void clipboardDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(clipboardDataType.SelectedItems.Count > 0 && clipboardDataType.SelectedItems[0] is String item) 
            {
                Object? data = Clipboard.GetData(item);

                if(data is IBindingTableRow row)
                {
                    StringBuilder value = new StringBuilder();
                    value.AppendLine(row.GetType().FullName);

                    foreach (System.Reflection.PropertyInfo property in row.GetType().GetProperties())
                    {
                        var itemValue = property.GetValue(row);
                        if (itemValue is not null)
                        { value.AppendLine(String.Format("{0}: {1}", property.Name, itemValue.ToString())); }
                        else { value.AppendLine(String.Format("{0}: NULL", property.Name)); }
                    }

                    clipboardData.Text = value.ToString();
                }
                else if (data is not null)
                { clipboardData.Text = data.ToString(); }
                else { clipboardData.Text = String.Empty; }
            }
        }
    }
}
