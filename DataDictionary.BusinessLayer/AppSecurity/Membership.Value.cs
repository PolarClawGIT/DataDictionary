using DataDictionary.DataLayer.AppSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <inheritdoc/>
    public interface IMembershipValue : ISecurityMembershipItem,
        IPrincipleIndex, IRoleIndex
    { }

    /// <inheritdoc/>
    public class MembershipValue : SecurityMembershipItem, IMembershipValue
    {
        /// <inheritdoc/>
        public MembershipValue() : base () { }

        /// <inheritdoc cref="SecurityMembershipItem.SecurityMembershipItem(ISecurityPrincipleKey, ISecurityRoleKey)"/>
        public MembershipValue(IPrincipleIndex principleKey, IRoleIndex roleKey) : base(principleKey, roleKey)
        { }
    }
}
