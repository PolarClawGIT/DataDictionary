// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.AppModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IProcessArgumentData :
        IBindingData<ProcessArgumentValue>,
        IRemoveItem<IProcessKey>
    { }

    /// <inheritdoc/>
    class ProcessArgumentData : ProcessArgumentCollection<ProcessArgumentValue>,
        IProcessArgumentData,
        ILoadData<IProcessIndex>, ISaveData<IProcessIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        #region ILoadData,ISaveData
        /// <inheritdoc/>
        /// <remarks>ProcessArgument</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessArgument</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessArgument</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IProcessIndex dataKey)
        { return factory.CreateLoad(this, (IProcessKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessArgument</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IProcessIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IProcessKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessArgument</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IProcessIndex dataKey)
        { return factory.CreateSave(this, (IProcessKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessArgument</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessArgument</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove ProcessArgument", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessArgument</remarks>
        public IReadOnlyList<WorkItem> Delete(IProcessIndex dataKey)
        { return new WorkItem() { WorkName = "Remove ProcessArgument", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ProcessArgument</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>ProcessArgument</remarks>
        public void Remove(IProcessIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>ProcessDefinition</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }


        #endregion
    }
}
