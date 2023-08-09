using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbTableKey : IDbSchemaKey
    {
        public String? TableName { get; }
    }

    public class DbTableKey : DbSchemaKey, IDbTableKey, IEquatable<DbTableKey>, IComparable<DbTableKey>, IComparable
    {
        public String TableName { get; init; } = String.Empty;

        public DbTableKey(IDbTableKey source) : base(source)
        {
            if (source.TableName is String) { TableName = source.TableName; }
            else { TableName = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(DbTableKey? other)
        {
            return (
                other is IDbSchemaKey &&
                new DbSchemaKey(this).Equals(other) &&
                !String.IsNullOrEmpty(TableName) &&
                !String.IsNullOrEmpty(other.TableName) &&
                TableName.Equals(other.TableName, ModelFactory.CompareString));
        }

        public Int32 CompareTo(DbTableKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKey(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return String.Compare(TableName, other.TableName, true); }
        }

        public override int CompareTo(object? obj)
        { if (obj is IDbTableKey value) { return this.CompareTo(new DbTableKey(value)); } else { return 1; } }

        public override bool Equals(object? obj)
        { if (obj is IDbTableKey value) { return this.Equals(new DbTableKey(value)); } else { return false; } }

        public static bool operator ==(DbTableKey left, DbTableKey right)
        { return left.Equals(right); }

        public static bool operator !=(DbTableKey left, DbTableKey right)
        { return !left.Equals(right); }

        public static bool operator <(DbTableKey left, DbTableKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbTableKey left, DbTableKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbTableKey left, DbTableKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbTableKey left, DbTableKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        { return HashCode.Combine(CatalogName, SchemaName, TableName); }
        #endregion

        public override string ToString()
        {
            if (TableName is String)
            { return String.Format("{0}.{1}", base.ToString(), TableName); }
            else { return String.Empty; }
        }

    }
}
