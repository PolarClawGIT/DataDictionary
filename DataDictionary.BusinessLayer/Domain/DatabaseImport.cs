using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.NamedScope;
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
        IList<EntityAttributeValue> entityAttributes = new List<EntityAttributeValue>();

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
            BuildAttributes(target);
            BuildEntities(target);
        }

        void BuildAttributes(IDomainModel target)
        {
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

            foreach (TableColumnValue column
                in tableColumns.
                OrderBy(o => constraints.
                    Count(w => new TableColumnIndexName(o).Equals(w.childKey))).
                ToList())
            {
                TableIndexName tableName = new TableIndexName(column);
                TableColumnIndexName columnName = new TableColumnIndexName(column);
                IReadOnlyList<TableColumnValue> alias = GetAlias(column);

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
                        AttributeTitle = column.ColumnName,
                        MemberName = GetMemberName(column).MemberFullPath,
                        IsDerived = column.IsComputed ?? false,
                        IsIntegral = !column.IsComputed ?? false,
                        IsNullable = column.IsNullable ?? false,
                        IsValued = !column.IsNullable ?? false,
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
                List<AttributePropertyValue> newProperties = properties.
                    Where(w => !String.IsNullOrWhiteSpace(w.ExtendedPropertyName)).
                    Join(tableColumnProperties.
                        Where(w => new ExtendedPropertyIndexName(column).Equals(w)),
                        model => model.ExtendedPropertyName,
                        data => data.PropertyName,
                        (model, data) => new AttributePropertyValue(attribute, model, data)).
                    ToList();

                foreach (AttributePropertyValue item in newProperties)
                { attributeProperties.Add(item); }
            }

            IReadOnlyList<TableColumnValue> GetAlias(TableColumnValue column)
            {
                TableColumnIndexName columnName = new TableColumnIndexName(column);
                List<TableColumnValue> result = new List<TableColumnValue>();

                // Itself
                result.Add(column);

                // Alias by Reference
                result.AddRange(tableReferences.
                    Where(w => columnName.Equals(new ReferencedIndexColumn(w).AsColumn())).
                    Join(tableColumns,
                        reference => new ReferenceIndexName(reference).AsTable(),
                        column => new TableIndexName(column),
                        (reference, column) => new { reference, column }).
                    Where(w => String.Equals(w.reference.ReferencedColumnName, w.column.ColumnName, KeyExtension.CompareString)).
                    Select(s => s.column));

                // Alias by Constraint
                ByConstraint(column);

                return result;

                void ByConstraint(TableColumnValue column)
                {   // Recursive Function that add values to result
                    // So Long as the Name of the column does not change,
                    // keep working down the constraint tree (parent to child).
                    List<TableColumnValue> newAlias = constraints.
                        Where(w => w.parentKey.Equals(column)).
                        Join(tableColumns,
                            constraint => constraint.childKey,
                            column => new TableColumnIndexName(column),
                            (constraint, column) => column
                        ).Except(result).
                        Where(w => String.Equals(column.ColumnName, w.ColumnName, KeyExtension.CompareString)).
                        ToList();

                    result.AddRange(newAlias);

                    foreach (var item in newAlias)
                    { ByConstraint(item); }
                }
            }

            NamedScopePath GetMemberName(TableColumnValue column)
            {
                TableColumnIndexName columnName = new TableColumnIndexName(column);
                List<String> nameParts = new List<String>();
                List<TableColumnValue> result = new List<TableColumnValue>();
                if (column.ColumnName is String value) { nameParts.Add(value); }

                ByConstraint(column);

                void ByConstraint(TableColumnValue column)
                {   // Recursive Function that add values to result.
                    // Works up the constraint tree (child to parent).
                    // In case of a circular relationship, the top cannot be determined.
                    // This will result in two (possible more) entries.
                    List<TableColumnValue> newNamePart = constraints.
                        Where(w => w.childKey.Equals(column)).
                        Join(tableColumns,
                            constraint => constraint.parentKey,
                            column => new TableColumnIndexName(column),
                            (constraint, column) => column
                        ).Except(result).
                        Where (w => !nameParts.Any(a => String.Equals(w.ColumnName,a, KeyExtension.CompareString))).
                        ToList();

                    result.AddRange(newNamePart);

                    // Most cases there is exactly one value.
                    // In complex db models, multiple could exist.
                    // Application cannot determine best option, so just pick one.
                    if (newNamePart.Count > 0 && newNamePart.First().ColumnName is String value)
                    { nameParts.Insert(0, value); }

                    foreach (var item in newNamePart)
                    { ByConstraint(item); }
                }

                return new NamedScopePath(nameParts.ToArray());
            }
        }

        void BuildEntities(IDomainModel target)
        {
            entities = target.Entities;
            entityAliases = target.Entities.Aliases;
            entityProperties = target.Entities.Properties;
            entityAttributes = target.Entities.Attributes;

            foreach (TableValue table in tables)
            {
                TableIndexName tableName = new TableIndexName(table);
                IReadOnlyList<TableValue> alias = new List<TableValue>() { table };

                // Add or find the existing Entity
                EntityValue entity;
                EntityIndex entityIndex;
                if (entityAliases.
                    FirstOrDefault(w => alias.Any(a => new AliasIndex(a).Equals(w)))
                    is EntityAliasValue existingAlias &&
                    entities.FirstOrDefault(w => new EntityIndex(existingAlias).Equals(w))
                    is EntityValue existingEntity)
                {
                    entity = existingEntity;
                    entityIndex = new EntityIndex(entity);
                }
                else
                {
                    entity = new EntityValue()
                    { EntityTitle = table.TableName };
                    entityIndex = new EntityIndex(entity);

                    entities.Add(entity);
                }

                // Add any missing Aliases
                foreach (TableValue item in alias)
                {
                    AliasIndex aliasIndex = new AliasIndex(item);

                    if (!attributeAliases.
                        Any(a => entityIndex.Equals(a) && aliasIndex.Equals(a)))
                    { entityAliases.Add(new EntityAliasValue(entity, aliasIndex)); }
                }

                // Add any missing Properties
                List<EntityPropertyValue> newProperties = properties.
                    Where(w => !String.IsNullOrWhiteSpace(w.ExtendedPropertyName)).
                    Join(tableColumnProperties.
                        Where(w => new ExtendedPropertyIndexName(table).Equals(w)),
                        model => model.ExtendedPropertyName,
                        data => data.PropertyName,
                        (model, data) => new EntityPropertyValue(entity, model, data)).
                    ToList();

                foreach (EntityPropertyValue item in newProperties)
                { entityProperties.Add(item); }

                // Associate Attributes to Entity (by column)
                foreach (TableColumnValue column in tableColumns.Where(w => tableName.Equals(w)))
                {
                    AliasIndex aliasName = new AliasIndex(column);
                    List<EntityAttributeValue> newAttributes = attributeAliases.
                        Where(w => aliasName.Equals(w)).
                        Join(attributes,
                            alias => new AttributeIndex(alias),
                            attribute => new AttributeIndex(attribute),
                            (alias, attribute) => new EntityAttributeValue(entity, attribute)
                            {
                                AttributeName = column.ColumnName,
                                IsNullable = column.IsNullable,
                                OrdinalPosition = column.OrdinalPosition
                            }).
                        ToList();

                    foreach (EntityAttributeValue attribute in newAttributes)
                    { entityAttributes.Add(attribute); }
                }
            }
        }
    }
}
