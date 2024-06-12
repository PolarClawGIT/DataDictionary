using DataDictionary.DataLayer.ScriptingData.Template;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Scripting.ComboBoxList
{
    record ScriptAsList: IScriptAsType
    {
        public ScriptAsType ScriptAs { get; protected set; } = ScriptAsType.none;
        public static ScriptAsType NullValue { get; } = ScriptAsType.none;

        protected ScriptAsList() : base() { }

        public static void Load(ComboBoxData control)
        {
            List<ScriptAsList> values = new List<ScriptAsList>();

            values.AddRange(
                Enum.GetValues(typeof(ScriptAsType))
                    .Cast<ScriptAsType>()
                    .Select(s => new ScriptAsList() { ScriptAs = s }));

            ScriptAsList names;
            control.ValueMember = nameof(names.ScriptAs);
            control.DataSource = values;
        }
    }
}
