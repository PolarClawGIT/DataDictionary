using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbColumnName : IDbTableName
    {
        public String? ColumnName { get; }
    }

    public class DbColumnName : DbTableName, IDbColumnName, IEquatable<IDbColumnName>, IComparable<IDbColumnName>, IComparable
    {
        public String ColumnName { get; init; } = String.Empty;

        public DbColumnName() : base() { }

        public DbColumnName(IDbColumnName source) : base(source)
        {
            if (source.ColumnName is String) { ColumnName = source.ColumnName; }
            else { ColumnName = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(IDbColumnName? other)
        {
            return (
                other is IDbSchemaName &&
                new DbTableName(this).Equals(other) &&
                !String.IsNullOrEmpty(ColumnName) &&
                !String.IsNullOrEmpty(other.ColumnName) &&
                ColumnName.Equals(other.ColumnName, ModelFactory.CompareString));
        }

        public Int32 CompareTo(IDbColumnName? other)
        {
            if (other is null) { return 1; }
            else if (new DbTableName(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return String.Compare(ColumnName, other.ColumnName, true); }
        }

        public override int CompareTo(object? obj)
        { if (obj is IDbColumnName value) { return this.CompareTo(value); } else { return 1; } }

        public override bool Equals(object? obj)
        { if (obj is IDbColumnName value) { return this.Equals(value); } else { return false; } }

        public static bool operator ==(DbColumnName left, IDbColumnName right)
        { return left.Equals(right); }

        public static bool operator !=(DbColumnName left, IDbColumnName right)
        { return !left.Equals(right); }

        public static bool operator <(DbColumnName left, IDbColumnName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbColumnName left, IDbColumnName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbColumnName left, IDbColumnName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbColumnName left, IDbColumnName right)
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
