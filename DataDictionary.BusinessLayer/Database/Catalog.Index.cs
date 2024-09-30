using DataDictionary.BusinessLayer.ToolSet;
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
        /// Convert CatalogIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(CatalogIndex source)
        { return new DataIndex() { SystemId = source.CatalogId ?? Guid.Empty }; }
    }

  
}
