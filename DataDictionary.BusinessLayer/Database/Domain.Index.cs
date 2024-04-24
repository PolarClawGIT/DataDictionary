using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Domain;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IDomainIndex : IDbDomainKey
    { }

    /// <inheritdoc/>
    public class DomainIndex : DbDomainKey, IDomainIndex
    {
        /// <inheritdoc cref="DbDomainKey(IDbDomainKey)"/>
        public DomainIndex(IDomainIndex source) : base(source)
        { }
    }

    /// <inheritdoc/>
    public interface IDomainIndexName : IDbDomainKeyName, IDbCatalogKeyName, ISchemaIndexName
    { }

    /// <inheritdoc/>
    public class DomainIndexName : DbDomainKeyName, IDomainIndexName
    {
        /// <inheritdoc cref="DbDomainKeyName(IDbDomainKeyName)"/>
        public DomainIndexName(IDomainIndexName source) : base(source) { }
    }
}
