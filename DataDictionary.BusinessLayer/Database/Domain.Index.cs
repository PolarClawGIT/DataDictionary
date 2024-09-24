using DataDictionary.BusinessLayer.ToolSet;
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
        /// Convert DomainIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(DomainIndex source)
        { return new DataIndex() { SystemId = source.DomainId ?? Guid.Empty }; }
    }

}
