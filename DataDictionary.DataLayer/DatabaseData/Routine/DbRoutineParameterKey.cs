using DataDictionary.DataLayer.DatabaseData.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Routine
{
    /// <summary>
    /// Interface for the Database Routine Parameter Key
    /// </summary>
    public interface IDbRoutineParameterKey : IKey, IDbRoutineKey
    {
        /// <summary>
        /// Name of the Database Parameter
        /// </summary>
        String? ParameterName { get; }
    }

    /// <summary>
    /// Implementation for Database Routine Parameter Key
    /// </summary>
    public class DbRoutineParameterKey : DbRoutineKey, IDbRoutineParameterKey, IKeyComparable<DbRoutineParameterKey>
    {
        /// <inheritdoc/>
        public String ParameterName { get; set; } = string.Empty;

        /// <summary>
        /// Constructor for Database Routine Parameter Key
        /// </summary>
        /// <param name="source"></param>
        public DbRoutineParameterKey(IDbRoutineParameterKey source) : base(source)
        {
            if (source.ParameterName is string) { ParameterName = source.ParameterName; }
            else { ParameterName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(DbRoutineParameterKey? other)
        {
            return 
                other is IDbRoutineKey &&
                new DbSchemaKey(this).Equals(other) &&
                !string.IsNullOrEmpty(RoutineName) &&
                !string.IsNullOrEmpty(other.RoutineName) &&
                ParameterName.Equals(other.ParameterName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbRoutineParameterKey value && Equals(new DbRoutineParameterKey(value)); }

        /// <inheritdoc/>
        public int CompareTo(DbRoutineParameterKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbRoutineKey(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ParameterName, other.ParameterName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbRoutineParameterKey value) { return CompareTo(new DbRoutineParameterKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbRoutineParameterKey left, DbRoutineParameterKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbRoutineParameterKey left, DbRoutineParameterKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbRoutineParameterKey left, DbRoutineParameterKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbRoutineParameterKey left, DbRoutineParameterKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbRoutineParameterKey left, DbRoutineParameterKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbRoutineParameterKey left, DbRoutineParameterKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ParameterName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (ParameterName is string)
            { return string.Format("{0}.{1}", base.ToString(), ParameterName); }
            else { return string.Empty; }
        }

    }
}
