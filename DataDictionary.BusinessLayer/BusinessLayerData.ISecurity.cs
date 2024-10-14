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
        public IAuthorizationItem Authorization
        {
            get
            {
                if (authorizationData.FirstOrDefault() is AuthorizationValue value)
                { return value; }
                return defaultAuthorization;
            }
        }
        private readonly AuthorizationData authorizationData = new AuthorizationData();
        IAuthorizationItem defaultAuthorization;

        /// <summary>
        /// Gets the Authorization data;
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> LoadAuthorization(IDatabaseWork factory)
        { return authorizationData.Load(factory, UserIdentity); }
    }
}
