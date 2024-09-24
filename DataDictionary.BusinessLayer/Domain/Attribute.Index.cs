using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeIndex : IDomainAttributeKey
    { }

    /// <inheritdoc/>
    public class AttributeIndex : DomainAttributeKey, IAttributeIndex,
        IKeyEquality<IAttributeIndex>, IKeyEquality<AttributeIndex>
    {
        /// <inheritdoc cref="DomainAttributeKey(IDomainAttributeKey)"/>
        public AttributeIndex(IAttributeIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(AttributeIndex? other)
        { return other is IDomainAttributeKey key && Equals(new DomainAttributeKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(IAttributeIndex? other)
        { return other is IDomainAttributeKey key && Equals(new DomainAttributeKey(key)); }

        /// <summary>
        /// Convert AttributeIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(AttributeIndex source)
        { return new DataIndex() { SystemId = source.AttributeId ?? Guid.Empty }; }
    }
}
