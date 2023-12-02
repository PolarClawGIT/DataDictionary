using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Domain
{
    /// <summary>
    /// Interface for the Database Domain Key.
    /// </summary>
    public interface IDbDomainKey : IKey
    {
        /// <summary>
        /// Application ID for the Domain.
        /// </summary>
        Guid? DomainId { get; }
    }

    /// <summary>
    /// Implementation for the Database Domain Key.
    /// </summary>
    public class DbDomainKey : IDbDomainKey, IKeyEquality<IDbDomainKey>
    {
        /// <inheritdoc/>
        public Guid? DomainId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Key.
        /// </summary>
        /// <param name="source"></param>
        public DbDomainKey(IDbDomainKey source) : base()
        {
            if (source.DomainId is Guid value) { DomainId = value; }
            else { DomainId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IDbDomainKey? other)
        { return other is IDbDomainKey && EqualityComparer<Guid?>.Default.Equals(DomainId, other.DomainId); }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbDomainKey value && Equals(new DbDomainKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbDomainKey left, DbDomainKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbDomainKey left, DbDomainKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(DomainId); }
        #endregion
    }
}
