using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database
{

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

        /// <summary>
        /// Convert DomainIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(DomainIndexName source)
        { return new DataIndexName() { Title = source.DomainName ?? String.Empty }; }
    }
}
