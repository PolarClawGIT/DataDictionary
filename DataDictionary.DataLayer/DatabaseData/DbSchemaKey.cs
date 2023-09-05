using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData
{
    public interface IDbSchemaKey : IDbCatalogKeyUnique
    {
        String? SchemaName { get; }
    }

    public class DbSchemaKey : DbCatalogKeyUnique, IDbSchemaKey, IEquatable<DbSchemaKey>, IComparable<DbSchemaKey>, IComparable
    {
        public String SchemaName { get; init; } = String.Empty;

        public DbSchemaKey(IDbSchemaKey source) : base(source)
        {
            if (source.SchemaName is String) { SchemaName = source.SchemaName; }
            else { SchemaName = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(DbSchemaKey? other)
        {
            return (
                other is IDbSchemaKey &&
                new DbCatalogKeyUnique(this).Equals(other) &&
                !String.IsNullOrEmpty(SchemaName) &&
                !String.IsNullOrEmpty(other.SchemaName) &&
                SchemaName.Equals(other.SchemaName, ModelFactory.CompareString));
        }

        public override bool Equals(object? obj)
        { return obj is IDbSchemaKey value && this.Equals(new DbSchemaKey(value)); }

        public Int32 CompareTo(DbSchemaKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbCatalogKeyUnique(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return String.Compare(SchemaName, other.SchemaName, true); }
        }

        public override int CompareTo(object? obj)
        { if (obj is IDbSchemaKey value) { return this.CompareTo(new DbSchemaKey(value)); } else { return 1; } }

        public static bool operator ==(DbSchemaKey left, DbSchemaKey right)
        { return left.Equals(right); }

        public static bool operator !=(DbSchemaKey left, DbSchemaKey right)
        { return !left.Equals(right); }

        public static bool operator <(DbSchemaKey left, DbSchemaKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbSchemaKey left, DbSchemaKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbSchemaKey left, DbSchemaKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbSchemaKey left, DbSchemaKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        { return HashCode.Combine(CatalogName, SchemaName); }
        #endregion

        public override string ToString()
        {
            if (SchemaName is String)
            { return String.Format("{0}.{1}", base.ToString(), SchemaName); }
            else { return String.Empty; }
        }
    }
}
