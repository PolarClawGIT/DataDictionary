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
        ILoadData, ILoadData<IPrincipalIndex>, ILoadData<IRoleIndex>,
        ISaveData, ISaveData<IPrincipalIndex>, ISaveData<IRoleIndex>
    {
        /// <summary>
        /// Security Principals (user/logins)
        /// </summary>
        IPrincipalData Principals { get; }

        /// <summary>
        /// Security Roles (groups and permissions)
        /// </summary>
        IRoleData Roles { get; }

        /// <summary>
        /// Security Role Membership
        /// </summary>
        IRoleMembershipData Memberships { get; }

        /// <summary>
        /// Object Ownership
        /// </summary>
        IObjectOwnerData Owners { get; }

        /// <summary>
        /// Object Permission
        /// </summary>
        IObjectPermissionData Permissions { get; }

        /// <summary>
        /// Creates an instance of the Security Objects and returns the interface.
        /// </summary>
        /// <returns></returns>
        static public ISecurity Create()
        { return new Security(); }
    }

    class Security : ISecurity
    {
        /// <inheritdoc/>
        public IPrincipalData Principals { get { return principalValues; } }
        PrincipalData principalValues = new PrincipalData();

        /// <inheritdoc/>
        public IRoleData Roles { get { return roleValues; } }
        RoleData roleValues = new RoleData();

        /// <inheritdoc/>
        public IRoleMembershipData Memberships { get { return membershipValues; } }
        RoleMembershipData membershipValues = new RoleMembershipData();

        /// <inheritdoc/>
        public IObjectOwnerData Owners { get { return ownerValues; } }
        ObjectOwnerData ownerValues = new ObjectOwnerData();

        /// <inheritdoc/>
        public IObjectPermissionData Permissions { get { return permissionValues; } }
        ObjectPermissionData permissionValues = new ObjectPermissionData();

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principalValues.Load(factory));
            work.AddRange(roleValues.Load(factory));
            work.AddRange(membershipValues.Load(factory));
            work.AddRange(ownerValues.Load(factory));
            work.AddRange(permissionValues.Load(factory));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IPrincipalIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principalValues.Load(factory, dataKey));
            work.AddRange(roleValues.Load(factory));
            work.AddRange(membershipValues.Load(factory, dataKey));
            work.AddRange(ownerValues.Load(factory, dataKey));
            //work.AddRange(permissionValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IRoleIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principalValues.Load(factory));
            work.AddRange(roleValues.Load(factory, dataKey));
            work.AddRange(membershipValues.Load(factory, dataKey));
            //work.AddRange(ownerValues.Load(factory, dataKey));
            work.AddRange(permissionValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principalValues.Save(factory));
            work.AddRange(roleValues.Save(factory));
            work.AddRange(membershipValues.Save(factory));
            work.AddRange(ownerValues.Save(factory));
            work.AddRange(permissionValues.Save(factory));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IPrincipalIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principalValues.Save(factory, dataKey));
            work.AddRange(membershipValues.Save(factory, dataKey));
            work.AddRange(ownerValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IRoleIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(roleValues.Save(factory, dataKey));
            work.AddRange(membershipValues.Save(factory, dataKey));
            work.AddRange(permissionValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principalValues.Delete());
            work.AddRange(roleValues.Delete());
            work.AddRange(membershipValues.Delete());
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IPrincipalIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principalValues.Delete(dataKey));
            work.AddRange(membershipValues.Delete(dataKey));
            work.AddRange(ownerValues.Delete(dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IRoleIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(roleValues.Delete(dataKey));
            work.AddRange(membershipValues.Delete(dataKey));
            work.AddRange(permissionValues.Delete(dataKey));
            return work;
        }
    }
}
