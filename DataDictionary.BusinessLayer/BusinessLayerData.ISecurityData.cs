using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using System.Security.Principal;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
    {
        /// <summary>
        /// Current User Identity
        /// </summary>
        public IIdentity UserIdentity { get; private set; }

        /// <summary>
        /// Authorization for the current user
        /// </summary>
        public IAuthorizationData Authorization
        { get { return authorizationData; } }
        private readonly AuthorizationData authorizationData = new AuthorizationData();

        /// <summary>
        /// Gets the Authorization data;
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> LoadAuthorization(IDatabaseWork factory)
        { return authorizationData.Load(factory); }

        /// <summary>
        /// Wrapper for the Security classes (Principal, Role, ...)
        /// </summary>
        /// <remarks>
        /// The normal state of this instance is empty.
        /// The Security screens need a common instance for Binding to work (ListChanged event).
        /// </remarks>
        public ISecurity Security { get { return securityValue; } }
        private readonly Security securityValue;
    }
}
