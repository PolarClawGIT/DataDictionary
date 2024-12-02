using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.DomainData.Property;

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
        public EntityPropertyValue(IDomainEntityKey EntityKey, IDomainPropertyKey propertyKey, IPropertyItem value) : base(EntityKey, propertyKey, value)
        { }
    }
}
