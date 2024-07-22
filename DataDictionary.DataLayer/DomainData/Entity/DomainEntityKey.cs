using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for the Domain Entity Key
    /// </summary>
    public interface IDomainEntityKey : IKey
    {
        /// <summary>
        /// Application ID for the Domain Entity.
        /// </summary>
        Guid? EntityId { get; }
    }

    /// <summary>
    /// Implementation for the Domain Entity Key
    /// </summary>
    public class DomainEntityKey : IDomainEntityKey, IKeyEquality<IDomainEntityKey>
    {
        /// <inheritdoc/>
        public Guid? EntityId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Entity Key
        /// </summary>
        /// <param name="source"></param>
        public DomainEntityKey(IDomainEntityKey source) : base()
        {
            if (source.EntityId is Guid) { EntityId = source.EntityId; }
            else { EntityId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDomainEntityKey? other)
        { return other is IDomainEntityKey key && EqualityComparer<Guid?>.Default.Equals(EntityId, key.EntityId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainEntityKey value && Equals(new DomainEntityKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DomainEntityKey left, DomainEntityKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainEntityKey left, DomainEntityKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(EntityId); }
        #endregion
    }
}