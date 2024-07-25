using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.Resource;

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
    public class DbCatalogKeyName : IDbCatalogKeyName,
        IKeyComparable<IDbCatalogKeyName>, IKeyComparable<DbCatalogKeyName>
    {
        /// <inheritdoc/>
        public String DatabaseName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for a blank Catalog Key
        /// </summary>
        protected internal DbCatalogKeyName() : base() { }

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
        public Boolean Equals(DbCatalogKeyName? other)
        {
            return
                other is DbCatalogKeyName &&
                !string.IsNullOrEmpty(DatabaseName) &&
                !string.IsNullOrEmpty(other.DatabaseName) &&
                DatabaseName.Equals(other.DatabaseName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IDbCatalogKeyName? other)
        { return other is IDbCatalogKeyName value && Equals(new DbCatalogKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDbCatalogKeyName value && Equals(new DbCatalogKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(DbCatalogKeyName? other)
        {
            if (other is DbCatalogKeyName value)
            { return string.Compare(DatabaseName, value.DatabaseName, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IDbCatalogKeyName? other)
        { if (other is IDbCatalogKeyName value) { return CompareTo(new DbCatalogKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IDbCatalogKeyName value) { return CompareTo(new DbCatalogKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(DbCatalogKeyName left, DbCatalogKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DbCatalogKeyName left, DbCatalogKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(DbCatalogKeyName left, DbCatalogKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(DbCatalogKeyName left, DbCatalogKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(DbCatalogKeyName left, DbCatalogKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(DbCatalogKeyName left, DbCatalogKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return DatabaseName.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return this.ToAliasName(); }




    }
}
