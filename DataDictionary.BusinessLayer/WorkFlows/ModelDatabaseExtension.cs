using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.DomainData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.WorkFlows
{
    public static class ModelDatabaseExtension
    {

        public static IReadOnlyList<WorkItem> LoadModelList(this ModelData data, IBindingTable<ModelItem> target)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            OpenConnection openConnection = new OpenConnection(data.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Get List of Models",
                Command = ModelItem.GetData,
                Target = target
            });

            return workItems.AsReadOnly();
        }

        public static IReadOnlyList<WorkItem> SaveModelList(this ModelData data, IBindingTable<ModelItem> source)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            IModelIdentifier modelId = new ModelIdentifier(data.Model);
            data.Models.Clear();

            OpenConnection openConnection = new OpenConnection(data.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save List of Models",
                Command = (conn) => ModelItem.SetData(conn, source)
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Model",
                Command = (conn) => ModelItem.GetData(conn, modelId),
                Target = data.Models
            });

            return workItems.AsReadOnly();
        }

        public static IReadOnlyList<WorkItem> LoadModel(this ModelData data, IModelIdentifier modelId)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            OpenConnection openConnection = new OpenConnection(data.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Model",
                Command = (conn) => ModelItem.GetData(conn, modelId),
                Target = data.Models
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Catalogs",
                Command = (conn) => DbCatalogItem.GetData(conn, modelId),
                Target = data.DbCatalogs
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Schemata",
                Command = (conn) => DbSchemaItem.GetData(conn, modelId),
                Target = data.DbSchemta
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Tables",
                Command = (conn) => DbTableItem.GetData(conn, modelId),
                Target = data.DbTables
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Columns",
                Command = (conn) => DbTableColumnItem.GetData(conn, modelId),
                Target = data.DbColumns
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Extended Properties",
                Command = (conn) => DbExtendedPropertyItem.GetData(conn, modelId),
                Target = data.DbExtendedProperties
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Domain Attributes",
                Command = (conn) => DomainAttributeItem.GetData(conn, modelId),
                Target = data.DomainAttributes
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Domain Attributes Aliases",
                Command = (conn) => DomainAttributeAliasItem.GetData(conn, modelId),
                Target = data.DomainAttributeAliases
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Domain Attributes Properties",
                Command = (conn) => DomainAttributePropertyItem.GetData(conn, modelId),
                Target = data.DomainAttributeProperties
            });

            return workItems.AsReadOnly();
        }

        public static IReadOnlyList<WorkItem> SaveModel(this ModelData data)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            IModelIdentifier modelId = data.Model;

            OpenConnection openConnection = new OpenConnection(data.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Model",
                Command = (conn) => ModelItem.SetData(conn, data.Models)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Catalogs",
                Command = (conn) => DbCatalogItem.SetData(conn, modelId, data.DbCatalogs)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Schemata",
                Command = (conn) => DbSchemaItem.SetData(conn, modelId, data.DbSchemta)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Tables",
                Command = (conn) => DbTableItem.SetData(conn, modelId, data.DbTables)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Columns",
                Command = (conn) => DbTableColumnItem.SetData(conn, modelId, data.DbColumns)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Extended Properties",
                Command = (conn) => DbExtendedPropertyItem.SetData(conn, modelId, data.DbExtendedProperties)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Domain Attributes",
                Command = (conn) => DomainAttributeItem.SetData(conn, modelId, data.DomainAttributes)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Domain Attributes Aliases",
                Command = (conn) => DomainAttributeAliasItem.SetData(conn, modelId, data.DomainAttributeAliases)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Domain Attributes Properties",
                Command = (conn) => DomainAttributePropertyItem.SetData(conn, modelId, data.DomainAttributeProperties)
            });

            return workItems.AsReadOnly();
        }

        public static IReadOnlyList<WorkItem> DeleteModel(this ModelData data, IModelIdentifier modelId)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            OpenConnection openConnection = new OpenConnection(data.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Delete Model",
                Command = (conn) => ModelItem.DeleteData(conn, modelId)
            });

            return workItems.AsReadOnly();
        }
    }
}
