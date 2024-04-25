using DataDictionary.DataLayer.DomainData.Attribute;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeIndex : IDomainAttributeKey
    { }

    /// <inheritdoc/>
    public class AttributeIndex : DomainAttributeKey, IAttributeIndex
    {
        /// <inheritdoc cref="DomainAttributeKey(IDomainAttributeKey)"/>
        public AttributeIndex(IAttributeIndex source) : base(source) { }

        /// <inheritdoc/>
        public AttributeIndex(IDomainAttributeKey source) : base(source) { }
    }
}
