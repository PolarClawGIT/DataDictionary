using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.AppScript;
using System.Data;
using Toolbox.Threading;
using Toolbox.BindingTable;

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
        IDeleteData
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
        /// Node Owners of the SchemaDefinitions
        /// </summary>
        ISchemaNodeOwnerData SchemataNodeOwners { get; }

        /// <summary>
        /// Transforms for the Templates
        /// </summary>
        ITransformData Transforms { get; }

        /// <summary>
        /// Schema Documents for the Templates
        /// </summary>
        ISchemaDocumentData SchemaDocuments { get; }

        /// <summary>
        /// Transform Documents for the Templates
        /// </summary>
        ITransformDocumentData TransformDocuments { get; }

        /// <summary>
        /// List of <b>default</b> XmlBuilders supported by the application.
        /// </summary>
        XmlBuilderDictionary XmlBuilders { get; }

        /// <summary>
        /// Creates an empty ITemplateData.
        /// </summary>
        /// <returns></returns>
        static ITemplateData Create()
        { return new TemplateData(); }
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
        public ISchemaNodeOwnerData SchemataNodeOwners { get { return schemaNodeOwnerValues; } }
        SchemaNodeOwnerData schemaNodeOwnerValues;

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
        public XmlBuilderDictionary XmlBuilders { get; } = new XmlBuilderDictionary();

        /// <inheritdoc cref="TemplateCollection{TItem}.TemplateCollection"/>
        public TemplateData() : base()
        {
            schemaDefinitionValues = new SchemaDefinitionData();
            schemaNodeValues = new SchemaNodeData();
            schemaNodeOwnerValues = new SchemaNodeOwnerData();
            schemaDocumentValues = new SchemaDocumentData();
            transformValues = new TransformData();
            transformDocumentValues = new TransformDocumentData();
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
            work.AddRange(factory.CreateLoad(schemaNodeOwnerValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaDocumentValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateLoad(transformValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateLoad(transformDocumentValues, (IModelKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaDefinitionValues, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaNodeValues, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaNodeOwnerValues, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaDocumentValues, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(transformValues, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(transformDocumentValues, (IModelKey)dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(factory.CreateLoad(this, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaDefinitionValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaNodeValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaNodeOwnerValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaDocumentValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(transformValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(transformDocumentValues, (ITemplateKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(factory.CreateLoad(this, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaDefinitionValues, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaNodeValues, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaNodeOwnerValues, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaDocumentValues, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(transformValues, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(transformDocumentValues, (ITemplateKey)dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(factory.CreateSave(this, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(schemaDefinitionValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(schemaNodeValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(schemaNodeOwnerValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(schemaDocumentValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(transformValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(transformDocumentValues, (ITemplateKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(factory.CreateSave(this, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(schemaDefinitionValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(schemaNodeValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(schemaNodeOwnerValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(schemaDocumentValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(transformValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(transformDocumentValues, (IModelKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(new WorkItem() { WorkName = "Remove Scripting Template", DoWork = () => { Remove(dataKey); } });
            work.AddRange(schemaNodeValues.Delete(dataKey));
            work.AddRange(schemaNodeOwnerValues.Delete(dataKey));
            work.AddRange(schemaDocumentValues.Delete(dataKey));
            work.AddRange(transformValues.Delete(dataKey));
            work.AddRange(transformDocumentValues.Delete(dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(new WorkItem() { WorkName = "Remove Scripting Template", DoWork = () => { Clear(); } });
            work.AddRange(schemaDefinitionValues.Delete());
            work.AddRange(schemaNodeValues.Delete());
            work.AddRange(schemaNodeOwnerValues.Delete());
            work.AddRange(schemaDocumentValues.Delete());
            work.AddRange(transformValues.Delete());
            work.AddRange(transformDocumentValues.Delete());
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
            schemaNodeOwnerValues.Remove(dataKey);
            schemaDocumentValues.Remove(dataKey);
            transformValues.Remove(dataKey);
            transformDocumentValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(IModelIndex dataKey)
        {
            base.Clear();

            schemaDefinitionValues.Clear();
            schemaNodeValues.Clear();
            schemaNodeOwnerValues.Clear();
            schemaDocumentValues.Clear();
            transformValues.Clear();
            transformDocumentValues.Clear();
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
            result.Add(schemaNodeOwnerValues.ToDataTable());
            result.Add(schemaDocumentValues.ToDataTable());
            result.Add(transformValues.ToDataTable());
            result.Add(transformDocumentValues.ToDataTable());

            return result;
        }

        /// <inheritdoc/>
        public void Import(DataSet source)
        {
            this.Import(source);
            schemaDefinitionValues.Load(source);
            schemaNodeValues.Load(source);
            schemaNodeOwnerValues.Load(source);
            schemaDocumentValues.Load(source);
            transformValues.Load(source);
            transformDocumentValues.Load(source);
        }
    }
}
