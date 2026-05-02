using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Entity Key
    /// </summary>
    public interface IEntityKey : IKey
    {
        /// <summary>
        /// Application ID for the Model Entity.
        /// </summary>
        Guid? EntityId { get; }
    }

    /// <summary>
    /// Implementation for the Model Entity Key
    /// </summary>
    public class EntityKey : IEntityKey,
        IKeyEquality<IEntityKey>, IKeyEquality<EntityKey>
    {
        /// <inheritdoc/>
        public Guid? EntityId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return EntityId.HasValue && EntityId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Blank/Empty Entity Key
        /// </summary>
        /// <remarks>Empty Key is never equal to anything.</remarks>
        public EntityKey()
        { }

        /// <summary>
        /// Constructor for the Domain Entity Key
        /// </summary>
        /// <param name="source"></param>
        public EntityKey(IEntityKey source) : base()
        {
            if (source.EntityId is Guid) { EntityId = source.EntityId; }
            else { EntityId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(EntityKey? other)
        {
            return other is EntityKey key
                && EntityId.HasValue && EntityId != Guid.Empty
                && key.EntityId.HasValue && key.EntityId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(EntityId, other.EntityId);
        }

        /// <inheritdoc/>
        public Boolean Equals(IEntityKey? other)
        { return other is IEntityKey value && Equals(new EntityKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IEntityKey value && Equals(new EntityKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(EntityKey left, EntityKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(EntityKey left, EntityKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(EntityId); }

        #endregion
    }
}