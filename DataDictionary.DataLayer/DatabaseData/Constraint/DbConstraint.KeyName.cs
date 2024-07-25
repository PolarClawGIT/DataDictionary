using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.Resource;

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
    public class DbConstraintKeyName : DbSchemaKeyName, IDbConstraintKeyName,
        IKeyComparable<IDbConstraintKeyName>, IKeyComparable<DbConstraintKeyName>
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
        public Boolean Equals(DbConstraintKeyName? other)
        {
            return
                other is DbSchemaKeyName &&
                new DbSchemaKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(ConstraintName) &&
                !string.IsNullOrEmpty(other.ConstraintName) &&
                ConstraintName.Equals(other.ConstraintName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(IDbConstraintKeyName? other)
        { return other is IDbConstraintKeyName value && Equals(new DbConstraintKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDbConstraintKeyName value && Equals(new DbConstraintKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(DbConstraintKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ConstraintName, other.ConstraintName, true); }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(IDbConstraintKeyName? other)
        { if (other is IDbConstraintKeyName value) { return CompareTo(new DbConstraintKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override Int32 CompareTo(object? obj)
        { if (obj is IDbConstraintKeyName value) { return CompareTo(new DbConstraintKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(DbConstraintKeyName left, DbConstraintKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DbConstraintKeyName left, DbConstraintKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(DbConstraintKeyName left, DbConstraintKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(DbConstraintKeyName left, DbConstraintKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(DbConstraintKeyName left, DbConstraintKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(DbConstraintKeyName left, DbConstraintKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ConstraintName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return this.ToAliasName(); }



    }
}
