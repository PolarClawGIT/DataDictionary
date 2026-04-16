using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Constraint Column Key.
    /// </summary>
    public interface IConstraintColumnKey : IKey
    {
        /// <summary>
        /// Application ID for the Constraint Column.
        /// </summary>
        Guid? ConstraintColumnId { get; }
    }

    /// <summary>
    /// Implementation for the Database Constraint Column Key.
    /// </summary>
    public class ConstraintColumnKey : IConstraintColumnKey,
        IKeyEquality<IConstraintColumnKey>, IKeyEquality<ConstraintColumnKey>
    {
        /// <inheritdoc/>
        public Guid? ConstraintColumnId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return ConstraintColumnId.HasValue && ConstraintColumnId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Catalog Key.
        /// </summary>
        /// <param name="source"></param>
        public ConstraintColumnKey(IConstraintColumnKey source) : base()
        {
            if (source.ConstraintColumnId is Guid value) { ConstraintColumnId = value; }
            else { ConstraintColumnId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(ConstraintColumnKey? other)
        {
            return other is ConstraintColumnKey key
                && ConstraintColumnId.HasValue && ConstraintColumnId != Guid.Empty
                && key.ConstraintColumnId.HasValue && key.ConstraintColumnId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(ConstraintColumnId, other.ConstraintColumnId);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IConstraintColumnKey? other)
        { return other is IConstraintColumnKey value && Equals(new ConstraintColumnKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IConstraintColumnKey value && Equals(new ConstraintColumnKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(ConstraintColumnKey left, ConstraintColumnKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ConstraintColumnKey left, ConstraintColumnKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(ConstraintColumnId); }


        #endregion
    }
}
