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
    public interface IRoleIndexName : ISecurityRoleKeyName
    { }

    /// <inheritdoc/>
    public class RoleIndexName : SecurityRoleKeyName, IRoleIndexName,
        IKeyEquality<IRoleIndexName>, IKeyEquality<RoleIndexName>
    {
        /// <inheritdoc cref="SecurityRoleKey.SecurityRoleKey(ISecurityRoleKey)"/>
        public RoleIndexName(IRoleIndexName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(RoleIndexName? other)
        { return other is ISecurityRoleKey value && Equals(new SecurityRoleKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IRoleIndexName? other)
        { return other is ISecurityRoleKey value && Equals(new SecurityRoleKey(value)); }
    }
}
