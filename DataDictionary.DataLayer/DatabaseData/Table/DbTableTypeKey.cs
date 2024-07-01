using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.DataLayer.DatabaseData.Table
{
    /// <summary>
    /// Interface for Database TableType Key.
    /// </summary>
    public interface IDbTableTypeKey : IKey
    {
        /// <summary>
        /// Type of Table (Table, Temporal Table, Historic Table, View)
        /// </summary>
        DbTableType TableType { get; }
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
            { DbTableType.Table,         "Table"},
            { DbTableType.TemporalTable, "Temporal Table"},
            { DbTableType.HistoryTable,  "History Table"},
            { DbTableType.View,          "View"},
        };

        /// <inheritdoc/>
        public static DbTableTypeKey Parse(String source, IFormatProvider? provider = null)
        {
            if (String.IsNullOrEmpty(source))
            { throw new ArgumentNullException(nameof(source)); }

            if (DbTableTypeKey.TryParse(source, provider, out DbTableTypeKey? result))
            { return result; }
            else
            { return DbTableType.Null; }
        }

        /// <inheritdoc/>
        public static bool TryParse([NotNullWhen(true)] String? source, IFormatProvider? provider, [MaybeNullWhen(false)] out DbTableTypeKey result)
        { return TryParse(source, out result); }

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
        { return Enum.GetValues(typeof(DbTableType)).Cast<DbTableType>().Select(s => new DbTableTypeKey() { TableType = s }); }

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
