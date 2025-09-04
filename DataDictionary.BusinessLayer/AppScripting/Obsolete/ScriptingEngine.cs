// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using System.Data;
using Toolbox.Threading;
using Toolbox.BindingTable;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface representing Scripting Engine data
    /// </summary>
    [Obsolete("replace", true)]
    public interface IScriptingEngine :
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        ILoadData<ITemplateIndex>, ISaveData<ITemplateIndex>,
        IBindListChanged
    {
        /// <summary>
        /// List of Scripting Engine Templates.
        /// </summary>
        ITemplateData Templates { get; }

        /// <summary>
        /// List of Scripting Elements for the Template
        /// </summary>
        ITemplateElementData Elements { get; }

        /// <summary>
        /// List of Scripting Attributes for the Template
        /// </summary>
        ITemplateAttributeData Attributes { get; }

        /// <summary>
        /// List of Scripting Node/Attribute owners for the Template.
        /// </summary>
        ITemplateNodeOwnerData AttributeOwners { get; }

        /// <summary>
        /// List of Scripting Data Sources
        /// </summary>
        IDataSourceData DataSources { get; }

        /// <summary>
        /// List of Scripting Data Objects within a Data Source.
        /// </summary>
        IDataObjectData DataObjects { get; }

        /// <summary>
        /// List of Scripting Data Sources asscoated with a Templates.
        /// </summary>
        ITemplateInputData TemplateSources { get; }


        //IXDocumentData Documents { get; }
    }

    /// <summary>
    /// Implementation for Scripting Engine data
    /// </summary>
    [Obsolete("replace", true)]
    class ScriptingEngine : IScriptingEngine, IDataTableFile
    {
        /// <inheritdoc/>
        public ITemplateData Templates { get { return templateValues; } }
        TemplateData templateValues = new TemplateData();

        /// <inheritdoc/>
        public ITemplateElementData Elements { get { return templateElements; } }
        TemplateElementData templateElements = new TemplateElementData();

        /// <inheritdoc/>
        public ITemplateAttributeData Attributes { get { return templateAttributes; } }
        TemplateAttributeData templateAttributes = new TemplateAttributeData();

        /// <inheritdoc/>
        public ITemplateNodeOwnerData AttributeOwners { get { return templateNodeOwners; } }
        TemplateNodeOwnerData templateNodeOwners = new TemplateNodeOwnerData();

        /// <inheritdoc/>
        public IDataSourceData DataSources { get { return sourceValues; } }
        DataSourceData sourceValues = new DataSourceData();

        /// <inheritdoc/>
        public IDataObjectData DataObjects { get { return sourceObjects; } }
        DataObjectData sourceObjects = new DataObjectData();

        /// <inheritdoc/>
        public ITemplateInputData TemplateSources { get { return templateSources; } }
        TemplateInputData templateSources = new TemplateInputData();

        // List of the XElement Builder function for each scope.
        Dictionary<ScopeType, Func<IEnumerable<XElementBuilder>>> builders = new Dictionary<ScopeType, Func<IEnumerable<XElementBuilder>>>();

        /// <summary>
        /// Constructor for the SpriptingEngine.
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="definitions"></param>
        public ScriptingEngine(IPropertyData properties, IDefinitionData definitions) : base()
        {
            // Create the Builders. This data is static once loaded.
            builders.Clear();
            //builders.Add(ScopeType.ModelAttribute, () => AttributeValue.CreateXElementBuilders());
            //builders.Add(ScopeType.ModelAttributeProperty, () => AttributePropertyValue.CreateXElementBuilders(properties));
            //builders.Add(ScopeType.ModelAttributeDefinition, () => AttributeDefinitionValue.CreateXElementBuilders(definitions));
         
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<DataTable> Export()
        {
            List<DataTable> work = new List<DataTable>();
            work.Add(templateValues.ToDataTable());
            work.Add(templateElements.ToDataTable());
            work.Add(templateAttributes.ToDataTable());
            work.Add(templateNodeOwners.ToDataTable());
            work.Add(sourceValues.ToDataTable());
            work.Add(sourceObjects.ToDataTable());
            work.Add(templateSources.ToDataTable());
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public void Import(DataSet source)
        {
            templateValues.Load(GetTable(templateValues.BindingName));
            templateElements.Load(GetTable(templateElements.BindingName));
            templateAttributes.Load(GetTable(templateAttributes.BindingName));
            templateNodeOwners.Load(GetTable(templateNodeOwners.BindingName));
            sourceValues.Load(GetTable(sourceValues.BindingName));
            sourceObjects.Load(GetTable(sourceObjects.BindingName));
            templateSources.Load(GetTable(templateSources.BindingName));

            DataTableReader GetTable(String tableName)
            {
                if (source.Tables.Contains(tableName) && source.Tables[tableName] is DataTable sourceTable)
                { return sourceTable.CreateDataReader(); }
                else
                {
                    Exception ex = new IndexOutOfRangeException();
                    ex.Data.Add(nameof(tableName), tableName);
                    ex.Data.Add(nameof(source.Tables),
                        String.Join(",", source.Tables.OfType<DataTable>().Select(s => s.TableName)));
                    throw ex;
                }
            }
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey));
            work.AddRange(templateElements.Load(factory, dataKey));
            work.AddRange(templateAttributes.Load(factory, dataKey));
            work.AddRange(templateNodeOwners.Load(factory, dataKey));
            work.AddRange(sourceValues.Load(factory, dataKey));
            work.AddRange(sourceObjects.Load(factory, dataKey));
            work.AddRange(templateSources.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateElements.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateAttributes.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateNodeOwners.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(sourceValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(sourceObjects.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateSources.Load(factory, dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey));
            work.AddRange(templateElements.Load(factory, dataKey));
            work.AddRange(templateAttributes.Load(factory, dataKey));
            work.AddRange(templateNodeOwners.Load(factory, dataKey));
            work.AddRange(sourceValues.Load(factory, dataKey));
            work.AddRange(sourceObjects.Load(factory, dataKey));
            work.AddRange(templateSources.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateElements.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateAttributes.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateNodeOwners.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(sourceValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(sourceObjects.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(templateSources.Load(factory, dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Save(factory, dataKey));
            work.AddRange(templateElements.Save(factory, dataKey));
            work.AddRange(templateAttributes.Save(factory, dataKey));
            work.AddRange(templateNodeOwners.Save(factory, dataKey));
            work.AddRange(sourceValues.Save(factory, dataKey));
            work.AddRange(sourceObjects.Save(factory, dataKey));
            work.AddRange(templateSources.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Save(factory, dataKey));
            work.AddRange(templateElements.Save(factory, dataKey));
            work.AddRange(templateAttributes.Save(factory, dataKey));
            work.AddRange(templateNodeOwners.Save(factory, dataKey));
            //work.AddRange(sourceValues.Save(factory, dataKey));
            //work.AddRange(sourceObjects.Save(factory, dataKey));
            work.AddRange(templateSources.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Delete(ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Delete(dataKey));
            work.AddRange(templateValues.Delete(dataKey));
            work.AddRange(templateElements.Delete(dataKey));
            work.AddRange(templateAttributes.Delete(dataKey));
            work.AddRange(templateNodeOwners.Delete(dataKey));
            //work.AddRange(sourceValues.Delete(dataKey));
            //work.AddRange(sourceObjects.Delete(dataKey));
            work.AddRange(templateSources.Delete(dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Delete(dataKey));
            work.AddRange(templateValues.Delete(dataKey));
            work.AddRange(templateElements.Delete(dataKey));
            work.AddRange(templateAttributes.Delete(dataKey));
            work.AddRange(templateNodeOwners.Delete(dataKey));
            work.AddRange(sourceValues.Delete(dataKey));
            work.AddRange(sourceObjects.Delete(dataKey));
            work.AddRange(templateSources.Delete(dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(templateValues.Delete());
            work.AddRange(templateValues.Delete());
            work.AddRange(templateElements.Delete());
            work.AddRange(templateAttributes.Delete());
            work.AddRange(templateNodeOwners.Delete());
            work.AddRange(sourceValues.Delete());
            work.AddRange(sourceObjects.Delete());
            work.AddRange(templateSources.Delete());
            return work;
        }

        /// <inheritdoc/>
        public void Remove(IModelIndex dataKey)
        {
            templateValues.Remove(dataKey);
            templateElements.Remove(dataKey);
            templateAttributes.Remove(dataKey);
            templateNodeOwners.Remove(dataKey);
            sourceValues.Remove(dataKey);
            sourceObjects.Remove(dataKey);
            templateSources.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(ITemplateIndex dataKey)
        {
            templateValues.Remove(dataKey);
            templateElements.Remove(dataKey);
            templateAttributes.Remove(dataKey);
            templateNodeOwners.Remove(dataKey);
            //sourceValues.Remove(dataKey);
            //sourceObjects.Remove(dataKey);
            templateSources.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(IDataSourceIndex dataKey)
        {
            //templateValues.Remove(dataKey);
            //templateElements.Remove(dataKey);
            //templateAttributes.Remove(dataKey);
            //templateNodeOwners.Remove(dataKey);
            sourceValues.Remove(dataKey);
            sourceObjects.Remove(dataKey);
            templateSources.Remove(dataKey);
        }


        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {   throw new NotImplementedException(); }

        /// <inheritdoc/>
        public void Clear()
        {
            templateValues.Clear();
            templateElements.Clear();
            templateAttributes.Clear();
            templateNodeOwners.Clear();
            sourceValues.Clear();
            sourceObjects.Clear();
            templateSources.Clear();
        }

        /// <inheritdoc/>
        public void ResetBindings()
        {
            templateValues.ResetBindings();
            templateElements.ResetBindings();
            templateAttributes.ResetBindings();
            templateNodeOwners.ResetBindings();
            sourceValues.ResetBindings();
            sourceObjects.ResetBindings();
            templateSources.ResetBindings();
        }



        /// <inheritdoc/>
        public Boolean RaiseListChangedEvents
        {
            get
            {
                return templateValues.RaiseListChangedEvents
                    && templateElements.RaiseListChangedEvents
                    && templateAttributes.RaiseListChangedEvents
                    && templateNodeOwners.RaiseListChangedEvents
                    && sourceValues.RaiseListChangedEvents
                    && sourceObjects.RaiseListChangedEvents
                    && templateSources.RaiseListChangedEvents;
            }
            set
            {
                templateValues.RaiseListChangedEvents = value;
                templateElements.RaiseListChangedEvents = value;
                templateAttributes.RaiseListChangedEvents = value;
                templateNodeOwners.RaiseListChangedEvents = value;
                sourceValues.RaiseListChangedEvents = value;
                sourceObjects.RaiseListChangedEvents = value;
                templateSources.RaiseListChangedEvents = value;
            }
        }
    }
}
