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
        /// Get the Definition Value for the given Attribute.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="definition"></param>
        /// <returns></returns>
        Boolean TryGetDefinition(IAttributeIndex value, out IDefinitionValue? definition);
    }

    class AttributeDefinitionData(TryGetValue<IDefinitionIndex, IDefinitionValue> getDefinition) : 
        AttributeDefinitionCollection<AttributeDefinitionValue>(), IAttributeDefinitionData,
        ILoadData<IAttributeIndex>, ISaveData<IAttributeIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        TryGetValue<IDefinitionIndex, IDefinitionValue> tryGetDefinition = getDefinition;

        /// <inheritdoc/>
        public Boolean TryGetDefinition(IAttributeIndex value, out IDefinitionValue? definition)
        {
            AttributeIndex key = new AttributeIndex(value);
            if (this.FirstOrDefault(w => key.Equals(w)) is IAttributeDefinitionValue attribute)
            { return tryGetDefinition(attribute, out definition); }
            else { definition = null; return false; }
        }

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
