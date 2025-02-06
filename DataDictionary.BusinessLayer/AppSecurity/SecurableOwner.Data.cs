// Ignore Spelling: Securable Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
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
    /// Interface component for the Security Owner data
    /// </summary>
    public interface ISecurableOwnerData :
        IBindingData<SecurableOwnerValue>
    { }

    class SecurableOwnerData : SecurableOwnerCollection<SecurableOwnerValue>, ISecurableOwnerData,
        ILoadData, ILoadData<ISecurableIndex>, ILoadData<IPrincipalIndex>,
        ISaveData, ISaveData<ISecurableIndex>, ISaveData<IPrincipalIndex>
    {
        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ISecurableIndex dataKey)
        { return factory.CreateLoad(this, (ISecurableKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ISecurableIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (ISecurableKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IPrincipalIndex dataKey)
        { return factory.CreateLoad(this, (IPrincipalKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IPrincipalIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IPrincipalKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ISecurableIndex dataKey)
        { return factory.CreateSave(this, (ISecurableKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IPrincipalIndex dataKey)
        { return factory.CreateSave(this, (IPrincipalKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Owner", DoWork = () => { this.Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Delete(ISecurableIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Owner", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Delete(IPrincipalIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Owner", DoWork = () => { this.Remove(dataKey); } }.ToList(); }
    }
}
