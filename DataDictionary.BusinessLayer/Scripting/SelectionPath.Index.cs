using DataDictionary.DataLayer.ScriptingData.Selection;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ISelectionPathIndex : ISelectionPathKey
    { }

    /// <inheritdoc/>
    public class SelectionPathIndex : SelectionPathKey, ISelectionPathIndex
    {
        /// <inheritdoc cref="SelectionPathKey(ISelectionPathKey)"/>
        public SelectionPathIndex(SelectionPathIndex source) : base(source) { }
    }
}
