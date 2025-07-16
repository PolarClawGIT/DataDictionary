// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using System.Data;
using Toolbox.Threading;
using Toolbox.BindingTable;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface representing Scripting Engine data
    /// </summary>
    public interface IScriptingEngine :
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        ILoadData<IScriptingTemplateIndex>, ISaveData<IScriptingTemplateIndex>,
        IBindListChanged
    {
        /// <summary>
        /// List of Scripting Engine Templates.
        /// </summary>
        IScriptingTemplateData Templates { get; }

        /// <summary>
        /// List of Scripting Nodes for the Template
        /// </summary>
        IScriptingNodeData TemplateNodes { get; }

        /// <summary>
        /// List of Scripting Attributes for the Template
        /// </summary>
        ITemplateAttributeData TemplateAttributes { get; }

        /// <summary>
        /// List of Scripting Paths for the Template
        /// </summary>
        IScriptingPathData TemplatePaths { get; }

        /// <summary>
        /// List of Scripting Documents (output) for the Template
        /// </summary>
        IXDocumentData TemplateDocuments { get; }

        /// <summary>
        /// List of Properties for each Scope.
        /// </summary>
        IEnumerable<ScriptingNodeIndexName> Properties { get; }
    }

    /// <summary>
    /// Implementation for Scripting Engine data
    /// </summary>
    class ScriptingEngine : IScriptingEngine, IDataTableFile
    {
        /// <summary>
        /// Reference to the containing Model
        /// </summary>
        public Model Model { get; private set; }

        /// <inheritdoc/>
        public IScriptingTemplateData Templates { get { return templateValues; } }
        private readonly ScriptingTemplateData templateValues;

        /// <inheritdoc/>
        public IScriptingNodeData TemplateNodes { get { return nodeValues; } }
        private readonly ScriptingNodeData nodeValues;

        /// <inheritdoc/>
        public ITemplateAttributeData TemplateAttributes { get { return attributeValues; } }
        private readonly TemplateAttributeData attributeValues;

        /// <inheritdoc/>
        public IScriptingPathData TemplatePaths { get { return pathValues; } }
        private readonly ScriptingPathData pathValues;

        /// <inheritdoc/>
        public IXDocumentData TemplateDocuments { get { return documentValues; } }
        private readonly XDocumentData documentValues;

        // List of the XElement Builder function for each scope.
        Dictionary<ScopeType, Func<IEnumerable<XElementBuilder>>> builders = new Dictionary<ScopeType, Func<IEnumerable<XElementBuilder>>>();

        /// <inheritdoc/>
        public IEnumerable<ScriptingNodeIndexName> Properties
        {
            get
            {
                return builders.
                    SelectMany(s => s.Value().
                        Select(i => new ScriptingNodeIndexName(s.Key, i.PropertyName))); 
            }
        }

        /// <inheritdoc/>
        public Boolean RaiseListChangedEvents
        {
            get
            {
                return templateValues.RaiseListChangedEvents
                    && nodeValues.RaiseListChangedEvents
                    && attributeValues.RaiseListChangedEvents
                    && pathValues.RaiseListChangedEvents
                    && documentValues.RaiseListChangedEvents;
            }
            set
            {
                templateValues.RaiseListChangedEvents = value;
                nodeValues.RaiseListChangedEvents = value;
                attributeValues.RaiseListChangedEvents = value;
                pathValues.RaiseListChangedEvents = value;
                documentValues.RaiseListChangedEvents = value;
            }
        }

        public ScriptingEngine(Model model) : base()
        {
            Model = model;
            templateValues = new ScriptingTemplateData() { Scripting = this };
            pathValues = new ScriptingPathData();
            documentValues = new XDocumentData();
            nodeValues = new ScriptingNodeData();
            attributeValues = new TemplateAttributeData();

            // Create the Builders. This data is static once loaded.
            builders.Clear();
            builders.Add(ScopeType.ModelAttribute, () => AttributeValue.CreateXElementBuilders());
            builders.Add(ScopeType.ModelAttributeProperty, () => AttributePropertyValue.CreateXElementBuilders(model.Properties));
            builders.Add(ScopeType.ModelAttributeDefinition, () => AttributeDefinitionValue.CreateXElementBuilders(model.Definitions));
            //TODO: Add all other scriptable objects.
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<DataTable> Export()
        {
            List<DataTable> result = new List<DataTable>();
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
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Delete(dataKey));
            work.AddRange(pathValues.Delete(dataKey));
            work.AddRange(nodeValues.Delete(dataKey));
            work.AddRange(attributeValues.Delete(dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Delete(IScriptingTemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Delete(dataKey));
            work.AddRange(pathValues.Delete(dataKey));
            work.AddRange(nodeValues.Delete(dataKey));
            work.AddRange(attributeValues.Delete(dataKey));
            return work;
        }


        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
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
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IScriptingTemplateIndex dataKey)
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
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
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
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(pathValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(nodeValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(attributeValues.Load(factory, dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IScriptingTemplateIndex dataKey)
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
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IScriptingTemplateIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(pathValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(nodeValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(attributeValues.Load(factory, dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(NameSpaceSource.Load<ScriptingTemplateData, ScriptingTemplateValue>(templateValues, addNamedScope));

            return work;
        }

        /// <inheritdoc/>
        public void Remove(IModelIndex dataKey)
        {
            templateValues.Remove(dataKey);
            pathValues.Remove(dataKey);
            nodeValues.Remove(dataKey);
            attributeValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(IScriptingTemplateIndex dataKey)
        {
            templateValues.Remove(dataKey);
            pathValues.Remove(dataKey);
            nodeValues.Remove(dataKey);
            attributeValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            templateValues.Clear();
            pathValues.Clear();
            nodeValues.Clear();
            attributeValues.Clear();
        }

        /// <inheritdoc/>
        public void ResetBindings()
        {
            templateValues.ResetBindings();
            pathValues.ResetBindings();
            nodeValues.ResetBindings();
            attributeValues.ResetBindings();
        }
    }
}
