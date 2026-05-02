using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.AppCatalog
{

    /// <summary>
    /// Interface for Level2 MS Extended Property Type.
    /// </summary>
    public interface IPropertyElementLevel : IPropertyObjectLevel, IDbLevelElementType
    { }

    /// <summary>
    /// Implementation of the Key for Level2 MS Extended Property Type.
    /// </summary>
    /// <remarks>
    /// Currently not used.
    /// </remarks>
    public class PropertyElementLevel : PropertyObjectLevel, IPropertyElementLevel, IKeyEquality<IPropertyElementLevel>
    {
        /// <inheritdoc/>
        public DbLevelElementType ElementScope { get; init; } = DbLevelElementType.Null;

        /// <inheritdoc/>
        public override Boolean HasValue { get { return base.HasValue && ElementScope != DbLevelElementType.Null; } }

        /// <summary>
        /// Constructor for a Element Scope.
        /// </summary>
        internal protected PropertyElementLevel() : base() { }

        /// <summary>
        /// Constructor for a Element Scope.
        /// </summary>
        public PropertyElementLevel(IPropertyElementLevel source) : base(source)
        { ElementScope = source.ElementScope; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IPropertyElementLevel? other)
        {
            return
                other is IPropertyObjectLevel
                && new PropertyObjectLevel(this).Equals(other)
                && ObjectScope != DbLevelObjectType.Null
                && other.ObjectScope != DbLevelObjectType.Null
                && ObjectScope == other.ObjectScope;
        }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IPropertyElementLevel value && Equals(new PropertyElementLevel(value)); }

        /// <inheritdoc/>
        public static bool operator ==(PropertyElementLevel left, PropertyElementLevel right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(PropertyElementLevel left, PropertyElementLevel right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ElementScope); }
        #endregion

    }
}
