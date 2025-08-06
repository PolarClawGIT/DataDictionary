using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.AppScript;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface component for the Scripting Data Object
    /// </summary>
    public interface IDataObjectData : IBindingData<DataObjectValue>
    { }

    class DataObjectData : DataObjectCollection<DataObjectValue>, IDataObjectData,
        ILoadData<IDataSourceIndex>, ISaveData<IDataSourceIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        /// <inheritdoc/>
        /// <remarks>ScriptingDataObject</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataObject</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataSource</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey)
        { return factory.CreateLoad(this, (ITemplateKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataSource</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (ITemplateKey)dataKey, asOfUtcDate).ToList(); }


        /// <inheritdoc/>
        /// <remarks>ScriptingDataObject</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDataSourceIndex dataKey)
        { return factory.CreateLoad(this, (IDataSourceKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataObject</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDataSourceIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IDataSourceKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataObject</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDataSourceIndex dataKey)
        { return factory.CreateSave(this, (IDataSourceKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataObject</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataObject</remarks>
        public IReadOnlyList<WorkItem> Delete(IDataSourceIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Scripting Data Object", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataObject</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Scripting Data Object", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataObject</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataObject</remarks>
        public void Remove(IDataSourceIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataObject</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }
    }
}
