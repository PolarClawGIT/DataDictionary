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
    /// Interface for the unique Login of a Securable (Security Object).
    /// </summary>
    public interface ISecurableKeyName : IKey
    {
        /// <summary>
        /// Login of the Securable (Security Object)
        /// </summary>
        String? SecurableTitle { get; }
    }

    /// <summary>
    /// Implementation of the unique Login of a Securable (Security Object).
    /// </summary>
    public class SecurableKeyName : ISecurableKeyName,
        IKeyComparable<ISecurableKeyName>, IKeyComparable<SecurableKeyName>
    {
        /// <inheritdoc/>
        public String SecurableTitle { get; init; } = String.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return !String.IsNullOrEmpty(SecurableTitle); } }

        /// <summary>
        /// Constructor for the Security Object Name Key.
        /// </summary>
        /// <param name="source"></param>
        public SecurableKeyName(ISecurableKeyName source) : base()
        {
            if (source.SecurableTitle is string) { SecurableTitle = source.SecurableTitle; }
            else { SecurableTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(SecurableKeyName? other)
        {
            return
                other is SecurableKeyName &&
                !string.IsNullOrEmpty(SecurableTitle) &&
                !string.IsNullOrEmpty(other.SecurableTitle) &&
                SecurableTitle.Equals(other.SecurableTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(ISecurableKeyName? other)
        { return other is ISecurableKeyName value && Equals(new SecurableKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ISecurableKeyName value && Equals(new SecurableKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(SecurableKeyName? other)
        {
            if (other is SecurableKeyName value)
            { return string.Compare(SecurableTitle, value.SecurableTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(ISecurableKeyName? other)
        { if (other is ISecurableKeyName value) { return CompareTo(new SecurableKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is ISecurableKeyName value) { return CompareTo(new SecurableKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(SecurableKeyName left, SecurableKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SecurableKeyName left, SecurableKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(SecurableKeyName left, SecurableKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(SecurableKeyName left, SecurableKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(SecurableKeyName left, SecurableKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(SecurableKeyName left, SecurableKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return SecurableTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (SecurableTitle is String) { return SecurableTitle; }
            else { return String.Empty; }
        }
    }
}
