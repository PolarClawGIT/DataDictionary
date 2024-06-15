using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ScriptingData.Template;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Template
    /// </summary>
    public interface ITemplateData : IBindingData<TemplateValue>, ILoadData<ITemplateIndex>, ISaveData<ITemplateIndex>
    {
        /// <summary>
        /// List of Scripting Nodes for the Template
        /// </summary>
        ITemplateNodeData Nodes { get; }

        /// <summary>
        /// List of Scripting Attributes for the Template
        /// </summary>
        ITemplateAttributeData Attributes { get; }

        /// <summary>
        /// List of Scripting Paths for the Template
        /// </summary>
        ITemplatePathData Paths { get; }

        /// <summary>
        /// List of Scripting Documents (output) for the Template
        /// </summary>
        ITemplateDocumentData Documents { get; }


    }

    class TemplateData : ScriptingTemplateCollection<TemplateValue>, ITemplateData, INamedScopeSource,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        ILoadData<IScriptingTemplateKey>, ISaveData<IScriptingTemplateKey>,
        IDataTableFile
    {
        /// <summary>
        /// Reference to the containing ScriptingEngine
        /// </summary>
        public required ScriptingEngine Scripting { get; init; }

        /// <inheritdoc/>
        public ITemplateNodeData Nodes { get { return nodeValues; } }
        private readonly TemplateNodeData nodeValues;

        /// <inheritdoc/>
        public ITemplateAttributeData Attributes { get { return attributeValues; } }
        private readonly TemplateAttributeData attributeValues;

        /// <inheritdoc/>
        public ITemplatePathData Paths { get { return pathValues; } }
        private readonly TemplatePathData pathValues;

        /// <inheritdoc/>
        public ITemplateDocumentData Documents { get { return documentValues; } }
        private readonly TemplateDocumentData documentValues;

        public TemplateData() : base()
        {
            pathValues = new TemplatePathData();
            documentValues = new TemplateDocumentData();
            nodeValues = new TemplateNodeData();
            attributeValues = new TemplateAttributeData();
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey)
        { return Load(factory, (IScriptingTemplateKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IScriptingTemplateKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, dataKey));
            work.Add(factory.CreateLoad(pathValues, dataKey));
            work.Add(factory.CreateLoad(nodeValues, dataKey));
            work.Add(factory.CreateLoad(attributeValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, dataKey));
            work.Add(factory.CreateLoad(pathValues, dataKey));
            work.Add(factory.CreateLoad(nodeValues, dataKey));
            work.Add(factory.CreateLoad(attributeValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ITemplateIndex dataKey)
        { return Save(factory, (IScriptingTemplateKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IScriptingTemplateKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, dataKey));
            work.Add(factory.CreateSave(pathValues, dataKey));
            work.Add(factory.CreateSave(nodeValues, dataKey));
            work.Add(factory.CreateSave(attributeValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, dataKey));
            work.Add(factory.CreateSave(pathValues, dataKey));
            work.Add(factory.CreateSave(nodeValues, dataKey));
            work.Add(factory.CreateSave(attributeValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public override void Remove(IScriptingTemplateKey templateKey)
        {
            pathValues.Remove(templateKey);
            base.Remove(templateKey);
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Delete(ITemplateIndex dataKey)
        { return Delete((IScriptingTemplateKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Delete(IScriptingTemplateKey dataKey)
        { return new WorkItem() { WorkName = "Remove Template", DoWork = () => { Remove((IScriptingTemplateKey)dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { WorkName = "Remove Template", DoWork = () => { this.Clear(); } });
            work.AddRange(pathValues.Delete());
            work.AddRange(nodeValues.Delete());
            work.AddRange(attributeValues.Delete());
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public void Import(DataSet source)
        {
            if (source.Tables.Contains(this.BindingName)
                && source.Tables[this.BindingName] is DataTable transformTable)
            { this.Load(transformTable.CreateDataReader()); }

            if (source.Tables.Contains(pathValues.BindingName)
                && source.Tables[pathValues.BindingName] is DataTable pathTable)
            { pathValues.Load(pathTable.CreateDataReader()); }

            if (source.Tables.Contains(nodeValues.BindingName)
                && source.Tables[nodeValues.BindingName] is DataTable nodeTable)
            { pathValues.Load(nodeTable.CreateDataReader()); }

            if (source.Tables.Contains(attributeValues.BindingName)
                && source.Tables[attributeValues.BindingName] is DataTable attributeTable)
            { pathValues.Load(attributeTable.CreateDataReader()); }
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(this.ToDataTable());
            result.Add(pathValues.ToDataTable());
            result.Add(nodeValues.ToDataTable());
            result.Add(attributeValues.ToDataTable());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            return this.Select(s => new NamedScopePair(GetValue(s)));

            NamedScopeValueCore GetValue(TemplateValue source)
            {
                NamedScopeValueCore result = new NamedScopeValueCore(source);
                source.PropertyChanged += Source_PropertyChanged;

                return result;

                void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName is
                        nameof(source.TemplateTitle))
                    { result.TitleChanged(); }
                }
            }
        }

    }
}
