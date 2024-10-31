using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the Security Principal Key.
    /// </summary>
    public interface IPrincipalKey : IKey
    {
        /// <summary>
        /// Application ID for the Principal.
        /// </summary>
        Guid? PrincipalId { get; }
    }

    /// <summary>
    /// Implementation for the Security Principal Key.
    /// </summary>
    public class PrincipalKey : IPrincipalKey,
        IKeyEquality<IPrincipalKey>, IKeyEquality<PrincipalKey>
    {
        /// <inheritdoc/>
        public Guid? PrincipalId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Security Principal Key.
        /// </summary>
        /// <param name="source"></param>
        public PrincipalKey(IPrincipalKey source) : base()
        {
            if (source.PrincipalId is Guid value) { PrincipalId = value; }
            else { PrincipalId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(PrincipalKey? other)
        { return other is PrincipalKey && EqualityComparer<Guid?>.Default.Equals(PrincipalId, other.PrincipalId); }

        /// <inheritdoc/>
        public virtual Boolean Equals(IPrincipalKey? other)
        { return other is IPrincipalKey value && Equals(new PrincipalKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IPrincipalKey value && Equals(new PrincipalKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(PrincipalKey left, PrincipalKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(PrincipalKey left, PrincipalKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(PrincipalId); }
        #endregion
    }
}
