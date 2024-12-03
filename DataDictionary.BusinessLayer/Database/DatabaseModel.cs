using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ModelData;
using Toolbox.BindingTable;
using Toolbox.Threading;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.BusinessLayer.AppCatalog;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Interface representing Catalog data
    /// </summary>
    public interface IDatabaseModel :
        ILoadData<ICatalogKey>, ISaveData<ICatalogKey>, IDeleteData<ICatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
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
        AppCatalog.IDomainData DbDomains { get; }

        /// <summary>
        /// List of Database Extended Properties within the Model.
        /// </summary>
        AppCatalog.IPropertyData DbProperties { get; }

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
        /// List of Database Columns for the Routines within the Model.
        /// </summary>
        IRoutineColumnData DbRoutineColumns { get; }

        /// <summary>
        /// List of Database References
        /// </summary>
        IReferenceData DbReferences { get; }

        /// <summary>
        /// List of Database Tables and Views within the Model.
        /// </summary>
        ITableData DbTables { get; }

        /// <summary>
        /// List of Database Columns for the Tables/Views within the Model.
        /// </summary>
        ITableColumnData DbTableColumns { get; }

        /// <summary>
        /// Imports a Catalog from a Database Source
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Import(DbSchemaContext source);

        /// <summary>
        /// Gets the MS SQL ExtendedProperty "MS_Description", if any.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Value or Null if no MS_Description.</returns>
        /// <example><![CDATA[
        ///  TableValue dbObject = new TableValue(); // PropertyIndexObject takes an DbObject Name Keys
        ///  String? result = GetDescription(new PropertyIndexObject(dbObject));
        /// ]]></example>
        String? GetDescription(PropertyIndexObject key);
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
    class DatabaseModel : IDatabaseModel, IDataTableFile,
        INamedScopeSourceData
    {
        /// <inheritdoc/>
        public ICatalogData DbCatalogs { get { return catalogs; } }
        private readonly CatalogData catalogs;

        /// <inheritdoc/>
        public ISchemaData DbSchemta { get { return schemta; } }
        private readonly SchemaData schemta;

        /// <inheritdoc/>
        public AppCatalog.IDomainData DbDomains { get { return domains; } }
        private readonly AppCatalog.DomainData domains;

        /// <inheritdoc/>
        public IConstraintData DbConstraints { get { return constraints; } }
        private readonly ConstraintData constraints;

        /// <inheritdoc/>
        public IConstraintColumnData DbConstraintColumns { get { return constraintColumns; } }
        private readonly ConstraintColumnData constraintColumns;

        /// <inheritdoc/>
        public AppCatalog.IPropertyData DbProperties { get { return properties; } }
        private readonly AppCatalog.PropertyData properties;

        /// <inheritdoc/>
        public IRoutineData DbRoutines { get { return routines; } }
        private readonly RoutineData routines;

        /// <inheritdoc/>
        public IRoutineParameterData DbRoutineParameters { get { return routineParameters; } }
        private readonly RoutineParameterData routineParameters;

        /// <inheritdoc/>
        public IRoutineColumnData DbRoutineColumns { get { return routineColumns; } }
        private readonly RoutineColumnData routineColumns;

        /// <inheritdoc/>
        public IReferenceData DbReferences { get { return references; } }
        private readonly ReferenceData references;

        /// <inheritdoc/>
        public ITableData DbTables { get { return tables; } }
        private readonly TableData tables;

        /// <inheritdoc/>
        public ITableColumnData DbTableColumns { get { return tableColumns; } }
        private readonly TableColumnData tableColumns;

        public DatabaseModel() : base()
        {
            catalogs = new CatalogData() { Database = this };
            schemta = new SchemaData() { Database = this };
            domains = new DomainData() { Database = this };

            tables = new TableData() { Database = this };
            tableColumns = new TableColumnData() { Database = this };

            routines = new RoutineData() { Database = this };
            routineParameters = new RoutineParameterData() { Database = this };
            routineColumns = new RoutineColumnData() { Database = this };
            references = new ReferenceData() { Database = this };

            constraints = new ConstraintData() { Database = this };
            constraintColumns = new ConstraintColumnData() { Database = this };

            properties = new PropertyData() { Database = this };
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(catalogs.Load(factory, dataKey));
            work.AddRange(schemta.Load(factory, dataKey));
            work.AddRange(domains.Load(factory, dataKey));
            work.AddRange(properties.Load(factory, dataKey));

            work.AddRange(tables.Load(factory, dataKey));
            work.AddRange(tableColumns.Load(factory, dataKey));

            work.AddRange(routines.Load(factory, dataKey));
            work.AddRange(routineParameters.Load(factory, dataKey));
            work.AddRange(routineColumns.Load(factory, dataKey));
            work.AddRange(references.Load(factory, dataKey));

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
            work.AddRange(properties.Save(factory, dataKey));

            work.AddRange(tables.Save(factory, dataKey));
            work.AddRange(tableColumns.Save(factory, dataKey));

            work.AddRange(routines.Save(factory, dataKey));
            work.AddRange(routineParameters.Save(factory, dataKey));
            work.AddRange(routineColumns.Save(factory, dataKey));
            work.AddRange(references.Save(factory, dataKey));

            work.AddRange(constraints.Save(factory, dataKey));
            work.AddRange(constraintColumns.Save(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(catalogs.Load(factory, dataKey));
            work.AddRange(schemta.Load(factory, dataKey));
            work.AddRange(domains.Load(factory, dataKey));
            work.AddRange(properties.Load(factory, dataKey));

            work.AddRange(tables.Load(factory, dataKey));
            work.AddRange(tableColumns.Load(factory, dataKey));

            work.AddRange(routines.Load(factory, dataKey));
            work.AddRange(routineParameters.Load(factory, dataKey));
            work.AddRange(routineColumns.Load(factory, dataKey));
            work.AddRange(references.Load(factory, dataKey));

            work.AddRange(constraints.Load(factory, dataKey));
            work.AddRange(constraintColumns.Load(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ICatalogKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(catalogs.Save(factory, dataKey));
            work.AddRange(schemta.Save(factory, dataKey));
            work.AddRange(domains.Save(factory, dataKey));
            work.AddRange(properties.Save(factory, dataKey));

            work.AddRange(tables.Save(factory, dataKey));
            work.AddRange(tableColumns.Save(factory, dataKey));

            work.AddRange(routines.Save(factory, dataKey));
            work.AddRange(routineParameters.Save(factory, dataKey));
            work.AddRange(routineColumns.Save(factory, dataKey));
            work.AddRange(references.Save(factory, dataKey));

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
            result.Add(routineColumns.ToDataTable());
            result.Add(references.ToDataTable());

            result.Add(constraints.ToDataTable());
            result.Add(constraintColumns.ToDataTable());

            result.Add(properties.ToDataTable());
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
            routineColumns.Load(source);
            references.Load(source);

            constraints.Load(source);
            constraintColumns.Load(source);

            properties.Load(source);
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        [Obsolete("Needs rework")]
        public IReadOnlyList<WorkItem> Import(DbSchemaContext source)
        {
            List<WorkItem> work = new List<WorkItem>();
            CatalogKey key = new CatalogKey(); // Dummy value

            //TODO: Need to re-work loading of the Db Schema
            //The ID's are not yet assigned so they need to be looked up.
            // Old methods assumed that the SQL Script assigned everything.
            // That does not work with temporal data.

            DatabaseWork factory = new DatabaseWork(source);
            work.Add(factory.OpenConnection());

            work.Add(factory.CreateImport(
                workName: "Import InformationSchema- Catalog",
                getData: CatalogMetaData.GetSchema,
                import: (data) =>
                {
                    ICatalogKey result = catalogs.Import(data);
                    if (result is ICatalogKey) { key = new CatalogKey(result); }
                    else { throw new InvalidOperationException("CatalogKey could not be determined."); }
                }));

            work.Add(factory.CreateImport(
                workName: "Import InformationSchema- Schema",
                getData: SchemaMetaData.GetSchema,
                import: (data) => schemta.Import(key, data)));

            work.Add(factory.CreateImport(
               workName: "Import InformationSchema- Domain",
               getData: DomainMetaData.GetSchema,
               import: (data) => domains.Import(key, data)));

            work.Add(factory.CreateImport(
               workName: "Import InformationSchema- Table",
               getData: TableMetaData.GetSchema,
               import: (data) => tables.Import(key, data)));

            work.Add(factory.CreateImport(
               workName: "Import InformationSchema- TableColumn",
               getData: TableColumnMetaData.GetSchema,
               import: (data) => tableColumns.Import(key, data)));

            work.Add(factory.CreateWork(
                workName: "Load DbConstraints",
                target: constraints,
                command: (conn) => constraints.SchemaCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DbConstraintColumns",
                target: constraintColumns,
                command: (conn) => constraintColumns.SchemaCommand(conn, key)));

            work.Add(factory.CreateImport(
               workName: "Import InformationSchema- Routine",
               getData: RoutineMetaData.GetSchema,
               import: (data) => routines.Import(key, data)));

            work.Add(factory.CreateImport(
               workName: "Import InformationSchema- RoutineParameter",
               getData: RoutineParameterMetaData.GetSchema,
               import: (data) => routineParameters.Import(key, data)));

            work.Add(factory.CreateImport(
               workName: "Import InformationSchema- RoutineColumn",
               getData: RoutineColumnMetaData.GetSchema,
               import: (data) => routineColumns.Import(key, data)));


            //work.Add(new WorkItem()
            //{
            //    WorkName = "Load DbReferences",
            //    DoWork = () =>
            //    {
            //        foreach (TableItem item in tables)
            //        {
            //            references.Load(
            //                factory.Connection.ExecuteReader(
            //                    references.SchemaCommand(
            //                        factory.Connection, item)));
            //        }

            //        foreach (RoutineItem item in routines)
            //        {
            //            references.Load(
            //                factory.Connection.ExecuteReader(
            //                    references.SchemaCommand(
            //                        factory.Connection, item)));
            //        }
            //    },
            //    IsCanceling = () => factory.IsCanceling
            //});

            work.Add(factory.CreateImport(
               workName: "Import Extended Properties",
               getData: PropertyMetaData.GetProperties,
               import: (data) => properties.Import(key, data)));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Delete(ICatalogKey key)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(catalogs.Delete(key));
            work.AddRange(schemta.Delete(key));
            work.AddRange(domains.Delete(key));

            work.AddRange(tables.Delete(key));
            work.AddRange(tableColumns.Delete(key));

            work.AddRange(routines.Delete(key));
            work.AddRange(routineParameters.Delete(key));
            work.AddRange(routineColumns.Delete(key));
            work.AddRange(references.Delete(key));

            work.AddRange(constraints.Delete(key));
            work.AddRange(constraintColumns.Delete(key));

            work.AddRange(properties.Delete(key));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(catalogs.Delete());
            work.AddRange(schemta.Delete());
            work.AddRange(domains.Delete());

            work.AddRange(tables.Delete());
            work.AddRange(tableColumns.Delete());

            work.AddRange(routines.Delete());
            work.AddRange(routineParameters.Delete());
            work.AddRange(routineColumns.Delete());
            work.AddRange(references.Delete());

            work.AddRange(constraints.Delete());
            work.AddRange(constraintColumns.Delete());

            work.AddRange(properties.Delete());

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        public String? GetDescription(PropertyIndexObject key)
        {
            if (DbProperties.FirstOrDefault(w => w.IsDescription && key.Equals(w)) is IPropertyValue value)
            { return value.PropertyValue; }
            else { return null; }
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(catalogs.LoadNamedScope(addNamedScope));
            work.AddRange(schemta.LoadNamedScope(addNamedScope));
            work.AddRange(domains.LoadNamedScope(addNamedScope));

            work.AddRange(tables.LoadNamedScope(addNamedScope));
            work.AddRange(tableColumns.LoadNamedScope(addNamedScope));

            work.AddRange(routines.LoadNamedScope(addNamedScope));
            work.AddRange(routineParameters.LoadNamedScope(addNamedScope));
            work.AddRange(routineColumns.LoadNamedScope(addNamedScope));

            work.AddRange(constraints.LoadNamedScope(addNamedScope));

            return work;
        }
    }
}
