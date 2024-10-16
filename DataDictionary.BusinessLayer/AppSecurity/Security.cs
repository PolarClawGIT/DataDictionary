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

        /// <summary>
        /// Security Role Membership
        /// </summary>
        IRoleMembershipData Memberships { get; }

        /// <summary>
        /// Creates an instance of the Security Objects and returns the interface.
        /// </summary>
        /// <returns></returns>
        static public ISecurity Create()
        { return new Security(); }

        // TODO: Ownership
        // TODO: Permissions
    }

    class Security : ISecurity,
        ILoadData<IPrincipleIndex>, ILoadData<IRoleIndex>,
        ISaveData<IPrincipleIndex>, ISaveData<IRoleIndex>
    {
        /// <inheritdoc/>
        public IPrincipleData Principles { get { return principleValues; } }
        PrincipleData principleValues = new PrincipleData();

        /// <inheritdoc/>
        public IRoleData Roles { get { return roleValues; } }
        RoleData roleValues = new RoleData();

        /// <inheritdoc/>
        public IRoleMembershipData Memberships { get { return membershipValues; } }
        RoleMembershipData membershipValues = new RoleMembershipData();

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principleValues.Load(factory));
            work.AddRange(roleValues.Load(factory));
            work.AddRange(membershipValues.Load(factory));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IPrincipleIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principleValues.Load(factory, dataKey));
            work.AddRange(roleValues.Load(factory));
            work.AddRange(membershipValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IRoleIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principleValues.Load(factory));
            work.AddRange(roleValues.Load(factory, dataKey));
            work.AddRange(membershipValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principleValues.Save(factory));
            work.AddRange(roleValues.Save(factory));
            work.AddRange(membershipValues.Save(factory));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IPrincipleIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principleValues.Save(factory, dataKey));
            work.AddRange(membershipValues.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IRoleIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(roleValues.Save(factory, dataKey));
            work.AddRange(membershipValues.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principleValues.Delete());
            work.AddRange(roleValues.Delete());
            work.AddRange(membershipValues.Delete());
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IPrincipleIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principleValues.Delete(dataKey));
            work.AddRange(membershipValues.Delete(dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IRoleIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(roleValues.Delete(dataKey));
            work.AddRange(membershipValues.Delete(dataKey));
            return work;
        }
    }
}
