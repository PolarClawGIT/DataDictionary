using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.AppModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface component for the Model Attribute Definition
    /// </summary>
    public interface IAttributeDefinitionData : IBindingData<AttributeDefinitionValue>
    { }

    class AttributeDefinitionData : AttributeDefinitionCollection<AttributeDefinitionValue>, IAttributeDefinitionData,
        ILoadData<IAttributeKey>, ISaveData<IAttributeKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IAttributeKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove AttributeDefinition", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete(IAttributeKey dataKey)
        { return new WorkItem() { WorkName = "Remove AttributeDefinition", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }
    }
}
