using DataDictionary.DataLayer.ScriptingData.Selection;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ISelectionPathValue : ISelectionPathItem
    { }

    /// <inheritdoc/>
    public class SelectionPathValue : SelectionPathItem, ISelectionPathValue
    {
        /// <inheritdoc/>
        public SelectionPathValue() : base() { }

        /// <inheritdoc cref="SelectionPathItem(ISelectionIndex)"/>
        public SelectionPathValue(ISelectionIndex key) : base(key) { }
    }
}
