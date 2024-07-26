using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.DatabaseData.ExtendedProperty
{
    /// <summary>
    /// Interface for Level0 MS Extended Property Type.
    /// </summary>
    public interface IDbLevelCatalogKey : IDbLevelKey, IDbLevelCatalogType 
    { }

    /// <summary>
    /// Implementation of the Key for Level0 MS Extended Property Type.
    /// </summary>
    /// <remarks>
    /// Currently not used.
    /// </remarks>
    public class DbLevelCatalogKey : IDbLevelCatalogKey, IKeyEquality<IDbLevelCatalogKey>
    {
        /// <inheritdoc/>
        public DbLevelCatalogType CatalogScope { get; init; } = DbLevelCatalogType.Null;

        /// <summary>
        /// Constructor for a Catalog Scope.
        /// </summary>
        internal protected DbLevelCatalogKey() : base() { }

        /// <summary>
        /// Constructor for a Catalog Scope.
        /// </summary>
        public DbLevelCatalogKey(IDbLevelCatalogKey source) : this()
        { CatalogScope = source.CatalogScope; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IDbLevelCatalogKey? other)
        {
            return
                other is IDbLevelCatalogKey
                && CatalogScope != DbLevelCatalogType.Null
                && other.CatalogScope != DbLevelCatalogType.Null
                && CatalogScope == other.CatalogScope;
        }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbLevelCatalogKey value && Equals(new DbLevelCatalogKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbLevelCatalogKey left, DbLevelCatalogKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbLevelCatalogKey left, DbLevelCatalogKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(CatalogScope); }
        #endregion
    }
}
