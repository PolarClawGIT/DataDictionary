using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Selection;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    [Obsolete("To be removed", true)]
    public interface ISelectionValue : ISelectionItem, ISelectionIndex, ISelectionIndexName
    { }

    /// <inheritdoc/>
    [Obsolete("To be removed", true)]
    public class SelectionValue : SelectionItem, ISelectionValue, INamedScopeSourceValue
    {
        /// <inheritdoc/>
        public SelectionValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new SelectionIndex(this); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(Scope); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return SelectionTitle ?? Scope.ToName(); }
    }
}
