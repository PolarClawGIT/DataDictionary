using DataDictionary.BusinessLayer.NamedScope;
using System.ComponentModel;
using Toolbox.BindingTable;
using DbLayer = DataDictionary.DataLayer.LibraryData.Source;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibrarySourceValue : DbLayer.ILibrarySourceItem, ILibrarySourceIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class LibrarySourceValue : DbLayer.LibrarySourceItem, ILibrarySourceValue, INamedScopeValue
    {
        /// <inheritdoc cref="DbLayer.LibrarySourceItem()"/>
        public LibrarySourceValue() : base()
        { PropertyChanged += OnPropertyChanged; }

        /// <inheritdoc/>
        public NamedScopeKey GetSystemId()
        { return new NamedScopeKey((ILibraryMemberIndex)this); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(AssemblyName); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return AssemblyName ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(AssemblyName)
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
