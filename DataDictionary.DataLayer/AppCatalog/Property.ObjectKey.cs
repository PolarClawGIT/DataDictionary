using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.AppCatalog
{


    /// <summary>
    /// Interface for Level1 MS Extended Property Type.
    /// </summary>
    public interface IPropertyObjectLevel : IPropertyCatalogLevel, IDbLevelObjectType
    { }

    /// <summary>
    /// Implementation of the Key for Level1 MS Extended Property Type.
    /// </summary>
    /// <remarks>
    /// Currently not used.
    /// </remarks>
    public class PropertyObjectLevel : PropertyCatalogLevel, IPropertyObjectLevel, IKeyEquality<IPropertyObjectLevel>
    {
        /// <inheritdoc/>
        public DbLevelObjectType ObjectScope { get; init; } = DbLevelObjectType.Null;

        /// <summary>
        /// Constructor for a Object Scope.
        /// </summary>
        internal protected PropertyObjectLevel() : base() { }

        /// <summary>
        /// Constructor for a Object Scope.
        /// </summary>
        public PropertyObjectLevel(IPropertyObjectLevel source) : base(source)
        { ObjectScope = source.ObjectScope; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IPropertyObjectLevel? other)
        {
            return
                other is IPropertyObjectLevel
                && new PropertyCatalogLevel(this).Equals(other)
                && ObjectScope != DbLevelObjectType.Null
                && other.ObjectScope != DbLevelObjectType.Null
                && ObjectScope == other.ObjectScope;
        }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IPropertyObjectLevel value && Equals(new PropertyObjectLevel(value)); }

        /// <inheritdoc/>
        public static bool operator ==(PropertyObjectLevel left, PropertyObjectLevel right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(PropertyObjectLevel left, PropertyObjectLevel right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ObjectScope); }
        #endregion
    }
}
