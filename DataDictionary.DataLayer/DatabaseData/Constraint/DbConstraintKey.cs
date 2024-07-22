using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class DbConstraintKey : IDbConstraintKey, IKeyEquality<IDbConstraintKey>
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
        public virtual bool Equals(IDbConstraintKey? other)
        { return other is IDbConstraintKey && EqualityComparer<Guid?>.Default.Equals(ConstraintId, other.ConstraintId); }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbConstraintKey value && Equals(new DbConstraintKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbConstraintKey left, DbConstraintKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbConstraintKey left, DbConstraintKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(ConstraintId); }
        #endregion
    }
}
