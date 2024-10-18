using DataDictionary.DataLayer.AppSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppSecurity
{

    /// <inheritdoc/>
    public interface IAuthorizationValue : IAuthorizationItem,
        IPrincipalIndex, IPrincipalIndexName
    { }

    /// <inheritdoc/>
    public class AuthorizationValue : AuthorizationItem, IAuthorizationValue
    {
        /// <inheritdoc/>
        public AuthorizationValue() : base() { }

        /// <inheritdoc/>
        public AuthorizationValue (IIdentity identity) : base(identity)
        { }
    }
}
