using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ISelectionPathValue : DataLayer.ScriptingData.Selection.ISelectionPathItem
    { }

    /// <inheritdoc/>
    public class SelectionPathValue : DataLayer.ScriptingData.Selection.SelectionPathItem, ISelectionPathValue
    {
        /// <inheritdoc/>
        public SelectionPathValue() : base() { }
    }
}
