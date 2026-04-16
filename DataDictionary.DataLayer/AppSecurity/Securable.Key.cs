// Ignore Spelling: Securable

using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the Securable (Security Object) Key.
    /// </summary>
    public interface ISecurableKey : IKey
    {
        /// <summary>
        /// Application ID for the Securable (Securable Object).
        /// </summary>
        Guid? SecurableId { get; }
    }

    /// <summary>
    /// Implementation for the Securable (Security Object) Key.
    /// </summary>
    public class SecurableKey : ISecurableKey,
        IKeyEquality<ISecurableKey>, IKeyEquality<SecurableKey>
    {
        /// <inheritdoc/>
        public Guid? SecurableId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return SecurableId.HasValue && SecurableId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Security Object Key.
        /// </summary>
        protected SecurableKey()
        { SecurableId = Guid.Empty; }

        /// <summary>
        /// Constructor for the Security Object Key.
        /// </summary>
        /// <param name="source"></param>
        public SecurableKey(ISecurableKey source) : base()
        {
            if (source.SecurableId is Guid value) { SecurableId = value; }
            else { SecurableId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(SecurableKey? other)
        {
            return other is SecurableKey key
                && SecurableId.HasValue && SecurableId != Guid.Empty
                && key.SecurableId.HasValue && key.SecurableId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(SecurableId, other.SecurableId);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(ISecurableKey? other)
        { return other is ISecurableKey value && Equals(new SecurableKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is ISecurableKey value && Equals(new SecurableKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(SecurableKey left, SecurableKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SecurableKey left, SecurableKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(SecurableId); }
        #endregion
    }
}
