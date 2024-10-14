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
    public interface IRoleKey : IKey
    {
        /// <summary>
        /// Application ID for the Role.
        /// </summary>
        Guid? RoleId { get; }
    }

    /// <summary>
    /// Implementation of the Security Role Key.
    /// </summary>
    public class RoleKey : IRoleKey,
        IKeyEquality<IRoleKey>, IKeyEquality<RoleKey>
    {
        /// <inheritdoc/>
        public Guid? RoleId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Security Role Key.
        /// </summary>
        /// <param name="source"></param>
        public RoleKey(IRoleKey source) : base()
        {
            if (source.RoleId is Guid value) { RoleId = value; }
            else { RoleId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(RoleKey? other)
        { return other is RoleKey && EqualityComparer<Guid?>.Default.Equals(RoleId, other.RoleId); }

        /// <inheritdoc/>
        public virtual Boolean Equals(IRoleKey? other)
        { return other is IRoleKey value && Equals(new RoleKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IRoleKey value && Equals(new RoleKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(RoleKey left, RoleKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(RoleKey left, RoleKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(RoleId); }
        #endregion
    }
}
