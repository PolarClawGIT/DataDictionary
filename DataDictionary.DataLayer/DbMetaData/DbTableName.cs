using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbTableName : IDbSchemaName
    {
        public String? TableName { get; }
    }

    public class DbTableName : DbSchemaName, IDbTableName, IEquatable<IDbTableName>, IComparable<IDbTableName>, IComparable
    {
        public String TableName { get; init; } = String.Empty;

        public DbTableName() : base() { }

        public DbTableName(IDbTableName source) : base(source)
        {
            if (source.TableName is String) { TableName = source.TableName; }
            else { TableName = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(IDbTableName? other)
        {
            return (
                other is IDbSchemaName &&
                new DbSchemaName(this).Equals(other) &&
                !String.IsNullOrEmpty(TableName) &&
                !String.IsNullOrEmpty(other.TableName) &&
                TableName.Equals(other.TableName, ModelFactory.CompareString));
        }

        public Int32 CompareTo(IDbTableName? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaName(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return String.Compare(TableName, other.TableName, true); }
        }

        public override int CompareTo(object? obj)
        { if (obj is IDbTableName value) { return this.CompareTo(value); } else { return 1; } }

        public override bool Equals(object? obj)
        { if (obj is IDbTableName value) { return this.Equals(value); } else { return false; } }

        public static bool operator ==(DbTableName left, IDbTableName right)
        { return left.Equals(right); }

        public static bool operator !=(DbTableName left, IDbTableName right)
        { return !left.Equals(right); }

        public static bool operator <(DbTableName left, IDbTableName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbTableName left, IDbTableName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbTableName left, IDbTableName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbTableName left, IDbTableName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        {
            if (CatalogName is String && SchemaName is String && TableName is String)
            { return (CatalogName, SchemaName, TableName).GetHashCode(); }
            else { return base.GetHashCode(); }
        }
        #endregion

        public override string ToString()
        {
            if (TableName is String)
            { return String.Format("{0}.{1}", base.ToString(), TableName); }
            else { return String.Empty; }
        }

    }
}
