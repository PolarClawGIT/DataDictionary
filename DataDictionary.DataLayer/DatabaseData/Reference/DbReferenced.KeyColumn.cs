using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Reference
{
    /// <summary>
    /// Interface for the Referenced Column Name
    /// </summary>
    public interface IDbReferencedKeyColumn: IDbReferencedKeyObject
    {
        /// <summary>
        /// The Database Column Name of the Referenced Object
        /// </summary>
        String? ReferencedColumnName { get; }
    }

    /// <summary>
    /// Implementation for the Referenced Column Name
    /// </summary>
    public class DbReferencedKeyColumn : DbReferencedKeyObject, IDbReferencedKeyColumn,
        IKeyComparable<IDbReferencedKeyColumn>, IKeyComparable<DbReferencedKeyColumn>
    {
        /// <inheritdoc/>
        public String ReferencedColumnName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for Referenced Object Name
        /// </summary>
        /// <param name="source"></param>
        public DbReferencedKeyColumn(IDbReferencedKeyColumn source) : base(source)
        {
            if (source.ReferencedColumnName is string)
            { ReferencedColumnName = source.ReferencedColumnName; }
            else { ReferencedColumnName = string.Empty; }
        }

        /// <summary>
        /// Constructor for Referenced Object Name by Table
        /// </summary>
        /// <param name="source"></param>
        public DbReferencedKeyColumn(IDbTableColumnKeyName source) : base(source)
        {
            if (source.ColumnName is string)
            { ReferencedColumnName = source.ColumnName; }
            else { ReferencedColumnName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(DbReferencedKeyColumn? other)
        {
            return
                other is DbReferencedKeyObject &&
                base.Equals(other) &&
                !string.IsNullOrEmpty(ReferencedColumnName) &&
                !string.IsNullOrEmpty(other.ReferencedColumnName) &&
                ReferencedColumnName.Equals(other.ReferencedColumnName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(IDbReferencedKeyColumn? other)
        { return other is IDbReferencedKeyColumn value && Equals(new DbReferencedKeyObject(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDbReferencedKeyColumn value && Equals(new DbReferencedKeyObject(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(DbReferencedKeyColumn? other)
        {
            if (other is null) { return 1; }
            else if (base.CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ReferencedColumnName, other.ReferencedColumnName, true); }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(IDbReferencedKeyColumn? other)
        { if (other is IDbReferencedKeyColumn value) { return CompareTo(new DbReferencedKeyColumn(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override Int32 CompareTo(object? obj)
        { if (obj is IDbReferencedKeyColumn value) { return CompareTo(new DbReferencedKeyColumn(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(DbReferencedKeyColumn left, DbReferencedKeyColumn right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DbReferencedKeyColumn left, DbReferencedKeyColumn right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(DbReferencedKeyColumn left, DbReferencedKeyColumn right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbReferencedKeyColumn left, DbReferencedKeyColumn right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(DbReferencedKeyColumn left, DbReferencedKeyColumn right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(DbReferencedKeyColumn left, DbReferencedKeyColumn right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(
                base.GetHashCode(),
                ReferencedColumnName.GetHashCode(KeyExtension.CompareString)
            );
        }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return AliasExtension.FormatName(ReferencedDatabaseName, ReferencedSchemaName, ReferencedObjectName, ReferencedColumnName); }


    }
}
