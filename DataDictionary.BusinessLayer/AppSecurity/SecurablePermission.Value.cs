// Ignore Spelling: Securable

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
    public interface ISecurablePermissionValue : ISecurablePermissionItem,
        IRoleIndex, ISecurableIndex
    { }

    /// <inheritdoc/>
    public class SecurablePermissionValue : SecurablePermissionItem, ISecurablePermissionValue
    {
        /// <inheritdoc/>
        public SecurablePermissionValue() : base() { }

        /// <inheritdoc cref="SecurablePermissionItem.SecurablePermissionItem(IRoleKey)"/>
        public SecurablePermissionValue(IRoleIndex roleIndex): base(roleIndex)
        { }

        /// <inheritdoc cref="SecurablePermissionItem.SecurablePermissionItem(IRoleKey, ISecurableKey)"/>
        public SecurablePermissionValue(IRoleIndex roleIndex, ISecurableIndex objectIndex): base(roleIndex, objectIndex) 
        { }
    }
}
