using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ScriptingData.Selection;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    [Obsolete("To be removed", true)]
    public interface ISelectionPathValue : ISelectionPathItem, ISelectionIndex
    { }

    /// <inheritdoc/>
    [Obsolete("To be removed", true)]
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
