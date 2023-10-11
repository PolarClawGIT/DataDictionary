namespace DataDictionary.DataLayer.DatabaseData.Catalog
{
    /// <summary>
    /// Interface for the unique Name of a Catalog.
    /// </summary>
    public interface IDbCatalogKeyUnique: IKey
    {
        /// <summary>
        /// Name of the Database Name
        /// </summary>
        String? DatabaseName { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a Catalog.
    /// </summary>
    public class DbCatalogKeyUnique : IDbCatalogKeyUnique, IKeyComparable<IDbCatalogKeyUnique>
    {
        /// <inheritdoc/>
        public String DatabaseName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Catalog Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DbCatalogKeyUnique(IDbCatalogKeyUnique source) : base()
        {
            if (source.DatabaseName is string) { DatabaseName = source.DatabaseName; }
            else { DatabaseName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IDbCatalogKeyUnique? other)
        {
            return 
                other is IDbCatalogKeyUnique &&
                !string.IsNullOrEmpty(DatabaseName) &&
                !string.IsNullOrEmpty(other.DatabaseName) &&
                DatabaseName.Equals(other.DatabaseName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbCatalogKeyUnique value && Equals(new DbCatalogKeyUnique(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IDbCatalogKeyUnique? other)
        {
            if (other is DbCatalogKeyUnique value)
            { return string.Compare(DatabaseName, value.DatabaseName, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDbCatalogKeyUnique value) { return CompareTo(new DbCatalogKeyUnique(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbCatalogKeyUnique left, DbCatalogKeyUnique right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbCatalogKeyUnique left, DbCatalogKeyUnique right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbCatalogKeyUnique left, DbCatalogKeyUnique right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbCatalogKeyUnique left, DbCatalogKeyUnique right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbCatalogKeyUnique left, DbCatalogKeyUnique right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbCatalogKeyUnique left, DbCatalogKeyUnique right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(DatabaseName); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (DatabaseName is string) { return DatabaseName; }
            else { return string.Empty; }
        }
    }
}
