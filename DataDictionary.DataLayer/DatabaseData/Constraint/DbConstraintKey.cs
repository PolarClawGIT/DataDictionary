using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDictionary.DataLayer.DatabaseData.Schema;

namespace DataDictionary.DataLayer.DatabaseData.Constraint
{
    /// <summary>
    /// Interface for the Database Constraint Key.
    /// </summary>
    public interface IDbConstraintKey : IDbSchemaKey, IKey
    {
        /// <summary>
        /// Name of the Database Constraint
        /// </summary>
        String? ConstraintName { get; }
    }

    /// <summary>
    /// Implementation for the Database Constraint Key.
    /// </summary>
    public class DbConstraintKey : DbSchemaKey, IDbConstraintKey, IKeyComparable<IDbConstraintKey>
    {
        /// <inheritdoc/>
        public String ConstraintName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Database Constraint Key.
        /// </summary>
        /// <param name="source"></param>
        public DbConstraintKey(IDbConstraintKey source) : base(source)
        {
            if (source.ConstraintName is string) { ConstraintName = source.ConstraintName; }
            else { ConstraintName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbConstraintKey? other)
        {
            return 
                other is IDbSchemaKey &&
                new DbSchemaKey(this).Equals(other) &&
                !string.IsNullOrEmpty(ConstraintName) &&
                !string.IsNullOrEmpty(other.ConstraintName) &&
                ConstraintName.Equals(other.ConstraintName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbConstraintKey value && Equals(new DbConstraintKey(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbConstraintKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKey(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ConstraintName, other.ConstraintName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbConstraintKey value) { return CompareTo(new DbConstraintKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbConstraintKey left, DbConstraintKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbConstraintKey left, DbConstraintKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbConstraintKey left, DbConstraintKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbConstraintKey left, DbConstraintKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbConstraintKey left, DbConstraintKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbConstraintKey left, DbConstraintKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ConstraintName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (ConstraintName is string)
            { return string.Format("{0}.{1}", base.ToString(), ConstraintName); }
            else { return string.Empty; }
        }

    }
}
