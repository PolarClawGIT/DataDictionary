using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataDictionary.DataLayer.DatabaseData.Table
{
    // This is all in one file because they are so closely related and was easer to find.

    /// <summary>
    /// List of supported Table Types.
    /// </summary>
    public enum DbTableType
    {
        /// <summary>
        /// Unknown Table Type
        /// </summary>
        Null,

        /// <summary>
        /// Base Table
        /// </summary>
        Table,

        /// <summary>
        /// Temporal Table
        /// </summary>
        TemporalTable,

        /// <summary>
        /// History Table, backed by Temporal Table
        /// </summary>
        HistoryTable,

        /// <summary>
        /// View
        /// </summary>
        View
    }

    /// <summary>
    /// Interface for Database TableType Key.
    /// </summary>
    public interface IDbTableTypeKey : IKey
    {
        /// <summary>
        /// Type of Table
        /// </summary>
        DbTableType TableType { get; }
    }

    /// <summary>
    /// Interface for Database TableType
    /// </summary>
    public interface IDbTableType : IDbTableTypeKey
    {
        /// <summary>
        /// Type of Table Object (Table, View, ...)
        /// </summary>
        String? TableTypeName { get; }
    }

    static class IDbTableTypeExtension
    {
        /// <summary>
        /// Given the interface IDbTableType, return the TableType Enum.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Use this to implement TypeType.
        /// </remarks>
        /// <example>
        /// public DbTableType TableType { get { return ((IDbTableType)this).GetTableType(); } }
        /// </example>
        public static DbTableType GetTableType(this IDbTableType source)
        {
            // This approach came about because default implementation within an Interface does not work as expected.
            // Sometimes you can override implementation by using New, but many times you end up with an infinite loop.

            if (DbTableTypeKey.TryParse(source.TableTypeName, out DbTableTypeKey? result))
            { return result.TableType; }
            else { return DbTableType.Null; }
        }
    }

    /// <summary>
    /// Implementation for Database TableType Key.
    /// </summary>
    public class DbTableTypeKey : IDbTableTypeKey, IKeyComparable<IDbTableTypeKey>, IParsable<DbTableTypeKey>
    {
        /// <inheritdoc/>
        public DbTableType TableType { get; init; } = DbTableType.Null;

        /// <summary>
        /// Basic Constructor for the TableType Key.
        /// </summary>
        protected DbTableTypeKey() : base() { }

        /// <summary>
        /// Constructor for the TableType Key.
        /// </summary>
        /// <param name="source"></param>
        public DbTableTypeKey(DbTableType source) : this()
        { TableType = source; }

        /// <summary>
        /// Constructor for the TableType Key.
        /// </summary>
        /// <param name="source"></param>
        public DbTableTypeKey(IDbTableTypeKey source) : this()
        { if (source is IDbTableTypeKey) { TableType = source.TableType; } }

        /// <summary>
        /// Converts a DbTableType into a DbTableTypeKey.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DbTableTypeKey(DbTableType source) { return new DbTableTypeKey(source); }

        /// <summary>
        /// Converts a DbTableTypeKey into a DbTableType.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DbTableType(DbTableTypeKey source) { return source.TableType; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual Boolean Equals(IDbTableTypeKey? other)
        {
            return other is IDbTableTypeKey
                && this.TableType != DbTableType.Null
                && other.TableType != DbTableType.Null
                && this.TableType.Equals(other.TableType);
        }

        /// <inheritdoc/>
        public virtual int CompareTo(IDbTableTypeKey? other)
        {
            if (other is IDbTableTypeKey value)
            { return string.Compare(this.ToString(), value.ToString(), true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDbTableTypeKey value) { return CompareTo(new DbTableTypeKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbTableTypeKey value && this.Equals(new DbTableTypeKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbTableTypeKey left, DbTableTypeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbTableTypeKey left, DbTableTypeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbTableTypeKey left, DbTableTypeKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbTableTypeKey left, DbTableTypeKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbTableTypeKey left, DbTableTypeKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbTableTypeKey left, DbTableTypeKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return TableType.GetHashCode(); }
        #endregion

        #region IParsable
        /// <summary>
        /// This is the list that translates the DbTableType Enum to what the Database uses as TableType Name.
        /// </summary>
        static Dictionary<DbTableType, String> parseName = new Dictionary<DbTableType, String>()
        {
            { DbTableType.Table,         "BASE TABLE"},
            { DbTableType.TemporalTable, "TEMPORAL TABLE"},
            { DbTableType.HistoryTable,  "HISTORY TABLE"},
            { DbTableType.View,          "VIEW"},
        };

        /// <inheritdoc/>
        public static DbTableTypeKey Parse(String source, IFormatProvider? provider)
        {
            if (String.IsNullOrEmpty(source))
            { throw new ArgumentNullException(nameof(source)); }

            if (DbTableTypeKey.TryParse(source, provider, out DbTableTypeKey? result))
            { return result; }
            else
            {
                Exception ex = new FormatException();
                ex.Data.Add(nameof(source), source);
                throw ex;
            }
        }

        /// <inheritdoc/>
        public static bool TryParse([NotNullWhen(true)] String? source, IFormatProvider? provider, [MaybeNullWhen(false)] out DbTableTypeKey result)
        {
            if (parseName.FirstOrDefault(w => w.Value.Equals(source, KeyExtension.CompareString))
                is KeyValuePair<DbTableType, String> dbItem
                && dbItem.Key != DbTableType.Null)
            { result = new DbTableTypeKey() { TableType = dbItem.Key }; return true; }
            else { result = null; return false; }
        }

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="result">When this method returns, contains the result of successfully parsing</param>
        /// <returns>true if s was successfully parsed; otherwise, false.</returns>
        public static bool TryParse([NotNullWhen(true)] String? source, [MaybeNullWhen(false)] out DbTableTypeKey result)
        {
            if (parseName.FirstOrDefault(w => w.Value.Equals(source, KeyExtension.CompareString))
                is KeyValuePair<DbTableType, String> dbItem
                && dbItem.Key != DbTableType.Null)
            { result = new DbTableTypeKey() { TableType = dbItem.Key }; return true; }
            else { result = null; return false; }
        }
        #endregion

        /// <summary>
        /// Returns an IEnumerable of DbTableTypeKey for each DbTableType.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<DbTableTypeKey> Items()
        { return Enum.GetValues(typeof(DbTableType)).Cast<DbTableType>().Select(s => new DbTableTypeKey() { TableType = s}); }

        /// <summary>
        /// Returns the TableType Name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (parseName.ContainsKey(TableType)) { return parseName[TableType]; }
            else { return TableType.ToString(); }
        }
    }
}
