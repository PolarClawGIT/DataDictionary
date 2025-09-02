using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntityPropertyValue : IEntityPropertyItem,
        IPropertyIndex, IEntityIndex, IPropertySubType,
        IScopeType
    { }

    /// <inheritdoc/>
    public partial class EntityPropertyValue : EntityPropertyItem, IEntityPropertyValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelEntityProperty; } }

        /// <inheritdoc cref="EntityPropertyItem.EntityPropertyItem()" />
        public EntityPropertyValue() : base() { }

        /// <inheritdoc cref="EntityPropertyItem.EntityPropertyItem(IEntityKey)"/>
        public EntityPropertyValue(IEntityIndex entity) : base(entity) { }

        /// <inheritdoc cref="EntityPropertyItem.EntityPropertyItem(IEntityKey, IPropertyKey)"/>
        public EntityPropertyValue(IEntityIndex entity, IPropertyIndex property) : base(entity, property)
        { }
    }
}
