using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbRoutineParameterKey: IDbRoutineKey
    {
        String? ParameterName { get; }
    }

    public class DbRoutineParameterKey : DbRoutineKey, IDbRoutineParameterKey, IEquatable<DbRoutineParameterKey>, IComparable<DbRoutineParameterKey>, IComparable
    {
        public String ParameterName { get; set; } = String.Empty;

        public DbRoutineParameterKey(IDbRoutineParameterKey source) : base(source)
        {
            if (source.ParameterName is String) { ParameterName = source.ParameterName; }
            else { ParameterName = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(DbRoutineParameterKey? other)
        {
            return (
                other is IDbRoutineKey &&
                new DbSchemaKey(this).Equals(other) &&
                !String.IsNullOrEmpty(RoutineName) &&
                !String.IsNullOrEmpty(other.RoutineName) &&
                ParameterName.Equals(other.ParameterName, ModelFactory.CompareString));
        }

        public Int32 CompareTo(DbRoutineParameterKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbRoutineKey(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return String.Compare(ParameterName, other.ParameterName, true); }
        }

        public override int CompareTo(object? obj)
        { if (obj is IDbRoutineParameterKey value) { return this.CompareTo(new DbRoutineParameterKey(value)); } else { return 1; } }

        public override bool Equals(object? obj)
        { if (obj is IDbRoutineParameterKey value) { return this.Equals(new DbRoutineParameterKey(value)); } else { return false; } }

        public static bool operator ==(DbRoutineParameterKey left, DbRoutineParameterKey right)
        { return left.Equals(right); }

        public static bool operator !=(DbRoutineParameterKey left, DbRoutineParameterKey right)
        { return !left.Equals(right); }

        public static bool operator <(DbRoutineParameterKey left, DbRoutineParameterKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbRoutineParameterKey left, DbRoutineParameterKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbRoutineParameterKey left, DbRoutineParameterKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbRoutineParameterKey left, DbRoutineParameterKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        {
            if (CatalogName is String && SchemaName is String && RoutineName is String && ParameterName is String)
            { return (CatalogName, SchemaName, RoutineName, ParameterName).GetHashCode(); }
            else { return base.GetHashCode(); }
        }
        #endregion

        public override string ToString()
        {
            if (ParameterName is String)
            { return String.Format("{0}.{1}", base.ToString(), ParameterName); }
            else { return String.Empty; }
        }

    }
}
