using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntityPropertyValue : IEntityPropertyItem, 
        IPropertyIndex, IEntityIndex,
        IScopeType
    { }

    /// <inheritdoc/>
    public class EntityPropertyValue : EntityPropertyItem, IEntityPropertyValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelEntityProperty; } }

        /// <inheritdoc/>
        public EntityPropertyValue() : base() { }

        /// <inheritdoc/>
        public EntityPropertyValue(IEntityIndex EntityKey) : base(EntityKey) { }

        /// <inheritdoc/>
        public EntityPropertyValue(IEntityKey EntityKey, DataLayer.AppModel.IPropertyKey propertyKey, DataLayer.AppCatalog.IPropertyItem value) : base(EntityKey, propertyKey, value)
        { }
    }
}
