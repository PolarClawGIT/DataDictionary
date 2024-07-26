using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.LibraryData;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibrarySourceValue : ILibrarySourceItem, ILibrarySourceIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class LibrarySourceValue : LibrarySourceItem, ILibrarySourceValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="LibrarySourceItem()"/>
        public LibrarySourceValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new LibrarySourceIndex(this); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(AssemblyName); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return LibraryTitle ?? ScopeEnumeration.Cast(Scope).Name; }
    }
}
