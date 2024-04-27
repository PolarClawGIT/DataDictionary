using DataDictionary.DataLayer.DomainData.Attribute;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeAliasValue: IDomainAttributeAliasItem, IAttributeIndex
    { }

    /// <inheritdoc/>
    public class AttributeAliasValue : DomainAttributeAliasItem, IAttributeAliasValue
    {
        /// <inheritdoc/>
        public AttributeAliasValue() : base() { }

        /// <inheritdoc cref="DomainAttributeAliasItem(IDomainAttributeKey)"/>
        public AttributeAliasValue(IAttributeIndex key) : base(key) { }

        /// <inheritdoc/>
        internal AttributeAliasValue(IDomainAttributeKey key) : base(key) { }
    }
}
