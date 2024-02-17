using DataDictionary.BusinessLayer.ContextName;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.CatalogData
{
    /// <summary>
    /// Interface representing Catalog data
    /// </summary>
    public interface ICatalogData :
        IBindingData<DbCatalogItem>,
        ILoadData<IDbCatalogKey>, ILoadData<IModelKey>,
        ISaveData<IDbCatalogKey>, ISaveData<IModelKey>
    {
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
        /// Removes the Catalog and its children by Key
        /// </summary>
        /// <param name="catalogItem"></param>
        void Remove(IDbCatalogKey catalogItem);
    }

    /// <summary>
    /// Implementation for Catalog data
    /// </summary>
    class CatalogData : DbCatalogCollection, ICatalogData
    {
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

        /// <summary>
        /// Constructor for CatalogData
        /// </summary>
        public CatalogData() : base()
        {
            schemta = new SchemaData() { Catalog = this };
            domains = new DomainData() { Catalog = this };
            extendedProperties = new ExtendedPropertyData() { Catalog = this };

            tables = new TableData() { Catalog = this };
            tableColumns = new TableColumnData() { Catalog = this };

            routines = new RoutineData() { Catalog = this };
            routineParameters = new RoutineParameterData() { Catalog = this };
            routineDependencies = new RoutineDependencyData() { Catalog = this };

            constraints = new ConstraintData() { Catalog = this };
            constraintColumns = new ConstraintColumnData() { Catalog = this };
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDbCatalogKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, dataKey));
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
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, dataKey));
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

        /// <summary>
        /// Create Work Items that Load Catalog meta data from a source
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load(DbSchemaContext source)
        {
            List<WorkItem> work = new List<WorkItem>();
            DbCatalogKey key = new DbCatalogKey(new DbCatalogItem());
            DatabaseWork factory = new DatabaseWork() { Connection = source.CreateConnection() };

            work.Add(factory.OpenConnection());

            work.Add(factory.CreateWork(
                workName: "Load DbCatalogs",
                target: this,
                command: (conn) => this.SchemaCommand(conn, key)));

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
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDbCatalogKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(factory.CreateSave(this, dataKey).ToList());
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
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(factory.CreateSave(this, dataKey).ToList());
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

        /// <inheritdoc />
        public override void Remove(IDbCatalogKey catalogItem)
        {
            base.Remove(catalogItem);
            schemta.Remove(catalogItem);
            domains.Remove(catalogItem);

            tables.Remove(catalogItem);
            tableColumns.Remove(catalogItem);

            routines.Remove(catalogItem);
            routineParameters.Remove(catalogItem);
            routineDependencies.Remove(catalogItem);

            constraints.Remove(catalogItem);
            constraintColumns.Remove(catalogItem);
        }

        internal IReadOnlyList<ContextNameItem> GetContextNames (Action<Int32, Int32> progress)
        {
            // This improve performance by reducing the number of calls
            List<ContextNameItem> result = new List<ContextNameItem>();
            List<DbCatalogItem> catalogs = this.Where(w => w.IsSystem == false).ToList();
            List<DbSchemaItem> schemta = DbSchemta.Where(w => w.IsSystem == false).ToList();
            List<DbTableItem> tables = DbTables.Where(w => w.IsSystem == false).ToList();
            List<DbTableColumnItem> tableColumns = DbTableColumns.ToList();
            List<DbConstraintItem> constraints = DbConstraints.ToList();
            List<DbRoutineItem> routines = DbRoutines.Where(w => w.IsSystem == false).ToList();
            List<DbRoutineParameterItem> routineParameters = DbRoutineParameters.ToList();
            List<DbDomainItem> domains = DbDomains.ToList();

            Int32 totalWork = catalogs.Count +
                    schemta.Count +
                    tables.Count +
                    tableColumns.Count +
                    constraints.Count +
                    routines.Count +
                    routineParameters.Count +
                    domains.Count;
            Int32 completedWork = 0;

            foreach (DbCatalogItem catalogItem in catalogs) // Expect zero or one
            {
                DbCatalogKey catalogKey = new DbCatalogKey(catalogItem);
                DbCatalogKeyName catalogName = new DbCatalogKeyName(catalogItem);
                result.Add(new ContextNameItem(catalogItem));
                progress(completedWork++, totalWork);

                foreach (DbSchemaItem schemaItem in schemta.Where(w => catalogName.Equals(w)).ToList())
                {
                    DbSchemaKey schemaKey = new DbSchemaKey(schemaItem);
                    DbSchemaKeyName schemaName = new DbSchemaKeyName(schemaItem);
                    result.Add(new ContextNameItem(catalogKey, schemaItem));
                    progress(completedWork++, totalWork);

                    foreach (DbTableItem tableItem in tables.Where(w => schemaName.Equals(w)).ToList())
                    {
                        DbTableKey tableKey = new DbTableKey(tableItem);
                        DbTableKeyName tableName = new DbTableKeyName(tableItem);
                        result.Add(new ContextNameItem(schemaKey, tableItem));
                        progress(completedWork++, totalWork);

                        foreach (DbTableColumnItem columnItem in tableColumns.Where(w => tableName.Equals(w)).ToList())
                        {
                            result.Add(new ContextNameItem(tableKey, columnItem));
                            progress(completedWork++, totalWork);
                        }

                        foreach (DbConstraintItem constraintItem in constraints.Where(w => tableName.Equals(w)).ToList())
                        {
                            result.Add(new ContextNameItem(tableKey, constraintItem));
                            progress(completedWork++, totalWork);
                        }
                    }

                    foreach (DbRoutineItem routineItem in routines.Where(w => schemaName.Equals(w)).ToList())
                    {
                        DbRoutineKey routineKey = new DbRoutineKey(routineItem);
                        DbRoutineKeyName routineName = new DbRoutineKeyName(routineItem);
                        result.Add(new ContextNameItem(schemaKey, routineItem));
                        progress(completedWork++, totalWork);

                        foreach (DbRoutineParameterItem parameterItem in routineParameters.Where(w => routineName.Equals(w)).ToList())
                        {
                            result.Add(new ContextNameItem(routineKey, parameterItem));
                            progress(completedWork++, totalWork);
                        }
                    }

                    foreach (DbDomainItem domainItem in domains.Where(w => schemaName.Equals(w)).ToList())
                    {
                        result.Add(new ContextNameItem(schemaKey, domainItem));
                        progress(completedWork++, totalWork);
                    }
                }
            }



            return result;
        }
    }
}
