using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Routine
{
    // This is all in one file because they are so closely related and was easer to find.

    /// <summary>
    /// List of supported Routine Types.
    /// </summary>
    public enum DbRoutineType
    {
        /// <summary>
        /// Unknown Routine Type
        /// </summary>
        Null,

        /// <summary>
        /// SQL Function
        /// </summary>
        Function,

        /// <summary>
        /// SQL Procedure
        /// </summary>
        Procedure,
    }

    /// <summary>
    /// Interface for Database RoutineType Key.
    /// </summary>
    public interface IDbRoutineTypeKey : IKey
    {
        /// <summary>
        /// Type of Routine (such as procedure or function)
        /// </summary>
        DbRoutineType RoutineType { get; }
    }

    /// <summary>
    /// Interface for Database RoutineType
    /// </summary>
    public interface IDbRoutineType : IDbRoutineTypeKey
    {
        /// <summary>
        /// Type of Routine Object (Routine, View, ...)
        /// </summary>
        String? RoutineTypeName { get; }
    }

    static class IDbRoutineTypeExtension
    {
        /// <summary>
        /// Given the interface IDbRoutineType, return the RoutineType Enum.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Use this to implement TypeType.
        /// </remarks>
        /// <example>
        /// public DbRoutineType RoutineType { get { return ((IDbRoutineType)this).GetRoutineType(); } }
        /// </example>
        public static DbRoutineType GetRoutineType(this IDbRoutineType source)
        {
            // This approach came about because default implementation within an Interface does not work as expected.
            // Sometimes you can override implementation by using New, but many times you end up with an infinite loop.

            if (DbRoutineTypeKey.TryParse(source.RoutineTypeName, out DbRoutineTypeKey? result))
            { return result.RoutineType; }
            else { return DbRoutineType.Null; }
        }
    }

    /// <summary>
    /// Implementation for Database RoutineType Key.
    /// </summary>
    public class DbRoutineTypeKey : IDbRoutineTypeKey, IKeyComparable<IDbRoutineTypeKey>, IParsable<DbRoutineTypeKey>
    {
        /// <inheritdoc/>
        public DbRoutineType RoutineType { get; init; } = DbRoutineType.Null;

        /// <summary>
        /// Basic Constructor for the RoutineType Key.
        /// </summary>
        protected DbRoutineTypeKey() : base() { }

        /// <summary>
        /// Constructor for the RoutineType Key.
        /// </summary>
        /// <param name="source"></param>
        public DbRoutineTypeKey(DbRoutineType source) : this()
        { RoutineType = source; }

        /// <summary>
        /// Constructor for the RoutineType Key.
        /// </summary>
        /// <param name="source"></param>
        public DbRoutineTypeKey(IDbRoutineTypeKey source) : this()
        { if (source is IDbRoutineTypeKey) { RoutineType = source.RoutineType; } }

        /// <summary>
        /// Converts a DbRoutineType into a DbRoutineTypeKey.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DbRoutineTypeKey(DbRoutineType source) { return new DbRoutineTypeKey(source); }

        /// <summary>
        /// Converts a DbRoutineTypeKey into a DbRoutineType.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DbRoutineType(DbRoutineTypeKey source) { return source.RoutineType; }

        #region IEquaRoutine
        /// <inheritdoc/>
        public Boolean Equals(IDbRoutineTypeKey? other)
        {
            return other is IDbRoutineTypeKey
                && this.RoutineType != DbRoutineType.Null
                && other.RoutineType != DbRoutineType.Null
                && this.RoutineType.Equals(other.RoutineType);
        }

        /// <inheritdoc/>
        public virtual int CompareTo(IDbRoutineTypeKey? other)
        {
            if (other is IDbRoutineTypeKey value)
            { return string.Compare(this.ToString(), value.ToString(), true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDbRoutineTypeKey value) { return CompareTo(new DbRoutineTypeKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbRoutineTypeKey value && this.Equals(new DbRoutineTypeKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbRoutineTypeKey left, DbRoutineTypeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbRoutineTypeKey left, DbRoutineTypeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbRoutineTypeKey left, DbRoutineTypeKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbRoutineTypeKey left, DbRoutineTypeKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbRoutineTypeKey left, DbRoutineTypeKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbRoutineTypeKey left, DbRoutineTypeKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return (RoutineType).GetHashCode(); }
        #endregion

        #region IParsable
        /// <summary>
        /// This is the list that translates the DbRoutineType Enum to what the Database uses as RoutineType Name.
        /// </summary>
        static Dictionary<DbRoutineType, String> parseName = new Dictionary<DbRoutineType, String>()
        {
            { DbRoutineType.Function,  "FUNCTION"},
            { DbRoutineType.Procedure, "PROCEDURE"},
        };

        /// <inheritdoc/>
        public static DbRoutineTypeKey Parse(String source, IFormatProvider? provider)
        {
            if (String.IsNullOrEmpty(source))
            { throw new ArgumentNullException(nameof(source)); }

            if (DbRoutineTypeKey.TryParse(source, provider, out DbRoutineTypeKey? result))
            { return result; }
            else
            {
                Exception ex = new FormatException();
                ex.Data.Add(nameof(source), source);
                throw ex;
            }
        }

        /// <inheritdoc/>
        public static bool TryParse([NotNullWhen(true)] String? source, IFormatProvider? provider, [MaybeNullWhen(false)] out DbRoutineTypeKey result)
        {
            if (parseName.FirstOrDefault(w => w.Value.Equals(source, KeyExtension.CompareString))
                is KeyValuePair<DbRoutineType, String> dbItem
                && dbItem.Key != DbRoutineType.Null)
            { result = new DbRoutineTypeKey() { RoutineType = dbItem.Key }; return true; }
            else { result = null; return false; }
        }

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="result">When this method returns, contains the result of successfully parsing</param>
        /// <returns>true if s was successfully parsed; otherwise, false.</returns>
        public static bool TryParse([NotNullWhen(true)] String? source, [MaybeNullWhen(false)] out DbRoutineTypeKey result)
        {
            if (parseName.FirstOrDefault(w => w.Value.Equals(source, KeyExtension.CompareString))
                is KeyValuePair<DbRoutineType, String> dbItem
                && dbItem.Key != DbRoutineType.Null)
            { result = new DbRoutineTypeKey() { RoutineType = dbItem.Key }; return true; }
            else { result = null; return false; }
        }
        #endregion

        /// <summary>
        /// Returns the RoutineType Name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (parseName.ContainsKey(RoutineType)) { return parseName[RoutineType]; }
            else { return RoutineType.ToString(); }
        }
    }
}

