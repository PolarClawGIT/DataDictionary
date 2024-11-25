using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.AppCatalog
{


    /// <summary>
    /// Interface for Level1 MS Extended Property Type.
    /// </summary>
    public interface IPropertyObjectKey : IPropertyCatalogKey, IDbLevelObjectType
    { }

    /// <summary>
    /// Implementation of the Key for Level1 MS Extended Property Type.
    /// </summary>
    /// <remarks>
    /// Currently not used.
    /// </remarks>
    public class PropertyObjectKey : PropertyCatalogKey, IPropertyObjectKey, IKeyEquality<IPropertyObjectKey>
    {
        /// <inheritdoc/>
        public DbLevelObjectType ObjectScope { get; init; } = DbLevelObjectType.Null;

        /// <summary>
        /// Constructor for a Object Scope.
        /// </summary>
        internal protected PropertyObjectKey() : base() { }

        /// <summary>
        /// Constructor for a Object Scope.
        /// </summary>
        public PropertyObjectKey(IPropertyObjectKey source) : base(source)
        { ObjectScope = source.ObjectScope; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IPropertyObjectKey? other)
        {
            return
                other is IPropertyObjectKey
                && new PropertyCatalogKey(this).Equals(other)
                && ObjectScope != DbLevelObjectType.Null
                && other.ObjectScope != DbLevelObjectType.Null
                && ObjectScope == other.ObjectScope;
        }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IPropertyObjectKey value && Equals(new PropertyObjectKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(PropertyObjectKey left, PropertyObjectKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(PropertyObjectKey left, PropertyObjectKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ObjectScope); }
        #endregion
    }
}
