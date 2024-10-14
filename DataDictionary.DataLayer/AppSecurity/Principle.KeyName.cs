using DataDictionary.Resource;
using System.Security.Principal;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the unique Login of a Security Principle.
    /// </summary>
    public interface IPrincipleKeyName : IKey
    {
        /// <summary>
        /// Login of the Security Principle
        /// </summary>
        String? PrincipleLogin { get; }
    }

    /// <summary>
    /// Implementation of the unique Login of a Security Principle.
    /// </summary>
    public class PrincipleKeyName : IPrincipleKeyName,
        IKeyComparable<IPrincipleKeyName>, IKeyComparable<PrincipleKeyName>
    {
        /// <inheritdoc/>
        public String PrincipleLogin { get; init; } = String.Empty;

        /// <summary>
        /// Constructor for the Security Principle Name Key.
        /// </summary>
        /// <param name="source"></param>
        public PrincipleKeyName(IPrincipleKeyName source) : base()
        {
            if (source.PrincipleLogin is string) { PrincipleLogin = source.PrincipleLogin; }
            else { PrincipleLogin = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Security Principle Name Key, given the identity
        /// </summary>
        /// <param name="identity"></param>
        /// <example>var key = new SecurityPrincipleKeyName(WindowsIdentity.GetCurrent());</example>
        public PrincipleKeyName(IIdentity identity) : base()
        {
            if (identity.Name is string) { PrincipleLogin = identity.Name; }
            else { PrincipleLogin = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(PrincipleKeyName? other)
        {
            return
                other is PrincipleKeyName &&
                !string.IsNullOrEmpty(PrincipleLogin) &&
                !string.IsNullOrEmpty(other.PrincipleLogin) &&
                PrincipleLogin.Equals(other.PrincipleLogin, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IPrincipleKeyName? other)
        { return other is IPrincipleKeyName value && Equals(new PrincipleKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IPrincipleKeyName value && Equals(new PrincipleKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(PrincipleKeyName? other)
        {
            if (other is PrincipleKeyName value)
            { return string.Compare(PrincipleLogin, value.PrincipleLogin, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IPrincipleKeyName? other)
        { if (other is IPrincipleKeyName value) { return CompareTo(new PrincipleKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IPrincipleKeyName value) { return CompareTo(new PrincipleKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(PrincipleKeyName left, PrincipleKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(PrincipleKeyName left, PrincipleKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(PrincipleKeyName left, PrincipleKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(PrincipleKeyName left, PrincipleKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(PrincipleKeyName left, PrincipleKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(PrincipleKeyName left, PrincipleKeyName right)
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
