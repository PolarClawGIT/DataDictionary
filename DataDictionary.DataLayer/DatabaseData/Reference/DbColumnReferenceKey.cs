using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Reference
{
    /// <summary>
    /// Interface for the Database Column Reference Key
    /// </summary>
    public interface IDbColumnReferenceKey : IDbObjectReferenceKey
    {
        /// <summary>
        /// Reference Column Name
        /// </summary>
        String? ReferenceColumnName { get; }
    }

    /// <summary>
    /// Implementation of the Database Column Reference Key
    /// </summary>
    public class DbColumnReferenceKey : DbObjectReferenceKey, IDbColumnReferenceKey, IKeyComparable<IDbColumnReferenceKey>
    {
        /// <inheritdoc/>
        public string ReferenceColumnName { get; init; } = String.Empty;

        /// <summary>
        /// Constructor for the Database Column Reference Key
        /// </summary>
        /// <param name="source"></param>
        public DbColumnReferenceKey(IDbColumnReferenceKey source) : base(source)
        {
            if (source.ReferenceColumnName is string) { ReferenceColumnName = source.ReferenceColumnName; }
            else { ReferenceColumnName = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Database Column (Table Column) Reference Key
        /// </summary>
        /// <param name="source"></param>
        public DbColumnReferenceKey(IDbTableColumnKeyName source) : base(source)
        {
            if (source.ColumnName is string) { ReferenceColumnName = source.ColumnName; }
            else { ReferenceColumnName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbColumnReferenceKey? other)
        {
            return
                other is IDbObjectReferenceKey &&
                new DbObjectReferenceKey(this).Equals(other) &&
                !string.IsNullOrEmpty(ReferenceColumnName) &&
                !string.IsNullOrEmpty(other.ReferenceColumnName) &&
                ReferenceColumnName.Equals(other.ReferenceColumnName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbColumnReferenceKey value && Equals(new DbColumnReferenceKey(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbColumnReferenceKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbObjectReferenceKey(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ReferenceColumnName, other.ReferenceColumnName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbColumnReferenceKey value) { return CompareTo(new DbColumnReferenceKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbColumnReferenceKey left, DbColumnReferenceKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbColumnReferenceKey left, DbColumnReferenceKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbColumnReferenceKey left, DbColumnReferenceKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbColumnReferenceKey left, DbColumnReferenceKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbColumnReferenceKey left, DbColumnReferenceKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbColumnReferenceKey left, DbColumnReferenceKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ReferenceColumnName.GetHashCode(KeyExtension.CompareString)); }
    #endregion

    /// <inheritdoc/>
    public override string ToString()
        {
            if (ReferenceColumnName is string)
            { return string.Format("{0}.{1}", base.ToString(), ReferenceColumnName); }
            else { return string.Empty; }
        }
    }
}
