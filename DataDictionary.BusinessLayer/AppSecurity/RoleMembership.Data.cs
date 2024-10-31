using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.AppSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <summary>
    /// Interface component for the Security Membership data
    /// </summary>
    public interface IRoleMembershipData :
        IBindingData<RoleMembershipValue>
    { }

    class RoleMembershipData : RoleMembershipCollection<RoleMembershipValue>, IRoleMembershipData,
        ILoadData, ILoadData<IPrincipalIndex>, ILoadData<IRoleIndex>,
        ISaveData, ISaveData<IPrincipalIndex>, ISaveData<IRoleIndex>
    {
        /// <inheritdoc/>
        /// <remarks>MembershipData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>MembershipData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IPrincipalIndex dataKey)
        { return factory.CreateLoad(this, (IPrincipalKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>MembershipData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IRoleIndex dataKey)
        { return factory.CreateLoad(this, (IRoleKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>MembershipData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>MembershipData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IPrincipalIndex dataKey)
        { return factory.CreateSave(this, (IPrincipalKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>MembershipData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IRoleIndex dataKey)
        { return factory.CreateSave(this, (IRoleKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>MembershipData</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Membership", DoWork = () => { this.Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>MembershipData</remarks>
        public IReadOnlyList<WorkItem> Delete(IPrincipalIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Membership", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>MembershipData</remarks>
        public IReadOnlyList<WorkItem> Delete(IRoleIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Membership", DoWork = () => { this.Remove(dataKey); } }.ToList(); }
    }
}
