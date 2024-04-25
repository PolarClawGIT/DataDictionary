using DataDictionary.DataLayer.ScriptingData.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public class SelectionPathKey : DataLayer.ScriptingData.Selection.SelectionPathKey
    {
        /// <inheritdoc/>
        public SelectionPathKey(ISelectionPathKey source) : base(source) { }
    }
}
