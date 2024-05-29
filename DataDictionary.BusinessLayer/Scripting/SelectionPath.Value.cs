using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ScriptingData.Selection;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ISelectionPathValue : ISelectionPathItem, ISelectionIndex
    { }

    /// <inheritdoc/>
    public class SelectionPathValue : SelectionPathItem, ISelectionPathValue
    {
        /// <inheritdoc/>
        public SelectionPathValue() : base() { }

        /// <inheritdoc cref="SelectionPathItem(ISelectionKey)"/>
        public SelectionPathValue(ISelectionIndex key) : base(key) { }

        /// <inheritdoc cref="INamedScopeSourceValue.GetPath"/>
        public NamedScopePath GetPath()
        { return new NamedScopePath(NamedScopePath.Parse(this.SelectionPath).ToArray()); }
    }
}
