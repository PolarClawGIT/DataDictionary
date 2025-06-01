// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface component for the Model Process Alias
    /// </summary>
    public interface IProcessAliasData :
        IBindingData<ProcessAliasValue>
    { }

    class ProcessAliasData : ProcessAliasCollection<ProcessAliasValue>, IProcessAliasData,
        ILoadData<IProcessIndex>, ISaveData<IProcessIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        /// <inheritdoc/>
        /// <remarks>ProcessAlias</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessAlias</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessAlias</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IProcessIndex dataKey)
        { return factory.CreateLoad(this, (IProcessKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessAlias</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IProcessIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IProcessKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessAlias</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IProcessIndex dataKey)
        { return factory.CreateSave(this, (IProcessKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessAlias</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessAlias</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove ProcessAlias", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessAlias</remarks>
        public IReadOnlyList<WorkItem> Delete(IProcessIndex dataKey)
        { return new WorkItem() { WorkName = "Remove ProcessAlias", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessAlias</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>ProcessAlias</remarks>
        public void Remove(IProcessIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>ProcessAlias</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }
    }
}
