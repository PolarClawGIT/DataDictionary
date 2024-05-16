using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.LibraryData.Member;
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
        { return new DataLayerIndex(MemberId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(NamedScopePath.Parse(MemberNameSpace).ToArray()); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return MemberName ?? Scope.ToName(); }
    }
}
