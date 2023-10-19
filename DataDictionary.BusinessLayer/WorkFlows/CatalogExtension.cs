using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.WorkFlows
{
    /// <summary>
    /// Extension for handling working with Catalog work items
    /// </summary>
    /// <remarks>
    /// Reminder during coding.
    /// Always create a new instance of a IKey derived class before trying to use it.
    /// This reduces errors because the object that implements the IKey can change (like being removed) before the key has a chance to be used.
    /// If this occurs, then the BindingTable classes throw an invalid operation exception.
    /// These errors can be specifically difficult to resolve.
    /// </remarks>
    public static class CatalogExtension
    {
        /// <summary>
        /// Loads the Catalog (by key) from the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="catalogKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadCatalog(this IModelCatalog data, IDbCatalogKey catalogKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            DbCatalogKey key = new DbCatalogKey(catalogKey);

            OpenConnection openConnection = new OpenConnection(ModelData.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbCatalogs",
                Command = (conn) => data.DbCatalogs.LoadCommand(conn, key),
                Target = data.DbCatalogs
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbSchemta",
                Command = (conn) => data.DbSchemta.LoadCommand(conn, key),
                Target = data.DbSchemta
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbTables",
                Command = (conn) => data.DbTables.LoadCommand(conn, key),
                Target = data.DbTables
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbColumns",
                Command = (conn) => data.DbTableColumns.LoadCommand(conn, key),
                Target = data.DbTableColumns
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbDomains",
                Command = (conn) => data.DbDomains.LoadCommand(conn, key),
                Target = data.DbDomains
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbConstraints",
                Command = (conn) => data.DbConstraints.LoadCommand(conn, key),
                Target = data.DbConstraints
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbConstraintColumns",
                Command = (conn) => data.DbConstraintColumns.LoadCommand(conn, key),
                Target = data.DbConstraintColumns
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbRoutines",
                Command = (conn) => data.DbRoutines.LoadCommand(conn, key),
                Target = data.DbRoutines
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbRoutineParameters",
                Command = (conn) => data.DbRoutineParameters.LoadCommand(conn, key),
                Target = data.DbRoutineParameters
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbRoutineDependencies",
                Command = (conn) => data.DbRoutineDependencies.LoadCommand(conn, key),
                Target = data.DbRoutineDependencies
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Extended Properties",
                Command = (conn) => data.DbExtendedProperties.LoadCommand(conn, key),
                Target = data.DbExtendedProperties
            });

            return workItems;
        }

        /// <summary>
        /// Loads the Catalog (by key) from the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadCatalog(this IModelCatalog data, IModelKey modelKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            OpenConnection openConnection = new OpenConnection(ModelData.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbCatalogs",
                Command = (conn) => data.DbCatalogs.LoadCommand(conn, key),
                Target = data.DbCatalogs
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbSchemta",
                Command = (conn) => data.DbSchemta.LoadCommand(conn, key),
                Target = data.DbSchemta
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbTables",
                Command = (conn) => data.DbTables.LoadCommand(conn, key),
                Target = data.DbTables
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbColumns",
                Command = (conn) => data.DbTableColumns.LoadCommand(conn, key),
                Target = data.DbTableColumns
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbDomains",
                Command = (conn) => data.DbDomains.LoadCommand(conn, key),
                Target = data.DbDomains
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbConstraints",
                Command = (conn) => data.DbConstraints.LoadCommand(conn, key),
                Target = data.DbConstraints
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbConstraintColumns",
                Command = (conn) => data.DbConstraintColumns.LoadCommand(conn, key),
                Target = data.DbConstraintColumns
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbRoutines",
                Command = (conn) => data.DbRoutines.LoadCommand(conn, key),
                Target = data.DbRoutines
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbRoutineParameters",
                Command = (conn) => data.DbRoutineParameters.LoadCommand(conn, key),
                Target = data.DbRoutineParameters
            });


            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbRoutineDependencies",
                Command = (conn) => data.DbRoutineDependencies.LoadCommand(conn, key),
                Target = data.DbRoutineDependencies
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Extended Properties",
                Command = (conn) => data.DbExtendedProperties.LoadCommand(conn, key),
                Target = data.DbExtendedProperties
            });

            return workItems;
        }

        /// <summary>
        /// Removes all the Catalog Data
        /// </summary>
        /// <param name="data"></param>
        public static IReadOnlyList<WorkItem> ClearCatalog(this IModelCatalog data)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            workItems.Add(new WorkItem() { WorkName = "Clear DbCatalogs", DoWork = data.DbCatalogs.Clear });
            workItems.Add(new WorkItem() { WorkName = "Clear DbSchemta", DoWork = data.DbSchemta.Clear });
            workItems.Add(new WorkItem() { WorkName = "Clear DbTables", DoWork = data.DbTables.Clear });
            workItems.Add(new WorkItem() { WorkName = "Clear DbTableColumns", DoWork = data.DbTableColumns.Clear });
            workItems.Add(new WorkItem() { WorkName = "Clear DbDomains", DoWork = data.DbDomains.Clear });
            workItems.Add(new WorkItem() { WorkName = "Clear DbConstraints", DoWork = data.DbConstraints.Clear });
            workItems.Add(new WorkItem() { WorkName = "Clear DbConstraintColumns", DoWork = data.DbConstraintColumns.Clear });
            workItems.Add(new WorkItem() { WorkName = "Clear DbRoutines", DoWork = data.DbRoutines.Clear });
            workItems.Add(new WorkItem() { WorkName = "Clear DbRoutineParameters", DoWork = data.DbRoutineParameters.Clear });
            workItems.Add(new WorkItem() { WorkName = "Clear DbRoutineDependencies", DoWork = data.DbRoutineDependencies.Clear });
            workItems.Add(new WorkItem() { WorkName = "Clear DbExtendedProperties", DoWork = data.DbExtendedProperties.Clear });

            return workItems;
        }

        /// <summary>
        /// Removes all the Catalog Data for the specific key
        /// </summary>
        /// <param name="data"></param>
        /// <param name="catalogKey"></param>
        public static IReadOnlyList<WorkItem> RemoveCatalog(this IModelCatalog data, IDbCatalogKey catalogKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            DbCatalogKey key = new DbCatalogKey(catalogKey);

            workItems.Add(new WorkItem() { WorkName = "Remove DbCatalogs", DoWork = () => data.DbCatalogs.Remove(key) });
            workItems.Add(new WorkItem() { WorkName = "Remove DbSchemta", DoWork = () => data.DbSchemta.Remove(key) });
            workItems.Add(new WorkItem() { WorkName = "Remove DbTables", DoWork = () => data.DbTables.Remove(key) });
            workItems.Add(new WorkItem() { WorkName = "Remove DbTableColumns", DoWork = () => data.DbTableColumns.Remove(key) });
            workItems.Add(new WorkItem() { WorkName = "Remove DbDomains", DoWork = () => data.DbDomains.Remove(key) });
            workItems.Add(new WorkItem() { WorkName = "Remove DbConstraints", DoWork = () => data.DbConstraints.Remove(key) });
            workItems.Add(new WorkItem() { WorkName = "Remove DbConstraintColumns", DoWork = () => data.DbConstraintColumns.Remove(key) });
            workItems.Add(new WorkItem() { WorkName = "Remove DbRoutines", DoWork = () => data.DbRoutines.Remove(key) });
            workItems.Add(new WorkItem() { WorkName = "Remove DbRoutineParameters", DoWork = () => data.DbRoutineParameters.Remove(key) });
            workItems.Add(new WorkItem() { WorkName = "Remove DbRoutineDependencies", DoWork = () => data.DbRoutineDependencies.Remove(key) });
            workItems.Add(new WorkItem() { WorkName = "Remove DbExtendedProperties", DoWork = () => data.DbExtendedProperties.Remove(key) });

            return workItems;
        }

        /// <summary>
        /// Save the Catalog (by key) from the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="catalogKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveCatalog(this IModelCatalog data, IDbCatalogKey catalogKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            DbCatalogKey key = new DbCatalogKey(catalogKey);

            OpenConnection openConnection = new OpenConnection(ModelData.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbCatalogs",
                Command = (conn) => data.DbCatalogs.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbSchemta",
                Command = (conn) => data.DbSchemta.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbTables",
                Command = (conn) => data.DbTables.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbColumns",
                Command = (conn) => data.DbTableColumns.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbExtendedProperties",
                Command = (conn) => data.DbExtendedProperties.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbDomains",
                Command = (conn) => data.DbDomains.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbConstraints",
                Command = (conn) => data.DbConstraints.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbConstraintColumns",
                Command = (conn) => data.DbConstraintColumns.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbRoutines",
                Command = (conn) => data.DbRoutines.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbRoutineParameters",
                Command = (conn) => data.DbRoutineParameters.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbRoutineDependencies",
                Command = (conn) => data.DbRoutineDependencies.SaveCommand(conn, key)
            });

            return workItems;
        }

        /// <summary>
        /// Save the Catalog (by key) from the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveCatalog(this IModelCatalog data, IModelKey modelKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            OpenConnection openConnection = new OpenConnection(ModelData.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbCatalogs",
                Command = (conn) => data.DbCatalogs.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbSchemta",
                Command = (conn) => data.DbSchemta.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbTables",
                Command = (conn) => data.DbTables.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbColumns",
                Command = (conn) => data.DbTableColumns.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbExtendedProperties",
                Command = (conn) => data.DbExtendedProperties.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbDomains",
                Command = (conn) => data.DbDomains.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbConstraints",
                Command = (conn) => data.DbConstraints.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbConstraintColumns",
                Command = (conn) => data.DbConstraintColumns.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbRoutines",
                Command = (conn) => data.DbRoutines.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbRoutineParameters",
                Command = (conn) => data.DbRoutineParameters.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save DbRoutineDependencies",
                Command = (conn) => data.DbRoutineDependencies.SaveCommand(conn, key)
            });

            return workItems;
        }

        /// <summary>
        /// Creates work items that Saves the Library to the Database by Library Key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="catalogKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveCatalog(this IWriteData<IDbCatalogKey> data, IDbCatalogKey catalogKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            DbCatalogKey key = new DbCatalogKey(catalogKey);

            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(ModelData.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Catalog sources",
                Command = (conn) => data.SaveCommand(conn, key)
            });

            return workItems;
        }

        /// <summary>
        /// Creates work items that Deletes the Catalog from the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="catalogKey"></param>
        /// <returns></returns>
        /// <remarks>If the Catalog belongs to any Model, the delete will fail.</remarks>
        public static IReadOnlyList<WorkItem> DeleteCatalog(this IModelCatalog data, IDbCatalogKey catalogKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            DbCatalogKey key = new DbCatalogKey(catalogKey);

            OpenConnection openConnection = new OpenConnection(ModelData.ModelContext);
            workItems.Add(openConnection);

            DbCatalogCollection empty = new DbCatalogCollection();

            // The Save performs a delta. As the collection is empty, anything matching that Catalog Key is deleted.
            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Delete DbCatalog (Cascade)",
                Command = (conn) => empty.SaveCommand(conn, key)
            });


            return workItems;
        }

        /// <summary>
        /// Creates work items that Deletes the Library from the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="catalogKey"></param>
        /// <returns></returns>
        /// <remarks>If the Library belongs to any Model, the delete will fail.</remarks>
        public static IReadOnlyList<WorkItem> DeleteCatalog(this IWriteData<IDbCatalogKey> data, IDbCatalogKey catalogKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            DbCatalogKey key = new DbCatalogKey(catalogKey);

            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(ModelData.ModelContext);
            workItems.Add(openConnection);

            DbCatalogCollection empty = new DbCatalogCollection();

            // The Save performs a delta. As the collection is empty, anything matching that Library Key is deleted.
            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Delete Catalog (Cascade)",
                Command = (conn) => empty.SaveCommand(conn, key)
            });

            return workItems;
        }
    }
}
