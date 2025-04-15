// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.AppModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Entity Attribute
    /// </summary>
    [Obsolete]
    public interface IEntityAttributeData_Old : IBindingData<EntityAttributeValue_Old>,
        IRemoveItem<IAttributeKey>, IRemoveItem<IEntityKey>
    { }

    /// <summary>
    /// Implementation for the Model Entity Attribute
    /// </summary>
    [Obsolete]
    public class EntityAttributeData_Old : EntityAttributeCollection<EntityAttributeValue_Old>,
        IEntityAttributeData_Old,
        ILoadData<IEntityIndex>, ISaveData<IEntityIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityIndex dataKey)
        { return factory.CreateLoad(this, (IEntityKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IEntityKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IEntityIndex dataKey)
        { return factory.CreateSave(this, (IEntityKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

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

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public void Remove(IEntityIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }
    }
}
