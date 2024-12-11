using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.AppModel;
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
        public EntityPropertyValue(IDomainEntityKey EntityKey, DataLayer.AppModel.IPropertyKey propertyKey, DataLayer.AppCatalog.IPropertyItem value) : base(EntityKey, propertyKey, value)
        { }
    }
}
