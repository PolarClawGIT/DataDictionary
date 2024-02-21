using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.ModelData;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Interface representing Catalog data
    /// </summary>
    public interface IDatabaseData :
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        INameScopeData
    {
        /// <summary>
        /// List of Database Catalogs within the Model.
        /// </summary>
        ICatalogData DbCatalogs { get; }

        /// <summary>
        /// List of Database Schemta within the Model.
        /// </summary>
        ISchemaData DbSchemta { get; }

        /// <summary>
        /// List of Database Domains (types) within the Model.
        /// </summary>
        IDomainData DbDomains { get; }

        /// <summary>
        /// List of Database Extended Properties within the Model.
        /// </summary>
        IExtendedPropertyData DbExtendedProperties { get; }

        /// <summary>
        /// List of Database Constraints (keys...) within the Model.
        /// </summary>
        IConstraintData DbConstraints { get; }

        /// <summary>
        /// List of Database Constraint Columns within the Model.
        /// </summary>
        IConstraintColumnData DbConstraintColumns { get; }

        /// <summary>
        /// List of Database Routines (procedures, functions, ...) within the Model.
        /// </summary>
        IRoutineData DbRoutines { get; }

        /// <summary>
        /// List of Database Parameters for the Routines within the Model.
        /// </summary>
        IRoutineParameterData DbRoutineParameters { get; }

        /// <summary>
        /// List of Database Dependencies for the Routines within the Model.
        /// </summary>
        IRoutineDependencyData DbRoutineDependencies { get; }

        /// <summary>
        /// List of Database Tables and Views within the Model.
        /// </summary>
        ITableData DbTables { get; }

        /// <summary>
        /// List of Database Columns for the Tables/Views within the Model.
        /// </summary>
        ITableColumnData DbTableColumns { get; }

        /// <summary>
        /// Removes a Catalog, by Key, and all child data.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Remove(IDbCatalogKey key);

        /// <summary>
        /// Imports a Catalog from a Database Source
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Import(DbSchemaContext source);
    }

    /// <summary>
    /// Interface used by the Database Data Items
    /// </summary>
    interface IDatabaseDataItem
    {
        /// <summary>
        /// The Wrapper Data Object that this object contained within.
        /// </summary>
        IDatabaseData Database { get; }
    }

    /// <summary>
    /// Implementation for Catalog data
    /// </summary>
    class DatabaseData : IDatabaseData, IDataTableFile
    {
        /// <inheritdoc/>
        public ICatalogData DbCatalogs { get { return catalogs; } }
        private readonly CatalogData catalogs;

        /// <inheritdoc/>
        public ISchemaData DbSchemta { get { return schemta; } }
        private readonly SchemaData schemta;

        /// <inheritdoc/>
        public IDomainData DbDomains { get { return domains; } }
        private readonly DomainData domains;

        /// <inheritdoc/>
        public IConstraintData DbConstraints { get { return constraints; } }
        private readonly ConstraintData constraints;

        /// <inheritdoc/>
        public IConstraintColumnData DbConstraintColumns { get { return constraintColumns; } }
        private readonly ConstraintColumnData constraintColumns;

        /// <inheritdoc/>
        public IExtendedPropertyData DbExtendedProperties { get { return extendedProperties; } }
        private readonly ExtendedPropertyData extendedProperties;

        /// <inheritdoc/>
        public IRoutineData DbRoutines { get { return routines; } }
        private readonly RoutineData routines;

        /// <inheritdoc/>
        public IRoutineParameterData DbRoutineParameters { get { return routineParameters; } }
        private readonly RoutineParameterData routineParameters;

        /// <inheritdoc/>
        public IRoutineDependencyData DbRoutineDependencies { get { return routineDependencies; } }
        private readonly RoutineDependencyData routineDependencies;

        /// <inheritdoc/>
        public ITableData DbTables { get { return tables; } }
        private readonly TableData tables;

        /// <inheritdoc/>
        public ITableColumnData DbTableColumns { get { return tableColumns; } }
        private readonly TableColumnData tableColumns;

        public DatabaseData() : base()
        {
            catalogs = new CatalogData() { Database = this };
            schemta = new SchemaData() { Database = this };
            domains = new DomainData() { Database = this };

            tables = new TableData() { Database = this };
            tableColumns = new TableColumnData() { Database = this };

            routines = new RoutineData() { Database = this };
            routineParameters = new RoutineParameterData() { Database = this };
            routineDependencies = new RoutineDependencyData() { Database = this };

            constraints = new ConstraintData() { Database = this };
            constraintColumns = new ConstraintColumnData() { Database = this };

            extendedProperties = new ExtendedPropertyData() { Database = this };
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(catalogs.Load(factory, dataKey));
            work.AddRange(schemta.Load(factory, dataKey));
            work.AddRange(domains.Load(factory, dataKey));
            work.AddRange(extendedProperties.Load(factory, dataKey));

            work.AddRange(tables.Load(factory, dataKey));
            work.AddRange(tableColumns.Load(factory, dataKey));

            work.AddRange(routines.Load(factory, dataKey));
            work.AddRange(routineParameters.Load(factory, dataKey));
            work.AddRange(routineDependencies.Load(factory, dataKey));

            work.AddRange(constraints.Load(factory, dataKey));
            work.AddRange(constraintColumns.Load(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(catalogs.Save(factory, dataKey));
            work.AddRange(schemta.Save(factory, dataKey));
            work.AddRange(domains.Save(factory, dataKey));
            work.AddRange(extendedProperties.Save(factory, dataKey));

            work.AddRange(tables.Save(factory, dataKey));
            work.AddRange(tableColumns.Save(factory, dataKey));

            work.AddRange(routines.Save(factory, dataKey));
            work.AddRange(routineParameters.Save(factory, dataKey));
            work.AddRange(routineDependencies.Save(factory, dataKey));

            work.AddRange(constraints.Save(factory, dataKey));
            work.AddRange(constraintColumns.Save(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDbCatalogKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(catalogs.Load(factory, dataKey));
            work.AddRange(schemta.Load(factory, dataKey));
            work.AddRange(domains.Load(factory, dataKey));
            work.AddRange(extendedProperties.Load(factory, dataKey));

            work.AddRange(tables.Load(factory, dataKey));
            work.AddRange(tableColumns.Load(factory, dataKey));

            work.AddRange(routines.Load(factory, dataKey));
            work.AddRange(routineParameters.Load(factory, dataKey));
            work.AddRange(routineDependencies.Load(factory, dataKey));

            work.AddRange(constraints.Load(factory, dataKey));
            work.AddRange(constraintColumns.Load(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDbCatalogKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(catalogs.Save(factory, dataKey));
            work.AddRange(schemta.Save(factory, dataKey));
            work.AddRange(domains.Save(factory, dataKey));
            work.AddRange(extendedProperties.Save(factory, dataKey));

            work.AddRange(tables.Save(factory, dataKey));
            work.AddRange(tableColumns.Save(factory, dataKey));

            work.AddRange(routines.Save(factory, dataKey));
            work.AddRange(routineParameters.Save(factory, dataKey));
            work.AddRange(routineDependencies.Save(factory, dataKey));

            work.AddRange(constraints.Save(factory, dataKey));
            work.AddRange(constraintColumns.Save(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(catalogs.ToDataTable());
            result.Add(schemta.ToDataTable());
            result.Add(domains.ToDataTable());

            result.Add(tables.ToDataTable());
            result.Add(tableColumns.ToDataTable());

            result.Add(routines.ToDataTable());
            result.Add(routineParameters.ToDataTable());
            result.Add(routineDependencies.ToDataTable());

            result.Add(constraints.ToDataTable());
            result.Add(constraintColumns.ToDataTable());

            result.Add(extendedProperties.ToDataTable());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public void Import(System.Data.DataSet source)
        {
            catalogs.Load(source);
            schemta.Load(source);
            domains.Load(source);

            tables.Load(source);
            tableColumns.Load(source);

            routines.Load(source);
            routineParameters.Load(source);
            routineDependencies.Load(source);

            constraints.Load(source);
            constraintColumns.Load(source);

            extendedProperties.Load(source);
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Import(DbSchemaContext source)
        {
            List<WorkItem> work = new List<WorkItem>();
            DbCatalogKey key = new DbCatalogKey(new DbCatalogItem());

            DatabaseWork factory = new DatabaseWork() { Connection = source.CreateConnection() };
            work.Add(factory.OpenConnection());

            work.Add(factory.CreateWork(
                            workName: "Load DbCatalogs",
                            target: catalogs,
                            command: (conn) => catalogs.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbSchemta",
                target: schemta,
                command: (conn) => schemta.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbDomains",
                target: domains,
                command: (conn) => domains.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbTables",
                target: tables,
                command: (conn) => tables.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbTableColumns",
                target: tableColumns,
                command: (conn) => tableColumns.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbConstraints",
                target: constraints,
                command: (conn) => constraints.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbConstraintColumns",
                target: constraintColumns,
                command: (conn) => constraintColumns.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbRoutines",
                target: routines,
                command: (conn) => routines.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbRoutineParameters",
                target: routineParameters,
                command: (conn) => routineParameters.SchemaCommand(conn, key)));

            work.Add(new WorkItem()
            {
                WorkName = "Load DbRoutineDependencies",
                DoWork = () =>
                {
                    foreach (DbRoutineItem item in routines)
                    {
                        routineDependencies.Load(
                            factory.Connection.ExecuteReader(
                                routineDependencies.SchemaCommand(
                                    factory.Connection, item)));
                    }
                },
                IsCanceling = () => factory.IsCanceling
            });

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties, DbSchemta",
                source: schemta,
                target: extendedProperties));

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties, DbTables",
                source: tables,
                target: extendedProperties));

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties, DbTableColumns",
                source: tableColumns,
                target: extendedProperties));

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties, DbConstraints",
                source: constraints,
                target: extendedProperties));

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties, DbDomains",
                source: domains,
                target: extendedProperties));

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties, DbRoutines",
                source: routines,
                target: extendedProperties));

            work.Add(factory.CreateWork(
                workName: "Load DbExtendedProperties, DbRoutineParameters",
                source: routineParameters,
                target: extendedProperties));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Remove(IDbCatalogKey key)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Remove Catalog", DoWork = () => { catalogs.Remove(key); } });
            work.Add(new WorkItem() { WorkName = "Remove Schemta", DoWork = () => { schemta.Remove(key); } });
            work.Add(new WorkItem() { WorkName = "Remove Domains", DoWork = () => { domains.Remove(key); } });

            work.Add(new WorkItem() { WorkName = "Remove Tables", DoWork = () => { tables.Remove(key); } });
            work.Add(new WorkItem() { WorkName = "Remove Table Columns", DoWork = () => { tableColumns.Remove(key); } });

            work.Add(new WorkItem() { WorkName = "Remove Routines", DoWork = () => { routines.Remove(key); } });
            work.Add(new WorkItem() { WorkName = "Remove Routine Parameters", DoWork = () => { routineParameters.Remove(key); } });
            work.Add(new WorkItem() { WorkName = "Remove Routine Dependencies", DoWork = () => { routineDependencies.Remove(key); } });

            work.Add(new WorkItem() { WorkName = "Remove Constraints", DoWork = () => { constraints.Remove(key); } });
            work.Add(new WorkItem() { WorkName = "Remove Constraint Columns", DoWork = () => { constraintColumns.Remove(key); } });

            work.Add(new WorkItem() { WorkName = "Remove Extended Properties", DoWork = () => { extendedProperties.Remove(key); } });
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Export(IList<NameScopeItem> target)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(catalogs.Export(target));
            work.AddRange(schemta.Export(target));
            work.AddRange(domains.Export(target));

            work.AddRange(tables.Export(target));
            work.AddRange(tableColumns.Export(target));

            work.AddRange(routines.Export(target));
            work.AddRange(routineParameters.Export(target));

            work.AddRange(constraints.Export(target));

            return work;
        }
    }
}
