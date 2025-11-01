// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppSecurity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <summary>
    /// Interface representing Security data
    /// </summary>
    public interface ISecurity :
        ILoadData, ILoadData<IPrincipalIndex>, ILoadData<IRoleIndex>, ILoadData<ISecurableIndex>,
        ISaveData, ISaveData<IPrincipalIndex>, ISaveData<IRoleIndex>, ISaveData<ISecurableIndex>,
        IBindListChanged
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
        /// Securable Ownership
        /// </summary>
        ISecurableOwnerData Owners { get; }

        /// <summary>
        /// Securable Permission
        /// </summary>
        ISecurablePermissionData Permissions { get; }

        /// <summary>
        /// Securables (Security Objects)
        /// </summary>
        ISecurableData Securables { get; }

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
        public ISecurableOwnerData Owners { get { return ownerValues; } }
        SecurableOwnerData ownerValues = new SecurableOwnerData();

        /// <inheritdoc/>
        public ISecurablePermissionData Permissions { get { return permissionValues; } }
        SecurablePermissionData permissionValues = new SecurablePermissionData();

        /// <inheritdoc/>
        public ISecurableData Securables { get { return securableValues; } }
        SecurableData securableValues = new SecurableData();

        public Security():base()
        {
            principalValues.ListChanged += OnListChanged;
            roleValues.ListChanged += OnListChanged;
            ownerValues.ListChanged += OnListChanged;
            membershipValues.ListChanged += OnListChanged;
            permissionValues.ListChanged += OnListChanged;
            securableValues.ListChanged += OnListChanged;

            void OnListChanged(Object? sender, ListChangedEventArgs e)
            {
                if (ListChanged is ListChangedEventHandler handler)
                { handler(sender, e); }
            }
        }

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
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IPrincipalIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            throw new NotSupportedException();

            //List<WorkItem> work = new List<WorkItem>();
            //work.AddRange(principalValues.Load(factory, dataKey, asOfUtcDate));
            //work.AddRange(roleValues.Load(factory));
            //work.AddRange(membershipValues.Load(factory, dataKey, asOfUtcDate));
            //work.AddRange(ownerValues.Load(factory, dataKey, asOfUtcDate));
            //return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IRoleIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principalValues.Load(factory));
            work.AddRange(roleValues.Load(factory, dataKey));
            work.AddRange(membershipValues.Load(factory, dataKey));
            work.AddRange(permissionValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IRoleIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            throw new NotSupportedException();

            //List<WorkItem> work = new List<WorkItem>();
            //work.AddRange(principalValues.Load(factory));
            //work.AddRange(roleValues.Load(factory, dataKey, asOfUtcDate));
            //work.AddRange(membershipValues.Load(factory, dataKey, asOfUtcDate));
            //work.AddRange(permissionValues.Load(factory, dataKey, asOfUtcDate));
            //return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ISecurableIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principalValues.Load(factory));
            work.AddRange(roleValues.Load(factory));
            work.AddRange(membershipValues.Load(factory));
            work.AddRange(ownerValues.Load(factory, dataKey));
            work.AddRange(permissionValues.Load(factory, dataKey));
            work.AddRange(securableValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ISecurableIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            throw new NotSupportedException();

            //List<WorkItem> work = new List<WorkItem>();
            //work.AddRange(principalValues.Load(factory));
            //work.AddRange(roleValues.Load(factory));
            //work.AddRange(membershipValues.Load(factory));
            //work.AddRange(ownerValues.Load(factory, dataKey, asOfUtcDate));
            //work.AddRange(permissionValues.Load(factory, dataKey, asOfUtcDate));
            //work.AddRange(securableValues.Load(factory, dataKey, asOfUtcDate));
            //return work;
        }

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
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ISecurableIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(ownerValues.Save(factory, dataKey));
            work.AddRange(permissionValues.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(principalValues.Delete());
            work.AddRange(roleValues.Delete());
            work.AddRange(ownerValues.Delete());
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

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(ISecurableIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(ownerValues.Delete(dataKey));
            work.AddRange(permissionValues.Delete(dataKey));
            return work;
        }

        /// <inheritdoc/>
        public void Remove(IPrincipalIndex dataKey)
        {
            principalValues.Remove(dataKey);
            membershipValues.Remove(dataKey);
            ownerValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(IRoleIndex dataKey)
        {
            membershipValues.Remove(dataKey);
            permissionValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(ISecurableIndex dataKey)
        {
            ownerValues.Remove(dataKey);
            permissionValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            principalValues.Clear();
            roleValues.Clear();
            ownerValues.Clear();
            permissionValues.Clear();
            membershipValues.Clear();
            securableValues.Clear();
        }

        #region IBindListChanged
        /// <inheritdoc/>
        public event ListChangedEventHandler? ListChanged;

        /// <inheritdoc/>
        public Boolean RaiseListChangedEvents
        {
            get
            {
                return principalValues.RaiseListChangedEvents
                    && roleValues.RaiseListChangedEvents
                    && membershipValues.RaiseListChangedEvents
                    && ownerValues.RaiseListChangedEvents
                    && permissionValues.RaiseListChangedEvents
                    && securableValues.RaiseListChangedEvents;
            }
            set
            {
                principalValues.RaiseListChangedEvents = value;
                roleValues.RaiseListChangedEvents = value;
                membershipValues.RaiseListChangedEvents = value;
                ownerValues.RaiseListChangedEvents = value;
                permissionValues.RaiseListChangedEvents = value;
                securableValues.RaiseListChangedEvents = value;
            }
        }

        /// <inheritdoc/>
        public void ResetBindings()
        {
            principalValues.ResetBindings();
            roleValues.ResetBindings();
            ownerValues.ResetBindings();
            membershipValues.ResetBindings();
            permissionValues.ResetBindings();
            securableValues.ResetBindings();
        }
        #endregion

    }
}
