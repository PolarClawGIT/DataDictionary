using DataDictionary.DataLayer.AppSecurity;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <inheritdoc/>
    public interface IRoleValue : ISecurityRoleItem, IScopeType,
        IRoleIndex, IRoleIndexName
    { }

    /// <inheritdoc/>
    public class RoleValue : SecurityRoleItem, IRoleValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.SecurityRole;
    }
}
