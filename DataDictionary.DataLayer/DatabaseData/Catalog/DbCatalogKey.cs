using DataDictionary.DataLayer.DomainData;

namespace DataDictionary.DataLayer.DatabaseData.Catalog
{
    /// <summary>
    /// Interface for the Database Catalog Key.
    /// </summary>
    public interface IDbCatalogKey :IKey
    {
        /// <summary>
        /// Application ID for the Catalog.
        /// </summary>
        Guid? CatalogId { get; }
    }

    /// <summary>
    /// Implementation for the Database Catalog Key.
    /// </summary>
    public class DbCatalogKey : IDbCatalogKey, IKeyEquality<IDbCatalogKey>
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Catalog Key.
        /// </summary>
        /// <param name="source"></param>
        public DbCatalogKey(IDbCatalogKey source) : base()
        {
            if (source.CatalogId is Guid value) { CatalogId = value; }
            else { CatalogId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IDbCatalogKey? other)
        { return other is DbCatalogKey && EqualityComparer<Guid?>.Default.Equals(CatalogId, other.CatalogId); }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbCatalogKey value && Equals(new DbCatalogKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbCatalogKey left, DbCatalogKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbCatalogKey left, DbCatalogKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(CatalogId); }
        #endregion
    }
}
