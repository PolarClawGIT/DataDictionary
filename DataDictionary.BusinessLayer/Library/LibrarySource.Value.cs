using DataDictionary.BusinessLayer.NamedScope;
<<<<<<< HEAD
=======
using DataDictionary.DataLayer.LibraryData.Source;
>>>>>>> RenameIndexValue
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
<<<<<<< HEAD
    public interface ILibrarySourceValue : DbLayer.ILibrarySourceItem, ILibrarySourceIndex,
=======
    public interface ILibrarySourceValue : ILibrarySourceItem, ILibrarySourceIndex,
>>>>>>> RenameIndexValue
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
<<<<<<< HEAD
    public class LibrarySourceValue : DbLayer.LibrarySourceItem, ILibrarySourceValue, INamedScopeValue
    {
        /// <inheritdoc cref="DbLayer.LibrarySourceItem()"/>
        public LibrarySourceValue() : base()
        { PropertyChanged += OnPropertyChanged; }

        /// <inheritdoc/>
        public NamedScopeKey GetSystemId()
        { return new NamedScopeKey((ILibraryMemberIndex)this); }
=======
    public class LibrarySourceValue : LibrarySourceItem, ILibrarySourceValue, INamedScopeValue
    {
        /// <inheritdoc cref="LibrarySourceItem()"/>
        public LibrarySourceValue() : base()
        { PropertyChanged += CatalogValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeKey GetSystemId()
        { return new NamedScopeKey(LibraryId); }
>>>>>>> RenameIndexValue

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(AssemblyName); }

        /// <inheritdoc/>
        public virtual String GetTitle()
<<<<<<< HEAD
        { return AssemblyName ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(AssemblyName)
=======
        { return LibraryTitle ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
        private void CatalogValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(LibraryTitle) or nameof(AssemblyName)
>>>>>>> RenameIndexValue
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
