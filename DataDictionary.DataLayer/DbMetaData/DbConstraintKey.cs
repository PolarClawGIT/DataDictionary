using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbConstraintKey : IDbSchemaKey
    {
        String? ConstraintName { get; }
    }

    public class DbConstraintKey : DbSchemaKey, IDbConstraintKey, IEquatable<DbConstraintKey>, IComparable<DbConstraintKey>, IComparable
    {
        public String ConstraintName { get; init; } = String.Empty;

        public DbConstraintKey(IDbConstraintKey source) : base(source)
        {
            if (source.ConstraintName is String) { ConstraintName = source.ConstraintName; }
            else { ConstraintName = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(DbConstraintKey? other)
        {
            return (
                other is IDbSchemaKey &&
                new DbSchemaKey(this).Equals(other) &&
                !String.IsNullOrEmpty(ConstraintName) &&
                !String.IsNullOrEmpty(other.ConstraintName) &&
                ConstraintName.Equals(other.ConstraintName, ModelFactory.CompareString));
        }

        public Int32 CompareTo(DbConstraintKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKey(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return String.Compare(ConstraintName, other.ConstraintName, true); }
        }

        public override int CompareTo(object? obj)
        { if (obj is IDbConstraintKey value) { return this.CompareTo(new DbConstraintKey(value)); } else { return 1; } }

        public override bool Equals(object? obj)
        { if (obj is IDbConstraintKey value) { return this.Equals(new DbConstraintKey(value)); } else { return false; } }

        public static bool operator ==(DbConstraintKey left, DbConstraintKey right)
        { return left.Equals(right); }

        public static bool operator !=(DbConstraintKey left, DbConstraintKey right)
        { return !left.Equals(right); }

        public static bool operator <(DbConstraintKey left, DbConstraintKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbConstraintKey left, DbConstraintKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbConstraintKey left, DbConstraintKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbConstraintKey left, DbConstraintKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        {
            if (CatalogName is String && SchemaName is String && ConstraintName is String)
            { return (CatalogName, SchemaName, ConstraintName).GetHashCode(); }
            else { return base.GetHashCode(); }
        }
        #endregion

        public override string ToString()
        {
            if (ConstraintName is String)
            { return String.Format("{0}.{1}", base.ToString(), ConstraintName); }
            else { return String.Empty; }
        }

    }
}
