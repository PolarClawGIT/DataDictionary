using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DomainData.Alias;

namespace DataDictionary.DataLayer.DatabaseData.Constraint
{
    /// <summary>
    /// Interface for the Database Constraint Key.
    /// </summary>
    public interface IDbConstraintKeyName : IKey, IDbSchemaKeyName, IToAliasName
    {
        /// <summary>
        /// Name of the Database Constraint
        /// </summary>
        String? ConstraintName { get; }
    }

    /// <summary>
    /// Implementation for IDbConstraintKeyName
    /// </summary>
    public static class DbConstraintKeyNameExtension
    {
        /// <summary>
        /// Gets the Alias Name for the Database Constraint.
        /// </summary>
        /// <returns></returns>
        public static String ToAliasName(this IDbConstraintKeyName source)
        { return AliasExtension.FormatName(source.DatabaseName, source.SchemaName, source.ConstraintName); }
    }

    /// <summary>
    /// Implementation for the Database Constraint Key.
    /// </summary>
    public class DbConstraintKeyName : DbSchemaKeyName, IDbConstraintKeyName, IKeyComparable<IDbConstraintKeyName>
    {
        /// <inheritdoc/>
        public String ConstraintName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for a blank Database Constraint Key
        /// </summary>
        protected internal DbConstraintKeyName() : base() { }

        /// <summary>
        /// Constructor for the Database Constraint Key.
        /// </summary>
        /// <param name="source"></param>
        public DbConstraintKeyName(IDbConstraintKeyName source) : base(source)
        {
            if (source.ConstraintName is string) { ConstraintName = source.ConstraintName; }
            else { ConstraintName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbConstraintKeyName? other)
        {
            return 
                other is IDbSchemaKeyName &&
                new DbSchemaKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(ConstraintName) &&
                !string.IsNullOrEmpty(other.ConstraintName) &&
                ConstraintName.Equals(other.ConstraintName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbConstraintKeyName value && Equals(new DbConstraintKeyName(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbConstraintKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ConstraintName, other.ConstraintName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbConstraintKeyName value) { return CompareTo(new DbConstraintKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbConstraintKeyName left, DbConstraintKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbConstraintKeyName left, DbConstraintKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbConstraintKeyName left, DbConstraintKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbConstraintKeyName left, DbConstraintKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbConstraintKeyName left, DbConstraintKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbConstraintKeyName left, DbConstraintKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ConstraintName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return this.ToAliasName(); }
    }
}
