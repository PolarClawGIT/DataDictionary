using DataDictionary.DataLayer.DatabaseData.Catalog;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ICatalogIndex : IDbCatalogKey
    { }

    /// <inheritdoc/>
    public class CatalogIndex : DbCatalogKey, ICatalogIndex
    {
        /// <inheritdoc cref="DbCatalogKey(IDbCatalogKey)"/>
        public CatalogIndex(ICatalogIndex source) : base(source) { }
    }

    /// <inheritdoc/>
    public interface ICatalogIndexName : IDbCatalogKeyName
    { }

    /// <inheritdoc/>
    public class CatalogKeyName : DbCatalogKeyName, ICatalogIndexName
    {
        /// <inheritdoc cref="DbCatalogKeyName(IDbCatalogKeyName)"/>
        public CatalogKeyName(ICatalogIndexName source) : base(source) { }
    }
}
