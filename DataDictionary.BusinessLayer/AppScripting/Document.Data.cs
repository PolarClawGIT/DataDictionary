using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.AppScript;
using System.Data;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface component for the Scripting Data Source
    /// </summary>
    public interface IDocumentData :
        IBindingData<DocumentValue>,
        IGetTemporal<IModelIndex>, IGetTemporal<IDocumentIndex>
    {
        /// <summary>
        /// Creates an empty IDocumentData.
        /// </summary>
        /// <returns></returns>
        static IDocumentData Create()
        { return new DocumentData(); }
    }

    class DocumentData : DocumentCollection<DocumentValue>, IDocumentData,
        ILoadData<IDocumentIndex>, ISaveData<IDocumentIndex>,
        ILoadData<ITemplateIndex>, ISaveData<ITemplateIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDocumentIndex dataKey)
        { return factory.CreateLoad(this, (IDocumentKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDocumentIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IDocumentKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey)
        { return factory.CreateLoad(this, (ITemplateKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (ITemplateKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDocumentIndex dataKey)
        { return factory.CreateSave(this, (IDocumentKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ITemplateIndex dataKey)
        { return factory.CreateSave(this, (ITemplateKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public IReadOnlyList<WorkItem> Delete(IDocumentIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Scripting Document", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public IReadOnlyList<WorkItem> Delete(ITemplateIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Scripting Document", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Scripting Document", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public void Remove(IDocumentIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public void Remove(ITemplateIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDocument</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataSource</remarks>
        public ITemporalData GetTemporal(IModelIndex model)
        {
            return new TemporalData<DocumentData, DocumentValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data, (IModelKey)model) };
        }

        /// <inheritdoc/>
        /// <remarks>ScriptingDataSource</remarks>
        public ITemporalData GetTemporal(IDocumentIndex Document)
        {
            return new TemporalData<DocumentData, DocumentValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data, (IDocumentKey)Document) };
        }
    }
}