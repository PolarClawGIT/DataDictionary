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

        /// <summary>
        /// Convert SelectionIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(SelectionIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.SelectionId ?? Guid.Empty }; }
    }
}
