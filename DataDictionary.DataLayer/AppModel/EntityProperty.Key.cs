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
    public class EntityPropertyKey : IEntityPropertyKey,
        IKeyEquality<IEntityPropertyKey>, IKeyEquality<EntityPropertyKey>
    {
        /// <inheritdoc/>
        public Guid? EntityId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public Guid? PropertyId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Entity Property Key
        /// </summary>
        /// <param name="source"></param>
        public EntityPropertyKey(IEntityPropertyKey source)
        {
            EntityId = source.EntityId;
            PropertyId = source.PropertyId;
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(EntityPropertyKey? other)
        {
            return other is EntityPropertyKey key &&
                   EqualityComparer<Guid?>.Default.Equals(EntityId, key.EntityId) &&
                   EqualityComparer<Guid?>.Default.Equals(PropertyId, key.PropertyId);
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
