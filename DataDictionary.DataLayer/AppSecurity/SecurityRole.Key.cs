using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppSecurity
{

    /// <summary>
    /// Interface for the Security Role Key.
    /// </summary>
    public interface ISecurityRoleKey : IKey
    {
        /// <summary>
        /// Application ID for the Role.
        /// </summary>
        Guid? RoleId { get; }
    }

    /// <summary>
    /// Implementation of the Security Role Key.
    /// </summary>
    public class SecurityRoleKey : ISecurityRoleKey,
        IKeyEquality<ISecurityRoleKey>, IKeyEquality<SecurityRoleKey>
    {
        /// <inheritdoc/>
        public Guid? RoleId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Security Role Key.
        /// </summary>
        /// <param name="source"></param>
        public SecurityRoleKey(ISecurityRoleKey source) : base()
        {
            if (source.RoleId is Guid value) { RoleId = value; }
            else { RoleId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(SecurityRoleKey? other)
        { return other is SecurityRoleKey && EqualityComparer<Guid?>.Default.Equals(RoleId, other.RoleId); }

        /// <inheritdoc/>
        public virtual Boolean Equals(ISecurityRoleKey? other)
        { return other is ISecurityRoleKey value && Equals(new SecurityRoleKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is ISecurityRoleKey value && Equals(new SecurityRoleKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(SecurityRoleKey left, SecurityRoleKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SecurityRoleKey left, SecurityRoleKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(RoleId); }
        #endregion
    }
}
