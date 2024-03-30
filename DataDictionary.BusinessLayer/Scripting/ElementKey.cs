using DataDictionary.DataLayer.ScriptingData.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface IElementKey : DataLayer.ScriptingData.Schema.IElementKey
    { }

    /// <inheritdoc/>
    public class ElementKey : DataLayer.ScriptingData.Schema.ElementKey, IElementKey
    {
        /// <inheritdoc/>
        public ElementKey(IElementKey source) : base(source) { }
    }
}
