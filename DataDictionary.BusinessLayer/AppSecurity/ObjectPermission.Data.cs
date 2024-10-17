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
    /// Interface component for the Security Permission data
    /// </summary>
    public interface IObjectPermissionData :
        IBindingData<ObjectPermissionValue>
    { }

    class ObjectPermissionData : ObjectPermissionCollection<ObjectPermissionValue>, IObjectPermissionData,
        ILoadData, ILoadData<IObjectIndex>, ILoadData<IRoleIndex>,
        ISaveData, ISaveData<IObjectIndex>, ISaveData<IRoleIndex>
    {
        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IObjectIndex dataKey)
        { return factory.CreateLoad(this, (IObjectKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IRoleIndex dataKey)
        { return factory.CreateLoad(this, (IRoleKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IObjectIndex dataKey)
        { return factory.CreateSave(this, (IObjectKey)dataKey).ToList(); }

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
        public IReadOnlyList<WorkItem> Delete(IObjectIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Permission", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>PermissionData</remarks>
        public IReadOnlyList<WorkItem> Delete(IRoleIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Permission", DoWork = () => { this.Remove(dataKey); } }.ToList(); }
    }
}
