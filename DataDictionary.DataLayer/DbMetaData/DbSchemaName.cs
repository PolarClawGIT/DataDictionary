using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbSchemaName : IDbCatalogName
    {
        String? SchemaName { get; }
    }

    public class DbSchemaName : IDbSchemaName, IEquatable<IDbSchemaName>, IComparable<IDbSchemaName>, IComparable
    {
        public String? SchemaName { get; init; }
        public String? CatalogName { get; init; }

        public DbSchemaName(IDbSchemaName source) : base()
        {
            CatalogName = source.CatalogName;
            SchemaName = source.SchemaName;
        }

        #region IEquatable, IComparable
        public Boolean Equals(IDbSchemaName? other)
        {
            return (
                other is IDbSchemaName &&
                new DbCatalogName(this).Equals(other) &&
                !String.IsNullOrEmpty(SchemaName) &&
                !String.IsNullOrEmpty(other.SchemaName) &&
                SchemaName.Equals(other.SchemaName, StringComparison.CurrentCultureIgnoreCase));
        }

        public Int32 CompareTo(IDbSchemaName? other)
        {
            if (other is null) { return 1; }
            else if (new DbCatalogName(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return String.Compare(SchemaName, other.SchemaName, true); }
        }

        public int CompareTo(object? obj)
        { if (obj is IDbSchemaName value) { return this.CompareTo(value); } else { return 1; } }

        public override bool Equals(object? obj)
        { if (obj is IDbSchemaName value) { return this.Equals(value); } else { return false; } }

        public static bool operator ==(DbSchemaName left, IDbSchemaName right)
        { return left.Equals(right); }

        public static bool operator !=(DbSchemaName left, IDbSchemaName right)
        { return !left.Equals(right); }

        public static bool operator <(DbSchemaName left, IDbSchemaName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbSchemaName left, IDbSchemaName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbSchemaName left, IDbSchemaName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbSchemaName left, IDbSchemaName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        {
            if (CatalogName is String && SchemaName is String)
            { return (CatalogName, SchemaName).GetHashCode(); }
            else { return String.Empty.GetHashCode(); }
        }
        #endregion

        public static implicit operator DbCatalogName(DbSchemaName value)
        { return new DbCatalogName(value); }

        public override string ToString()
        {
            if (SchemaName is String)
            { return String.Format("{0}.{1}", ((DbCatalogName)this).ToString(), SchemaName); }
            else { return String.Empty; }
        }
    }
}
