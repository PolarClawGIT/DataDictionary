using DataDictionary.DataLayer.ApplicationData.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for the Domain Entity Property Key
    /// </summary>
    public interface IDomainEntityPropertyKey : IDomainEntityKey, IPropertyKey
    { }

    /// <summary>
    /// Implantation for the Domain Entity Property Key
    /// </summary>
    public class DomainEntityPropertyKey : IDomainEntityPropertyKey, IKeyEquality<IDomainEntityPropertyKey>
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
        public bool Equals(IDomainEntityPropertyKey? other)
        {
            return other is IDomainEntityPropertyKey key &&
                   EqualityComparer<Guid?>.Default.Equals(EntityId, key.EntityId) &&
                   EqualityComparer<Guid?>.Default.Equals(PropertyId, key.PropertyId);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainEntityPropertyKey value && Equals(new DomainEntityPropertyKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DomainEntityPropertyKey left, DomainEntityPropertyKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainEntityPropertyKey left, DomainEntityPropertyKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(EntityId, PropertyId); }
        #endregion
    }
}
