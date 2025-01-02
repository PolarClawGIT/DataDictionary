using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Domain Attribute Property Key
    /// </summary>
    public interface IAttributePropertyKey : IAttributeKey, IPropertyKey
    { }

    /// <summary>
    /// Implantation for the Domain Attribute Property Key
    /// </summary>
    public class AttributePropertyKey : IAttributePropertyKey,
        IKeyEquality<IAttributePropertyKey>, IKeyEquality<AttributePropertyKey>
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public Guid? PropertyId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Attribute Property Key
        /// </summary>
        /// <param name="source"></param>
        public AttributePropertyKey(IAttributePropertyKey source)
        {
            AttributeId = source.AttributeId;
            PropertyId = source.PropertyId;
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(AttributePropertyKey? other)
        {
            return other is AttributePropertyKey key &&
                   EqualityComparer<Guid?>.Default.Equals(AttributeId, key.AttributeId) &&
                   EqualityComparer<Guid?>.Default.Equals(PropertyId, key.PropertyId);
        }

        /// <inheritdoc/>
        public bool Equals(IAttributePropertyKey? other)
        { return other is IAttributePropertyKey value && Equals(new AttributePropertyKey(value)); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IAttributePropertyKey value && Equals(new AttributePropertyKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(AttributePropertyKey left, AttributePropertyKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(AttributePropertyKey left, AttributePropertyKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(AttributeId, PropertyId); }
        #endregion
    }
}
