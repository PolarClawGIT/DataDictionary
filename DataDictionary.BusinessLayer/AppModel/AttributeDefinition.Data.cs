// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>  
    /// Interface component for the Model Attribute Definition  
    /// </summary>  
    public interface IAttributeDefinitionData : IBindingData<AttributeDefinitionValue>
    {
        /// <summary>  
        /// Delegate to attempt retrieving a definition based on a key.  
        /// </summary>  
        TryGetDefinition TryGetDefinition { get; }
    }

    class AttributeDefinitionData(IDefinitionGetValue definition) : AttributeDefinitionCollection<AttributeDefinitionValue>(), IAttributeDefinitionData,
        ILoadData<IAttributeIndex>, ISaveData<IAttributeIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        /// <inheritdoc/>
        public TryGetDefinition TryGetDefinition { get; init; } = definition.TryGetValue;

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex dataKey)
        { return factory.CreateLoad(this, (IAttributeKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IAttributeKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IAttributeIndex dataKey)
        { return factory.CreateSave(this, (IAttributeKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove AttributeDefinition", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete(IAttributeIndex dataKey)
        { return new WorkItem() { WorkName = "Remove AttributeDefinition", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public void Remove(IAttributeIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>AttributeDefinition</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }
    }
}
