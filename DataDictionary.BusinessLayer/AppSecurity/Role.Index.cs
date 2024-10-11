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
    public interface IRoleIndex : ISecurityRoleKey
    { }

    /// <inheritdoc/>
    public class RoleIndex : SecurityRoleKey, IRoleIndex,
        IKeyEquality<IRoleIndex>, IKeyEquality<RoleIndex>
    {
        /// <inheritdoc cref="SecurityRoleKey.SecurityRoleKey(ISecurityRoleKey)"/>
        public RoleIndex(IRoleIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(RoleIndex? other)
        { return other is ISecurityRoleKey value && Equals(new SecurityRoleKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IRoleIndex? other)
        { return other is ISecurityRoleKey value && Equals(new SecurityRoleKey(value)); }

        /// <summary>
        /// Convert RoleIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(RoleIndex source)
        { return new DataIndex() { SystemId = source.RoleId ?? Guid.Empty }; }
    }
}
