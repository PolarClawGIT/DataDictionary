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

        /// <summary>
        /// Convert CatalogIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(CatalogIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.CatalogId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface ICatalogIndexName : IDbCatalogKeyName
    { }

    /// <inheritdoc/>
    public class CatalogIndexName : DbCatalogKeyName, ICatalogIndexName
    {
        /// <inheritdoc cref="DbCatalogKeyName(IDbCatalogKeyName)"/>
        public CatalogIndexName(ICatalogIndexName source) : base(source) { }
    }
}
