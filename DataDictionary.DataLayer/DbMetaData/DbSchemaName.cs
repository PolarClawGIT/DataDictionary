using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbSchemaName : IDbCatalogName
    {
        String? SchemaName { get; }
    }

    public record DbSchemaName : IDbSchemaName, IEquatable<IDbSchemaName>, IComparable<IDbSchemaName>
    {
        public string? SchemaName { get; init; }
        public string? CatalogName { get; init; }

        public DbSchemaName(IDbSchemaName source) : base()
        {
            CatalogName = source.CatalogName;
            SchemaName = source.SchemaName;
        }

        public bool Equals(IDbSchemaName? other)
        {
            return (
                other is IDbSchemaName &&
                !String.IsNullOrEmpty(CatalogName) &&
                !String.IsNullOrEmpty(other.CatalogName) &&
                ((IDbCatalogName)this).Equals((IDbCatalogName)other) &&
                CatalogName.Equals(other.CatalogName, StringComparison.CurrentCultureIgnoreCase));
        }

        public int CompareTo(IDbSchemaName? other)
        {
            if (other is IDbSchemaName && (new DbCatalogName(this).Equals(new DbCatalogName(other))))
            { return String.Compare(SchemaName, other.SchemaName, true); }
            else if (other is IDbSchemaName) { return new DbCatalogName(this).CompareTo(new DbCatalogName(other)); }
            else
            {
                if (String.IsNullOrEmpty(SchemaName)) { return 1; }
                else { return -1; }
            }
        }

        public static bool operator <(DbSchemaName left, DbSchemaName right)
        {
            return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0;
        }

        public static bool operator <=(DbSchemaName left, DbSchemaName right)
        {
            return ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
        }

        public static bool operator >(DbSchemaName left, DbSchemaName right)
        {
            return !ReferenceEquals(left, null) && left.CompareTo(right) > 0;
        }

        public static bool operator >=(DbSchemaName left, DbSchemaName right)
        {
            return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
        }

    }
}
