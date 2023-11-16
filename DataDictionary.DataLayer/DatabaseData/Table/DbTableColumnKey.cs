using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DomainData.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Table
{
    /// <summary>
    /// Interface for the Database Table Column Key
    /// </summary>
    public interface IDbTableColumnKey : IKey, IDbTableKey
    {
        /// <summary>
        /// Name of the Database Column
        /// </summary>
        String? ColumnName { get; }
    }

    /// <summary>
    /// Implementation of the Database Table Column Key
    /// </summary>
    public class DbTableColumnKey : DbTableKey, IDbTableColumnKey, IKeyComparable<IDbTableColumnKey>
    {
        /// <inheritdoc/>
        public string ColumnName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Database Column Key
        /// </summary>
        /// <param name="source"></param>
        public DbTableColumnKey(IDbTableColumnKey source) : base(source)
        { if (source.ColumnName is string) { ColumnName = source.ColumnName; } }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbTableColumnKey? other)
        {
            return
                other is IDbSchemaKey &&
                new DbTableKey(this).Equals(other) &&
                !string.IsNullOrEmpty(ColumnName) &&
                !string.IsNullOrEmpty(other.ColumnName) &&
                ColumnName.Equals(other.ColumnName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbTableColumnKey value && Equals(new DbTableColumnKey(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbTableColumnKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbTableKey(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ColumnName, other.ColumnName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbTableColumnKey value) { return CompareTo(new DbTableColumnKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbTableColumnKey left, DbTableColumnKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbTableColumnKey left, DbTableColumnKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbTableColumnKey left, DbTableColumnKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbTableColumnKey left, DbTableColumnKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbTableColumnKey left, DbTableColumnKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbTableColumnKey left, DbTableColumnKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ColumnName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (ColumnName is string)
            { return string.Format("{0}.{1}", base.ToString(), ColumnName); }
            else { return string.Empty; }
        }
    }
}
