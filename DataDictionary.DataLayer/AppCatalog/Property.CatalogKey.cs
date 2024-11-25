using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for Level0 MS Extended Property Type.
    /// </summary>
    public interface IPropertyCatalogKey : IDbLevelKey, IDbLevelCatalogType
    { }

    /// <summary>
    /// Implementation of the Key for Level0 MS Extended Property Type.
    /// </summary>
    /// <remarks>
    /// Currently not used.
    /// </remarks>
    public class PropertyCatalogKey : IPropertyCatalogKey, IKeyEquality<IPropertyCatalogKey>
    {
        /// <inheritdoc/>
        public DbLevelCatalogType CatalogScope { get; init; } = DbLevelCatalogType.Null;

        /// <summary>
        /// Constructor for a Catalog Scope.
        /// </summary>
        internal protected PropertyCatalogKey() : base() { }

        /// <summary>
        /// Constructor for a Catalog Scope.
        /// </summary>
        public PropertyCatalogKey(IPropertyCatalogKey source) : this()
        { CatalogScope = source.CatalogScope; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IPropertyCatalogKey? other)
        {
            return
                other is IPropertyCatalogKey
                && CatalogScope != DbLevelCatalogType.Null
                && other.CatalogScope != DbLevelCatalogType.Null
                && CatalogScope == other.CatalogScope;
        }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IPropertyCatalogKey value && Equals(new PropertyCatalogKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(PropertyCatalogKey left, PropertyCatalogKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(PropertyCatalogKey left, PropertyCatalogKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(CatalogScope); }
        #endregion
    }
}
