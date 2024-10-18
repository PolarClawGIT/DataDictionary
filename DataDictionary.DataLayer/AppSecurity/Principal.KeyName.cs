using DataDictionary.Resource;
using System.Security.Principal;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the unique Login of a Security Principal.
    /// </summary>
    public interface IPrincipalKeyName : IKey
    {
        /// <summary>
        /// Login of the Security Principal
        /// </summary>
        String? PrincipalLogin { get; }
    }

    /// <summary>
    /// Implementation of the unique Login of a Security Principal.
    /// </summary>
    public class PrincipalKeyName : IPrincipalKeyName,
        IKeyComparable<IPrincipalKeyName>, IKeyComparable<PrincipalKeyName>
    {
        /// <inheritdoc/>
        public String PrincipalLogin { get; init; } = String.Empty;

        /// <summary>
        /// Constructor for the Security Principal Name Key.
        /// </summary>
        /// <param name="source"></param>
        public PrincipalKeyName(IPrincipalKeyName source) : base()
        {
            if (source.PrincipalLogin is string) { PrincipalLogin = source.PrincipalLogin; }
            else { PrincipalLogin = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Security Principal Name Key, given the identity
        /// </summary>
        /// <param name="identity"></param>
        /// <example>var key = new SecurityPrincipalKeyName(WindowsIdentity.GetCurrent());</example>
        public PrincipalKeyName(IIdentity identity) : base()
        {
            if (identity.Name is string) { PrincipalLogin = identity.Name; }
            else { PrincipalLogin = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(PrincipalKeyName? other)
        {
            return
                other is PrincipalKeyName &&
                !string.IsNullOrEmpty(PrincipalLogin) &&
                !string.IsNullOrEmpty(other.PrincipalLogin) &&
                PrincipalLogin.Equals(other.PrincipalLogin, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IPrincipalKeyName? other)
        { return other is IPrincipalKeyName value && Equals(new PrincipalKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IPrincipalKeyName value && Equals(new PrincipalKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(PrincipalKeyName? other)
        {
            if (other is PrincipalKeyName value)
            { return string.Compare(PrincipalLogin, value.PrincipalLogin, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IPrincipalKeyName? other)
        { if (other is IPrincipalKeyName value) { return CompareTo(new PrincipalKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IPrincipalKeyName value) { return CompareTo(new PrincipalKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(PrincipalKeyName left, PrincipalKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(PrincipalKeyName left, PrincipalKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(PrincipalKeyName left, PrincipalKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(PrincipalKeyName left, PrincipalKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(PrincipalKeyName left, PrincipalKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(PrincipalKeyName left, PrincipalKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return PrincipalLogin.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (PrincipalLogin is String) { return PrincipalLogin; }
            else { return String.Empty; }
        }
    }
}
