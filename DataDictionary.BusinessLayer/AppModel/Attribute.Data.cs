// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface component for the Model Attribute
    /// </summary>
    public interface IAttributeData :
        IBindingData<AttributeValue>,
        IGetTemporal<IModelIndex>, IGetTemporal<IAttributeIndex>,
        ITryGetValue<IAttributeIndex, IAttributeValue>
    { }

    class AttributeData : AttributeCollection<AttributeValue>, IAttributeData,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        ILoadData<IAttributeIndex>, ISaveData<IAttributeIndex>
    {
        public AttributeData() : base()
        { }

        #region ILoadData, ISaveData
        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex dataKey)
        { return factory.CreateLoad(this, (IAttributeKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IAttributeKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IAttributeIndex dataKey)
        { return factory.CreateSave(this, (IAttributeKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Attribute", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Delete(IAttributeIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Attribute", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        #endregion

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public ITemporalData GetTemporal(IModelIndex model)
        {
            return new TemporalData<AttributeData, AttributeValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data, (IModelKey)model) };
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public ITemporalData GetTemporal(IAttributeIndex attribute)
        {
            return new TemporalData<AttributeData, AttributeValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data, (IAttributeKey)attribute) };
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public void Remove(IAttributeIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }

        /// <inheritdoc/> 
        /// <remarks>Attribute</remarks>
        public Boolean TryGetValue(IAttributeIndex index, [NotNullWhen(true)] out IAttributeValue? value)
        {
            AttributeIndex key = new AttributeIndex(index);

            if (this.FirstOrDefault(w => key.Equals(w)) is IAttributeValue attribute)
            { value = attribute; return true; }
            else { value = null; return false; }
        }
    }
}
