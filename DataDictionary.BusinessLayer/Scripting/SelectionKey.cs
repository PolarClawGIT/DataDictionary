using DataDictionary.DataLayer.ScriptingData.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public class SelectionKey : DataLayer.ScriptingData.Selection.SelectionKey
    {
        /// <inheritdoc/>
        public SelectionKey(ISelectionKey source) : base(source) { }
    }
}
