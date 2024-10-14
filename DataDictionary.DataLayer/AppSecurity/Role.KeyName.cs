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
    public interface IRoleKeyName : IKey
    {
        /// <summary>
        /// Role Name of the Security Principle
        /// </summary>
        String? RoleName { get; }
    }

    /// <summary>
    /// Implementation of the unique Name of a Security Role.
    /// </summary>
    public class RoleKeyName : IRoleKeyName,
        IKeyComparable<IRoleKeyName>, IKeyComparable<RoleKeyName>
    {
        /// <inheritdoc/>
        public String RoleName { get; init; } = String.Empty;

        /// <summary>
        /// Constructor for the Security Principle Name Key.
        /// </summary>
        /// <param name="source"></param>
        public RoleKeyName(IRoleKeyName source) : base()
        {
            if (source.RoleName is string) { RoleName = source.RoleName; }
            else { RoleName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(RoleKeyName? other)
        {
            return
                other is RoleKeyName &&
                !string.IsNullOrEmpty(RoleName) &&
                !string.IsNullOrEmpty(other.RoleName) &&
                RoleName.Equals(other.RoleName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IRoleKeyName? other)
        { return other is IRoleKeyName value && Equals(new RoleKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IRoleKeyName value && Equals(new RoleKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(RoleKeyName? other)
        {
            if (other is RoleKeyName value)
            { return string.Compare(RoleName, value.RoleName, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IRoleKeyName? other)
        { if (other is IRoleKeyName value) { return CompareTo(new RoleKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IRoleKeyName value) { return CompareTo(new RoleKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(RoleKeyName left, RoleKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(RoleKeyName left, RoleKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(RoleKeyName left, RoleKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(RoleKeyName left, RoleKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(RoleKeyName left, RoleKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(RoleKeyName left, RoleKeyName right)
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
