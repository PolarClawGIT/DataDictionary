using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{

    /// <inheritdoc/>
    public interface ISelectionItem : DataLayer.ScriptingData.Selection.ISelectionItem
    { }

    /// <inheritdoc/>
    public class SelectionItem : DataLayer.ScriptingData.Selection.SelectionItem, ISelectionItem
    {
        /// <inheritdoc/>
        public SelectionItem() : base() { }
    }
}
