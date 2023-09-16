using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDictionary.DataLayer.DatabaseData.Schema;

namespace DataDictionary.DataLayer.DatabaseData.Routine
{
    /// <summary>
    /// Interface for the Database Routine Key
    /// </summary>
    public interface IDbRoutineKey : IKey, IDbSchemaKey
    {
        /// <summary>
        /// Name of the Database Routine (Procedure or Function)
        /// </summary>
        String? RoutineName { get; }
    }

    /// <summary>
    /// Implementation of the Database Routine Key
    /// </summary>
    public class DbRoutineKey : DbSchemaKey, IDbRoutineKey, IKeyComparable<IDbRoutineKey>
    {
        /// <inheritdoc/>
        public String RoutineName { get; set; } = string.Empty;

        /// <summary>
        /// Constructor for the Database Routine Key
        /// </summary>
        /// <param name="source"></param>
        public DbRoutineKey(IDbRoutineKey source) : base(source)
        {
            if (source.RoutineName is string) { RoutineName = source.RoutineName; }
            else { RoutineName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbRoutineKey? other)
        {
            return 
                other is IDbSchemaKey &&
                new DbSchemaKey(this).Equals(other) &&
                !string.IsNullOrEmpty(RoutineName) &&
                !string.IsNullOrEmpty(other.RoutineName) &&
                RoutineName.Equals(other.RoutineName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbRoutineKey value && Equals(new DbRoutineKey(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbRoutineKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKey(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(RoutineName, other.RoutineName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbRoutineKey value) { return CompareTo(new DbRoutineKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbRoutineKey left, DbRoutineKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbRoutineKey left, DbRoutineKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbRoutineKey left, DbRoutineKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbRoutineKey left, DbRoutineKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbRoutineKey left, DbRoutineKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbRoutineKey left, DbRoutineKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(CatalogName, SchemaName, RoutineName); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (RoutineName is string)
            { return string.Format("{0}.{1}", base.ToString(), RoutineName); }
            else { return string.Empty; }
        }
    }
}
