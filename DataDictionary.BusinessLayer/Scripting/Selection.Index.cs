using DataDictionary.DataLayer.ScriptingData.Selection;
<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
>>>>>>> RenameIndexValue

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
<<<<<<< HEAD
    public class SelectionKey : DataLayer.ScriptingData.Selection.SelectionKey
    {
        /// <inheritdoc/>
        public SelectionKey(ISelectionKey source) : base(source) { }
=======
    public interface ISelectionIndex : ISelectionKey
    { }

    /// <inheritdoc/>
    public class SelectionIndex : SelectionKey, ISelectionIndex
    {
        /// <inheritdoc cref="SelectionKey(ISelectionKey)"/>
        public SelectionIndex(ISelectionIndex source) : base(source) { }
>>>>>>> RenameIndexValue
    }
}
