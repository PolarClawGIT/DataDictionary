using DataDictionary.DataLayer.DomainData.Alias;

namespace DataDictionary.DataLayer.DatabaseData.Catalog
{
    /// <summary>
    /// Interface for the unique Name of a Catalog.
    /// </summary>
    public interface IDbCatalogKeyName : IKey, IToAliasName
    {
        /// <summary>
        /// Name of the Database Name
        /// </summary>
        String? DatabaseName { get; }
    }

    /// <summary>
    /// Implementation for IDbCatalogKeyName
    /// </summary>
    public static class DbCatalogKeyNameExtension
    {
        /// <summary>
        /// Gets the Alias Name for the Database Catalog.
        /// </summary>
        /// <returns></returns>
        public static String ToAliasName(this IDbCatalogKeyName source)
        { return AliasExtension.FormatName(source.DatabaseName); }
    }

    /// <summary>
    /// Implementation for the unique Name of a Catalog.
    /// </summary>
    public class DbCatalogKeyName : IDbCatalogKeyName, IKeyComparable<IDbCatalogKeyName>
    {
        /// <inheritdoc/>
        public String DatabaseName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Catalog Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DbCatalogKeyName(IDbCatalogKeyName source) : base()
        {
            if (source.DatabaseName is string) { DatabaseName = source.DatabaseName; }
            else { DatabaseName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IDbCatalogKeyName? other)
        {
            return
                other is IDbCatalogKeyName &&
                !string.IsNullOrEmpty(DatabaseName) &&
                !string.IsNullOrEmpty(other.DatabaseName) &&
                DatabaseName.Equals(other.DatabaseName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbCatalogKeyName value && Equals(new DbCatalogKeyName(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IDbCatalogKeyName? other)
        {
            if (other is DbCatalogKeyName value)
            { return string.Compare(DatabaseName, value.DatabaseName, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDbCatalogKeyName value) { return CompareTo(new DbCatalogKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbCatalogKeyName left, DbCatalogKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbCatalogKeyName left, DbCatalogKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbCatalogKeyName left, DbCatalogKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbCatalogKeyName left, DbCatalogKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbCatalogKeyName left, DbCatalogKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbCatalogKeyName left, DbCatalogKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return DatabaseName.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return this.ToAliasName(); }
    }
}
