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
    /// Interface component for the Security Role data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IRoleData :
        IBindingData<RoleValue>
    { }

    /// <summary>
    /// Wrapper Class for Security Role.
    /// </summary>
    class RoleData : SecurityRoleCollection<RoleValue>, IRoleData,
        ILoadData, ILoadData<IRoleIndex>,
        ISaveData, ISaveData<IRoleIndex>
    {
        /// <inheritdoc/>
        /// <remarks>RoleData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>RoleData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IRoleIndex dataKey)
        { return factory.CreateLoad(this, (ISecurityRoleKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>RoleData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>RoleData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IRoleIndex dataKey)
        { return factory.CreateSave(this, (ISecurityRoleKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>RoleData</remarks>
        public IReadOnlyList<WorkItem> Delete(IRoleIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Role", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>RoleData</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Role", DoWork = () => { this.Clear(); } }.ToList(); }

    }
}
