using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Entity;
using System.Xml.Linq;

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
