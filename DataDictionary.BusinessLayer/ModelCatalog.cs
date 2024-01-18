using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
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
    /// Interface component for the Model Catalog
    /// </summary>
    /// <remarks>When combined with the Extension class, this approximates multi-inheritance.</remarks>
    public interface IModelCatalog
    {
        /// <summary>
        /// List of Database Catalogs within the Model.
        /// </summary>
        DbCatalogCollection DbCatalogs { get; }

        /// <summary>
        /// List of Database Schemta within the Model.
        /// </summary>
        DbSchemaCollection DbSchemta { get; }

        /// <summary>
        /// List of Database Domains (types) within the Model.
        /// </summary>
        DbDomainCollection DbDomains { get; }

        /// <summary>
        /// List of Database Tables and Views within the Model.
        /// </summary>
        DbTableCollection DbTables { get; }

        /// <summary>
        /// List of Database Columns for the Tables/Views within the Model.
        /// </summary>
        DbTableColumnCollection DbTableColumns { get; }

        /// <summary>
        /// List of Database Extended Properties within the Model.
        /// </summary>
        DbExtendedPropertyCollection DbExtendedProperties { get; }

        /// <summary>
        /// List of Database Constraints (keys...) within the Model.
        /// </summary>
        DbConstraintCollection DbConstraints { get; }

        /// <summary>
        /// List of Database Constraint Columns within the Model.
        /// </summary>
        DbConstraintColumnCollection DbConstraintColumns { get; }

        /// <summary>
        /// List of Database Routines (procedures, functions, ...) within the Model.
        /// </summary>
        DbRoutineCollection DbRoutines { get; }

        /// <summary>
        /// List of Database Parameters for the Routines within the Model.
        /// </summary>
        DbRoutineParameterCollection DbRoutineParameters { get; }

        /// <summary>
        /// List of Database Dependencies for the Routines within the Model.
        /// </summary>
        DbRoutineDependencyCollection DbRoutineDependencies { get; }
    }

    /// <summary>
    /// Implementation of the Model Catalog
    /// </summary>
    /// <remarks>When combined with the Interface class, this approximates multi-inheritance.</remarks>
    public static class ModelCatalog
    {
        /// <summary>
        /// Creates the work items to Load the Model Catalog using the catalog key passed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="catalogKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadCatalog(this IModelCatalog data, IDatabaseWork factory, IDbCatalogKey catalogKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            DbCatalogKey key = new DbCatalogKey(catalogKey);

            work.Add(factory.CreateWork(
                workName: "Load DbCatalogs",
                target: data.DbCatalogs,
                command: (conn) => data.DbCatalogs.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbSchemta",
                target: data.DbSchemta,
                command: (conn) => data.DbSchemta.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbTables",
                target: data.DbTables,
                command: (conn) => data.DbTables.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbColumns",
                target: data.DbTableColumns,
                command: (conn) => data.DbTableColumns.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbDomains",
                target: data.DbDomains,
                command: (conn) => data.DbDomains.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbConstraints",
                target: data.DbConstraints,
                command: (conn) => data.DbConstraints.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbConstraintColumns",
                target: data.DbConstraintColumns,
                command: (conn) => data.DbConstraintColumns.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbRoutines",
                target: data.DbRoutines,
                command: (conn) => data.DbRoutines.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbRoutineParameters",
                target: data.DbRoutineParameters,
                command: (conn) => data.DbRoutineParameters.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbRoutineDependencies",
                target: data.DbRoutineDependencies,
                command: (conn) => data.DbRoutineDependencies.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties",
                target: data.DbExtendedProperties,
                command: (conn) => data.DbExtendedProperties.LoadCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Creates the work items to Load the Model Catalog using the model key passed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadCatalog(this IModelCatalog data, IDatabaseWork factory, IModelKey modelKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            work.Add(factory.CreateWork(
                workName: "Load DbCatalogs",
                target: data.DbCatalogs,
                command: (conn) => data.DbCatalogs.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbSchemta",
                target: data.DbSchemta,
                command: (conn) => data.DbSchemta.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbTables",
                target: data.DbTables,
                command: (conn) => data.DbTables.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbColumns",
                target: data.DbTableColumns,
                command: (conn) => data.DbTableColumns.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbDomains",
                target: data.DbDomains,
                command: (conn) => data.DbDomains.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbConstraints",
                target: data.DbConstraints,
                command: (conn) => data.DbConstraints.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbConstraintColumns",
                target: data.DbConstraintColumns,
                command: (conn) => data.DbConstraintColumns.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbRoutines",
                target: data.DbRoutines,
                command: (conn) => data.DbRoutines.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbRoutineParameters",
                target: data.DbRoutineParameters,
                command: (conn) => data.DbRoutineParameters.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbRoutineDependencies",
                target: data.DbRoutineDependencies,
                command: (conn) => data.DbRoutineDependencies.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties",
                target: data.DbExtendedProperties,
                command: (conn) => data.DbExtendedProperties.LoadCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Creates the work items to Load the Model Catalog using the Schema of a source database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadCatalog(this IModelCatalog data, DbSchemaContext source)
        {
            List<WorkItem> work = new List<WorkItem>();
            DbCatalogKey key = new DbCatalogKey(new DbCatalogItem());

            DatabaseWork factory = new DatabaseWork() { Connection = source.CreateConnection() };
            work.Add(factory.OpenConnection());

            work.Add(factory.CreateWork(
                workName: "Load DbCatalogs",
                target: data.DbCatalogs,
                command: (conn) => data.DbCatalogs.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbSchemta",
                target: data.DbSchemta,
                command: (conn) => data.DbSchemta.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbDomains",
                target: data.DbDomains,
                command: (conn) => data.DbDomains.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbTables",
                target: data.DbTables,
                command: (conn) => data.DbTables.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbTableColumns",
                target: data.DbTableColumns,
                command: (conn) => data.DbTableColumns.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbConstraints",
                target: data.DbConstraints,
                command: (conn) => data.DbConstraints.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbConstraintColumns",
                target: data.DbConstraintColumns,
                command: (conn) => data.DbConstraintColumns.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbRoutines",
                target: data.DbRoutines,
                command: (conn) => data.DbRoutines.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbRoutineParameters",
                target: data.DbRoutineParameters,
                command: (conn) => data.DbRoutineParameters.SchemaCommand(conn, key)));

            work.Add(new WorkItem()
            {
                WorkName = "Load DbRoutineDependencies",
                DoWork = () =>
                {
                    foreach (DbRoutineItem item in data.DbRoutines)
                    {
                        data.DbRoutineDependencies.Load(
                            factory.Connection.ExecuteReader(
                                data.DbRoutineDependencies.SchemaCommand(
                                    factory.Connection, item)));
                    }
                },
                IsCanceling = () => factory.IsCanceling
            });

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties, DbSchemta",
                source: data.DbSchemta,
                target: data.DbExtendedProperties));

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties, DbTables",
                source: data.DbTables,
                target: data.DbExtendedProperties));

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties, DbTableColumns",
                source: data.DbTableColumns,
                target: data.DbExtendedProperties));

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties, DbConstraints",
                source: data.DbConstraints,
                target: data.DbExtendedProperties));

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties, DbDomains",
                source: data.DbDomains,
                target: data.DbExtendedProperties));

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties, DbRoutines",
                source: data.DbRoutines,
                target: data.DbExtendedProperties));

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties, DbRoutineParameters",
                source: data.DbRoutineParameters,
                target: data.DbExtendedProperties));
            return work;
        }

        /// <summary>
        /// Creates the work items to Load the a list of Catalogs.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadCatalog<T>(this T target, IDatabaseWork factory)
            where T : IBindingTable<DbCatalogItem>, IReadData
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(factory.CreateWork(
                workName: "Load DbCatalogs",
                target: target,
                command: (conn) => target.LoadCommand(conn)));

            return work;
        }

        /// <summary>
        /// Removes all the Catalogs Data from the Model (Clear)
        /// </summary>
        /// <param name="data"></param>
        public static IReadOnlyList<WorkItem> RemoveCatalog(this IModelCatalog data)
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
        /// Creates the work items to Save the Model Catalog using the Catalog key passed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="catalogKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveCatalog(this IModelCatalog data, IDatabaseWork factory, IDbCatalogKey catalogKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            DbCatalogKey key = new DbCatalogKey(catalogKey);

            work.Add(factory.CreateWork(
                workName: "Save DbCatalogs",
                command: (conn) => data.DbCatalogs.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbSchemta",
                command: (conn) => data.DbSchemta.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbTables",
                command: (conn) => data.DbTables.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbTableColumns",
                command: (conn) => data.DbTableColumns.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbDomains",
                command: (conn) => data.DbDomains.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbConstraints",
                command: (conn) => data.DbConstraints.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbConstraintColumns",
                command: (conn) => data.DbConstraintColumns.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbRoutines",
                command: (conn) => data.DbRoutines.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbRoutineParameters",
                command: (conn) => data.DbRoutineParameters.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbRoutineDependencies",
                command: (conn) => data.DbRoutineDependencies.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbExtendedProperties",
                command: (conn) => data.DbExtendedProperties.SaveCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Creates the work items to Save the Model Catalog using the Model key passed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveCatalog(this IModelCatalog data, IDatabaseWork factory, IModelKey modelKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            work.Add(factory.CreateWork(
                workName: "Save DbCatalogs",
                command: (conn) => data.DbCatalogs.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbSchemta",
                command: (conn) => data.DbSchemta.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbTables",
                command: (conn) => data.DbTables.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbTableColumns",
                command: (conn) => data.DbTableColumns.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbDomains",
                command: (conn) => data.DbDomains.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbConstraints",
                command: (conn) => data.DbConstraints.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbConstraintColumns",
                command: (conn) => data.DbConstraintColumns.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbRoutines",
                command: (conn) => data.DbRoutines.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbRoutineParameters",
                command: (conn) => data.DbRoutineParameters.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbRoutineDependencies",
                command: (conn) => data.DbRoutineDependencies.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DbExtendedProperties",
                command: (conn) => data.DbExtendedProperties.SaveCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Creates the work items to Save the Catalog using the Catalog key passed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="catalogKey"></param>
        /// <returns>This Saves only the Catalog in the Application Database. The Model is not impacted.</returns>
        public static IReadOnlyList<WorkItem> SaveCatalog(this IWriteData<IDbCatalogKey> data, IDatabaseWork factory, IDbCatalogKey catalogKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            DbCatalogKey key = new DbCatalogKey(catalogKey);

            work.Add(factory.CreateWork(
                workName: "Save DbCatalogs",
                command: (conn) => data.SaveCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Creates the work items to Delete a Catalog using the Catalog key passed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="catalogKey"></param>
        /// <returns>This does a Cascade Delete on the Application Database.</returns>
        public static IReadOnlyList<WorkItem> DeleteCatalog(this IModelCatalog data, IDatabaseWork factory, IDbCatalogKey catalogKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            DbCatalogKey key = new DbCatalogKey(catalogKey);

            DbCatalogCollection empty = new DbCatalogCollection();

            work.Add(factory.CreateWork(
                workName: "Delete DbCatalogs (cascade)",
                command: (conn) => empty.SaveCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Creates the work items to Delete a Catalog using the Catalog key passed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="catalogKey"></param>
        /// <returns>This does a Cascade Delete on the Application Database.</returns>
        public static IReadOnlyList<WorkItem> DeleteCatalog(this IWriteData<IDbCatalogKey> data, IDatabaseWork factory, IDbCatalogKey catalogKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            DbCatalogKey key = new DbCatalogKey(catalogKey);

            DbCatalogCollection empty = new DbCatalogCollection();

            work.Add(factory.CreateWork(
                workName: "Delete DbCatalogs (cascade)",
                command: (conn) => empty.SaveCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Gets a list of Routine Parameters given a Key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static BindingView<DbRoutineParameterItem> GetRoutineParameters(this IModelCatalog data, IDbRoutineKeyName source)
        {
            DbRoutineKeyName key = new DbRoutineKeyName(source);
            return new BindingView<DbRoutineParameterItem>(data.DbRoutineParameters, w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Routine Parameters given a Key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static BindingView<DbRoutineDependencyItem> GetRoutineDependencies(this IModelCatalog data, IDbRoutineKeyName source)
        {
            DbRoutineKeyName key = new DbRoutineKeyName(source);
            return new BindingView<DbRoutineDependencyItem>(data.DbRoutineDependencies, w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(this IModelCatalog data, IDbExtendedPropertyKeyName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return data.DbExtendedProperties.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(this IModelCatalog data, IDbTableColumnKeyName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return data.DbExtendedProperties.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(this IModelCatalog data, IDbTableKeyName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return data.DbExtendedProperties.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static BindingView<DbExtendedPropertyItem> GetExtendedProperty(this IModelCatalog data, IDbRoutineKeyName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return new BindingView<DbExtendedPropertyItem>(data.DbExtendedProperties, w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(this IModelCatalog data, IDbRoutineParameterKeyName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return data.DbExtendedProperties.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(this IModelCatalog data, IDbConstraintKeyName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return data.DbExtendedProperties.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(this IModelCatalog data, IDbSchemaKeyName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return data.DbExtendedProperties.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets a column given the key.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static DbTableColumnItem? GetColumn(this IEnumerable<DbTableColumnItem> source, IDbTableColumnKeyName item)
        { return source.FirstOrDefault(w => new DbTableColumnKeyName(item) == new DbTableColumnKeyName(w)); }

        /// <summary>
        /// Gets a column given the key.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DbTableColumnItem? GetColumn(this IDbTableColumnKeyName item, IEnumerable<DbTableColumnItem> source)
        { return source.FirstOrDefault(w => new DbTableColumnKeyName(item) == new DbTableColumnKeyName(w)); }

        /// <summary>
        /// Gets a List of Columns given a Key
        /// </summary>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<DbTableColumnItem> GetColumns(this IEnumerable<DbTableColumnItem> source, IDbTableKeyName item)
        { return source.Where(w => new DbTableKeyName(item) == new DbTableKeyName(w)); }

        /// <summary>
        /// Gets a List of Columns given a Key
        /// </summary>
        /// <param name="item"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DbTableColumnItem> GetColumns(this IDbTableKeyName item, IEnumerable<DbTableColumnItem> source)
        { return source.Where(w => new DbTableKeyName(item) == new DbTableKeyName(w)); }

        /// <summary>
        /// Gets a Schema given a Key
        /// </summary>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static DbSchemaItem? GetSchema(this IEnumerable<DbSchemaItem> source, IDbSchemaKeyName item)
        { return source.FirstOrDefault(w => new DbSchemaKeyName(item) == new DbSchemaKeyName(w)); }

        /// <summary>
        /// Gets a Schema given a Key
        /// </summary>
        /// <param name="item"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DbSchemaItem? GetSchema(this IDbSchemaKeyName item, IEnumerable<DbSchemaItem> source)
        { return source.FirstOrDefault(w => new DbSchemaKeyName(item) == new DbSchemaKeyName(w)); }

        /// <summary>
        /// Gets a Table Given a Key
        /// </summary>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static DbTableItem? GetTable(this IEnumerable<DbTableItem> source, IDbTableKeyName item)
        { return source.FirstOrDefault(w => new DbTableKeyName(item) == new DbTableKeyName(w)); }

        /// <summary>
        /// Gets a Table Given a Key
        /// </summary>
        /// <param name="item"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DbTableItem? GetTable(this IDbTableKeyName item, IEnumerable<DbTableItem> source)
        { return source.FirstOrDefault(w => new DbTableKeyName(item) == new DbTableKeyName(w)); }

        /// <summary>
        /// Gets a Table Given a Key
        /// </summary>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<DbTableItem> GetTables(this IEnumerable<DbTableItem> source, IDbSchemaKeyName item)
        { return source.Where(w => new DbSchemaKeyName(item) == new DbSchemaKeyName(w)); }

        /// <summary>
        /// Gets a list of Tables Given a Key
        /// </summary>
        /// <param name="item"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DbTableItem> GetTables(this IDbSchemaKeyName item, IEnumerable<DbTableItem> source)
        { return source.Where(w => new DbSchemaKeyName(item) == new DbSchemaKeyName(w)); }

        /// <summary>
        /// Gets a list of Tables Given a Key
        /// </summary>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<DbTableItem> GetTables(this IEnumerable<DbTableItem> source, IDbCatalogKeyName item)
        { return source.Where(w => new DbCatalogKeyName(item) == new DbCatalogKeyName(w)); }

        /// <summary>
        /// Gets a list of Tables Given a Key
        /// </summary>
        /// <param name="item"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DbTableItem> GetTables(this IDbCatalogKeyName item, IEnumerable<DbTableItem> source)
        { return source.Where(w => new DbCatalogKeyName(item) == new DbCatalogKeyName(w)); }

    }
}
