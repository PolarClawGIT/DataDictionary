using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbDomainKey: IDbSchemaKey
    {
        String? DomainName { get; }
    }

    public class DbDomainKey : DbSchemaKey, IDbSchemaKey, IEquatable<DbDomainKey>, IComparable<DbDomainKey>, IComparable
    {
        public String DomainName { get; init; } = String.Empty;

        public DbDomainKey(IDbDomainKey source) : base(source)
        {
            if (source.DomainName is String) { DomainName = source.DomainName; }
            else { DomainName = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(DbDomainKey? other)
        {
            return (
                other is IDbSchemaKey &&
                new DbSchemaKey(this).Equals(other) &&
                !String.IsNullOrEmpty(DomainName) &&
                !String.IsNullOrEmpty(other.DomainName) &&
                DomainName.Equals(other.DomainName, ModelFactory.CompareString));
        }

        public Int32 CompareTo(DbDomainKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKey(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return String.Compare(DomainName, other.DomainName, true); }
        }

        public override int CompareTo(object? obj)
        { if (obj is IDbDomainKey value) { return this.CompareTo(new DbDomainKey(value)); } else { return 1; } }

        public override bool Equals(object? obj)
        { if (obj is IDbDomainKey value) { return this.Equals(new DbDomainKey(value)); } else { return false; } }

        public static bool operator ==(DbDomainKey left, DbDomainKey right)
        { return left.Equals(right); }

        public static bool operator !=(DbDomainKey left, DbDomainKey right)
        { return !left.Equals(right); }

        public static bool operator <(DbDomainKey left, DbDomainKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbDomainKey left, DbDomainKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbDomainKey left, DbDomainKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbDomainKey left, DbDomainKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        {
            if (CatalogName is String && SchemaName is String && DomainName is String)
            { return (CatalogName, SchemaName, DomainName).GetHashCode(); }
            else { return base.GetHashCode(); }
        }
        #endregion

        public override string ToString()
        {
            if (DomainName is String)
            { return String.Format("{0}.{1}", base.ToString(), DomainName); }
            else { return String.Empty; }
        }
    }
}
