using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Catalog Key.
    /// </summary>
    public interface ICatalogKey : IKey
    {
        /// <summary>
        /// Application ID for the Catalog.
        /// </summary>
        Guid? CatalogId { get; }
    }

    /// <summary>
    /// Implementation for the Database Catalog Key.
    /// </summary>
    public class CatalogKey : ICatalogKey,
        IKeyEquality<ICatalogKey>, IKeyEquality<CatalogKey>
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return CatalogId.HasValue && CatalogId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Catalog Key. Empty Value.
        /// </summary>
        public CatalogKey() : base()
        { }

        /// <summary>
        /// Constructor for the Catalog Key.
        /// </summary>
        /// <param name="source"></param>
        public CatalogKey(ICatalogKey source) : base()
        {
            if (source.CatalogId is Guid value) { CatalogId = value; }
            else { CatalogId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(CatalogKey? other)
        {
            return other is CatalogKey key
                && CatalogId.HasValue && CatalogId != Guid.Empty
                && key.CatalogId.HasValue && key.CatalogId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(CatalogId, other.CatalogId);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(ICatalogKey? other)
        { return other is ICatalogKey value && Equals(new CatalogKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is ICatalogKey value && Equals(new CatalogKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(CatalogKey left, CatalogKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(CatalogKey left, CatalogKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(CatalogId); }


        #endregion
    }
}
