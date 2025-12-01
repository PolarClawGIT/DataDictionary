using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.AppScript;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface component for the Scripting Template Node
    /// </summary>
    public interface ITemplateNodeData : IBindingData<TemplateNodeValue>
    {
    }

    class TemplateNodeData : TemplateNodeCollection<TemplateNodeValue>, ITemplateNodeData,
        ILoadData<ITemplateIndex>, ISaveData<ITemplateIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        IRemoveData<ITemplateNodeIndex>
    {
        /// <inheritdoc/>
        /// <remarks>TemplateNodeData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TemplateNodeData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TemplateNodeData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey)
        { return factory.CreateLoad(this, (ITemplateKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TemplateNodeData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (ITemplateKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TemplateNodeData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ITemplateIndex dataKey)
        { return factory.CreateSave(this, (ITemplateKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TemplateNodeData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TemplateNodeData</remarks>
        public IReadOnlyList<WorkItem> Delete(ITemplateIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Scripting Template Node", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>TemplateNodeData</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Scripting Template Node", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>TemplateNodeData</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>TemplateNodeData</remarks>
        public void Remove(ITemplateIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>TemplateNodeData</remarks>
        public void Remove(ITemplateNodeIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>TemplateNodeData</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }
    }
}
