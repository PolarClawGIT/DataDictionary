using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbTableName : IDbSchemaName
    {
        public String? ObjectName { get; }
    }

    public class DbTableName : DbSchemaName, IDbTableName, IEquatable<IDbTableName>, IComparable<IDbTableName>, IComparable
    {
        public String ObjectName { get; init; } = String.Empty;

        public DbTableName() : base() { }

        public DbTableName(IDbTableName source) : base(source)
        {
            if (source.ObjectName is String) { ObjectName = source.ObjectName; }
            else { ObjectName = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(IDbTableName? other)
        {
            return (
                other is IDbSchemaName &&
                new DbSchemaName(this).Equals(other) &&
                !String.IsNullOrEmpty(ObjectName) &&
                !String.IsNullOrEmpty(other.ObjectName) &&
                ObjectName.Equals(other.ObjectName, StringComparison.CurrentCultureIgnoreCase));
        }

        public Int32 CompareTo(IDbTableName? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaName(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return String.Compare(ObjectName, other.ObjectName, true); }
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
            if (CatalogName is String && SchemaName is String && ObjectName is String)
            { return (CatalogName, SchemaName, ObjectName).GetHashCode(); }
            else { return base.GetHashCode(); }
        }
        #endregion

        public override string ToString()
        {
            if (ObjectName is String)
            { return String.Format("{0}.{1}", base.ToString(), ObjectName); }
            else { return String.Empty; }
        }

    }
}
