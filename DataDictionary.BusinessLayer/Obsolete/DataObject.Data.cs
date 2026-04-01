using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.Obsolete;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Obsolete
{
    /// <summary>
    /// Interface component for the Scripting Data Object
    /// </summary>
    [Obsolete]
    public interface IDataObjectData :
        IBindingData<DataObjectValue>,
        IGetTemporal<IModelIndex>, IGetTemporal<IDataSourceIndex>
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

        /// <inheritdoc/>
        /// <remarks>ScriptingDataSource</remarks>
        public ITemporalData GetTemporal(IModelIndex model)
        {
            return new TemporalData<DataSourceData, DataSourceValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data, (IModelKey)model) };
        }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataSource</remarks>
        public ITemporalData GetTemporal(IDataSourceIndex dataSource)
        {
            return new TemporalData<DataObjectData, DataObjectValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data, (IDataSourceKey)dataSource) };
        }
    }
}
