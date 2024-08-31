using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Constraint
{
    /// <summary>
    /// Interface for the Database Constraint Column Key.
    /// </summary>
    public interface IDbConstraintColumnKey: IKey
    {
        /// <summary>
        /// Application ID for the Constraint Column.
        /// </summary>
        Guid? ConstraintColumnId { get; }
    }

    /// <summary>
    /// Implementation for the Database Constraint Column Key.
    /// </summary>
    public class DbConstraintColumnKey : IDbConstraintColumnKey,
        IKeyEquality<IDbConstraintColumnKey>, IKeyEquality<DbConstraintColumnKey>
    {
        /// <inheritdoc/>
        public Guid? ConstraintColumnId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Catalog Key.
        /// </summary>
        /// <param name="source"></param>
        public DbConstraintColumnKey(IDbConstraintColumnKey source) : base()
        {
            if (source.ConstraintColumnId is Guid value) { ConstraintColumnId = value; }
            else { ConstraintColumnId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(DbConstraintColumnKey? other)
        { return other is DbConstraintColumnKey && EqualityComparer<Guid?>.Default.Equals(ConstraintColumnId, other.ConstraintColumnId); }

        /// <inheritdoc/>
        public virtual Boolean Equals(IDbConstraintColumnKey? other)
        { return other is IDbConstraintColumnKey value && Equals(new DbConstraintColumnKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IDbConstraintColumnKey value && Equals(new DbConstraintColumnKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(DbConstraintColumnKey left, DbConstraintColumnKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DbConstraintColumnKey left, DbConstraintColumnKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(ConstraintColumnId); }


        #endregion
    }
}
