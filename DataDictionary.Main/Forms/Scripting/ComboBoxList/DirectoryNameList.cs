using DataDictionary.DataLayer.ScriptingData.Template;
using DataDictionary.Main.Controls;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Scripting.ComboBoxList
{
    record DirectoryNameList : IDirectoryTypeKey
    {
        public TemplateDirectoryType RootDirectory { get; protected set; }
        public static TemplateDirectoryType NullValue { get; } = TemplateDirectoryType.Null;

        protected DirectoryNameList() : base() { }

        public DirectoryInfo? Directory
        { get { return new DirectoryTypeKey(RootDirectory).ToDirectoryInfo(); } }

        public static void Load(ComboBoxData control)
        {
            List<DirectoryNameList> values = new List<DirectoryNameList>();

            values.AddRange(
                Enum.GetValues(typeof(TemplateDirectoryType))
                    .Cast<TemplateDirectoryType>()
                    .Select(s => new DirectoryNameList() { RootDirectory = s }));

            DirectoryNameList names;
            control.ValueMember = nameof(names.RootDirectory);
            control.DataSource = values;
        }
    }
}
