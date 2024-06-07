using DataDictionary.DataLayer.ScriptingData.Selection;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    [Obsolete("To be removed", true)]
    public interface ISelectionIndex : ISelectionKey
    { }

    /// <inheritdoc/>
    [Obsolete("To be removed", true)]
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

    /// <inheritdoc/>
    [Obsolete("To be removed", true)]
    public interface ISelectionIndexName : ISelectionKeyName
    { }

    /// <inheritdoc/>
    [Obsolete("To be removed", true)]
    public class SelectionIndexName : SelectionKeyName, ISelectionIndexName
    {
        /// <inheritdoc cref="SelectionKeyName(ISelectionKeyName)"/>
        public SelectionIndexName(ISelectionIndexName source) : base(source) { }
    }
}
