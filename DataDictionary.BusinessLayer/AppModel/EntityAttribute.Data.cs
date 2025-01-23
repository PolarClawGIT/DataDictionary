// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.AppModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Entity Attribute
    /// </summary>
    public interface IEntityAttributeData : IBindingData<EntityAttributeValue>,
        IRemoveItem<IAttributeKey>, IRemoveItem<IEntityKey>
    { }

    /// <summary>
    /// Implementation for the Model Entity Attribute
    /// </summary>
    public class EntityAttributeData : EntityAttributeCollection<EntityAttributeValue>,
        IEntityAttributeData,
        ILoadData<IEntityIndex>, ISaveData<IEntityIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, DateTime asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityIndex dataKey)
        { return factory.CreateLoad(this, (IEntityKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityIndex dataKey, DateTime asOfUtcDate)
        { return factory.CreateLoad(this, (IEntityKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IEntityIndex dataKey)
        { return factory.CreateSave(this, (IEntityKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IEntityKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove EntityAttribute", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete(IEntityIndex dataKey)
        { return new WorkItem() { WorkName = "Remove EntityAttribute", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }
    }
}
