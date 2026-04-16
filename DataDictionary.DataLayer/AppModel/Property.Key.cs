using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Property Key
    /// </summary>
    public interface IPropertyKey : IKey
    {
        /// <summary>
        /// Application ID for the Model Property.
        /// </summary>
        Guid? PropertyId { get; }
    }

    /// <summary>
    /// Implementation for the Model Property Key
    /// </summary>
    public class PropertyKey : IPropertyKey,
        IKeyEquality<IPropertyKey>, IKeyEquality<PropertyKey>
    {
        /// <inheritdoc/>
        public Guid? PropertyId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return PropertyId.HasValue && PropertyId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Domain Property Key
        /// </summary>
        /// <param name="source"></param>
        public PropertyKey(IPropertyKey source) : base()
        {
            if (source.PropertyId is Guid) { PropertyId = source.PropertyId; }
            else { PropertyId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(PropertyKey? other)
        {
            return other is PropertyKey key
                && PropertyId.HasValue && PropertyId != Guid.Empty
                && key.PropertyId.HasValue && key.PropertyId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(PropertyId, other.PropertyId);
        }

        /// <inheritdoc/>
        public Boolean Equals(IPropertyKey? other)
        { return other is IPropertyKey value && Equals(new PropertyKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IPropertyKey value && Equals(new PropertyKey(value)); }

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
