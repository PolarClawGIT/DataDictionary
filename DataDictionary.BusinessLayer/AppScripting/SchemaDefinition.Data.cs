using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.AppScript;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface component for the Scripting SchemaDefinition
    /// </summary>
    public interface ISchemaDefinitionData :
        IBindingData<SchemaDefinitionValue>,
        IGetTemporal<IModelIndex>, IGetTemporal<ITemplateIndex>,
        ILoadData, ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        ILoadData<ITemplateIndex>, ISaveData<ITemplateIndex>,
        IDeleteData, IRemoveData<ISchemaDefinitionIndex>
    { }

    class SchemaDefinitionData : SchemaDefinitionCollection<SchemaDefinitionValue>, ISchemaDefinitionData
    {
        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, (IModelKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, (ITemplateKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, (ITemplateKey)dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, (ITemplateKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, (IModelKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { WorkName = "Remove Scripting SchemaDefinition", DoWork = () => { Remove(dataKey); } });
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { WorkName = "Remove Scripting SchemaDefinition", DoWork = () => { Clear(); } });
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        public void Remove(ITemplateIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        public void Remove(ISchemaDefinitionIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        public void Remove(IModelIndex dataKey)
        { Clear(); }

        /// <inheritdoc/>
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
