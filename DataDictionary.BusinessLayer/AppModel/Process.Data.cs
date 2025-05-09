// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface component for the Model Process
    /// </summary>
    public interface IProcessData :
        IBindingData<ProcessValue>,
        IGetTemporal<IModelIndex>, IGetTemporal<IProcessIndex>
    { }

    class ProcessData : ProcessCollection<ProcessValue>, IProcessData,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        ILoadData<IProcessIndex>, ISaveData<IProcessIndex>
    {
        public ProcessData() : base()
        { }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IProcessIndex dataKey)
        { return factory.CreateLoad(this, (IProcessKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IProcessIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IProcessKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IProcessIndex dataKey)
        { return factory.CreateSave(this, (IProcessKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Process", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Delete(IProcessIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Process", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public void Remove(IProcessIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public ITemporalData GetTemporal(IModelIndex key)
        {
            return new TemporalData<ProcessData, ProcessValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data, (IModelKey)key) };
        }

        /// <inheritdoc/>
        /// <remarks>Process</remarks>
        public ITemporalData GetTemporal(IProcessIndex key)
        {
            return new TemporalData<ProcessData, ProcessValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data, (IProcessKey)key) };
        }
    }
}
