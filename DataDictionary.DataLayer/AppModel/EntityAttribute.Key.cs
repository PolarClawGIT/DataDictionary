using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model EntityAttribute Key 
    /// </summary>
    public interface IEntityAttributeKey : IEntityKey, IAttributeKey
    { }

    /// <summary>
    /// Implementation of the DomainEntityAttribute Key 
    /// </summary>
    public class EntityAttributeKey : IEntityAttributeKey,
        IKeyEquality<IEntityAttributeKey>, IKeyEquality<EntityAttributeKey>
    {
        /// <inheritdoc/>
        public Guid? EntityId { get; init; }

        /// <inheritdoc/>
        public Guid? AttributeId { get; init; }

        /// <summary>
        /// Constructor for the DomainEntityAttribute Key 
        /// </summary>
        /// <param name="source"></param>
        public EntityAttributeKey(IEntityAttributeKey source) : base()
        {
            if (source.EntityId is Guid) { EntityId = source.EntityId; }
            else { EntityId = Guid.Empty; }

            if (source.AttributeId is Guid) { AttributeId = source.AttributeId; }
            else { AttributeId = Guid.Empty; }
        }

        /// <summary>
        /// Constructor for the DomainEntityAttribute Key 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="attribute"></param>
        public EntityAttributeKey(IEntityKey entity, IAttributeKey attribute) : base()
        {
            if (entity.EntityId is Guid) { EntityId = entity.EntityId; }
            else { EntityId = Guid.Empty; }

            if (attribute.AttributeId is Guid) { AttributeId = attribute.AttributeId; }
            else { AttributeId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(EntityAttributeKey? other)
        {
            return other is IEntityAttributeKey key
                && EqualityComparer<Guid?>.Default.Equals(EntityId, key.EntityId)
                && EqualityComparer<Guid?>.Default.Equals(AttributeId, key.AttributeId);
        }

        /// <inheritdoc/>
        public Boolean Equals(IEntityAttributeKey? other)
        { return other is IEntityAttributeKey value && Equals(new EntityAttributeKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IEntityAttributeKey value && Equals(new EntityAttributeKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(EntityAttributeKey left, EntityAttributeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(EntityAttributeKey left, EntityAttributeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(EntityId, AttributeId); }

        #endregion
    }
}
