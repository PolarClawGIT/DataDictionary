using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.AppModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntitySubjectAreaData : IBindingData<EntitySubjectAreaValue>
    { }

    /// <inheritdoc/>
    class EntitySubjectAreaData : EntitySubjectAreaCollection<EntitySubjectAreaValue>, IEntitySubjectAreaData,
        ILoadData<IEntityKey>, ISaveData<IEntityKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <inheritdoc/>
        /// <remarks>EntitySubjectArea</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntitySubjectArea</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }


        /// <inheritdoc/>
        /// <remarks>EntitySubjectArea</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IEntityKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntitySubjectArea</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntitySubjectArea</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove EntitySubjectArea", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntitySubjectArea</remarks>
        public IReadOnlyList<WorkItem> Delete(IEntityKey dataKey)
        { return new WorkItem() { WorkName = "Remove EntitySubjectArea", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntitySubjectArea</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }
    }
}
