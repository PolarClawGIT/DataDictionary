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
            OpenConnection openConnection = new OpenConnection(ModelData.ModelContext);
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
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadModel(this ModelData data, IModelKey modelKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            data.ModelKey = new ModelKey(modelKey);

            OpenConnection openConnection = new OpenConnection(ModelData.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new WorkItem()
            {
                WorkName = "Clear Model",
                DoWork = () => { data.Clear(); }
            });

            workItems.AddRange(data.LoadCatalog(modelKey));

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Domain Attributes",
                Command = (conn) => data.DomainAttributes.LoadCommand(conn, modelKey),
                Target = data.DomainAttributes
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Domain Attributes Aliases",
                Command = (conn) => data.DomainAttributeAliases.LoadCommand(conn, modelKey),
                Target = data.DomainAttributeAliases
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Domain Attributes Properties",
                Command = (conn) => data.DomainAttributeProperties.LoadCommand(conn, modelKey),
                Target = data.DomainAttributeProperties
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Domain Entities",
                Command = (conn) => data.DomainEntities.LoadCommand(conn, modelKey),
                Target = data.DomainEntities
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Domain Entities Aliases",
                Command = (conn) => data.DomainEntityAliases.LoadCommand(conn, modelKey),
                Target = data.DomainEntityAliases
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Domain Entities Properties",
                Command = (conn) => data.DomainEntityProperties.LoadCommand(conn, modelKey),
                Target = data.DomainEntityProperties
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Domain Subject Areas",
                Command = (conn) => data.DomainSubjectAreas.LoadCommand(conn, modelKey),
                Target = data.DomainSubjectAreas
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Library Sources",
                Command = (conn) => data.LibrarySources.LoadCommand(conn, modelKey),
                Target = data.LibrarySources
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Library Members",
                Command = (conn) => data.LibraryMembers.LoadCommand(conn, modelKey),
                Target = data.LibraryMembers
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

            OpenConnection openConnection = new OpenConnection(ModelData.ModelContext);
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

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Subject Areas",
                Command = (conn) => data.DomainSubjectAreas.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Library Sources",
                Command = (conn) => data.LibrarySources.SaveCommand(conn, modelId)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Library Members",
                Command = (conn) => data.LibraryMembers.SaveCommand(conn, modelId)
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

            OpenConnection openConnection = new OpenConnection(ModelData.ModelContext);
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
                WorkName = "Delete Subject Area",
                Command = (conn) => data.DomainSubjectAreas.DeleteCommand(conn, modelId)
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
