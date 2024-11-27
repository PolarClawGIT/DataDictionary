using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Property Key.
    /// </summary>
    public interface IPropertyKey : IKey
    {
        /// <summary>
        /// Application ID for the Property.
        /// </summary>
        Guid? PropertyId { get; }
    }

    /// <summary>
    /// Implementation for the Database Property Key.
    /// </summary>
    public class PropertyKey : IPropertyKey,
        IKeyEquality<IPropertyKey>, IKeyEquality<PropertyKey>
    {
        /// <inheritdoc/>
        public Guid? PropertyId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Property Key.
        /// </summary>
        /// <param name="source"></param>
        public PropertyKey(IPropertyKey source) : base()
        {
            if (source.PropertyId is Guid value) { PropertyId = value; }
            else { PropertyId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public virtual Boolean Equals(PropertyKey? other)
        { return other is PropertyKey && EqualityComparer<Guid?>.Default.Equals(PropertyId, other.PropertyId); }

        /// <inheritdoc/>
        public virtual Boolean Equals(IPropertyKey? other)
        { return other is IPropertyKey value && Equals(new PropertyKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IPropertyKey value && Equals(new PropertyKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(PropertyKey left, PropertyKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(PropertyKey left, PropertyKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(PropertyId); }
        #endregion
    }
}
