using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Entity Property Key
    /// </summary>
    public interface IEntityPropertyKey : IEntityKey, IPropertyKey
    { }

    /// <summary>
    /// Implantation for the Model Entity Property Key
    /// </summary>
    public class EntityPropertyKey : PropertyKey,
        IEntityPropertyKey, IKeyEquality<IEntityPropertyKey>, IKeyEquality<EntityPropertyKey>
    {
        /// <inheritdoc/>
        public Guid? EntityId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Entity Property Key
        /// </summary>
        /// <param name="source"></param>
        public EntityPropertyKey(IEntityPropertyKey source):base(source)
        {   EntityId = source.EntityId; }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(EntityPropertyKey? other)
        {
            return other is EntityPropertyKey key
                && EntityId.HasValue && EntityId != Guid.Empty
                && key.EntityId.HasValue && key.EntityId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(EntityId, other.EntityId)
                && base.Equals(key);
        }

        /// <inheritdoc/>
        public Boolean Equals(IEntityPropertyKey? other)
        { return other is IEntityPropertyKey value && Equals(new EntityPropertyKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IEntityPropertyKey value && Equals(new EntityPropertyKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(EntityPropertyKey left, EntityPropertyKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(EntityPropertyKey left, EntityPropertyKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(EntityId, PropertyId); }
        #endregion
    }
}
