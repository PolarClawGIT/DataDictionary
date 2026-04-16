using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Domain Key.
    /// </summary>
    public interface IDomainKey : IKey
    {
        /// <summary>
        /// Application ID for the Domain.
        /// </summary>
        Guid? DomainId { get; }
    }

    /// <summary>
    /// Implementation for the Database Domain Key.
    /// </summary>
    public class DomainKey : IDomainKey,
        IKeyEquality<IDomainKey>, IKeyEquality<DomainKey>
    {
        /// <inheritdoc/>
        public Guid? DomainId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return DomainId.HasValue && DomainId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Domain Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainKey(IDomainKey source) : base()
        {
            if (source.DomainId is Guid value) { DomainId = value; }
            else { DomainId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public virtual Boolean Equals(DomainKey? other)
        {
            return other is DomainKey key
                && DomainId.HasValue && DomainId != Guid.Empty
                && key.DomainId.HasValue && key.DomainId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(DomainId, other.DomainId);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IDomainKey? other)
        { return other is IDomainKey value && Equals(new DomainKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IDomainKey value && Equals(new DomainKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(DomainKey left, DomainKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DomainKey left, DomainKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(DomainId); }
        #endregion
    }
}
