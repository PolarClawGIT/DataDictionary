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
    public class AttributePropertyKey : PropertyKey,
        IAttributePropertyKey, IKeyEquality<IAttributePropertyKey>, IKeyEquality<AttributePropertyKey>
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public override Boolean HasValue { get { return base.HasValue && AttributeId.HasValue && AttributeId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Domain Attribute Property Key
        /// </summary>
        /// <param name="source"></param>
        public AttributePropertyKey(IAttributePropertyKey source) : base(source)
        { AttributeId = source.AttributeId; }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(AttributePropertyKey? other)
        {
            return other is AttributePropertyKey key
                && AttributeId.HasValue && AttributeId != Guid.Empty
                && key.AttributeId.HasValue && key.AttributeId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(AttributeId, other.AttributeId)
                && base.Equals(key);
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
