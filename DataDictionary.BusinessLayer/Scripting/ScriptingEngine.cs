using DataDictionary.BusinessLayer.DbWorkItem;
using System.Data;
using Toolbox.Threading;
using Toolbox.BindingTable;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.DataLayer.ScriptingData.Template;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface representing Scripting Engine data
    /// </summary>
    public interface IScriptingEngine :
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        ILoadData<IScriptingTemplateKey>, ISaveData<IScriptingTemplateKey>
    {
        /// <summary>
        /// List of Scripting Engine Templates.
        /// </summary>
        ITemplateData Templates { get; }

        /// <summary>
        /// List of Scripting Nodes for the Template
        /// </summary>
        ITemplateNodeData TemplateNodes { get; }

        /// <summary>
        /// List of Scripting Attributes for the Template
        /// </summary>
        ITemplateAttributeData TemplateAttributes { get; }

        /// <summary>
        /// List of Scripting Paths for the Template
        /// </summary>
        ITemplatePathData TemplatePaths { get; }

        /// <summary>
        /// List of Scripting Documents (output) for the Template
        /// </summary>
        ITemplateDocumentData TemplateDocuments { get; }

        /// <summary>
        /// List of Scripting Engine Column definitions
        /// </summary>
        INodePropertyData Properties { get; }
    }

    /// <summary>
    /// Implementation for Scripting Engine data
    /// </summary>
    class ScriptingEngine : IScriptingEngine, IDataTableFile
    {
        /// <summary>
        /// Reference to the containing Model
        /// </summary>
        public required IModelData Models { get; init; }

        /// <inheritdoc/>
        public ITemplateData Templates { get { return templateValues; } }
        private readonly TemplateData templateValues;

        /// <inheritdoc/>
        public ITemplateNodeData TemplateNodes { get { return nodeValues; } }
        private readonly TemplateNodeData nodeValues;

        /// <inheritdoc/>
        public ITemplateAttributeData TemplateAttributes { get { return attributeValues; } }
        private readonly TemplateAttributeData attributeValues;

        /// <inheritdoc/>
        public ITemplatePathData TemplatePaths { get { return pathValues; } }
        private readonly TemplatePathData pathValues;

        /// <inheritdoc/>
        public ITemplateDocumentData TemplateDocuments { get { return documentValues; } }
        private readonly TemplateDocumentData documentValues;

        /// <inheritdoc/>
        public INodePropertyData Properties { get { return propertyValues; } }
        private readonly NodePropertyData propertyValues;

        public ScriptingEngine() : base()
        {
            templateValues = new TemplateData() { Scripting = this };
            pathValues = new TemplatePathData();
            documentValues = new TemplateDocumentData();
            nodeValues = new TemplateNodeData();
            attributeValues = new TemplateAttributeData();
            propertyValues = new NodePropertyData();
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(templateValues.ToDataTable());
            result.Add(pathValues.ToDataTable());
            result.Add(nodeValues.ToDataTable());
            result.Add(attributeValues.ToDataTable());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public void Import(DataSet source)
        {

            if (source.Tables.Contains(templateValues.BindingName)
                && source.Tables[templateValues.BindingName] is DataTable transformTable)
            { templateValues.Load(transformTable.CreateDataReader()); }

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
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Delete());
            work.AddRange(pathValues.Delete());
            work.AddRange(nodeValues.Delete());
            work.AddRange(attributeValues.Delete());
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Delete(dataKey));
            work.AddRange(pathValues.Delete(dataKey));
            work.AddRange(nodeValues.Delete(dataKey));
            work.AddRange(attributeValues.Delete(dataKey));
            return work;
        }

        public IReadOnlyList<WorkItem> Delete(IScriptingTemplateKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Delete(dataKey));
            work.AddRange(pathValues.Delete(dataKey));
            work.AddRange(nodeValues.Delete(dataKey));
            work.AddRange(attributeValues.Delete(dataKey));
            return work;
        }

        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            List<NamedScopePair> result = new List<NamedScopePair>();
            result.AddRange(templateValues.GetNamedScopes());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Save(factory, dataKey));
            work.AddRange(pathValues.Save(factory, dataKey));
            work.AddRange(nodeValues.Save(factory, dataKey));
            work.AddRange(attributeValues.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IScriptingTemplateKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Save(factory, dataKey));
            work.AddRange(pathValues.Save(factory, dataKey));
            work.AddRange(nodeValues.Save(factory, dataKey));
            work.AddRange(attributeValues.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey));
            work.AddRange(pathValues.Load(factory, dataKey));
            work.AddRange(nodeValues.Load(factory, dataKey));
            work.AddRange(attributeValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IScriptingTemplateKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey));
            work.AddRange(pathValues.Load(factory, dataKey));
            work.AddRange(nodeValues.Load(factory, dataKey));
            work.AddRange(attributeValues.Load(factory, dataKey));
            return work;
        }


    }
}
