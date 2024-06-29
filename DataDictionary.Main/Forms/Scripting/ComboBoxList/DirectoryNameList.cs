using DataDictionary.DataLayer.ScriptingData.Template;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Scripting.ComboBoxList
{
    record DirectoryNameList : IDirectoryType
    {
        public DirectoryType RootDirectory { get; protected set; }
        public static DirectoryType NullValue { get; } = DirectoryType.Null;

        protected DirectoryNameList() : base() { }

        public DirectoryInfo? Directory
        { get { return new DirectoryTypeKey(RootDirectory).ToDirectoryInfo(); } }

        public static void Load(ComboBoxData control)
        {
            List<DirectoryNameList> values = new List<DirectoryNameList>();

            values.AddRange(
                Enum.GetValues(typeof(DirectoryType))
                    .Cast<DirectoryType>()
                    .Select(s => new DirectoryNameList() { RootDirectory = s }));

            DirectoryNameList names;
            control.ValueMember = nameof(names.RootDirectory);
            control.DataSource = values;
        }
    }
}
