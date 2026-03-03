// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.Obsolete;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Obsolete
{
    /// <summary>
    /// Interface component for the Scripting Data Source
    /// </summary>
    [Obsolete]
    public interface ITemplateData : 
        IBindingData<TemplateValue>,
        IGetTemporal<IModelIndex>, IGetTemporal<ITemplateIndex>,
        ILoadData
    {
        /// <summary>
        /// Creates an empty ITemplateData.
        /// </summary>
        /// <returns></returns>
        static ITemplateData Create()
        { return new TemplateData(); }
    }

    [Obsolete]
    class TemplateData : TemplateCollection<TemplateValue>, ITemplateData,
        ILoadData<ITemplateIndex>, ISaveData<ITemplateIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        /// <inheritdoc/>
        /// <remarks>ScriptingTemplate</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplate</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplate</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplate</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey)
        { return factory.CreateLoad(this, (ITemplateKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplate</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (ITemplateKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplate</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ITemplateIndex dataKey)
        { return factory.CreateSave(this, (ITemplateKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplate</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplate</remarks>
        public IReadOnlyList<WorkItem> Delete(ITemplateIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Scripting Template", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplate</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Scripting Template", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplate</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplate</remarks>
        public void Remove(ITemplateIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplate</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataSource</remarks>
        public ITemporalData GetTemporal(IModelIndex model)
        {
            return new TemporalData<TemplateData, TemplateValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data, (IModelKey)model) };
        }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataSource</remarks>
        public ITemporalData GetTemporal(ITemplateIndex template)
        {
            return new TemporalData<TemplateData, TemplateValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data, (ITemplateKey)template) };
        }
    }
}
