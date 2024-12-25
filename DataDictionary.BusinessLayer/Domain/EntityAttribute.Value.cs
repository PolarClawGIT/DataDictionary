using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.BusinessLayer.AppModel;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IEntityAttributeValue : IDomainEntityAttributeItem,
        IAttributeIndex, IEntityIndex, IEntityAttributeIndex
    { }

    /// <inheritdoc/>
    public class EntityAttributeValue : DomainEntityAttributeItem, IEntityAttributeValue
    {
        /// <inheritdoc/>
        public EntityAttributeValue() : base() { }

        /// <inheritdoc cref="DomainEntityAttributeItem.DomainEntityAttributeItem(IDomainEntityKey)"/>
        public EntityAttributeValue(IEntityIndex entity) : base(entity) { }

        /// <inheritdoc cref="DomainEntityAttributeItem.DomainEntityAttributeItem(IDomainEntityKey, IAttributeKey)"/>
        public EntityAttributeValue(IEntityIndex entity, IAttributeIndex attribute) : base(entity, attribute) { }
    }
}
