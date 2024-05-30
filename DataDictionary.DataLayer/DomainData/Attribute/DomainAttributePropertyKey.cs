﻿using DataDictionary.DataLayer.DomainData.Property;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for the Domain Attribute Property Key
    /// </summary>
    public interface IDomainAttributePropertyKey : IDomainAttributeKey, IDomainPropertyKey
    { }

    /// <summary>
    /// Implantation for the Domain Attribute Property Key
    /// </summary>
    public class DomainAttributePropertyKey : IDomainAttributePropertyKey, IKeyEquality<IDomainAttributePropertyKey>
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public Guid? PropertyId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Attribute Property Key
        /// </summary>
        /// <param name="source"></param>
        public DomainAttributePropertyKey(IDomainAttributePropertyKey source)
        {
            AttributeId = source.AttributeId;
            PropertyId = source.PropertyId;
        }

        #region IEquatable
        /// <inheritdoc/>
        public bool Equals(IDomainAttributePropertyKey? other)
        {
            return other is IDomainAttributePropertyKey key &&
                   EqualityComparer<Guid?>.Default.Equals(AttributeId, key.AttributeId) &&
                   EqualityComparer<Guid?>.Default.Equals(PropertyId, key.PropertyId);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {   return obj is IDomainAttributePropertyKey value && Equals(new DomainAttributePropertyKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DomainAttributePropertyKey left, DomainAttributePropertyKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainAttributePropertyKey left, DomainAttributePropertyKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(AttributeId, PropertyId); }
        #endregion
    }
}
