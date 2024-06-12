using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Scripting.ComboBoxList
{
    record SupportedFolderList
    {

        public Environment.SpecialFolder? Folder { get; protected set; } = null;

        public static String NullValue = "(not defined)";

        public String FolderName
        {
            get
            {
                if (Folder is Environment.SpecialFolder value)
                { return value.ToString(); }
                else { return NullValue; }
            }
        }

        public DirectoryInfo? Directory
        {
            get
            {
                if (Folder is Environment.SpecialFolder value)
                { return new DirectoryInfo(Environment.GetFolderPath(value)); }
                else { return null; }
            }
        }

        protected SupportedFolderList() : base() { }

        public static void Load(ComboBoxData control)
        {
            List<SupportedFolderList> values = new List<SupportedFolderList>();

            values.Add(new SupportedFolderList() { Folder = null });
            values.Add(new SupportedFolderList() { Folder = Environment.SpecialFolder.ApplicationData });
            values.Add(new SupportedFolderList() { Folder = Environment.SpecialFolder.MyDocuments });

            SupportedFolderList nameOfValues;
            control.ValueMember = nameof(nameOfValues.Folder);
            control.DisplayMember = nameof(nameOfValues.FolderName);
            control.DataSource = values;
        }
    }
}
