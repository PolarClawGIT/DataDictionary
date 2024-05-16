using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
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
        { return new DataLayerIndex(CatalogId); }

        /// <inheritdoc/>
        public NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName); }

        /// <inheritdoc/>
        public String GetTitle()
        { return CatalogTitle ?? Scope.ToName(); }
    }
}
