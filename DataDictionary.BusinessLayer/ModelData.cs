using DataDictionary.BusinessLayer.WorkItem;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.DomainData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Represents the main data object used by the application.
    /// </summary>
    public class ModelData
    {
        public required IWorkerQueue WorkerQueue { get; init; }

        public IModelItem Model { get; } = new ModelItem();
        public FileInfo? ModelFile { get; private set; }

        // Database Model
        public BindingTable<DbCatalogItem> DbCatalogs { get; } = ModelFactory.Create<DbCatalogItem>();
        public BindingTable<DbSchemaItem> DbSchemta { get; } = ModelFactory.Create<DbSchemaItem>();
        public BindingTable<DbTableItem> DbTables { get; } = ModelFactory.Create<DbTableItem>();
        public BindingTable<DbColumnItem> DbColumns { get; } = ModelFactory.Create<DbColumnItem>();
        public BindingTable<DbExtendedPropertyItem> DbExtendedProperties = ModelFactory.Create<DbExtendedPropertyItem>();

        // Domain Model
        public BindingTable<DomainAttributeItem> DomainAttributes = ModelFactory.Create<DomainAttributeItem>();
        public BindingTable<DomainAttributePropertyItem> DomainAttributeProperties = ModelFactory.Create<DomainAttributePropertyItem>();
        public BindingTable<DomainAttributeAliasItem> DomainAttributeAliases = ModelFactory.Create<DomainAttributeAliasItem>();

        public ModelData() : base()
        { }

        public void Load(IModelId modelId) { }

        public void Load(FileInfo file) { }

        public void Load(DbSchemaContext context)
        {
            WorkerQueue.Enqueue(
                new DbVerifyConnection(context)
                {
                    WorkName = "Open Connection"
                }
                );

        }

        public void Save() { }

        public void Save(FileInfo file) { ModelFile = file; }

    }
}
