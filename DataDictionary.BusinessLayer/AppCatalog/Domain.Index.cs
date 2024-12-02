using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IDomainIndex : IDomainKey { }

    /// <inheritdoc/>
    public class DomainIndex : DomainKey, IDomainIndex,
        IKeyEquality<IDomainIndex>, IKeyEquality<DomainIndex>
    {
        /// <inheritdoc cref="DomainKey(IDomainKey)"/>
        public DomainIndex(IDomainIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(IDomainIndex? other)
        { return other is IDomainKey key && Equals(new DomainKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(DomainIndex? other)
        { return other is IDomainKey key && Equals(new DomainKey(key)); }

        /// <summary>
        /// Convert DomainIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(DomainIndex source)
        { return new DataIndex() { SystemId = source.DomainId ?? Guid.Empty }; }
    }

}
