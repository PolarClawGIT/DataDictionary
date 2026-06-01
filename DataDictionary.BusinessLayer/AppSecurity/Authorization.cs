using DataDictionary.BusinessLayer.AppScripting;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <summary>
    /// Interface to support getting Authorization.
    /// </summary>
    public interface IAuthorization
    {
        /// <summary>
        /// Gets the Authorization for the Value;
        /// </summary>
        /// <param name="authorizations"></param>
        /// <returns></returns>
        (Boolean IsAdmin, Boolean IsOwner, Boolean IsGrant) GetAuthorization(IAuthorizationData authorizations);
    }
}