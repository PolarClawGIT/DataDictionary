using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for the Domain Attribute Key
    /// </summary>
    public interface IDomainAttributeKey : IKey
    {
        /// <summary>
        /// Application ID for the Domain Attribute.
        /// </summary>
        Guid? AttributeId { get; }
    }

    /// <summary>
    /// Implementation for the Domain Attribute Key
    /// </summary>
    public class DomainAttributeKey : IDomainAttributeKey, IKeyEquality<IDomainAttributeKey>
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Attribute Key
        /// </summary>
        /// <param name="source"></param>
        public DomainAttributeKey(IDomainAttributeKey source) : base()
        {
            if (source.AttributeId is Guid) { AttributeId = source.AttributeId; }
            else { AttributeId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDomainAttributeKey? other)
        { return other is IDomainAttributeKey key && EqualityComparer<Guid?>.Default.Equals(AttributeId, key.AttributeId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainAttributeKey value && Equals(new DomainAttributeKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DomainAttributeKey left, DomainAttributeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainAttributeKey left, DomainAttributeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(AttributeId); }
        #endregion
    }
}
