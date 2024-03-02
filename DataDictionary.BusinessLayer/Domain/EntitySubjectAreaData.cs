using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ModelData.Entity;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface component for the Model Entity Subject Areas
    /// </summary>
    public interface IEntitySubjectAreaData :
        IBindingData<ModelEntityItem>
    { }

    class EntitySubjectAreaData: ModelEntityCollection, IEntitySubjectAreaData,
        ILoadData<IDomainEntityKey>, ISaveData<IDomainEntityKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <inheritdoc/>
        /// <remarks>ModelEntity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDomainEntityKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ModelEntity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ModelEntity</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainEntityKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ModelEntity</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }
    }
}
