using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Routine
{


    /// <summary>
    /// Implementation for Database RoutineType Key.
    /// </summary>
    public class DbRoutineTypeKey : IDbRoutineType, IKeyComparable<IDbRoutineType>, IParsable<DbRoutineTypeKey>
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
        public DbRoutineTypeKey(IDbRoutineType source) : this()
        { if (source is IDbRoutineType) { RoutineType = source.RoutineType; } }

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
        public Boolean Equals(IDbRoutineType? other)
        {
            return other is IDbRoutineType
                && this.RoutineType != DbRoutineType.Null
                && other.RoutineType != DbRoutineType.Null
                && this.RoutineType.Equals(other.RoutineType);
        }

        /// <inheritdoc/>
        public virtual int CompareTo(IDbRoutineType? other)
        {
            if (other is IDbRoutineType value)
            { return string.Compare(this.ToString(), value.ToString(), true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDbRoutineType value) { return CompareTo(new DbRoutineTypeKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbRoutineType value && this.Equals(new DbRoutineTypeKey(value)); }

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
            { DbRoutineType.Function,  "Function"},
            { DbRoutineType.Procedure, "Procedure"},
        };

        /// <inheritdoc/>
        public static DbRoutineTypeKey Parse(String source, IFormatProvider? provider = null)
        {
            if (String.IsNullOrEmpty(source))
            { throw new ArgumentNullException(nameof(source)); }

            if (DbRoutineTypeKey.TryParse(source, provider, out DbRoutineTypeKey? result))
            { return result; }
            else
            { return DbRoutineType.Null; }
        }

        /// <inheritdoc/>
        public static bool TryParse([NotNullWhen(true)] String? source, IFormatProvider? provider, [MaybeNullWhen(false)] out DbRoutineTypeKey result)
        { return TryParse(source, out result); }

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
        /// Returns an IEnumerable of DbRoutineTypeKey for each DbRoutineType.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<DbRoutineTypeKey> Items()
        { return Enum.GetValues(typeof(DbRoutineType)).Cast<DbRoutineType>().Select(s => new DbRoutineTypeKey(s)); }

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
