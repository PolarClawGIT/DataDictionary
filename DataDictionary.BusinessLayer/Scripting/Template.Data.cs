// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.AppScript;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Template
    /// </summary>
    public interface ITemplateData : IBindingData<TemplateValue>
    { }

    class TemplateData : ScriptingTemplateCollection<TemplateValue>, ITemplateData, 
        ILoadData<ITemplateIndex>, ISaveData<ITemplateIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        /// <summary>
        /// Reference to the containing ScriptingEngine
        /// </summary>
        public required ScriptingEngine Scripting { get; init; }

        public TemplateData() : base()
        { }

        /// <inheritdoc/>
        /// <remarks>TemplatePath</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TemplatePath</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TemplatePath</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey)
        { return factory.CreateLoad(this, (IScriptingTemplateKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TemplatePath</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IScriptingTemplateKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ITemplateIndex dataKey)
        { return factory.CreateSave(this, (IScriptingTemplateKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Delete(ITemplateIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Template", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Template", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public void Import(DataSet source)
        {
            if (source.Tables.Contains(this.BindingName)
                && source.Tables[this.BindingName] is DataTable transformTable)
            { this.Load(transformTable.CreateDataReader()); }
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<DataTable> Export()
        { return this.ToDataTable().ToList(); }

        /// <inheritdoc/>
        /// <remarks>TemplatePath</remarks>
        public void Remove(ITemplateIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>TemplatePath</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }
    }
}
