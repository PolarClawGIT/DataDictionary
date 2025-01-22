using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IAttributeSubjectAreaData : IBindingData<AttributeSubjectAreaValue>
    { }

    /// <inheritdoc/>
    class AttributeSubjectAreaData : AttributeSubjectAreaCollection<AttributeSubjectAreaValue>, IAttributeSubjectAreaData,
        ILoadData<IAttributeKey>, ISaveData<IAttributeKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <inheritdoc/>
        /// <remarks>AttributeSubjectArea</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeSubjectArea</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeSubjectArea</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IAttributeKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeSubjectArea</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeSubjectArea</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove AttributeSubjectArea", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeSubjectArea</remarks>
        public IReadOnlyList<WorkItem> Delete(IAttributeKey dataKey)
        { return new WorkItem() { WorkName = "Remove AttributeSubjectArea", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeSubjectArea</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }
    }
}
