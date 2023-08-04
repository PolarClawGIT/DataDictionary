using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbTableColumnKey : IDbTableKey
    {
        public String? ColumnName { get; }
    }

    public class DbTableColumnKey : DbTableKey, IDbTableColumnKey, IEquatable<DbTableColumnKey>, IComparable<DbTableColumnKey>, IComparable
    {
        public String ColumnName { get; init; } = String.Empty;

        public DbTableColumnKey(IDbTableColumnKey source) : base(source)
        {
            if (source.ColumnName is String) { ColumnName = source.ColumnName; }
            else { ColumnName = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(DbTableColumnKey? other)
        {
            return (
                other is IDbSchemaKey &&
                new DbTableKey(this).Equals(other) &&
                !String.IsNullOrEmpty(ColumnName) &&
                !String.IsNullOrEmpty(other.ColumnName) &&
                ColumnName.Equals(other.ColumnName, ModelFactory.CompareString));
        }

        public Int32 CompareTo(DbTableColumnKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbTableKey(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return String.Compare(ColumnName, other.ColumnName, true); }
        }

        public override int CompareTo(object? obj)
        { if (obj is DbTableColumnKey value) { return this.CompareTo(value); } else { return 1; } }

        public override bool Equals(object? obj)
        { if (obj is DbTableColumnKey value) { return this.Equals(value); } else { return false; } }

        public static bool operator ==(DbTableColumnKey left, DbTableColumnKey right)
        { return left.Equals(right); }

        public static bool operator !=(DbTableColumnKey left, DbTableColumnKey right)
        { return !left.Equals(right); }

        public static bool operator <(DbTableColumnKey left, DbTableColumnKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbTableColumnKey left, DbTableColumnKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbTableColumnKey left, DbTableColumnKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbTableColumnKey left, DbTableColumnKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        {
            if (CatalogName is String && SchemaName is String && TableName is String && ColumnName is String)
            { return (CatalogName, SchemaName, TableName, ColumnName).GetHashCode(); }
            else { return base.GetHashCode(); }
        }
        #endregion

        public override string ToString()
        {
            if (ColumnName is String)
            { return String.Format("{0}.{1}", base.ToString(), ColumnName); }
            else { return String.Empty; }
        }
    }
}
