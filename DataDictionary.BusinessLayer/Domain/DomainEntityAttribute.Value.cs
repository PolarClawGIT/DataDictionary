using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.DomainData.Attribute;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IDomainEntityAttributeValue : IDomainEntityAttributeItem, IAttributeIndex, IEntityIndex
    { }

    /// <inheritdoc/>
    public class DomainEntityAttributeValue : DomainEntityAttributeItem, IDomainEntityAttributeValue
    {
        /// <inheritdoc/>
        public DomainEntityAttributeValue() : base() { }

        /// <inheritdoc cref="DomainEntityAttributeItem.DomainEntityAttributeItem(IDomainEntityKey, IDomainAttributeKey)"/>
        public DomainEntityAttributeValue(IEntityIndex entity, IAttributeIndex attribute) : base(entity, attribute) { }
    }
}
