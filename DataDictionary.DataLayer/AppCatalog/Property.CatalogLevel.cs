using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for Level0 MS Extended Property Type.
    /// </summary>
    public interface IPropertyCatalogLevel : IDbLevelKey, IDbLevelCatalogType
    { }

    /// <summary>
    /// Implementation of the Key for Level0 MS Extended Property Type.
    /// </summary>
    /// <remarks>
    /// Currently not used.
    /// </remarks>
    public class PropertyCatalogLevel : IPropertyCatalogLevel, IKeyEquality<IPropertyCatalogLevel>
    {
        /// <inheritdoc/>
        public DbLevelCatalogType CatalogScope { get; init; } = DbLevelCatalogType.Null;

        /// <summary>
        /// Constructor for a Catalog Scope.
        /// </summary>
        internal protected PropertyCatalogLevel() : base() { }

        /// <summary>
        /// Constructor for a Catalog Scope.
        /// </summary>
        public PropertyCatalogLevel(IPropertyCatalogLevel source) : this()
        { CatalogScope = source.CatalogScope; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IPropertyCatalogLevel? other)
        {
            return
                other is IPropertyCatalogLevel
                && CatalogScope != DbLevelCatalogType.Null
                && other.CatalogScope != DbLevelCatalogType.Null
                && CatalogScope == other.CatalogScope;
        }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IPropertyCatalogLevel value && Equals(new PropertyCatalogLevel(value)); }

        /// <inheritdoc/>
        public static bool operator ==(PropertyCatalogLevel left, PropertyCatalogLevel right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(PropertyCatalogLevel left, PropertyCatalogLevel right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(CatalogScope); }
        #endregion
    }
}
