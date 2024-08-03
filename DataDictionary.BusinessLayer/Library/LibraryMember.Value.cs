using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.LibraryData;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibraryMemberValue : ILibraryMemberItem, ILibraryMemberIndex, ILibraryMemberIndexParent, ILibrarySourceIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class LibraryMemberValue : LibraryMemberItem, ILibraryMemberValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="LibraryMemberItem()"/>
        public LibraryMemberValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new LibraryMemberIndex(this); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(NamedScopePath.Parse(MemberNameSpace).ToArray()); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return MemberName ?? ScopeEnumeration.Cast(Scope).Name; }
    }
}
