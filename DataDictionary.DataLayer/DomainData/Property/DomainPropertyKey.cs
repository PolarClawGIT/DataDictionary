using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Property
{
    /// <summary>
    /// Interface for the Domain Property Key
    /// </summary>
    public interface IDomainPropertyKey : IKey
    {
        /// <summary>
        /// Application ID for the Domain Property.
        /// </summary>
        Guid? PropertyId { get; }
    }

    /// <summary>
    /// Implementation for the Domain Property Key
    /// </summary>
    public class DomainPropertyKey : IDomainPropertyKey, IKeyEquality<IDomainPropertyKey>
    {
        /// <inheritdoc/>
        public Guid? PropertyId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Property Key
        /// </summary>
        /// <param name="source"></param>
        public DomainPropertyKey(IDomainPropertyKey source) : base()
        {
            if (source.PropertyId is Guid) { PropertyId = source.PropertyId; }
            else { PropertyId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDomainPropertyKey? other)
        { return other is IDomainPropertyKey key && EqualityComparer<Guid?>.Default.Equals(PropertyId, key.PropertyId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainPropertyKey value && Equals(new DomainPropertyKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DomainPropertyKey left, DomainPropertyKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainPropertyKey left, DomainPropertyKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(PropertyId); }
        #endregion
    }
}
