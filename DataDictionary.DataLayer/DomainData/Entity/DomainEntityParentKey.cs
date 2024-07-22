using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for the Domain Entity Parent Key.
    /// </summary>
    [Obsolete("Not used", true)]
    public interface IDomainEntityParentKey : IKey
    {
        /// <summary>
        /// Application ID for the Domain Parent Entity.
        /// </summary>
        Guid? ParentEntityId { get; }
    }

    /// <summary>
    /// Implementation for the Domain Entity Parent Key.
    /// </summary>
    [Obsolete("Not used", true)]
    public class DomainEntityParentKey : IDomainEntityParentKey, IKeyEquality<IDomainEntityParentKey>, IKeyEquality<IDomainEntityKey>
    {
        /// <inheritdoc/>
        public Guid? ParentEntityId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Entity Parent Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainEntityParentKey(IDomainEntityParentKey source) : base()
        {
            if (source.ParentEntityId is Guid) { ParentEntityId = source.ParentEntityId; }
            else { ParentEntityId = Guid.Empty; }
        }

        /// <summary>
        /// Constructor for the Domain Entity Parent Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainEntityParentKey(IDomainEntityKey source) : base()
        {
            if (source.EntityId is Guid) { ParentEntityId = source.EntityId; }
            else { ParentEntityId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDomainEntityParentKey? other)
        { return other is IDomainEntityParentKey key && EqualityComparer<Guid?>.Default.Equals(ParentEntityId, key.ParentEntityId); }

        /// <inheritdoc/>
        public bool Equals(IDomainEntityKey? other)
        { return other is IDomainEntityKey key && EqualityComparer<Guid?>.Default.Equals(ParentEntityId, key.EntityId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is IDomainEntityParentKey value && Equals(new DomainEntityParentKey(value))
                || obj is IDomainEntityKey keyValue && Equals(new DomainEntityParentKey(keyValue));
        }

        /// <inheritdoc/>
        public static bool operator ==(DomainEntityParentKey left, DomainEntityParentKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainEntityParentKey left, DomainEntityParentKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(ParentEntityId); }
        #endregion
    }
}