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
    public interface ISecurableOwnerValue : ISecurableOwnerItem,
        IPrincipalIndex, ISecurableIndex
    { }

    /// <inheritdoc/>
    public class SecurableOwnerValue : SecurableOwnerItem, ISecurableOwnerValue
    {
        /// <inheritdoc/>
        public SecurableOwnerValue() : base() { }

        /// <inheritdoc cref="SecurableOwnerItem.SecurableOwnerItem(IPrincipalKey)"/>
        public SecurableOwnerValue(IPrincipalIndex principalIndex) : base(principalIndex)
        { }

        /// <inheritdoc cref="SecurableOwnerItem.SecurableOwnerItem(IPrincipalKey, ISecurableKey)"/>
        public SecurableOwnerValue(IPrincipalIndex principalIndex, ISecurableIndex objectIndex) : base(principalIndex, objectIndex)
        { }
    }
}
