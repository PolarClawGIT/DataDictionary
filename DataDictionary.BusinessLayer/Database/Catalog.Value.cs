using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ICatalogValue : IDbCatalogItem, ICatalogIndex, ICatalogIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class CatalogValue : DbCatalogItem, ICatalogValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="DbCatalogItem()"/>
        public CatalogValue() : base() { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new CatalogIndex(this); }

        /// <inheritdoc/>
        public NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName); }

        /// <inheritdoc/>
        public String GetTitle()
        { return CatalogTitle ?? ScopeEnumeration.Cast(Scope).Name; }

        /// <inheritdoc/>
        public Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs)
        { return eventArgs.PropertyName is nameof(CatalogTitle) or nameof(DatabaseName); }
    }
}
