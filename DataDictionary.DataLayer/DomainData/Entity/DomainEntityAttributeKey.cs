using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for the DomainEntityAttribute Key 
    /// </summary>
    public interface IDomainEntityAttributeKey : IDomainEntityKey, IDomainAttributeKey
    { }

    /// <summary>
    /// Implementation of the DomainEntityAttribute Key 
    /// </summary>
    public class DomainEntityAttributeKey : IDomainEntityAttributeKey, IKeyEquality<IDomainEntityAttributeKey>
    {
        /// <inheritdoc/>
        public Guid? EntityId { get; init; }

        /// <inheritdoc/>
        public Guid? AttributeId { get; init; }

        /// <summary>
        /// Constructor for the DomainEntityAttribute Key 
        /// </summary>
        /// <param name="source"></param>
        public DomainEntityAttributeKey(IDomainEntityAttributeKey source) : base()
        {
            if (source.EntityId is Guid) { EntityId = source.EntityId; }
            else { EntityId = Guid.Empty; }

            if (source.AttributeId is Guid) { AttributeId = source.AttributeId; }
            else { AttributeId = Guid.Empty; }
        }

        /// <summary>
        /// Constructor for the DomainEntityAttribute Key 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="attribute"></param>
        public DomainEntityAttributeKey(IDomainEntityKey entity, IDomainAttributeKey attribute) : base()
        {
            if (entity.EntityId is Guid) { EntityId = entity.EntityId; }
            else { EntityId = Guid.Empty; }

            if (attribute.AttributeId is Guid) { AttributeId = attribute.AttributeId; }
            else { AttributeId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDomainEntityAttributeKey? other)
        { return other is IDomainEntityAttributeKey key
                && EqualityComparer<Guid?>.Default.Equals(EntityId, key.EntityId)
                && EqualityComparer<Guid?>.Default.Equals(AttributeId, key.AttributeId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainEntityAttributeKey value && Equals(new DomainEntityAttributeKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DomainEntityAttributeKey left, DomainEntityAttributeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainEntityAttributeKey left, DomainEntityAttributeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(EntityId, AttributeId); }
        #endregion
    }
}
