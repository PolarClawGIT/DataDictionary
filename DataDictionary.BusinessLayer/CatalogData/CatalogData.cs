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
    public interface ICatalogData:
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <summary>
        /// List of Database Catalogs within the Model.
        /// </summary>
        IDatabaseData DbCatalogs { get; }

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
    }

    /// <summary>
    /// Implementation for Catalog data
    /// </summary>
    class CatalogData :ICatalogData, IContextNameData
    {
        /// <inheritdoc/>
        public IDatabaseData DbCatalogs { get { return catalogs; } }
        private readonly DatabaseData catalogs;

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

        public CatalogData() : base()
        {
            catalogs = new DatabaseData();
            schemta = new SchemaData();
            domains = new DomainData();
            extendedProperties = new ExtendedPropertyData();

            tables = new TableData();
            tableColumns = new TableColumnData();

            routines = new RoutineData();
            routineParameters = new RoutineParameterData();
            routineDependencies = new RoutineDependencyData();

            constraints = new ConstraintData();
            constraintColumns = new ConstraintColumnData();
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


        /// <inheritdoc />
        public void Remove(IDbCatalogKey catalogItem)
        {
            catalogs.Remove(catalogItem);
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

        /// <inheritdoc />
        public IReadOnlyList<ContextNameItem> GetContextNames (Action<Int32, Int32> progress)
        {
            // This improve performance by reducing the number of calls
            List<ContextNameItem> result = new List<ContextNameItem>();
            List<DbCatalogItem> catalogs = DbCatalogs.Where(w => w.IsSystem == false).ToList();
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
