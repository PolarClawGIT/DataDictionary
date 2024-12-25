using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntityAttributeValue : IEntityAttributeItem,
        IAttributeIndex, IEntityIndex, IEntityAttributeIndex,
        IScopeType, ITemporalValue
    { }

    /// <inheritdoc/>
    public class EntityAttributeValue : EntityAttributeItem, IEntityAttributeValue
    {
        /// <inheritdoc/>
        public DataIndex Index => throw new NotImplementedException();

        /// <inheritdoc/>
        public String Title => throw new NotImplementedException();

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelEntityAttribute; } }

        /// <inheritdoc/>
        public EntityAttributeValue() : base() { }

        /// <inheritdoc cref="EntityAttributeItem(IEntityKey)"/>
        public EntityAttributeValue(IEntityIndex entity) : base(entity) { }

        /// <inheritdoc cref="EntityAttributeItem(IEntityKey, IAttributeKey)"/>
        public EntityAttributeValue(IEntityIndex entity, IAttributeIndex attribute) : base(entity, attribute) { }
    }
}
