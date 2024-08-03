using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ICatalogIndex : IDbCatalogKey
    { }

    /// <inheritdoc/>
    public class CatalogIndex : DbCatalogKey, ICatalogIndex,
        IKeyEquality<ICatalogIndex>, IKeyEquality<CatalogIndex>
    {
        /// <inheritdoc cref="DbCatalogKey(IDbCatalogKey)"/>
        public CatalogIndex(ICatalogIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ICatalogIndex? other)
        { return other is IDbCatalogKey value && Equals(new DbCatalogKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(CatalogIndex? other)
        { return other is IDbCatalogKey value && Equals(new DbCatalogKey(value)); }

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
    public class CatalogIndexName : DbCatalogKeyName, ICatalogIndexName,
        IKeyEquality<ICatalogIndexName>, IKeyEquality<CatalogIndexName>
    {
        /// <inheritdoc cref="DbCatalogKeyName(IDbCatalogKeyName)"/>
        public CatalogIndexName(ICatalogIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ICatalogIndexName? other)
        { return other is IDbCatalogKeyName value && Equals(new DbCatalogKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(CatalogIndexName? other)
        { return other is IDbCatalogKeyName value && Equals(new DbCatalogKeyName(value)); }
    }
}
