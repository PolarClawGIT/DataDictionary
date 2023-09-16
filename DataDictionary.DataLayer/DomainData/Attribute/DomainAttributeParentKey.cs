using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for the Domain Attribute Parent Key.
    /// </summary>
    public interface IDomainAttributeParentKey: IKey
    {
        /// <summary>
        /// Application ID for the Domain Parent Attribute.
        /// </summary>
        Guid? ParentAttributeId { get; }
    }

    /// <summary>
    /// Implementation for the Domain Attribute Parent Key.
    /// </summary>
    public class DomainAttributeParentKey : IDomainAttributeParentKey, IKeyEquality<IDomainAttributeParentKey>, IKeyEquality<IDomainAttributeKey>
    {
        /// <inheritdoc/>
        public Guid? ParentAttributeId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Attribute Parent Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainAttributeParentKey(IDomainAttributeParentKey source) : base()
        {
            if (source.ParentAttributeId is Guid) { ParentAttributeId = source.ParentAttributeId; }
            else { ParentAttributeId = Guid.Empty; }
        }

        /// <summary>
        /// Constructor for the Domain Attribute Parent Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainAttributeParentKey(IDomainAttributeKey source) : base()
        {
            if (source.AttributeId is Guid) { ParentAttributeId = source.AttributeId; }
            else { ParentAttributeId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDomainAttributeParentKey? other)
        { return other is IDomainAttributeParentKey key && EqualityComparer<Guid?>.Default.Equals(ParentAttributeId, key.ParentAttributeId); }

        /// <inheritdoc/>
        public bool Equals(IDomainAttributeKey? other)
        { return other is IDomainAttributeKey key && EqualityComparer<Guid?>.Default.Equals(ParentAttributeId, key.AttributeId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is IDomainAttributeParentKey value && Equals(new DomainAttributeParentKey(value))
                || obj is IDomainAttributeKey keyValue && Equals(new DomainAttributeParentKey(keyValue));
        }

        /// <inheritdoc/>
        public static bool operator ==(DomainAttributeParentKey left, DomainAttributeParentKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainAttributeParentKey left, DomainAttributeParentKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(ParentAttributeId); }
        #endregion
    }
}
