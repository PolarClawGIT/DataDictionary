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

    public class DbSchemaName : IDbSchemaName, IEquatable<IDbSchemaName>, IComparable<IDbSchemaName>
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
                new DbCatalogName(this).Equals(other) &&
                !String.IsNullOrEmpty(SchemaName) &&
                !String.IsNullOrEmpty(other.SchemaName) &&
                SchemaName.Equals(other.SchemaName, StringComparison.CurrentCultureIgnoreCase));
        }

        public int CompareTo(IDbSchemaName? other)
        {
            if (other is null) { return 1; }
            else if (new DbCatalogName(this).CompareTo(other) is Int32 catalogValue && catalogValue != 0) { return catalogValue; }
            else { return String.Compare(SchemaName, other.SchemaName, true); }
        }

        public static bool operator <(DbSchemaName left, IDbSchemaName right)
        {
            return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0;
        }

        public static bool operator <=(DbSchemaName left, IDbSchemaName right)
        {
            return ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
        }

        public static bool operator >(DbSchemaName left, IDbSchemaName right)
        {
            return !ReferenceEquals(left, null) && left.CompareTo(right) > 0;
        }

        public static bool operator >=(DbSchemaName left, IDbSchemaName right)
        {
            return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
        }

        public override bool Equals(object? obj)
        {
            if (obj is IDbCatalogName value) { return this.Equals(value); }
            else { return false; }
        }

        public override int GetHashCode()
        {
            if(SchemaName is String) { return (new DbCatalogName(this), SchemaName).GetHashCode(); }
            else { return (new DbCatalogName(this), String.Empty).GetHashCode(); }
        }

        public static bool operator ==(DbSchemaName left, IDbSchemaName right)
        { return left.Equals(right); }

        public static bool operator !=(DbSchemaName left, IDbSchemaName right)
        { return !left.Equals(right); }
    }
}
