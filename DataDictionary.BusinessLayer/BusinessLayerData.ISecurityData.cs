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
    }
}
