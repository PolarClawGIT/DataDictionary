using DataDictionary.BusinessLayer.NamedScope;
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
    public class LibraryMemberValue : LibraryMemberItem, ILibraryMemberValue, INamedScopeSource
    {
        /// <inheritdoc cref="LibraryMemberItem()"/>
        public LibraryMemberValue() : base()
        { PropertyChanged += CatalogValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeIndex GetKey()
        { return new NamedScopeIndex(MemberId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(MemberNameSpace); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return MemberName ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
        private void CatalogValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(MemberName) or nameof(MemberNameSpace)
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
