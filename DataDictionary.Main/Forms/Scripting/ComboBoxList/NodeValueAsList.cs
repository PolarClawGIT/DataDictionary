using DataDictionary.DataLayer.ScriptingData.Template;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Scripting.ComboBoxList
{
    record NodeValueAsList : INodeValueAsType
    {
        public NodeValueAsType NodeValueAs { get; protected set; } = NodeValueAsType.none;
        public static NodeValueAsType NullValue { get; } = NodeValueAsType.none;

        protected NodeValueAsList() : base() { }

        public static void Load(ComboBoxData control)
        {
            List<NodeValueAsList> values = new List<NodeValueAsList>();

            values.AddRange(
                Enum.GetValues(typeof(NodeValueAsType))
                    .Cast<NodeValueAsType>()
                    .Select(s => new NodeValueAsList() { NodeValueAs = s }));

            NodeValueAsList names;
            control.ValueMember = nameof(names.NodeValueAs);
            control.DataSource = values;
        }
    }
}
