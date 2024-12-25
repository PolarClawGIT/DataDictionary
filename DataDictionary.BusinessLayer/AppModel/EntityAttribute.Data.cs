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
        { return new WorkItem() { WorkName = "Remove EntityAttribute", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete(IEntityKey dataKey)
        { return new WorkItem() { WorkName = "Remove EntityAttribute", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }
    }
}
