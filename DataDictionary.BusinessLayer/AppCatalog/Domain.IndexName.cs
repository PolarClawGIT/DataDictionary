using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{

    /// <inheritdoc/>
    public interface IDomainIndexName : IDomainKeyName
    { }

    /// <inheritdoc/>
    public class DomainIndexName : DomainKeyName, IDomainIndexName,
        IKeyEquality<IDomainIndexName>, IKeyEquality<DomainIndexName>
    {
        /// <inheritdoc cref="DomainKeyName(IDomainKeyName)"/>
        public DomainIndexName(IDomainIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IDomainIndexName? other)
        { return other is IDomainKeyName key && Equals(new DomainKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(DomainIndexName? other)
        { return other is IDomainKeyName key && Equals(new DomainKeyName(key)); }

        /// <summary>
        /// Convert DomainIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(DomainIndexName source)
        { return new DataIndexName() { Title = source.DomainName ?? String.Empty }; }
    }
}
