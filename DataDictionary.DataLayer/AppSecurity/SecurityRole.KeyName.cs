using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the Role Name of a Security Role.
    /// </summary>
    public interface ISecurityRoleKeyName : IKey
    {
        /// <summary>
        /// Role Name of the Security Principle
        /// </summary>
        String? RoleName { get; }
    }

    /// <summary>
    /// Implementation of the unique Name of a Security Role.
    /// </summary>
    public class SecurityRoleKeyName : ISecurityRoleKeyName,
        IKeyComparable<ISecurityRoleKeyName>, IKeyComparable<SecurityRoleKeyName>
    {
        /// <inheritdoc/>
        public String RoleName { get; init; } = String.Empty;

        /// <summary>
        /// Constructor for the Security Principle Name Key.
        /// </summary>
        /// <param name="source"></param>
        public SecurityRoleKeyName(ISecurityRoleKeyName source) : base()
        {
            if (source.RoleName is string) { RoleName = source.RoleName; }
            else { RoleName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(SecurityRoleKeyName? other)
        {
            return
                other is SecurityRoleKeyName &&
                !string.IsNullOrEmpty(RoleName) &&
                !string.IsNullOrEmpty(other.RoleName) &&
                RoleName.Equals(other.RoleName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(ISecurityRoleKeyName? other)
        { return other is ISecurityRoleKeyName value && Equals(new SecurityRoleKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ISecurityRoleKeyName value && Equals(new SecurityRoleKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(SecurityRoleKeyName? other)
        {
            if (other is SecurityRoleKeyName value)
            { return string.Compare(RoleName, value.RoleName, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(ISecurityRoleKeyName? other)
        { if (other is ISecurityRoleKeyName value) { return CompareTo(new SecurityRoleKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is ISecurityRoleKeyName value) { return CompareTo(new SecurityRoleKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(SecurityRoleKeyName left, SecurityRoleKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SecurityRoleKeyName left, SecurityRoleKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(SecurityRoleKeyName left, SecurityRoleKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(SecurityRoleKeyName left, SecurityRoleKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(SecurityRoleKeyName left, SecurityRoleKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(SecurityRoleKeyName left, SecurityRoleKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return RoleName.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (RoleName is String) { return RoleName; }
            else { return String.Empty; }
        }
    }
}
