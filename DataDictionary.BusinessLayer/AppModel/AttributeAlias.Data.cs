// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface component for the Model Attribute Alias
    /// </summary>
    public interface IAttributeAliasData :
        IBindingData<AttributeAliasValue>
    { }

    class AttributeAliasData : AttributeAliasCollection<AttributeAliasValue>, IAttributeAliasData,
        ILoadData<IAttributeIndex>, ISaveData<IAttributeIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        /// <inheritdoc/>
        /// <remarks>AttributeAlias</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeAlias</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeAlias</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex dataKey)
        { return factory.CreateLoad(this, (IAttributeKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeAlias</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IAttributeKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeAlias</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IAttributeIndex dataKey)
        { return factory.CreateSave(this, (IAttributeKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeAlias</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeAlias</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove AttributeAlias", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeAlias</remarks>
        public IReadOnlyList<WorkItem> Delete(IAttributeIndex dataKey)
        { return new WorkItem() { WorkName = "Remove AttributeAlias", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeAlias</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }
    }
}
