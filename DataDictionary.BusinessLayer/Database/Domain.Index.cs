using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IDomainIndex : IDbDomainKey { }

    /// <inheritdoc/>
    public class DomainIndex : DbDomainKey, IDomainIndex,
        IKeyEquality<IDomainIndex>, IKeyEquality<DomainIndex>
    {
        /// <inheritdoc cref="DbDomainKey(IDbDomainKey)"/>
        public DomainIndex(IDomainIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(IDomainIndex? other)
        { return other is IDbDomainKey key && Equals(new DbDomainKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(DomainIndex? other)
        { return other is IDbDomainKey key && Equals(new DbDomainKey(key)); }

        /// <summary>
        /// Convert DomainIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(DomainIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.DomainId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface IDomainIndexName : IDbDomainKeyName
    { }

    /// <inheritdoc/>
    public class DomainIndexName : DbDomainKeyName, IDomainIndexName,
        IKeyEquality<IDomainIndexName>, IKeyEquality<DomainIndexName>
    {
        /// <inheritdoc cref="DbDomainKeyName(IDbDomainKeyName)"/>
        public DomainIndexName(IDomainIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IDomainIndexName? other)
        { return other is IDbDomainKeyName key && Equals(new DbDomainKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(DomainIndexName? other)
        { return other is IDbDomainKeyName key && Equals(new DbDomainKeyName(key)); }
    }
}
