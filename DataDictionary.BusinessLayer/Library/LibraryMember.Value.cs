using DataDictionary.BusinessLayer.NamedScope;
using System.ComponentModel;
using Toolbox.BindingTable;
using DbLayer = DataDictionary.DataLayer.LibraryData.Member;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibraryMemberValue : DbLayer.ILibraryMemberItem, ILibraryMemberIndex, ILibraryMemberIndexParent,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class LibraryMemberValue : DbLayer.LibraryMemberItem, ILibraryMemberValue, INamedScopeValue
    {
        /// <inheritdoc cref="DbLayer.LibraryMemberItem()"/>
        public LibraryMemberValue() : base()
        { PropertyChanged += OnPropertyChanged; }

        /// <inheritdoc/>
        public NamedScopeKey GetSystemId()
        { return new NamedScopeKey((ILibraryMemberIndex)this); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(new[] { AssemblyName }.Concat(NamedScopePath.Parse(MemberNameSpace)).ToArray()); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return MemberName ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(MemberNameSpace) or nameof(MemberName)
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
