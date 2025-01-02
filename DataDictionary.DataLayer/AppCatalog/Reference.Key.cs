using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Reference Key.
    /// </summary>
    public interface IReferenceKey : IKey
    {
        /// <summary>
        /// Application ID for the Reference.
        /// </summary>
        Guid? ReferenceId { get; }
    }

    /// <summary>
    /// Implementation for the Database Reference Key.
    /// </summary>
    public class ReferenceKey : IReferenceKey,
        IKeyEquality<IReferenceKey>, IKeyEquality<ReferenceKey>
    {
        /// <inheritdoc/>
        public Guid? ReferenceId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Constraint Key.
        /// </summary>
        /// <param name="source"></param>
        public ReferenceKey(IReferenceKey source) : base()
        {
            if (source.ReferenceId is Guid value) { ReferenceId = value; }
            else { ReferenceId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(ReferenceKey? other)
        { return other is ReferenceKey && EqualityComparer<Guid?>.Default.Equals(ReferenceId, other.ReferenceId); }

        /// <inheritdoc/>
        public virtual Boolean Equals(IReferenceKey? other)
        { return other is IReferenceKey value && Equals(new ReferenceKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IReferenceKey value && Equals(new ReferenceKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(ReferenceKey left, ReferenceKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ReferenceKey left, ReferenceKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(ReferenceId); }

        #endregion
    }
}
