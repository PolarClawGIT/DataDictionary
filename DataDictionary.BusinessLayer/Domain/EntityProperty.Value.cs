using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer.DomainData.Entity;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IEntityPropertyValue : IDomainEntityPropertyItem, IPropertyIndex
    { }

    /// <inheritdoc/>
    public class EntityPropertyValue : DomainEntityPropertyItem, IEntityPropertyValue
    {
        /// <inheritdoc/>
        public EntityPropertyValue() : base() { }

        /// <inheritdoc/>
        public EntityPropertyValue(IEntityIndex EntityKey) : base(EntityKey) { }

        /// <inheritdoc/>
        public EntityPropertyValue(IEntityIndex EntityKey,
                                     IPropertyIndex propertyKey,
                                     IExtendedPropertyValue value)
            : base(EntityKey, propertyKey, value) { }
    }
}
