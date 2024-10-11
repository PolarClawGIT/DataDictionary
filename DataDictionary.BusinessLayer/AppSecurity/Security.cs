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
    /// Interface representing Security for a specific user
    /// </summary>
    /// <remarks>This data set is intended to be read-only.</remarks>
    public interface IUserSecurity
    {
        /// <summary>
        /// Loads the Security with the security about a specific identity
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IIdentity identity);

        /// <summary>
        /// Principal for the current Identity
        /// </summary>
        IPrincipleValue Principle { get; }

        // TODO: Role Members
        // TODO: Ownership
        // TODO: Permissions

    }

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

    class Security : ISecurity, IUserSecurity
    {
        /// <inheritdoc/>
        public IPrincipleData Principles { get { return principles; } }
        PrincipleData principles = new PrincipleData();

        /// <inheritdoc/>
        public IRoleData Roles { get { return roles; } }
        RoleData roles = new RoleData();

        /// <inheritdoc/>
        public IPrincipleValue Principle
        {
            get
            {
                SecurityPrincipleKeyName key = new SecurityPrincipleKeyName(userIdentity);
                if (principles.FirstOrDefault(w => key.Equals(w)) is PrincipleValue value)
                { return value; }
                else
                { return emptyPrinciple; }

            }
        }
        IIdentity userIdentity;
        PrincipleValue emptyPrinciple;

        public Security(IIdentity identity) : base()
        {
            userIdentity = identity;
            emptyPrinciple = new PrincipleValue()
            { PrincipleLogin = identity.Name, PrincipleName = identity.Name };
        }

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

        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IIdentity identity)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principles.Load(factory, identity));
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
