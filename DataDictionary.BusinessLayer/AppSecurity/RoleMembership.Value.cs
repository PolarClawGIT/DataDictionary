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
    public interface IRoleMembershipValue : IRoleMembershipItem,
        IPrincipalIndex, IRoleIndex, IScopeType
    { }

    /// <inheritdoc/>
    public class RoleMembershipValue : RoleMembershipItem, IRoleMembershipValue
    {
        /// <inheritdoc/>
        public RoleMembershipValue() : base () { }

        /// <inheritdoc cref="RoleMembershipItem.RoleMembershipItem(IPrincipalKey)"/>
        public RoleMembershipValue(IPrincipalIndex principalKey) : base(principalKey)
        { }

        /// <inheritdoc cref="RoleMembershipItem.RoleMembershipItem(IRoleKey)"/>
        public RoleMembershipValue(IRoleIndex roleKey) : base(roleKey)
        { }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.SecurityMembership; } }
    }
}
