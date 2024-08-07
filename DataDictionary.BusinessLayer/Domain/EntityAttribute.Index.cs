using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IEntityAttributeIndex: IDomainEntityAttributeKey, IEntityIndex, IAttributeIndex
    { }

    /// <inheritdoc/>
    public class EntityAttributeIndex : DomainEntityAttributeKey, IEntityAttributeIndex,
        IKeyEquality<IEntityAttributeIndex>, IKeyEquality<EntityAttributeIndex>
    {
        /// <inheritdoc cref="DomainEntityAttributeKey(IDomainEntityAttributeKey)"/>
        public EntityAttributeIndex(IEntityAttributeIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IEntityAttributeIndex? other)
        { return other is IEntityAttributeIndex key && Equals(new DomainEntityAttributeKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(EntityAttributeIndex? other)
        { return other is IEntityAttributeIndex key && Equals(new DomainEntityAttributeKey(key)); }
    }
}
