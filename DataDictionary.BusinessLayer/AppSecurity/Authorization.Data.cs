using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.AppSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <summary>
    /// Interface component for the Authorization data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IAuthorizationData :
        IBindingData<AuthorizationValue>
    { }


    /// <summary>
    /// Wrapper Class for Security Authorization.
    /// </summary>
    class AuthorizationData : AuthorizationCollection<AuthorizationValue>,
        IAuthorizationData
    {
        /// <summary>
        /// Load the Authorization.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IIdentity identity)
        {
            ISecurityPrincipleKeyName key = new SecurityPrincipleKeyName(identity);
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { DoWork = Clear });
            work.Add(factory.CreateLoad(this, key));
            return work;
        }
    }
}
