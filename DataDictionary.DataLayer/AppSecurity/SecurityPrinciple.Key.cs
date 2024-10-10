using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the Security Principle Key.
    /// </summary>
    public interface ISecurityPrincipleKey : IKey
    {
        /// <summary>
        /// Application ID for the Principle.
        /// </summary>
        Guid? PrincipleId { get; }
    }

    /// <summary>
    /// Implementation for the Security Principle Key.
    /// </summary>
    public class SecurityPrincipleKey : ISecurityPrincipleKey,
        IKeyEquality<ISecurityPrincipleKey>, IKeyEquality<SecurityPrincipleKey>
    {
        /// <inheritdoc/>
        public Guid? PrincipleId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Security Principle Key.
        /// </summary>
        /// <param name="source"></param>
        public SecurityPrincipleKey(ISecurityPrincipleKey source) : base()
        {
            if (source.PrincipleId is Guid value) { PrincipleId = value; }
            else { PrincipleId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(SecurityPrincipleKey? other)
        { return other is SecurityPrincipleKey && EqualityComparer<Guid?>.Default.Equals(PrincipleId, other.PrincipleId); }

        /// <inheritdoc/>
        public virtual Boolean Equals(ISecurityPrincipleKey? other)
        { return other is ISecurityPrincipleKey value && Equals(new SecurityPrincipleKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is ISecurityPrincipleKey value && Equals(new SecurityPrincipleKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(SecurityPrincipleKey left, SecurityPrincipleKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SecurityPrincipleKey left, SecurityPrincipleKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(PrincipleId); }
        #endregion
    }
}
