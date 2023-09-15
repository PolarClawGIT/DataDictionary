using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData
{
    public interface IDbRoutineKey : IDbSchemaKey
    {
        String? RoutineName { get; }
    }

    public class DbRoutineKey : DbSchemaKey, IDbRoutineKey, IEquatable<DbRoutineKey>, IComparable<DbRoutineKey>, IComparable
    {
        public String RoutineName { get; set; } = String.Empty;

        public DbRoutineKey(IDbRoutineKey source) : base(source)
        {
            if (source.RoutineName is String) { RoutineName = source.RoutineName; }
            else { RoutineName = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(DbRoutineKey? other)
        {
            return (
                other is IDbSchemaKey &&
                new DbSchemaKey(this).Equals(other) &&
                !String.IsNullOrEmpty(RoutineName) &&
                !String.IsNullOrEmpty(other.RoutineName) &&
                RoutineName.Equals(other.RoutineName, KeyExtension.CompareString));
        }

        public override bool Equals(object? obj)
        { return obj is IDbRoutineKey value && this.Equals(new DbRoutineKey(value)); }

        public Int32 CompareTo(DbRoutineKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKey(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return String.Compare(RoutineName, other.RoutineName, true); }
        }

        public override int CompareTo(object? obj)
        { if (obj is IDbRoutineKey value) { return this.CompareTo(new DbRoutineKey(value)); } else { return 1; } }

        public static bool operator ==(DbRoutineKey left, DbRoutineKey right)
        { return left.Equals(right); }

        public static bool operator !=(DbRoutineKey left, DbRoutineKey right)
        { return !left.Equals(right); }

        public static bool operator <(DbRoutineKey left, DbRoutineKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbRoutineKey left, DbRoutineKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbRoutineKey left, DbRoutineKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbRoutineKey left, DbRoutineKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        { return HashCode.Combine(CatalogName, SchemaName, RoutineName); }
        #endregion

        public override string ToString()
        {
            if (RoutineName is String)
            { return String.Format("{0}.{1}", base.ToString(), RoutineName); }
            else { return String.Empty; }
        }
    }
}
