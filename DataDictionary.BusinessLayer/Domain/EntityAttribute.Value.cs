using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.DomainData.Attribute;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IEntityAttributeValue : IDomainEntityAttributeItem, IAttributeIndex, IEntityIndex
    { }

    /// <inheritdoc/>
    public class EntityAttributeValue : DomainEntityAttributeItem, IEntityAttributeValue
    {
        /// <inheritdoc/>
        public EntityAttributeValue() : base() { }

        /// <inheritdoc cref="DomainEntityAttributeItem.DomainEntityAttributeItem(IDomainEntityKey, IDomainAttributeKey)"/>
        public EntityAttributeValue(IEntityIndex entity, IAttributeIndex attribute) : base(entity, attribute) { }
    }
}
