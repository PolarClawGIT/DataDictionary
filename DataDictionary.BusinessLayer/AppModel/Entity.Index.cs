using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntityIndex : IEntityKey
    { }

    /// <inheritdoc/>
    public class EntityIndex : EntityKey, IEntityIndex,
        IKeyEquality<IEntityIndex>, IKeyEquality<EntityIndex>
    {
        /// <inheritdoc cref="EntityKey()"/>
        public EntityIndex() : base() { }

        /// <inheritdoc cref="EntityKey(IEntityKey)"/>
        public EntityIndex(IEntityIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IEntityIndex? other)
        { return other is IEntityKey key && Equals(new EntityKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(EntityIndex? other)
        { return other is IEntityKey key && Equals(new EntityKey(key)); }

        /// <summary>
        /// Convert EntityIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(EntityIndex source)
        { return new DataIndex() { SystemId = source.EntityId ?? Guid.Empty }; }
    }


}
