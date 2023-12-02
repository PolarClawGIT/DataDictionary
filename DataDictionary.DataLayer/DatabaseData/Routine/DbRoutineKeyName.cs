﻿using System;
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
    public interface IDbRoutineKeyName : IKey, IDbSchemaKeyName
    {
        /// <summary>
        /// Name of the Database Routine (Procedure or Function)
        /// </summary>
        String? RoutineName { get; }
    }

    /// <summary>
    /// Implementation of the Database Routine Key
    /// </summary>
    public class DbRoutineKeyName : DbSchemaKeyName, IDbRoutineKeyName, IKeyComparable<IDbRoutineKeyName>
    {
        /// <inheritdoc/>
        public String RoutineName { get; set; } = string.Empty;

        /// <summary>
        /// Constructor for the Database Routine Key
        /// </summary>
        /// <param name="source"></param>
        public DbRoutineKeyName(IDbRoutineKeyName source) : base(source)
        {
            if (source.RoutineName is string) { RoutineName = source.RoutineName; }
            else { RoutineName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbRoutineKeyName? other)
        {
            return 
                other is IDbSchemaKeyName &&
                new DbSchemaKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(RoutineName) &&
                !string.IsNullOrEmpty(other.RoutineName) &&
                RoutineName.Equals(other.RoutineName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbRoutineKeyName value && Equals(new DbRoutineKeyName(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbRoutineKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(RoutineName, other.RoutineName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbRoutineKeyName value) { return CompareTo(new DbRoutineKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbRoutineKeyName left, DbRoutineKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbRoutineKeyName left, DbRoutineKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbRoutineKeyName left, DbRoutineKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbRoutineKeyName left, DbRoutineKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbRoutineKeyName left, DbRoutineKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbRoutineKeyName left, DbRoutineKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), RoutineName.GetHashCode(KeyExtension.CompareString)); }
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
