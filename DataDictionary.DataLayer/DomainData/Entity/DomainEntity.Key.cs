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
    public class DomainEntityKey : IDomainEntityKey,
        IKeyEquality<IDomainEntityKey>, IKeyEquality<DomainEntityKey>
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
        public Boolean Equals(DomainEntityKey? other)
        { return other is DomainEntityKey key && EqualityComparer<Guid?>.Default.Equals(EntityId, key.EntityId); }

        /// <inheritdoc/>
        public Boolean Equals(IDomainEntityKey? other)
        { return other is IDomainEntityKey value && Equals(new DomainEntityKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDomainEntityKey value && Equals(new DomainEntityKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(DomainEntityKey left, DomainEntityKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DomainEntityKey left, DomainEntityKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(EntityId); }

        #endregion
    }
}