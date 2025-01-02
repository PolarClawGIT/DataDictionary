using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.AppModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface component for the Model Entity Definition
    /// </summary>
    public interface IEntityDefinitionData : IBindingData<EntityDefinitionValue>
    { }

    class EntityDefinitionData : EntityDefinitionCollection<EntityDefinitionValue>, IEntityDefinitionData,
        ILoadData<IEntityKey>, ISaveData<IEntityKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }
        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IEntityKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove EntityDefinition", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete(IEntityKey dataKey)
        { return new WorkItem() { WorkName = "Remove EntityDefinition", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }
    }
}
