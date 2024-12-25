using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.AppModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface component for the Model Entity Alias
    /// </summary>
    public interface IEntityAliasData :
        IBindingData<EntityAliasValue>
    { }

    class EntityAliasData : EntityAliasCollection<EntityAliasValue>, IEntityAliasData,
        ILoadData<IEntityKey>, ISaveData<IEntityKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <inheritdoc/>
        /// <remarks>EntityAlias</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAlias</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }


        /// <inheritdoc/>
        /// <remarks>EntityAlias</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IEntityKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAlias</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAlias</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove EntityAlias", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAlias</remarks>
        public IReadOnlyList<WorkItem> Delete(IEntityKey dataKey)
        { return new WorkItem() { WorkName = "Remove EntityAlias", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAlias</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }
    }
}
