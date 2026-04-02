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
        IGetTemporal<IModelIndex>, IGetTemporal<ITemplateIndex>,
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
        /// Objects for the Templates
        /// </summary>
        ITemplateObjectData Objects { get; }

        /// <summary>
        /// Transforms for the Templates
        /// </summary>
        ITransformData Transforms { get; }

        /// <summary>
        /// Documents for the Templates
        /// </summary>
        IDocumentData Documents { get; }

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
        public ITemplateObjectData Objects { get { return templateObjectValues; } }
        TemplateObjectData templateObjectValues;

        /// <inheritdoc/>
        public ITransformData Transforms { get { return transformValues; } }
        TransformData transformValues;

        /// <inheritdoc/>
        public IDocumentData Documents { get { return documentValues; } }
        DocumentData documentValues;


        public TemplateData() : base()
        {
            schemaDefinitionValues = new SchemaDefinitionData();
            schemaNodeValues = new SchemaNodeData();
            schemaNodeOwnerValues = new SchemaNodeOwnerData();
            templateObjectValues = new TemplateObjectData();
            transformValues = new TransformData();
            documentValues = new DocumentData();
        }

        /// <inheritdoc/>
        /// <remarks>Only loads the base Templates, not the child objects.</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this));
            //work.AddRange(factory.CreateLoad(schemaNodeValues)); 
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
            work.Add(factory.CreateLoad(this, (IModelKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaNodeValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaNodeValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaNodeOwnerValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateLoad(templateObjectValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateLoad(documentValues, (IModelKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaNodeValues, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaNodeValues, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaNodeOwnerValues, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(templateObjectValues, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(documentValues, (IModelKey)dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaNodeValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaNodeValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(schemaNodeOwnerValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(templateObjectValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateLoad(documentValues, (ITemplateKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaNodeValues, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaNodeValues, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(schemaNodeOwnerValues, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(templateObjectValues, (ITemplateKey)dataKey, asOfUtcDate));
            work.AddRange(factory.CreateLoad(documentValues, (ITemplateKey)dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(schemaNodeValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(schemaNodeValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(schemaNodeOwnerValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(templateObjectValues, (ITemplateKey)dataKey));
            work.AddRange(factory.CreateSave(documentValues, (ITemplateKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(schemaNodeValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(schemaNodeValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(schemaNodeOwnerValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(templateObjectValues, (IModelKey)dataKey));
            work.AddRange(factory.CreateSave(documentValues, (IModelKey)dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(ITemplateIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { WorkName = "Remove Scripting Template", DoWork = () => { Remove(dataKey); } });

            work.AddRange(schemaNodeValues.Delete(dataKey));
            work.AddRange(schemaNodeValues.Delete(dataKey));
            work.AddRange(schemaNodeOwnerValues.Delete(dataKey));
            work.AddRange(templateObjectValues.Delete(dataKey));
            work.AddRange(documentValues.Delete(dataKey));

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { WorkName = "Remove Scripting Template", DoWork = () => { Clear(); } });

            work.AddRange(schemaNodeValues.Delete());
            work.AddRange(schemaNodeValues.Delete());
            work.AddRange(schemaNodeOwnerValues.Delete());
            work.AddRange(templateObjectValues.Delete());
            work.AddRange(documentValues.Delete());

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        public void Remove(ITemplateIndex dataKey)
        {
            base.Remove(dataKey);

            schemaNodeValues.Remove(dataKey);
            schemaNodeValues.Remove(dataKey);
            schemaNodeOwnerValues.Remove(dataKey);
            templateObjectValues.Remove(dataKey);
            documentValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(IModelIndex dataKey)
        {
            base.Clear();

            schemaNodeValues.Clear();
            schemaNodeValues.Clear();
            schemaNodeOwnerValues.Clear();
            templateObjectValues.Clear();
            documentValues.Clear();
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
        public IReadOnlyList<DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();

            result.Add(this.ToDataTable());
            result.Add(schemaNodeValues.ToDataTable());
            result.Add(schemaNodeValues.ToDataTable());
            result.Add(schemaNodeOwnerValues.ToDataTable());
            result.Add(templateObjectValues.ToDataTable());
            result.Add(documentValues.ToDataTable());

            return result;
        }

        /// <inheritdoc/>
        public void Import(DataSet source)
        { 
            this.Import(source);
            schemaNodeValues.Load(source);
            schemaNodeValues.Load(source);
            schemaNodeOwnerValues.Load(source);
            templateObjectValues.Load(source);
            documentValues.Load(source);
        }        
    }
}
