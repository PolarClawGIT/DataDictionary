// Ignore Spelling: Securable Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppSecurity;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppSecurity
{

    /// <summary>
    /// Interface component for the Security Permission data
    /// </summary>
    public interface ISecurablePermissionData :
        IBindingData<SecurablePermissionValue>
    { }

    class SecurablePermissionData : SecurablePermissionCollection<SecurablePermissionValue>, ISecurablePermissionData,
        ILoadData, ILoadData<ISecurableIndex>, ILoadData<IRoleIndex>,
        ISaveData, ISaveData<ISecurableIndex>, ISaveData<IRoleIndex>
    {
        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ISecurableIndex dataKey)
        { return factory.CreateLoad(this, (ISecurableKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ISecurableIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (ISecurableKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IRoleIndex dataKey)
        { return factory.CreateLoad(this, (IRoleKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IRoleIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IRoleKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ISecurableIndex dataKey)
        { return factory.CreateSave(this, (ISecurableKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IRoleIndex dataKey)
        { return factory.CreateSave(this, (IRoleKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Permission", DoWork = () => { this.Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Delete(ISecurableIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Permission", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Delete(IRoleIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Permission", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public void Remove(ISecurableIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public void Remove(IRoleIndex dataKey)
        { base.Remove(dataKey); }
    }
}
