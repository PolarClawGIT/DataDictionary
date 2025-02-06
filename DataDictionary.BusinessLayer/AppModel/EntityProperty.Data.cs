// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface component for the Model Entity Property
    /// </summary>
    public interface IEntityPropertyData :
        IBindingData<EntityPropertyValue>
    {

    }

    class EntityPropertyData : EntityPropertyCollection<EntityPropertyValue>, IEntityPropertyData,
        ILoadData<IEntityIndex>, ISaveData<IEntityIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        /// <inheritdoc/>
        /// <remarks>EntityProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityIndex dataKey)
        { return factory.CreateLoad(this, (IEntityKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IEntityKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityProperty</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IEntityIndex dataKey)
        { return factory.CreateSave(this, (IEntityKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityProperty</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IEntityKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityProperty</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove EntityProperty", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityProperty</remarks>
        public IReadOnlyList<WorkItem> Delete(IEntityIndex dataKey)
        { return new WorkItem() { WorkName = "Remove EntityProperty", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityProperty</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }
    }
}
