using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.AppScript;
using System.Data;
using Toolbox.Threading;
using Toolbox.BindingTable;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Collections;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface component for the Scripting Template
    /// </summary>
    public interface ITemplateData :
        IBindingData<TemplateValue>,
        IGetTemporal, IGetTemporal<IModelIndex>, IGetTemporal<ITemplateIndex>,
        ILoadData, ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        ILoadData<ITemplateIndex>, ISaveData<ITemplateIndex>,
        IDeleteData, IRemoveData<ISchemaDefinitionIndex>, IRemoveData<ITransformIndex>
    {
        /// <summary>
        /// SchemaDefinition for the Templates (XSD)
        /// </summary>
        ISchemaDefinitionData Schemata { get; }

        /// <summary>
        /// Nodes of the SchemaDefinitions
        /// </summary>
        ISchemaNodeData SchemataNodes { get; }

        /// <summary>
        /// Schema Documents for the Templates
        /// </summary>
        ISchemaDocumentData SchemaDocuments { get; }

        /// <summary>
        /// Transforms for the Templates
        /// </summary>
        ITransformData Transforms { get; }

        /// <summary>
        /// Transform Documents for the Templates
        /// </summary>
        ITransformDocumentData TransformDocuments { get; }

        /// <summary>
        /// The files/documents of the templates
        /// </summary>
        ITemplateDocumentData Documents { get; }

        /// <summary>
        /// List of <b>default</b> XmlBuilders supported by the application.
        /// </summary>
        IEnumerable <XmlBuilder> XmlBuilders { get; }

        /// <summary>
        /// Creates an empty ITemplateData.
        /// </summary>
        /// <returns></returns>
        static ITemplateData Create()
        { return new TemplateData(); }

        /// <summary>
        /// Try to Create and Add a TemplateValue.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        Boolean TryCreate([NotNullWhen(true)] out TemplateValue? template, [NotNullWhen(false)] out Exception? exception);

        /// <summary>
        /// Given the Template, Try to Create and Add a SchemaDefinition.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="schema"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        Boolean TryCreate(ITemplateIndex template, [NotNullWhen(true)] out SchemaDefinitionValue? schema, [NotNullWhen(false)] out Exception? exception);

        /// <summary>
        /// Given the SchemaDefinition, Try to Create and Add a SchemaNode.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="schemaNode"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        Boolean TryCreate(ISchemaDefinitionIndex schema, [NotNullWhen(true)] out SchemaNodeValue? schemaNode, [NotNullWhen(false)] out Exception? exception);

        /// <summary>
        /// Given the SchemaDefinition, Try to Create and Add a SchemaDocument.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="schemaDocument"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        Boolean TryCreate(ISchemaDefinitionIndex schema, [NotNullWhen(true)] out SchemaDocumentValue? schemaDocument, [NotNullWhen(false)] out Exception? exception);

        /// <summary>
        /// Given the Template, Try to Create and Add a TransformValue.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="transform"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        Boolean TryCreate(ITemplateIndex template, [NotNullWhen(true)] out TransformValue? transform, [NotNullWhen(false)] out Exception? exception);

        /// <summary>
        /// Given the Transform, Try to Create and Add a TransformDocumentValue.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="transformDocument"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        Boolean TryCreate(ITransformIndex transform, [NotNullWhen(true)] out TransformDocumentValue? transformDocument, [NotNullWhen(false)] out Exception? exception);

        /// <summary>
        /// Given the Transform, Try to Create and Add a TemplateDocumentValue.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="templateDocument"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        Boolean TryCreate(ITemplateIndex template, [NotNullWhen(true)] out TemplateDocumentValue? templateDocument, [NotNullWhen(false)] out Exception? exception);
    }

    class TemplateData : TemplateCollection<TemplateValue>, ITemplateData,
        IDataTableFile
    {
        /// <inheritdoc/>
        public ISchemaDefinitionData Schemata { get { return schemaDefinitionValues; } }
        SchemaDefinitionData schemaDefinitionValues;

        /// <inheritdoc/>
        public ISchemaNodeData SchemataNodes { get { return schemaNodeValues; } }
        SchemaNodeData schemaNodeValues;

        /// <inheritdoc/>
        public ITransformData Transforms { get { return transformValues; } }
        TransformData transformValues;

        /// <inheritdoc/>
        public ISchemaDocumentData SchemaDocuments { get { return schemaDocumentValues; } }
        SchemaDocumentData schemaDocumentValues;

        /// <inheritdoc/>
        public ITransformDocumentData TransformDocuments { get { return transformDocumentValues; } }
        TransformDocumentData transformDocumentValues;

        /// <inheritdoc/>
        public ITemplateDocumentData Documents { get { return templateDocumentValues; } }
        TemplateDocumentData templateDocumentValues;

        /// <inheritdoc/>
        public IEnumerable<XmlBuilder> XmlBuilders { get { return xmlBuilderValues; } }
        static Collection<XmlBuilder> xmlBuilderValues = new Collection<XmlBuilder>();

        /// <inheritdoc cref="TemplateCollection{TItem}.TemplateCollection"/>
        public TemplateData() : base()
        {
            schemaDefinitionValues = new SchemaDefinitionData();
            schemaNodeValues = new SchemaNodeData();
            schemaDocumentValues = new SchemaDocumentData();
            transformValues = new TransformData();
            transformDocumentValues = new TransformDocumentData();
            templateDocumentValues = new TemplateDocumentData();
        }

        /// <inheritdoc/>
        /// <remarks>Only loads the base Templates, not the child objects.</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this));
            //work.AddRange(factory.CreateLoad(schemaDefinitionValues)); 
            //work.AddRange(factory.CreateLoad(schemaNodeValues));
            //work.AddRange(factory.CreateLoad(schemaNodeOwnerValues));
            //work.AddRange(factory.CreateLoad(templateObjectValues));
            //work.AddRange(factory.CreateLoad(documentValues));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(factory.CreateLoad(this, (IModelKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaDefinitionValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaNodeValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaDocumentValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateLoad(transformValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateLoad(transformDocumentValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateLoad(templateDocumentValues, (IModelKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaDefinitionValues, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaNodeValues, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaDocumentValues, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(transformValues, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(transformDocumentValues, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(templateDocumentValues, (IModelKey)dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(factory.CreateLoad(this, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaDefinitionValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaNodeValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaDocumentValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(transformValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(transformDocumentValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(templateDocumentValues, (ITemplateKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(factory.CreateLoad(this, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaDefinitionValues, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaNodeValues, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaDocumentValues, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(transformValues, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(transformDocumentValues, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(templateDocumentValues, (ITemplateKey)dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(factory.CreateSave(this, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(schemaDefinitionValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(schemaNodeValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(schemaDocumentValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(transformValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(transformDocumentValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(templateDocumentValues, (ITemplateKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(factory.CreateSave(this, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(schemaDefinitionValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(schemaNodeValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(schemaDocumentValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(transformValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(transformDocumentValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(templateDocumentValues, (IModelKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(new WorkItem() { WorkName = "Remove Scripting Template", DoWork = () => { Remove(dataKey); } });
            work.AddRange(schemaNodeValues.Delete(dataKey));
            work.AddRange(schemaDocumentValues.Delete(dataKey));
            work.AddRange(transformValues.Delete(dataKey));
            work.AddRange(transformDocumentValues.Delete(dataKey));
            work.AddRange(templateDocumentValues.Delete(dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(new WorkItem() { WorkName = "Remove Scripting Template", DoWork = () => { Clear(); } });
            work.AddRange(schemaDefinitionValues.Delete());
            work.AddRange(schemaNodeValues.Delete());
            work.AddRange(schemaDocumentValues.Delete());
            work.AddRange(transformValues.Delete());
            work.AddRange(transformDocumentValues.Delete());
            work.AddRange(templateDocumentValues.Delete());
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        public void Remove(ITemplateIndex dataKey)
        {
            base.Remove(dataKey);

            schemaDefinitionValues.Remove(dataKey);
            schemaNodeValues.Remove(dataKey);
            schemaDocumentValues.Remove(dataKey);
            transformValues.Remove(dataKey);
            transformDocumentValues.Remove(dataKey);
            templateDocumentValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(IModelIndex dataKey)
        {
            base.Clear();

            schemaDefinitionValues.Clear();
            schemaNodeValues.Clear();
            schemaDocumentValues.Clear();
            transformValues.Clear();
            transformDocumentValues.Clear();
            templateDocumentValues.Clear();
        }

        /// <inheritdoc/>
        public void Remove(ISchemaDefinitionIndex dataKey)
        {
            schemaDefinitionValues.Remove(dataKey);
            schemaNodeValues.Remove(dataKey);
            schemaDocumentValues.Remove(dataKey);
            templateDocumentValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(ITransformIndex dataKey)
        {
            transformValues.Remove(dataKey);
            transformDocumentValues.Remove(dataKey);
            templateDocumentValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public ITemporalData GetTemporal(IModelIndex model)
        {
            return new TemporalData<TemplateData, TemplateValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data, (IModelKey)model) };
        }

        /// <inheritdoc/>
        public ITemporalData GetTemporal(ITemplateIndex template)
        {
            return new TemporalData<TemplateData, TemplateValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data, (ITemplateKey)template) };
        }

        /// <inheritdoc/>
        public ITemporalData GetTemporal()
        {
            return new TemporalData<TemplateData, TemplateValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data) };
        }

        /// <inheritdoc/>
        public IReadOnlyList<DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();

            result.Add(this.ToDataTable());
            result.Add(schemaDefinitionValues.ToDataTable());
            result.Add(schemaNodeValues.ToDataTable());
            result.Add(schemaDocumentValues.ToDataTable());
            result.Add(transformValues.ToDataTable());
            result.Add(transformDocumentValues.ToDataTable());
            result.Add(templateDocumentValues.ToDataTable());

            return result;
        }

        /// <inheritdoc/>
        public void Import(DataSet source)
        {
            this.Import(source);
            schemaDefinitionValues.Load(source);
            schemaNodeValues.Load(source);
            schemaDocumentValues.Load(source);
            transformValues.Load(source);
            transformDocumentValues.Load(source);
            templateDocumentValues.Load(source);
        }

        /// <summary>
        /// Adds Xml Builders to the list.
        /// </summary>
        /// <param name="builders"></param>
        /// <exception cref="ArgumentException">
        /// A XmlBuilder with the same key already exists in the list. Duplicates are not allowed.
        /// </exception>
        public IReadOnlyList<WorkItem> AddBuilders(Func<IEnumerable<XmlBuilder>> builders)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                DoWork = () =>
                {
                    foreach (var item in builders().ToList())
                    {
                        XmlBuilderIndex key = new XmlBuilderIndex(item);
                        if (xmlBuilderValues.Any(w => key.Equals(w)))
                        {
                            Exception ex = new ArgumentException("Duplicate Key");
                            ex.Data.Add(nameof(key), key);
                            throw ex;
                        }
                        else
                        { xmlBuilderValues.Add(item); }
                    }
                }
            });

            return work;

        }

        /// <inheritdoc/>
        /// <remarks>Does not generate exceptions.</remarks>
        public Boolean TryCreate([NotNullWhen(true)] out TemplateValue? template, [NotNullWhen(false)] out Exception? exception)
        {   
            exception = null;
            template = new TemplateValue();
            Add(template);
            return exception is null;
        }

        /// <inheritdoc/>
        public Boolean TryCreate(ITemplateIndex template, [NotNullWhen(true)] out SchemaDefinitionValue? schema, [NotNullWhen(false)] out Exception? exception)
        {
            schema = null;
            TemplateIndex key = new TemplateIndex(template);

            if (this.TryGetSingle(w => key.Equals(w), out TemplateValue? templateValue, out exception))
            {
                schema = new SchemaDefinitionValue(templateValue)
                {
                    RootFolder = templateValue.RootFolder,
                    RelativePath = templateValue.RelativePath
                };

                Schemata.Add(schema);
            }

            return exception is null;
        }


        /// <inheritdoc/>
        public Boolean TryCreate(ISchemaDefinitionIndex schema, [NotNullWhen(true)] out SchemaNodeValue? schemaNode, [NotNullWhen(false)] out Exception? exception)
        {
            SchemaDefinitionIndex key = new SchemaDefinitionIndex(schema);
            schemaNode = null;

            if (schemaDefinitionValues.TryGetSingle(w => key.Equals(w), out SchemaDefinitionValue? schemaValue, out exception))
            {
                TemplateIndex template = new TemplateIndex(schemaValue);
                schemaNode = new SchemaNodeValue(template, schemaValue);
                schemaNodeValues.Add(schemaNode);
            }

            return exception is null;
        }

        /// <inheritdoc/>
        public Boolean TryCreate(ISchemaDefinitionIndex schema, [NotNullWhen(true)] out SchemaDocumentValue? schemaDocument, [NotNullWhen(false)] out Exception? exception)
        {
            SchemaDefinitionIndex key = new SchemaDefinitionIndex(schema);
            schemaDocument = null;

            if (schemaDefinitionValues.TryGetSingle(w => key.Equals(w), out SchemaDefinitionValue? schemaValue, out exception))
            {
                TemplateIndex template = new TemplateIndex(schemaValue);
                schemaDocument = new SchemaDocumentValue(template, schemaValue);
                schemaDocumentValues.Add(schemaDocument);
            }

            return exception is null;
        }

        /// <inheritdoc/>
        public Boolean TryCreate(ITemplateIndex template, [NotNullWhen(true)] out TransformValue? transform, [NotNullWhen(false)] out Exception? exception)
        {
            TemplateIndex key = new TemplateIndex(template);
            transform = null;

            if (this.TryGetSingle(w => key.Equals(w), out TemplateValue? templateValue, out exception))
            {
                transform = new TransformValue(templateValue)
                {
                    RootFolder = templateValue.RootFolder,
                    RelativePath = templateValue.RelativePath
                };

                transformValues.Add(transform);
            }

            return exception is null;
        }

        /// <inheritdoc/>
        public Boolean TryCreate(ITransformIndex transform, [NotNullWhen(true)] out TransformDocumentValue? transformDocument, [NotNullWhen(false)] out Exception? exception)
        {
            TransformIndex key = new TransformIndex(transform);
            transformDocument = null;

            if (transformValues.TryGetSingle(w => key.Equals(w), out TransformValue? transformValue, out exception))
            {
                TemplateIndex template = new TemplateIndex(transformValue);
                transformDocument = new TransformDocumentValue(template, transformValue);
                transformDocumentValues.Add(transformDocument);
            }

            return exception is null;
        }

        /// <inheritdoc/>
        public Boolean TryCreate(ITemplateIndex template, [NotNullWhen(true)] out TemplateDocumentValue? templateDocument, [NotNullWhen(false)] out Exception? exception)
        {
            templateDocument = null;
            TemplateIndex key = new TemplateIndex(template);

            if (this.TryGetSingle(w => key.Equals(w), out TemplateValue? templateValue, out exception))
            {
                templateDocument = new TemplateDocumentValue(templateValue);
                Documents.Add(templateDocument);
            }

            return exception is null;
        }
    }
}
