using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Alias
{
    /// <summary>
    /// Interface for the Domain Alias Parent Key
    /// </summary>
    public interface IDomainAliasParentKey: IKey
    {
        /// <summary>
        /// Application ID for the Domain Alias Parent.
        /// </summary>
        Guid? AliasParentId { get; }
    }

    /// <summary>
    /// Implementation for the Domain Alias Parent Key
    /// </summary>
    public class DomainAliasParentKey : IDomainAliasParentKey, IKeyEquality<IDomainAliasKey>, IKeyEquality<IDomainAliasParentKey>
    {
        /// <inheritdoc/>
        public Guid? AliasParentId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainAliasParentKey(IDomainAliasParentKey source) : base()
        {
            if (source.AliasParentId is Guid) { AliasParentId = source.AliasParentId; }
            else { AliasParentId = Guid.Empty; }
        }

        /// <summary>
        /// Constructor for the Domain Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainAliasParentKey(IDomainAliasKey source) : base()
        {
            if (source.AliasId is Guid) { AliasParentId = source.AliasId; }
            else { AliasParentId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDomainAliasParentKey? other)
        { return other is IDomainAliasParentKey key && EqualityComparer<Guid?>.Default.Equals(AliasParentId, key.AliasParentId); }

        /// <inheritdoc/>
        public bool Equals(IDomainAliasKey? other)
        { return other is IDomainAliasKey key && EqualityComparer<Guid?>.Default.Equals(AliasParentId, key.AliasId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainAliasParentKey value && Equals(new DomainAliasParentKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DomainAliasParentKey left, DomainAliasParentKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainAliasParentKey left, DomainAliasParentKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(AliasParentId); }
        #endregion
    }
}
