using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Attribute Key
    /// </summary>
    public interface IAttributeKey : IKey
    {
        /// <summary>
        /// Application ID for the Domain Attribute.
        /// </summary>
        Guid? AttributeId { get; }
    }

    /// <summary>
    /// Implementation for the Model Attribute Key
    /// </summary>
    public class AttributeKey : IAttributeKey,
        IKeyEquality<AttributeKey>,
        IKeyEquality<IAttributeKey>
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Attribute Key
        /// </summary>
        /// <param name="source"></param>
        public AttributeKey(IAttributeKey source) : base()
        {
            if (source.AttributeId is Guid) { AttributeId = source.AttributeId; }
            else { AttributeId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(AttributeKey? other)
        { return other is AttributeKey key && EqualityComparer<Guid?>.Default.Equals(AttributeId, key.AttributeId); }

        /// <inheritdoc/>
        public bool Equals(IAttributeKey? other)
        { return other is IAttributeKey key && Equals(new AttributeKey(key)); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IAttributeKey key && Equals(new AttributeKey(key)); }

        /// <inheritdoc/>
        public static bool operator ==(AttributeKey left, AttributeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(AttributeKey left, AttributeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(AttributeId); }


        #endregion
    }
}
