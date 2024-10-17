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
    /// Interface component for the Security Owner data
    /// </summary>
    public interface IObjectOwnerData :
        IBindingData<ObjectOwnerValue>
    { }

    class ObjectOwnerData : ObjectOwnerCollection<ObjectOwnerValue>, IObjectOwnerData,
        ILoadData, ILoadData<IObjectIndex>, ILoadData<IPrincipleIndex>,
        ISaveData, ISaveData<IObjectIndex>, ISaveData<IPrincipleIndex>
    {
        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IObjectIndex dataKey)
        { return factory.CreateLoad(this, (IObjectKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IPrincipleIndex dataKey)
        { return factory.CreateLoad(this, (IPrincipleKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IObjectIndex dataKey)
        { return factory.CreateSave(this, (IObjectKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IPrincipleIndex dataKey)
        { return factory.CreateSave(this, (IPrincipleKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Owner", DoWork = () => { this.Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Delete(IObjectIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Owner", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>OwnerData</remarks>
        public IReadOnlyList<WorkItem> Delete(IPrincipleIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Owner", DoWork = () => { this.Remove(dataKey); } }.ToList(); }
    }
}
