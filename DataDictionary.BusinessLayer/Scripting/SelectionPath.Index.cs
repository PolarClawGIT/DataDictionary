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
    public class SelectionPathKey : DataLayer.ScriptingData.Selection.SelectionPathKey
    {
        /// <inheritdoc/>
        public SelectionPathKey(ISelectionPathKey source) : base(source) { }
=======
    public interface ISelectionPathIndex : ISelectionPathKey
    { }

    /// <inheritdoc/>
    public class SelectionPathIndex : SelectionPathKey, ISelectionPathIndex
    {
        /// <inheritdoc cref="SelectionPathKey(ISelectionPathKey)"/>
        public SelectionPathIndex(SelectionPathIndex source) : base(source) { }
>>>>>>> RenameIndexValue
    }
}
