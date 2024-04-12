using DataDictionary.DataLayer.ScriptingData.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public class InstanceKey : DataLayer.ScriptingData.Selection.InstanceKey
    {
        /// <inheritdoc/>
        public InstanceKey(IInstanceKey source) : base(source) { }
    }
}
