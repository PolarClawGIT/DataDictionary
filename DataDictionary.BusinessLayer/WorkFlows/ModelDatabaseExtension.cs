using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
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

        public static IReadOnlyList<WorkItem> LoadModelList(this ModelData data)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            OpenConnection openConnection = new OpenConnection(data.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new WorkItem()
            {
                WorkName = "Clear Model list",
                DoWork = () => { data.Models.Clear(); }
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Get list of Models",
                Command = data.Models.LoadCommand,
                Target = data.Models
            });

            return workItems.AsReadOnly();
        }

        public static IReadOnlyList<WorkItem> LoadModel(this ModelData data, IModelKey modelId)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            data.ModelKey = new ModelKey(modelId);

            OpenConnection openConnection = new OpenConnection(data.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new WorkItem()
            {
                WorkName = "Clear Model",
                DoWork = () => { data.Clear(); }
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Models",
                Command = data.Models.LoadCommand,
                Target = data.Models
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbCatalogs",
                Command = (conn) => DbCatalogItem.GetData(conn, modelId),
                Target = data.DbCatalogs
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbSchemta",
                Command = (conn) => DbSchemaItem.GetData(conn, modelId),
                Target = data.DbSchemta
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbTables",
                Command = (conn) => DbTableItem.GetData(conn, modelId),
                Target = data.DbTables
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbColumns",
                Command = (conn) => DbTableColumnItem.GetData(conn, modelId),
                Target = data.DbColumns
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbDomains",
                Command = (conn) => DbDomainItem.GetData(conn, modelId),
                Target = data.DbDomains
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbConstraints",
                Command = (conn) => DbConstraintItem.GetData(conn, modelId),
                Target = data.DbConstraints
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbConstraintColumns",
                Command = (conn) => DbConstraintColumnItem.GetData(conn, modelId),
                Target = data.DbConstraintColumns
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbRoutines",
                Command = (conn) => DbRoutineItem.GetData(conn, modelId),
                Target = data.DbRoutines
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbRoutineParameters",
                Command = (conn) => DbRoutineParameterItem.GetData(conn, modelId),
                Target = data.DbRoutineParameters
            });


            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbRoutineDependencies",
                Command = (conn) => DbRoutineDependencyItem.GetData(conn, modelId),
                Target = data.DbRoutineDependencies
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Extended Properties",
                Command = (conn) => data.DbExtendedProperties.LoadCommand(conn, modelId),
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
            IModelKey modelId = new ModelKey(data.Model);

            OpenConnection openConnection = new OpenConnection(data.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Model",
                Command = data.Models.SaveCommand
            });

            workItems.Add(new WorkItem()
            {
                WorkName = "Clear Model list",
                DoWork = () => { data.Models.Clear(); }
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Get list of Models",
                Command = data.Models.LoadCommand,
                Target = data.Models
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbCatalogs",
                Command = (conn) => DbCatalogItem.SetData(conn, modelId, data.DbCatalogs)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbSchemta",
                Command = (conn) => DbSchemaItem.SetData(conn, modelId, data.DbSchemta)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbTables",
                Command = (conn) => DbTableItem.SetData(conn, modelId, data.DbTables)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbColumns",
                Command = (conn) => DbTableColumnItem.SetData(conn, modelId, data.DbColumns)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbExtendedProperties",
                Command = (conn) => data.DbExtendedProperties.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbDomains",
                Command = (conn) => DbDomainItem.SetData(conn, modelId, data.DbDomains)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbConstraints",
                Command = (conn) => DbConstraintItem.SetData(conn, modelId, data.DbConstraints)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbConstraintColumns",
                Command = (conn) => DbConstraintColumnItem.SetData(conn, modelId, data.DbConstraintColumns)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbRoutines",
                Command = (conn) => DbRoutineItem.SetData(conn, modelId, data.DbRoutines)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbConstraintColumns",
                Command = (conn) => DbRoutineParameterItem.SetData(conn, modelId, data.DbRoutineParameters)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbRoutineDependencies",
                Command = (conn) => DbRoutineDependencyItem.SetData(conn, modelId, data.DbRoutineDependencies)
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

        public static IReadOnlyList<WorkItem> DeleteModel(this ModelData data, IModelKey modelId)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            data.NewModel();

            OpenConnection openConnection = new OpenConnection(data.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Delete Attributes",
                Command = (conn) => DomainAttributeItem.DeleteData(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Delete Model",
                Command = (conn) => data.Models.DeleteCommand(conn, modelId)
            });

            workItems.Add(new WorkItem()
            {
                WorkName = "Clear Model",
                DoWork = () => { data.Clear(); }
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Get list of Models",
                Command = data.Models.LoadCommand,
                Target = data.Models
            });

            return workItems.AsReadOnly();
        }
    }
}
