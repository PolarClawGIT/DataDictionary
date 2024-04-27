using DataDictionary.DataLayer.ScriptingData.Selection;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ISelectionIndex : ISelectionKey
    { }

    /// <inheritdoc/>
    public class SelectionIndex : SelectionKey, ISelectionIndex
    {
        /// <inheritdoc cref="SelectionKey(ISelectionKey)"/>
        public SelectionIndex(ISelectionIndex source) : base(source) { }
    }
}
