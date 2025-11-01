// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.DbWorkItem;
using Toolbox.BindingTable;
using Toolbox.Threading;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.BusinessLayer.ToolSet;
using System.ComponentModel;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <summary>
    /// Interface representing Catalog data
    /// </summary>
    public interface ICatalog :
        ILoadData<ICatalogIndex>, ISaveData<ICatalogIndex>, IDeleteData<ICatalogIndex>,
        ILoadData<AppModel.IModelIndex>, ISaveData<AppModel.IModelIndex>,
        IBindListChanged
    {
        /// <summary>
        /// List of Model Catalogs within the Model.
        /// </summary>
        ICatalogData DbCatalogs { get; }

        /// <summary>
        /// List of Model Schemta within the Model.
        /// </summary>
        ISchemaData DbSchemta { get; }

        /// <summary>
        /// List of Model Domains (types) within the Model.
        /// </summary>
        IDomainData DbDomains { get; }

        /// <summary>
        /// List of Model Extended Properties within the Model.
        /// </summary>
        IPropertyData DbProperties { get; }

        /// <summary>
        /// List of Model Constraints (keys...) within the Model.
        /// </summary>
        IConstraintData DbConstraints { get; }

        /// <summary>
        /// List of Model Constraint Columns within the Model.
        /// </summary>
        IConstraintColumnData DbConstraintColumns { get; }

        /// <summary>
        /// List of Model Routines (procedures, functions, ...) within the Model.
        /// </summary>
        IRoutineData DbRoutines { get; }

        /// <summary>
        /// List of Model Parameters for the Routines within the Model.
        /// </summary>
        IRoutineParameterData DbRoutineParameters { get; }

        /// <summary>
        /// List of Model Columns for the Routines within the Model.
        /// </summary>
        IRoutineColumnData DbRoutineColumns { get; }

        /// <summary>
        /// List of Model References
        /// </summary>
        IReferenceData DbReferences { get; }

        /// <summary>
        /// List of Model Tables and Views within the Model.
        /// </summary>
        ITableData DbTables { get; }

        /// <summary>
        /// List of Model Columns for the Tables/Views within the Model.
        /// </summary>
        ITableColumnData DbTableColumns { get; }

        /// <summary>
        /// Imports a Catalog from a Model Source
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
    /// Implementation for Catalog data
    /// </summary>
    class Catalog : ICatalog, IDataTableFile
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
        public IPropertyData DbProperties { get { return properties; } }
        private readonly PropertyData properties;

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


        public Catalog() : base()
        {
            catalogs = new CatalogData() { Model = this };
            schemta = new SchemaData() { Model = this };
            domains = new DomainData() { Model = this };

            tables = new TableData() { Model = this };
            tableColumns = new TableColumnData() { Model = this };

            routines = new RoutineData() { Model = this };
            routineParameters = new RoutineParameterData() { Model = this };
            routineColumns = new RoutineColumnData() { Model = this };
            references = new ReferenceData() { Model = this };

            constraints = new ConstraintData() { Model = this };
            constraintColumns = new ConstraintColumnData() { Model = this };

            properties = new PropertyData() { Model = this };

            catalogs.ListChanged += OnListChanged;
            schemta.ListChanged += OnListChanged;
            domains.ListChanged += OnListChanged;
            properties.ListChanged += OnListChanged;
            tables.ListChanged += OnListChanged;
            tableColumns.ListChanged += OnListChanged;
            routines.ListChanged += OnListChanged;
            routineParameters.ListChanged += OnListChanged;
            routineColumns.ListChanged += OnListChanged;
            references.ListChanged += OnListChanged;
            constraints.ListChanged += OnListChanged;
            constraintColumns.ListChanged += OnListChanged;

            void OnListChanged(Object? sender, ListChangedEventArgs e)
            {
                if (ListChanged is ListChangedEventHandler handler)
                { handler(sender, e); }
            }
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, AppModel.IModelIndex dataKey)
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
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, AppModel.IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(catalogs.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(schemta.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(domains.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(properties.Load(factory, dataKey, asOfUtcDate));

            work.AddRange(tables.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(tableColumns.Load(factory, dataKey, asOfUtcDate));

            work.AddRange(routines.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(routineParameters.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(routineColumns.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(references.Load(factory, dataKey, asOfUtcDate));

            work.AddRange(constraints.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(constraintColumns.Load(factory, dataKey, asOfUtcDate));

            return work;
        }


        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogIndex dataKey)
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
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(catalogs.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(schemta.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(domains.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(properties.Load(factory, dataKey, asOfUtcDate));

            work.AddRange(tables.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(tableColumns.Load(factory, dataKey, asOfUtcDate));

            work.AddRange(routines.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(routineParameters.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(routineColumns.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(references.Load(factory, dataKey, asOfUtcDate));

            work.AddRange(constraints.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(constraintColumns.Load(factory, dataKey, asOfUtcDate));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, AppModel.IModelIndex dataKey)
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
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ICatalogIndex dataKey)
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
        public IReadOnlyList<WorkItem> Import(DbSchemaContext source)
        {
            List<WorkItem> work = new List<WorkItem>();
            CatalogKey key = new CatalogKey(); // Dummy value

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

            work.Add(factory.CreateImport(
               workName: "Import InformationSchema- Constraint",
               getData: ConstraintMetaData.GetSchema,
               import: (data) => constraints.Import(key, data)));

            work.Add(factory.CreateImport(
               workName: "Import InformationSchema- ConstraintColumn",
               getData: ConstraintColumnMetaData.GetSchema,
               import: (data) => constraintColumns.Import(key, data)));

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

            work.Add(factory.CreateImport(
               workName: "Import Object Reference",
               getData: ReferenceMetaData.GetSchema,
               import: (data) => references.Import(key, data)));

            work.Add(factory.CreateImport(
               workName: "Import Extended Properties",
               getData: PropertyMetaData.GetProperties,
               import: (data) => properties.Import(key, data)));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Delete(ICatalogIndex key)
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
        public IReadOnlyList<WorkItem> Delete(AppModel.IModelIndex dataKey)
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

            work.AddRange(NameSpaceSource.Load<CatalogData, CatalogValue>(catalogs, addNamedScope));
            work.AddRange(NameSpaceSource.Load<SchemaData, SchemaValue>(schemta, addNamedScope,
                (parent) => catalogs.FirstOrDefault(w => new CatalogKeyName(parent).Equals(w))));

            work.AddRange(NameSpaceSource.Load<DomainData, DomainValue>(domains, addNamedScope,
                (parent) => schemta.FirstOrDefault(w => new SchemaKeyName(parent).Equals(w))));

            work.AddRange(NameSpaceSource.Load<TableData, TableValue>(tables, addNamedScope,
                (parent) => schemta.FirstOrDefault(w => new SchemaKeyName(parent).Equals(w))));
            work.AddRange(NameSpaceSource.Load<TableColumnData, TableColumnValue>(tableColumns, addNamedScope,
                (parent) => tables.FirstOrDefault(w => new TableKeyName(parent).Equals(w))));
            work.AddRange(NameSpaceSource.Load<ConstraintData, ConstraintValue>(constraints, addNamedScope,
                (parent) => tables.FirstOrDefault(w => new TableKeyName(parent).Equals(w))));

            work.AddRange(NameSpaceSource.Load<RoutineData, RoutineValue>(routines, addNamedScope,
                (parent) => schemta.FirstOrDefault(w => new SchemaKeyName(parent).Equals(w))));
            work.AddRange(NameSpaceSource.Load<RoutineParameterData, RoutineParameterValue>(routineParameters, addNamedScope,
                (parent) => routines.FirstOrDefault(w => new RoutineKeyName(parent).Equals(w))));
            work.AddRange(NameSpaceSource.Load<RoutineColumnData, RoutineColumnValue>(routineColumns, addNamedScope,
                (parent) => routines.FirstOrDefault(w => new RoutineKeyName(parent).Equals(w))));

            return work;
        }

        /// <inheritdoc/>
        public void Remove(ICatalogIndex dataKey)
        {
            catalogs.Remove(dataKey);
            schemta.Remove(dataKey);
            domains.Remove(dataKey);

            tables.Remove(dataKey);
            tableColumns.Remove(dataKey);

            routines.Remove(dataKey);
            routineParameters.Remove(dataKey);
            routineColumns.Remove(dataKey);
            references.Remove(dataKey);

            constraints.Remove(dataKey);
            constraintColumns.Remove(dataKey);

            properties.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(AppModel.IModelIndex dataKey)
        {
            catalogs.Remove(dataKey);
            schemta.Remove(dataKey);
            domains.Remove(dataKey);
            tables.Remove(dataKey);
            tableColumns.Remove(dataKey);
            routines.Remove(dataKey);
            routineParameters.Remove(dataKey);
            routineColumns.Remove(dataKey);
            references.Remove(dataKey);
            constraints.Remove(dataKey);
            constraintColumns.Remove(dataKey);
            properties.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            catalogs.Clear();
            schemta.Clear();
            domains.Clear();
            tables.Clear();
            tableColumns.Clear();
            routines.Clear();
            routineParameters.Clear();
            routineColumns.Clear();
            references.Clear();
            constraints.Clear();
            constraintColumns.Clear();
            properties.Clear();
        }

        #region IBindListChanged
        /// <inheritdoc/>
        public event ListChangedEventHandler? ListChanged;

        /// <inheritdoc/>
        public void ResetBindings()
        {
            catalogs.ResetBindings();
            schemta.ResetBindings();
            domains.ResetBindings();
            properties.ResetBindings();
            tables.ResetBindings();
            tableColumns.ResetBindings();
            routines.ResetBindings();
            routineParameters.ResetBindings();
            routineColumns.ResetBindings();
            references.ResetBindings();
            constraints.ResetBindings();
            constraintColumns.ResetBindings();
        }


        /// <inheritdoc/>
        public Boolean RaiseListChangedEvents
        {
            get
            {
                return catalogs.RaiseListChangedEvents
                    && schemta.RaiseListChangedEvents
                    && domains.RaiseListChangedEvents
                    && properties.RaiseListChangedEvents
                    && tables.RaiseListChangedEvents
                    && tableColumns.RaiseListChangedEvents
                    && routines.RaiseListChangedEvents
                    && routineParameters.RaiseListChangedEvents
                    && routineColumns.RaiseListChangedEvents
                    && references.RaiseListChangedEvents
                    && constraints.RaiseListChangedEvents
                    && constraintColumns.RaiseListChangedEvents;
            }
            set
            {
                catalogs.RaiseListChangedEvents = value;
                schemta.RaiseListChangedEvents = value;
                domains.RaiseListChangedEvents = value;
                properties.RaiseListChangedEvents = value;
                tables.RaiseListChangedEvents = value;
                tableColumns.RaiseListChangedEvents = value;
                routines.RaiseListChangedEvents = value;
                routineParameters.RaiseListChangedEvents = value;
                routineColumns.RaiseListChangedEvents = value;
                references.RaiseListChangedEvents = value;
                constraints.RaiseListChangedEvents = value;
                constraintColumns.RaiseListChangedEvents = value;
            }
        }
        #endregion
    }
}
