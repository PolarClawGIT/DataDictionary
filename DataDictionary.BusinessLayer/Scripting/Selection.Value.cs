using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Selection;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ISelectionValue : ISelectionItem, ISelectionIndex
    { }

    /// <inheritdoc/>
    public class SelectionValue : SelectionItem, ISelectionValue, INamedScopeSourceValue
    {
        /// <inheritdoc/>
        public SelectionValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new DataLayerIndex(SelectionId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(Scope); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return SelectionTitle ?? Scope.ToName(); }
    }
}
