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
    public interface IPrincipleKey : IKey
    {
        /// <summary>
        /// Application ID for the Principle.
        /// </summary>
        Guid? PrincipleId { get; }
    }

    /// <summary>
    /// Implementation for the Security Principle Key.
    /// </summary>
    public class PrincipleKey : IPrincipleKey,
        IKeyEquality<IPrincipleKey>, IKeyEquality<PrincipleKey>
    {
        /// <inheritdoc/>
        public Guid? PrincipleId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Security Principle Key.
        /// </summary>
        /// <param name="source"></param>
        public PrincipleKey(IPrincipleKey source) : base()
        {
            if (source.PrincipleId is Guid value) { PrincipleId = value; }
            else { PrincipleId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(PrincipleKey? other)
        { return other is PrincipleKey && EqualityComparer<Guid?>.Default.Equals(PrincipleId, other.PrincipleId); }

        /// <inheritdoc/>
        public virtual Boolean Equals(IPrincipleKey? other)
        { return other is IPrincipleKey value && Equals(new PrincipleKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IPrincipleKey value && Equals(new PrincipleKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(PrincipleKey left, PrincipleKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(PrincipleKey left, PrincipleKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(PrincipleId); }
        #endregion
    }
}
