using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.AppSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
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
