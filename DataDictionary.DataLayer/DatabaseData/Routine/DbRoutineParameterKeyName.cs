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
    public interface IDbRoutineParameterKeyName : IKey, IDbRoutineKeyName
    {
        /// <summary>
        /// Name of the Database Parameter
        /// </summary>
        String? ParameterName { get; }
    }

    /// <summary>
    /// Implementation for Database Routine Parameter Key
    /// </summary>
    public class DbRoutineParameterKeyName : DbRoutineKeyName, IDbRoutineParameterKeyName, IKeyComparable<IDbRoutineParameterKeyName>
    {
        /// <inheritdoc/>
        public String ParameterName { get; set; } = string.Empty;

        /// <summary>
        /// Constructor for Database Routine Parameter Key
        /// </summary>
        /// <param name="source"></param>
        public DbRoutineParameterKeyName(IDbRoutineParameterKeyName source) : base(source)
        {
            if (source.ParameterName is string) { ParameterName = source.ParameterName; }
            else { ParameterName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbRoutineParameterKeyName? other)
        {
            return 
                other is IDbRoutineKeyName &&
                new DbRoutineKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(ParameterName) &&
                !string.IsNullOrEmpty(other.ParameterName) &&
                ParameterName.Equals(other.ParameterName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbRoutineParameterKeyName value && Equals(new DbRoutineParameterKeyName(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbRoutineParameterKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new DbRoutineKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ParameterName, other.ParameterName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbRoutineParameterKeyName value) { return CompareTo(new DbRoutineParameterKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
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
