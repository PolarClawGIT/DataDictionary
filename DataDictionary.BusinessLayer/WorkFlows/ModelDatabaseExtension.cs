using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
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
    /// <summary>
    /// Functions/Operations used to create WorkItems for the Model against the Database.
    /// </summary>
    public static class ModelDatabaseExtension
    {

        /// <summary>
        /// Loads the List of Models from the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Loads the Model (by key) from the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modelId"></param>
        /// <returns></returns>
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
                Command = data.DbCatalogs.LoadCommand,
                Target = data.DbCatalogs
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbSchemta",
                Command = (conn) => data.DbSchemta.LoadCommand(conn, modelId),
                Target = data.DbSchemta
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbTables",
                Command = (conn) => data.DbTables.LoadCommand(conn, modelId),
                Target = data.DbTables
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbColumns",
                Command = (conn) => data.DbTableColumns.LoadCommand(conn, modelId),
                Target = data.DbTableColumns
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbDomains",
                Command = (conn) => data.DbDomains.LoadCommand(conn, modelId),
                Target = data.DbDomains
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbConstraints",
                Command = (conn) => data.DbConstraints.LoadCommand(conn, modelId),
                Target = data.DbConstraints
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbConstraintColumns",
                Command = (conn) => data.DbConstraintColumns.LoadCommand(conn, modelId),
                Target = data.DbConstraintColumns
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbRoutines",
                Command = (conn) => data.DbRoutines.LoadCommand(conn, modelId),
                Target = data.DbRoutines
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbRoutineParameters",
                Command = (conn) => data.DbRoutineParameters.LoadCommand(conn, modelId),
                Target = data.DbRoutineParameters
            });


            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbRoutineDependencies",
                Command = (conn) => data.DbRoutineDependencies.LoadCommand(conn, modelId),
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
                Command = (conn) => data.DomainAttributes.LoadCommand(conn, modelId),
                Target = data.DomainAttributes
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Domain Attributes Aliases",
                Command = (conn) => data.DomainAttributeAliases.LoadCommand(conn, modelId),
                Target = data.DomainAttributeAliases
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Domain Attributes Properties",
                Command = (conn) => data.DomainAttributeProperties.LoadCommand(conn, modelId),
                Target = data.DomainAttributeProperties
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Domain Entities",
                Command = (conn) => data.DomainEntities.LoadCommand(conn, modelId),
                Target = data.DomainEntities
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Domain Entities Aliases",
                Command = (conn) => data.DomainEntityAliases.LoadCommand(conn, modelId),
                Target = data.DomainEntityAliases
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Domain Entities Properties",
                Command = (conn) => data.DomainEntityProperties.LoadCommand(conn, modelId),
                Target = data.DomainEntityProperties
            });


            return workItems.AsReadOnly();
        }

        /// <summary>
        /// Save the Model to the Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveModel(this ModelData data)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            IModelKey modelId = new ModelKey(data.Model);

            OpenConnection openConnection = new OpenConnection(data.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Model",
                Command = (conn) => data.Models.SaveCommand(conn, data.Model)
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
                Command = (conn) => data.DbCatalogs.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbSchemta",
                Command = (conn) => data.DbSchemta.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbTables",
                Command = (conn) => data.DbTables.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbColumns",
                Command = (conn) => data.DbTableColumns.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbExtendedProperties",
                Command = (conn) => data.DbExtendedProperties.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbDomains",
                Command = (conn) => data.DbDomains.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbConstraints",
                Command = (conn) => data.DbConstraints.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbConstraintColumns",
                Command = (conn) => data.DbConstraintColumns.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbRoutines",
                Command = (conn) => data.DbRoutines.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbRoutineParameters",
                Command = (conn) => data.DbRoutineParameters.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbRoutineDependencies",
                Command = (conn) => data.DbRoutineDependencies.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Domain Attributes",
                Command = (conn) => data.DomainAttributes.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Domain Attributes Aliases",
                Command = (conn) => data.DomainAttributeAliases.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Domain Attributes Properties",
                Command = (conn) => data.DomainAttributeProperties.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Domain Entities",
                Command = (conn) => data.DomainEntities.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Domain Entities Aliases",
                Command = (conn) => data.DomainEntityAliases.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Domain Entities Properties",
                Command = (conn) => data.DomainEntityProperties.SaveCommand(conn, modelId)
            });

            return workItems.AsReadOnly();
        }

        /// <summary>
        /// Removes the Model From the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modelId"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> DeleteModel(this ModelData data, IModelKey modelId)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            data.NewModel();

            OpenConnection openConnection = new OpenConnection(data.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Delete Attributes",
                Command = (conn) => data.DomainAttributes.DeleteCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Delete Entities",
                Command = (conn) => data.DomainEntities.DeleteCommand(conn, modelId)
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
