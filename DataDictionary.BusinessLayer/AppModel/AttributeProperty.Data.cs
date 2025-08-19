// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface component for the Model Attribute Property
    /// </summary>
    public interface IAttributePropertyData : IBindingData<AttributePropertyValue>
    {
        /// <summary>
        /// Delegate to attempt retrieving a property value based on a key.
        /// </summary>
        Boolean TryGetProperty(IAttributeIndex value, out IPropertyValue? property);
    }

    class AttributePropertyData(TryGetValue<IPropertyIndex, IPropertyValue> getProperty) :
        AttributePropertyCollection<AttributePropertyValue>(), IAttributePropertyData,
        ILoadData<IAttributeIndex>, ISaveData<IAttributeIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        TryGetValue<IPropertyIndex, IPropertyValue> tryGetProperty = getProperty;

        /// <inheritdoc/>
        public Boolean TryGetProperty(IAttributeIndex value, out IPropertyValue? property)
        {
            AttributeIndex key = new AttributeIndex(value);
            if (this.FirstOrDefault(w => key.Equals(w)) is IAttributePropertyValue attribute)
            { return tryGetProperty(attribute, out property); }
            else { property = null; return false; }
        }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex dataKey)
        { return factory.CreateLoad(this, (IAttributeKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IAttributeKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IAttributeIndex dataKey)
        { return factory.CreateSave(this, (IAttributeKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove AttributeProperty", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Delete(IAttributeIndex dataKey)
        { return new WorkItem() { WorkName = "Remove AttributeProperty", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public void Remove(IAttributeIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }
    }
}
