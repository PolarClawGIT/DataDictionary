using DataDictionary.BusinessLayer.NamedScope;
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
    public interface IDatabaseModel :
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>, IRemoveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        INamedScopeData
    {
        /// <summary>
        /// List of Database Catalogs within the Model.
        /// </summary>
        ICatalogData<CatalogValue> DbCatalogs { get; }

        /// <summary>
        /// List of Database Schemta within the Model.
        /// </summary>
        ISchemaData<SchemaValue> DbSchemta { get; }

        /// <summary>
        /// List of Database Domains (types) within the Model.
        /// </summary>
        IDomainData<DomainValue> DbDomains { get; }

        /// <summary>
        /// List of Database Extended Properties within the Model.
        /// </summary>
        IExtendedPropertyData<ExtendedPropertyValue> DbExtendedProperties { get; }

        /// <summary>
        /// List of Database Constraints (keys...) within the Model.
        /// </summary>
        IConstraintData<ConstraintValue> DbConstraints { get; }

        /// <summary>
        /// List of Database Constraint Columns within the Model.
        /// </summary>
        IConstraintColumnData<ConstraintColumnValue> DbConstraintColumns { get; }

        /// <summary>
        /// List of Database Routines (procedures, functions, ...) within the Model.
        /// </summary>
        IRoutineData<RoutineValue> DbRoutines { get; }

        /// <summary>
        /// List of Database Parameters for the Routines within the Model.
        /// </summary>
        IRoutineParameterData<RoutineParameterValue> DbRoutineParameters { get; }

        /// <summary>
        /// List of Database Dependencies for the Routines within the Model.
        /// </summary>
        IRoutineDependencyData<RoutineDependencyValue> DbRoutineDependencies { get; }

        /// <summary>
        /// List of Database Tables and Views within the Model.
        /// </summary>
        ITableData<TableValue> DbTables { get; }

        /// <summary>
        /// List of Database Columns for the Tables/Views within the Model.
        /// </summary>
        ITableColumnData<TableColumnValue> DbTableColumns { get; }

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
    interface IDatabaseModelItem
    {
        /// <summary>
        /// The Wrapper Data Object that this object contained within.
        /// </summary>
        IDatabaseModel Database { get; }
    }

    /// <summary>
    /// Implementation for Catalog data
    /// </summary>
    class DatabaseModel : IDatabaseModel, IDataTableFile
    {
        /// <inheritdoc/>
        public ICatalogData<CatalogValue> DbCatalogs { get { return catalogs; } }
        private readonly CatalogData<CatalogValue> catalogs;

        /// <inheritdoc/>
        public ISchemaData<SchemaValue> DbSchemta { get { return schemta; } }
        private readonly SchemaData<SchemaValue> schemta;

        /// <inheritdoc/>
        public IDomainData<DomainValue> DbDomains { get { return domains; } }
        private readonly DomainData<DomainValue> domains;

        /// <inheritdoc/>
        public IConstraintData<ConstraintValue> DbConstraints { get { return constraints; } }
        private readonly ConstraintData<ConstraintValue> constraints;

        /// <inheritdoc/>
        public IConstraintColumnData<ConstraintColumnValue> DbConstraintColumns { get { return constraintColumns; } }
        private readonly ConstraintColumnData<ConstraintColumnValue> constraintColumns;

        /// <inheritdoc/>
        public IExtendedPropertyData<ExtendedPropertyValue> DbExtendedProperties { get { return extendedProperties; } }
        private readonly ExtendedPropertyData<ExtendedPropertyValue> extendedProperties;

        /// <inheritdoc/>
        public IRoutineData<RoutineValue> DbRoutines { get { return routines; } }
        private readonly RoutineData<RoutineValue> routines;

        /// <inheritdoc/>
        public IRoutineParameterData<RoutineParameterValue> DbRoutineParameters { get { return routineParameters; } }
        private readonly RoutineParameterData<RoutineParameterValue> routineParameters;

        /// <inheritdoc/>
        public IRoutineDependencyData<RoutineDependencyValue> DbRoutineDependencies { get { return routineDependencies; } }
        private readonly RoutineDependencyData<RoutineDependencyValue> routineDependencies;

        /// <inheritdoc/>
        public ITableData<TableValue> DbTables { get { return tables; } }
        private readonly TableData<TableValue> tables;

        /// <inheritdoc/>
        public ITableColumnData<TableColumnValue> DbTableColumns { get { return tableColumns; } }
        private readonly TableColumnData<TableColumnValue> tableColumns;

        public DatabaseModel() : base()
        {
            catalogs = new CatalogData<CatalogValue>() { Database = this };
            schemta = new SchemaData<SchemaValue>() { Database = this };
            domains = new DomainData<DomainValue>() { Database = this };

            tables = new TableData<TableValue>() { Database = this };
            tableColumns = new TableColumnData<TableColumnValue>() { Database = this };

            routines = new RoutineData<RoutineValue>() { Database = this };
            routineParameters = new RoutineParameterData<RoutineParameterValue>() { Database = this };
            routineDependencies = new RoutineDependencyData<RoutineDependencyValue>() { Database = this };

            constraints = new ConstraintData<ConstraintValue>() { Database = this };
            constraintColumns = new ConstraintColumnData<ConstraintColumnValue>() { Database = this };

            extendedProperties = new ExtendedPropertyData<ExtendedPropertyValue>() { Database = this };
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

            DatabaseWork factory = new DatabaseWork(source);
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
        public IReadOnlyList<WorkItem> Remove()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Remove Catalog", DoWork = () => { catalogs.Clear(); } });
            work.Add(new WorkItem() { WorkName = "Remove Schemta", DoWork = () => { schemta.Clear(); } });
            work.Add(new WorkItem() { WorkName = "Remove Domains", DoWork = () => { domains.Clear(); } });

            work.Add(new WorkItem() { WorkName = "Remove Tables", DoWork = () => { tables.Clear(); } });
            work.Add(new WorkItem() { WorkName = "Remove Table Columns", DoWork = () => { tableColumns.Clear(); } });

            work.Add(new WorkItem() { WorkName = "Remove Routines", DoWork = () => { routines.Clear(); } });
            work.Add(new WorkItem() { WorkName = "Remove Routine Parameters", DoWork = () => { routineParameters.Clear(); } });
            work.Add(new WorkItem() { WorkName = "Remove Routine Dependencies", DoWork = () => { routineDependencies.Clear(); } });

            work.Add(new WorkItem() { WorkName = "Remove Constraints", DoWork = () => { constraints.Clear(); } });
            work.Add(new WorkItem() { WorkName = "Remove Constraint Columns", DoWork = () => { constraintColumns.Clear(); } });

            work.Add(new WorkItem() { WorkName = "Remove Extended Properties", DoWork = () => { extendedProperties.Clear(); } });


            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Build(INamedScopeDictionary target)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(catalogs.Build(target));
            work.AddRange(schemta.Build(target));
            work.AddRange(domains.Build(target));

            work.AddRange(tables.Build(target));
            work.AddRange(tableColumns.Build(target));

            work.AddRange(routines.Build(target));
            work.AddRange(routineParameters.Build(target));

            work.AddRange(constraints.Build(target));

            return work;
        }
    }
}
