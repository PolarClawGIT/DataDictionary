using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DatabaseData.Constraint
{
    /// <summary>
    /// Interface for the Database Constraint Key.
    /// </summary>
    public interface IDbConstraintKey : IKey
    {
        /// <summary>
        /// Application ID for the Constraint.
        /// </summary>
        Guid? ConstraintId { get; }
    }

    /// <summary>
    /// Implementation for the Database Constraint Key.
    /// </summary>
    public class DbConstraintKey : IDbConstraintKey,
        IKeyEquality<IDbConstraintKey>, IKeyEquality<DbConstraintKey>
    {
        /// <inheritdoc/>
        public Guid? ConstraintId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Constraint Key.
        /// </summary>
        /// <param name="source"></param>
        public DbConstraintKey(IDbConstraintKey source) : base()
        {
            if (source.ConstraintId is Guid value) { ConstraintId = value; }
            else { ConstraintId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(DbConstraintKey? other)
        { return other is DbConstraintKey && EqualityComparer<Guid?>.Default.Equals(ConstraintId, other.ConstraintId); }

        /// <inheritdoc/>
        public virtual Boolean Equals(IDbConstraintKey? other)
        { return other is IDbConstraintKey value && Equals(new DbConstraintKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IDbConstraintKey value && Equals(new DbConstraintKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(DbConstraintKey left, DbConstraintKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DbConstraintKey left, DbConstraintKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(ConstraintId); }

        #endregion
    }
}
