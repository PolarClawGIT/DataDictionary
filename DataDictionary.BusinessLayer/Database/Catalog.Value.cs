using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ICatalogValue : IDbCatalogItem, ICatalogIndex, ICatalogIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class CatalogValue : DbCatalogItem, ICatalogValue, INamedScopeValue
    {
        /// <inheritdoc cref="DbCatalogItem()"/>
        public CatalogValue() : base()
        { PropertyChanged += OnPropertyChanged; }

        /// <inheritdoc/>
<<<<<<< HEAD
        public NamedScopeKey GetSystemId()
=======
        public virtual NamedScopeKey GetSystemId()
>>>>>>> RenameIndexValue
        { return new NamedScopeKey(CatalogId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return CatalogTitle ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(CatalogTitle) or nameof(DatabaseName)
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
