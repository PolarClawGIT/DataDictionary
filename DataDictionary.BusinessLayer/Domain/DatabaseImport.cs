using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Reference;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Performs the function of Importing a Database into the Model.
    /// </summary>
    public class DatabaseImport
    {
        // Lookup Data
        IReadOnlyList<PropertyValue> properties = new List<PropertyValue>();

        // Source Data
        IList<TableValue> tables = new List<TableValue>();
        IList<TableColumnValue> tableColumns = new List<TableColumnValue>();
        IList<ConstraintValue> tableConstraints = new List<ConstraintValue>();
        IList<ConstraintColumnValue> tableConstraintColumns = new List<ConstraintColumnValue>();
        IList<ExtendedPropertyValue> tableProperties = new List<ExtendedPropertyValue>();
        IList<ExtendedPropertyValue> tableColumnProperties = new List<ExtendedPropertyValue>();
        IList<ReferenceValue> tableReferences = new List<ReferenceValue>();

        // Target Data
        IList<EntityValue> entities = new List<EntityValue>();
        IList<EntityAliasValue> entityAliases = new List<EntityAliasValue>();
        IList<EntityPropertyValue> entityProperties = new List<EntityPropertyValue>();

        IList<AttributeValue> attributes = new List<AttributeValue>();
        IList<AttributeAliasValue> attributeAliases = new List<AttributeAliasValue>();
        IList<AttributePropertyValue> attributeProperties = new List<AttributePropertyValue>();

        /// <summary>
        /// Constructor for the DatabaseImport
        /// Imports the Database to a work Structure without extended properties.
        /// </summary>
        public DatabaseImport() { }

        /// <summary>
        /// Constructor for the DatabaseImport
        /// Imports the Database to a work Structure
        /// </summary>
        /// <param name="propertyValues"></param>
        public DatabaseImport(IEnumerable<PropertyValue> propertyValues) : this()
        { properties = propertyValues.ToList(); }


        //TODO: Make something that returns the data.

        /// <summary>
        /// Create work items to Loads the process with all tables/routines from the Database Model
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load(IDatabaseModel source)
        {
            List<WorkItem> work = new List<WorkItem>();

            foreach (CatalogValue item in source.DbCatalogs)
            { work.AddRange(Load(source, item)); }

            return work;
        }

        /// <summary>
        /// Create work items to Loads the process with all tables/routines from the specified Catalog
        /// </summary>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load(IDatabaseModel source, ICatalogIndex index)
        {
            List<WorkItem> work = new List<WorkItem>();
            CatalogIndex key = new CatalogIndex(index);

            foreach (TableValue item in source.DbTables.Where(w => key.Equals(w)))
            { work.AddRange(Load(source, (ITableIndex)item)); }

            //foreach (RoutineValue item in source.DbRoutines.Where(w => key.Equals(w)))
            //{ work.AddRange(Load(source, (IRoutineIndex)item)); }

            return work;
        }

        /// <summary>
        /// Create work items to Loads the process with all tables from the specified table
        /// </summary>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Load(IDatabaseModel source, ITableIndex index)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem()
            {
                WorkName = "Load the object from Database Tables",
                DoWork = DoWork
            });

            return work;

            void DoWork()
            { AddSource(source, index); }
        }

        /// <summary>
        /// Loads the process with all tables from the specified routine
        /// </summary>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Load(IDatabaseModel source, IRoutineIndex index)
        {
            Boolean isCanceled = false;
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem()
            {
                WorkName = "Load the object from Database Routines",
                DoWork = DoWork,
                IsCanceling = () => isCanceled
            });

            return work;

            void DoWork()
            { AddSource(source, index); }
        }

        /// <summary>
        /// Adds specific Table from the data model.
        /// Includes Columns, Properties, Constraints and References.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="index"></param>
        void AddSource(IDatabaseModel source, ITableIndex index)
        {
            TableIndex key = new TableIndex(index);

            if (source.DbTables.
                FirstOrDefault(
                    w => key.Equals(w)
                    && !tables.Any(t => key.Equals(t)))
                is TableValue value)
            {
                tables.Add(value);
                TableIndexName tableName = new TableIndexName(value);

                TableProperties(source, tableName);
                TableColumns(source, tableName);
                TableConstraints(source, tableName);
                TableRefrences(source, tableName);
            }

            void TableProperties(IDatabaseModel source, TableIndexName tableName)
            {
                ExtendedPropertyIndexName tablePropertyName = new ExtendedPropertyIndexName(tableName);

                foreach (ExtendedPropertyValue item in
                    source.DbExtendedProperties.
                    Where(
                        w => tablePropertyName.Equals(w)
                        && !tableProperties.
                        Any(
                            e => tablePropertyName.Equals(e)
                            && new DbExtendedPropertyKey(w).Equals(e))).
                    ToList())
                { tableProperties.Add(item); }
            }

            void TableColumns(IDatabaseModel source, TableIndexName tableName)
            {
                foreach (TableColumnValue column in
                    source.DbTableColumns.
                    Where(
                        w => tableName.Equals(w)
                        && !tableColumns.
                        Any(c => new TableColumnIndexName(w).Equals(c))))
                {
                    tableColumns.Add(column);
                    TableColumnIndexName columnName = new TableColumnIndexName(column);
                    ColumnProperties(source, columnName);
                }
            }

            void ColumnProperties(IDatabaseModel source, TableColumnIndexName columnName)
            {
                ExtendedPropertyIndexName columnPropertyName = new ExtendedPropertyIndexName(columnName);

                foreach (ExtendedPropertyValue item in
                    source.DbExtendedProperties.
                    Where(
                        w => columnPropertyName.Equals(w)
                        && !tableColumnProperties.
                        Any(e => columnPropertyName.Equals(e)
                        && new DbExtendedPropertyKey(w).Equals(e))))
                { tableColumnProperties.Add(item); }
            }

            void TableConstraints(IDatabaseModel source, TableIndexName tableName)
            {
                foreach (ConstraintValue item in
                    source.DbConstraints.
                    Where(
                        w => tableName.Equals(w)
                        && !tableConstraints.
                        Any(c => new ConstraintIndexName(w).Equals(c))))
                {
                    tableConstraints.Add(item);
                    ConstraintIndexName constraintIndex = new ConstraintIndexName(item);

                    foreach (ConstraintColumnValue column in
                        source.DbConstraintColumns.
                        Where(
                            w => constraintIndex.Equals(w)))
                    { tableConstraintColumns.Add(column); }
                }
            }

            void TableRefrences(IDatabaseModel source, TableIndexName tableName)
            {
                DbReferenceKeyName tableRefrenceName = new DbReferenceKeyName(tableName);
                foreach (ReferenceValue item in
                    source.DbReferences.
                    Where(
                        w => tableRefrenceName.Equals(w)
                        && !tableReferences.Any(r => new DbReferenceKey(w).Equals(r))).
                    ToList())
                { tableReferences.Add(item); }
            }
        }

        /// <summary>
        /// Adds specific Routine from the data model.
        /// Includes Properties and References.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <exception cref="NotImplementedException"></exception>
        void AddSource(IDatabaseModel source, IRoutineIndex index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create work items to Build the Entities and Attributes.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Build(IDomainModel target)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem()
            {
                WorkName = "Build Entities and Attributes",
                DoWork = () => BuildModel(target)
            });
            return work;
        }

        void BuildModel(IDomainModel target)
        {
            //Associate results with target
            entities = target.Entities;
            entityAliases = target.Entities.Aliases;
            entityProperties = target.Entities.Properties;

            attributes = target.Attributes;
            attributeAliases = target.Attributes.Aliases;
            attributeProperties = target.Attributes.Properties;

            var constraints = tableConstraints.
                Where(w => w.ConstraintType is DbConstraintType.ForeignKey).
                Join(tableConstraintColumns,
                constraint => new ConstraintIndexName(constraint),
                columns => new ConstraintIndexName(columns),
                (constraint, column) => new
                {
                    constraint,
                    parentKey = new ConstraintColumnIndexReferenced(column).AsColumnName(),
                    childKey = new TableColumnIndexName(column),
                }).ToList();

            var columns = tableColumns.
                Select(s => new
                {
                    value = s,
                    count = constraints.Count(w => new TableColumnIndexName(s).Equals(w.childKey))
                }).OrderBy(o => o.count).
                ToList();

            // Build Model
            foreach (var column in columns)
            {
                TableIndexName tableName = new TableIndexName(column.value);
                TableColumnIndexName columnName = new TableColumnIndexName(column.value);

                var alias = new List<TableColumnValue>() { column.value }.
                        Union(GetConstraint(column.value).
                        Union(GetReference(column.value))).
                        ToList();

                // Add or find the existing Attribute
                // Note: Duplicates Attributes can exist.
                // This is caused by two fields with the same name but
                // the relationship between them cannot be determined.
                // As such, the fields have different alias chains.
                AttributeValue attribute;
                AttributeIndex attributeIndex;

                if (attributeAliases.
                    FirstOrDefault(w => alias.Any(a => new AliasIndex(a).Equals(w)))
                    is AttributeAliasValue existingAlias &&
                    attributes.FirstOrDefault(w => new AttributeIndex(existingAlias).Equals(w))
                    is AttributeValue existingAttribute)
                {
                    attribute = existingAttribute;
                    attributeIndex = new AttributeIndex(existingAttribute);
                }
                else
                {
                    attribute = new AttributeValue()
                    {
                        AttributeTitle = column.value.ColumnName,
                        IsDerived = column.value.IsComputed ?? false,
                        IsIntegral = !column.value.IsComputed ?? false,
                        IsNullable = column.value.IsNullable ?? false,
                        IsValued = !column.value.IsNullable ?? false,
                    };

                    attributes.Add(attribute);
                    attributeIndex = new AttributeIndex(attribute);
                }

                // Add any missing Aliases
                foreach (TableColumnValue item in alias)
                {
                    AliasIndex aliasIndex = new AliasIndex(item);

                    if (!attributeAliases.
                        Any(a => attributeIndex.Equals(a) && aliasIndex.Equals(a)))
                    { attributeAliases.Add(new AttributeAliasValue(attribute, aliasIndex)); }
                }

                // Add any missing Properties

            }

            IReadOnlyList<TableColumnValue> GetConstraint(TableColumnValue value)
            {
                List<TableColumnValue> result = new List<TableColumnValue>();

                result.AddRange(constraints.
                    Where(w => w.childKey.Equals(value)).
                    Join(tableColumns,
                        constraint => constraint.parentKey,
                        column => new TableColumnIndexName(column),
                        (constraint, column) => column
                    ));

                //result.AddRange(constraints.
                //    Where(w => w.parentKey.Equals(value)).
                //    Join(tableColumns,
                //        constraint => constraint.childKey,
                //        column => new TableColumnIndexName(column),
                //        (constraint, column) => column
                //    ));

                foreach (var item in result.ToList())
                {
                    //TODO: Validate this handles recursive relationships.
                    var alias = GetConstraint(item).Except(result);
                    result.AddRange(alias);
                }

                return result;
            }

            IReadOnlyList<TableColumnValue> GetReference(TableColumnValue column)
            {
                TableColumnIndexName columnName = new TableColumnIndexName(column);

                return tableReferences.
                    Where(w => columnName.Equals(new ReferencedIndexColumn(w).AsColumn())).
                    Join(tableColumns,
                        reference => new ReferenceIndexName(reference).AsTable(),
                        column => new TableIndexName(column),
                        (reference, column) => new { reference, column }).
                    Where(w => String.Equals(w.reference.ReferencedColumnName, w.column.ColumnName, KeyExtension.CompareString)).
                    Select(s => s.column).
                    ToList();
            }
        }

        IReadOnlyList<TableColumnValue> GetAlias(TableColumnValue column)
        {
            TableColumnIndexName columnName = new TableColumnIndexName(column);
            List<TableColumnValue> alias = new List<TableColumnValue>();
            alias.Add(column);

            // Alias by Constraint
            List<TableColumnValue> constraint = tableConstraints.Join(tableConstraintColumns,
                parent => new ConstraintIndexName(parent),
                child => new ConstraintIndexName(child),
                (constraint, column) => new { constraint, column }).
                Where(w => columnName.Equals(w.column)).
                Join(tableConstraintColumns,
                left => new ConstraintColumnIndexReferenced(left.column),
                right => new ConstraintColumnIndexReferenced(right),
                (left, right) => new TableColumnIndexName(right)).
                Join(tableColumns,
                    key => key,
                    column => new TableColumnIndexName(column),
                    (key, column) => column).
                ToList();

            // Combine both lists
            alias.AddRange(constraint);

            return alias;
        }

        IReadOnlyList<TableColumnValue> GetReferenceAlias(TableColumnValue column)
        {
            TableColumnIndexName columnName = new TableColumnIndexName(column);

            return tableReferences.
                Where(w => columnName.Equals(new ReferencedIndexColumn(w).AsColumn())).
                Join(tableColumns,
                    reference => new ReferenceIndexName(reference).AsTable(),
                    column => new TableIndexName(column),
                    (reference, column) => new { reference, column }).
                Where(w => String.Equals(w.reference.ReferencedColumnName, w.column.ColumnName, KeyExtension.CompareString)).
                Select(s => s.column).
                ToList();
        }

        [Obsolete]
        public void Import(ICatalogIndex catalog, ITableColumnIndexName column)
        {
            CatalogIndex key = new CatalogIndex(catalog);
            TableColumnIndexName columnIndex = new TableColumnIndexName(column);
            List<AliasIndex> alias = new List<AliasIndex>();

            var currentColumn = tableColumns.FirstOrDefault(w => columnIndex.Equals(w));

            var aliasColumns = tableColumns.
                Where(w => key.Equals(w) && String.Equals(w.ColumnName, columnIndex.ColumnName, KeyExtension.CompareString)).
                ToList();

            // Discover all Alias
            foreach (TableColumnValue item in aliasColumns)
            { GetAlias(item); }

            // Experiment to find Table and Constraint for each alias
            //var x = alias.Join(
            //        database.DbTables,
            //        alias => new TableIndexName(alias),
            //        table => new TableIndexName(table),
            //        (alias, table) => new { alias , table }).
            //    Join (
            //        database.DbConstraints,
            //        alias => new TableIndexName(alias.alias),
            //        constraint => new TableIndexName(constraint),
            //        (alias, constraint) => new {alias.alias, alias.table, constraint }).
            //    ToList();

            if (currentColumn is not null)
            {
                AttributeValue newAttribute = new AttributeValue()
                {
                    AttributeTitle = currentColumn.ColumnName,
                    MemberName = currentColumn.ColumnName,
                    IsDerived = currentColumn.IsComputed ?? false,
                    IsIntegral = !currentColumn.IsComputed ?? false,
                    IsNullable = currentColumn.IsNullable ?? false,
                    IsValued = !currentColumn.IsNullable ?? false,
                };

                List<AttributeAliasValue> attributeAliases = alias.
                    Select(s => new AttributeAliasValue(newAttribute, s)).
                    ToList();

                List<AttributePropertyValue> attributeProperties = properties.
                    Where(w => !String.IsNullOrWhiteSpace(w.ExtendedPropertyName)).
                    Join(tableColumnProperties.
                        Where(w => new ExtendedPropertyIndexName(currentColumn).Equals(w)),
                        model => model.ExtendedPropertyName,
                        data => data.PropertyName,
                        (model, data) => new AttributePropertyValue(newAttribute, model, data)).
                    ToList();

                //TODO: Handle items that already exists.
                attributes.Add(newAttribute);
                attributeAliases.ForEach(f => attributeAliases.Add(f));
                attributeProperties.ForEach(f => attributeProperties.Add(f));

                //TODO: Hook up to the Entities
            }

            void GetAlias(TableColumnValue value)
            {
                TableColumnIndexName columnKey = new TableColumnIndexName(value);
                alias.Add(new AliasIndex(value));

                var constraints = tableConstraintColumns.
                    Where(w => columnKey.Equals(w)).
                    Join(tableColumns,
                        constraint => new ConstraintColumnIndexReferenced(constraint).AsColumnName(),
                        column => new TableColumnIndexName(column),
                        (constraint, column) => new { constraint, column }).
                    Where(w => !alias.Contains(new AliasIndex(w.column))).
                    ToList();

                foreach (var item in constraints)
                { GetAlias(item.column); }

            }

        }
    }
}
