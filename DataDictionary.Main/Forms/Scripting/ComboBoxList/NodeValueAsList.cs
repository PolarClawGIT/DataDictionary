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
    record NodeValueAsList : INodeValueAsTypeKey
    {
        public TemplateNodeValueAsType NodeValueAs { get; protected set; } = TemplateNodeValueAsType.none;
        public static TemplateNodeValueAsType NullValue { get; } = TemplateNodeValueAsType.none;

        protected NodeValueAsList() : base() { }

        public static void Load(ComboBoxData control)
        {
            List<NodeValueAsList> values = new List<NodeValueAsList>();

            values.AddRange(
                Enum.GetValues(typeof(TemplateNodeValueAsType))
                    .Cast<TemplateNodeValueAsType>()
                    .Select(s => new NodeValueAsList() { NodeValueAs = s }));

            NodeValueAsList names;
            control.ValueMember = nameof(names.NodeValueAs);
            control.DataSource = values;
        }
    }
}
