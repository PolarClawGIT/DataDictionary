using DataDictionary.DataLayer.ScriptingData.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface IElementItem : DataLayer.ScriptingData.Schema.IElementItem
    { }

    /// <inheritdoc/>
    public class ElementItem: DataLayer.ScriptingData.Schema.ElementItem, IElementItem
    {
        /// <inheritdoc/>
        public ElementItem() : base() { }
    }
}
