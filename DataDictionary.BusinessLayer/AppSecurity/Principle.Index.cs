using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppSecurity;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <inheritdoc/>
    public interface IPrincipleIndex : ISecurityPrincipleKey
    { }

    /// <inheritdoc/>
    public class PrincipleIndex : SecurityPrincipleKey, IPrincipleIndex,
        IKeyEquality<IPrincipleIndex>, IKeyEquality<PrincipleIndex>
    {
        /// <inheritdoc cref="SecurityPrincipleKey.SecurityPrincipleKey(ISecurityPrincipleKey)"/>
        public PrincipleIndex(IPrincipleIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(PrincipleIndex? other)
        { return other is ISecurityPrincipleKey value && Equals(new SecurityPrincipleKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IPrincipleIndex? other)
        { return other is ISecurityPrincipleKey value && Equals(new SecurityPrincipleKey(value)); }

        /// <summary>
        /// Convert PrincipleIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(PrincipleIndex source)
        { return new DataIndex() { SystemId = source.PrincipleId ?? Guid.Empty }; }
    }
}
