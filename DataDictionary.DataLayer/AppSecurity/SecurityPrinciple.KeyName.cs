using DataDictionary.Resource;
using System.Security.Principal;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the unique Login of a Security Principle.
    /// </summary>
    public interface ISecurityPrincipleKeyName : IKey
    {
        /// <summary>
        /// Login of the Security Principle
        /// </summary>
        String? PrincipleLogin { get; }
    }

    /// <summary>
    /// Implementation of the unique Login of a Security Principle.
    /// </summary>
    public class SecurityPrincipleKeyName : ISecurityPrincipleKeyName,
        IKeyComparable<ISecurityPrincipleKeyName>, IKeyComparable<SecurityPrincipleKeyName>
    {
        /// <inheritdoc/>
        public String PrincipleLogin { get; init; } = String.Empty;

        /// <summary>
        /// Constructor for the Security Principle Name Key.
        /// </summary>
        /// <param name="source"></param>
        public SecurityPrincipleKeyName(ISecurityPrincipleKeyName source) : base()
        {
            if (source.PrincipleLogin is string) { PrincipleLogin = source.PrincipleLogin; }
            else { PrincipleLogin = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Security Principle Name Key, given the identity
        /// </summary>
        /// <param name="identity"></param>
        /// <example>var key = new SecurityPrincipleKeyName(WindowsIdentity.GetCurrent());</example>
        public SecurityPrincipleKeyName(IIdentity identity) : base()
        {
            if (identity.Name is string) { PrincipleLogin = identity.Name; }
            else { PrincipleLogin = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(SecurityPrincipleKeyName? other)
        {
            return
                other is SecurityPrincipleKeyName &&
                !string.IsNullOrEmpty(PrincipleLogin) &&
                !string.IsNullOrEmpty(other.PrincipleLogin) &&
                PrincipleLogin.Equals(other.PrincipleLogin, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(ISecurityPrincipleKeyName? other)
        { return other is ISecurityPrincipleKeyName value && Equals(new SecurityPrincipleKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ISecurityPrincipleKeyName value && Equals(new SecurityPrincipleKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(SecurityPrincipleKeyName? other)
        {
            if (other is SecurityPrincipleKeyName value)
            { return string.Compare(PrincipleLogin, value.PrincipleLogin, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(ISecurityPrincipleKeyName? other)
        { if (other is ISecurityPrincipleKeyName value) { return CompareTo(new SecurityPrincipleKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is ISecurityPrincipleKeyName value) { return CompareTo(new SecurityPrincipleKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(SecurityPrincipleKeyName left, SecurityPrincipleKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SecurityPrincipleKeyName left, SecurityPrincipleKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(SecurityPrincipleKeyName left, SecurityPrincipleKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(SecurityPrincipleKeyName left, SecurityPrincipleKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(SecurityPrincipleKeyName left, SecurityPrincipleKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(SecurityPrincipleKeyName left, SecurityPrincipleKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return PrincipleLogin.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (PrincipleLogin is String) { return PrincipleLogin; }
            else { return String.Empty; }
        }
    }
}
