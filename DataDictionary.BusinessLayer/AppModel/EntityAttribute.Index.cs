using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntityAttributeIndex : IEntityAttributeKey, IEntityIndex, IAttributeIndex
    { }

    /// <inheritdoc/>
    public class EntityAttributeIndex : EntityAttributeKey, IEntityAttributeIndex,
        IKeyEquality<IEntityAttributeIndex>, IKeyEquality<EntityAttributeIndex>
    {
        /// <inheritdoc cref="EntityAttributeKey(IEntityAttributeKey)"/>
        public EntityAttributeIndex(IEntityAttributeIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IEntityAttributeIndex? other)
        { return other is IEntityAttributeIndex key && Equals(new EntityAttributeKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(EntityAttributeIndex? other)
        { return other is IEntityAttributeIndex key && Equals(new EntityAttributeKey(key)); }
    }
}
