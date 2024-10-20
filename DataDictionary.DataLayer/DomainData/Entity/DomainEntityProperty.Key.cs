﻿using DataDictionary.DataLayer.DomainData.Property;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for the Domain Entity Property Key
    /// </summary>
    public interface IDomainEntityPropertyKey : IDomainEntityKey, IDomainPropertyKey
    { }

    /// <summary>
    /// Implantation for the Domain Entity Property Key
    /// </summary>
    public class DomainEntityPropertyKey : IDomainEntityPropertyKey,
        IKeyEquality<IDomainEntityPropertyKey>, IKeyEquality<DomainEntityPropertyKey>
    {
        /// <inheritdoc/>
        public Guid? EntityId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public Guid? PropertyId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Entity Property Key
        /// </summary>
        /// <param name="source"></param>
        public DomainEntityPropertyKey(IDomainEntityPropertyKey source)
        {
            EntityId = source.EntityId;
            PropertyId = source.PropertyId;
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(DomainEntityPropertyKey? other)
        {
            return other is DomainEntityPropertyKey key &&
                   EqualityComparer<Guid?>.Default.Equals(EntityId, key.EntityId) &&
                   EqualityComparer<Guid?>.Default.Equals(PropertyId, key.PropertyId);
        }

        /// <inheritdoc/>
        public Boolean Equals(IDomainEntityPropertyKey? other)
        { return other is IDomainEntityPropertyKey value && Equals(new DomainEntityPropertyKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDomainEntityPropertyKey value && Equals(new DomainEntityPropertyKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(DomainEntityPropertyKey left, DomainEntityPropertyKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DomainEntityPropertyKey left, DomainEntityPropertyKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(EntityId, PropertyId); }
        #endregion
    }
}
