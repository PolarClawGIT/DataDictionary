using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Constraint Key.
    /// </summary>
    public interface IConstraintKey : IKey
    {
        /// <summary>
        /// Application ID for the Constraint.
        /// </summary>
        Guid? ConstraintId { get; }
    }

    /// <summary>
    /// Implementation for the Database Constraint Key.
    /// </summary>
    public class ConstraintKey : IConstraintKey,
        IKeyEquality<IConstraintKey>, IKeyEquality<ConstraintKey>
    {
        /// <inheritdoc/>
        public Guid? ConstraintId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return ConstraintId.HasValue && ConstraintId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Constraint Key.
        /// </summary>
        /// <param name="source"></param>
        public ConstraintKey(IConstraintKey source) : base()
        {
            if (source.ConstraintId is Guid value) { ConstraintId = value; }
            else { ConstraintId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(ConstraintKey? other)
        {
            return other is ConstraintKey key
                && ConstraintId.HasValue && ConstraintId != Guid.Empty
                && key.ConstraintId.HasValue && key.ConstraintId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(ConstraintId, other.ConstraintId);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IConstraintKey? other)
        { return other is IConstraintKey value && Equals(new ConstraintKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IConstraintKey value && Equals(new ConstraintKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(ConstraintKey left, ConstraintKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ConstraintKey left, ConstraintKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(ConstraintId); }

        #endregion
    }
}
