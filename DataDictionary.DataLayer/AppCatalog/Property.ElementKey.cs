using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.AppCatalog
{

    /// <summary>
    /// Interface for Level2 MS Extended Property Type.
    /// </summary>
    public interface IPropertyElementKey : IPropertyObjectKey, IDbLevelElementType
    { }

    /// <summary>
    /// Implementation of the Key for Level2 MS Extended Property Type.
    /// </summary>
    /// <remarks>
    /// Currently not used.
    /// </remarks>
    public class PropertyElementKey : PropertyObjectKey, IPropertyElementKey, IKeyEquality<IPropertyElementKey>
    {
        /// <inheritdoc/>
        public DbLevelElementType ElementScope { get; init; } = DbLevelElementType.Null;

        /// <summary>
        /// Constructor for a Element Scope.
        /// </summary>
        internal protected PropertyElementKey() : base() { }

        /// <summary>
        /// Constructor for a Element Scope.
        /// </summary>
        public PropertyElementKey(IPropertyElementKey source) : base(source)
        { ElementScope = source.ElementScope; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IPropertyElementKey? other)
        {
            return
                other is IPropertyObjectKey
                && new PropertyObjectKey(this).Equals(other)
                && ObjectScope != DbLevelObjectType.Null
                && other.ObjectScope != DbLevelObjectType.Null
                && ObjectScope == other.ObjectScope;
        }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IPropertyElementKey value && Equals(new PropertyElementKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(PropertyElementKey left, PropertyElementKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(PropertyElementKey left, PropertyElementKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ElementScope); }
        #endregion

    }
}
