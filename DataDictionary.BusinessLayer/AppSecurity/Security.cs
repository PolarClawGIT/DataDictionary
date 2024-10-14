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
    /// Interface representing Security data
    /// </summary>
    public interface ISecurity :
        ILoadData,
        ISaveData
    {
        /// <summary>
        /// Security Principles (user/logins)
        /// </summary>
        IPrincipleData Principles { get; }

        /// <summary>
        /// Security Roles (groups and permissions)
        /// </summary>
        IRoleData Roles { get; }

        // TODO: Role Members
        // TODO: Ownership
        // TODO: Permissions
    }

    class Security : ISecurity
    {
        /// <inheritdoc/>
        public IPrincipleData Principles { get { return principles; } }
        PrincipleData principles = new PrincipleData();

        /// <inheritdoc/>
        public IRoleData Roles { get { return roles; } }
        RoleData roles = new RoleData();

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principles.Save(factory));
            work.AddRange(roles.Save(factory));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principles.Load(factory));
            work.AddRange(roles.Load(factory));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principles.Delete());
            work.AddRange(roles.Delete());
            return work;
        }


    }
}
