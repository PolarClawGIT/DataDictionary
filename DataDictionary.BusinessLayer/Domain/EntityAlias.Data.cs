using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface component for the Model Entity Alias
    /// </summary>
    public interface IEntityAliasData:
        IBindingData<EntityAliasValue>
    { }

    internal class EntityAliasData: DomainEntityAliasCollection<EntityAliasValue>, IEntityAliasData,
        ILoadData<IDomainEntityKey>, ISaveData<IDomainEntityKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <inheritdoc/>
        /// <remarks>EntityAlias</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDomainEntityKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAlias</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAlias</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainEntityKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAlias</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }
    }
}
