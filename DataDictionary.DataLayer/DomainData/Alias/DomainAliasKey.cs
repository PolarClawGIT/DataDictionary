using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Alias
{
    /// <summary>
    /// Interface for the Domain Alias Key
    /// </summary>
    public interface IDomainAliasKey : IKey
    {
        /// <summary>
        /// Application ID for the Domain Alias.
        /// </summary>
        Guid? AliasId { get; }
    }

    /// <summary>
    /// Implementation for the Domain Alias Key
    /// </summary>
    public class DomainAliasKey : IDomainAliasKey, IKeyEquality<IDomainAliasKey>, IKeyEquality<IDomainAliasParentKey>
    {
        /// <inheritdoc/>
        public Guid? AliasId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainAliasKey(IDomainAliasKey source) : base()
        {
            if (source.AliasId is Guid) { AliasId = source.AliasId; }
            else { AliasId = Guid.Empty; }
        }

        /// <summary>
        /// Constructor for the Domain Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainAliasKey(IDomainAliasParentKey source) : base()
        {
            if (source.AliasParentId is Guid) { AliasId = source.AliasParentId; }
            else { AliasId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDomainAliasKey? other)
        { return other is IDomainAliasKey key && EqualityComparer<Guid?>.Default.Equals(AliasId, key.AliasId); }

        /// <inheritdoc/>
        public bool Equals(IDomainAliasParentKey? other)
        { return other is IDomainAliasParentKey key && EqualityComparer<Guid?>.Default.Equals(AliasId, key.AliasParentId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainAliasKey value && Equals(new DomainAliasKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DomainAliasKey left, DomainAliasKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainAliasKey left, DomainAliasKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(AliasId); }
        #endregion
    }
}
