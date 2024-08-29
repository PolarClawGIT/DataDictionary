using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Xml.Linq;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface component for the Model Attribute
    /// </summary>
    public interface IAttributeData :
        IBindingData<AttributeValue>,
        ILoadData<IAttributeIndex>, ISaveData<IAttributeIndex>
    {
        /// <summary>
        /// List of Domain Aliases for the Attributes within the Model.
        /// </summary>
        IAttributeAliasData Aliases { get; }

        /// <summary>
        /// List of Domain Properties for the Attributes within the Model.
        /// </summary>
        IAttributePropertyData Properties { get; }

        /// <summary>
        /// List of Domain Definitions for the Attributes within the Model.
        /// </summary>
        IAttributeDefinitionData Definitions { get; }

        /// <summary>
        /// List of Subject Areas for the Attributes within the Model.
        /// </summary>
        IAttributeSubjectAreaData SubjectArea { get; }

        /// <summary>
        /// Generates the XElement using the ScriptingData
        /// </summary>
        /// <param name="scripting"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks>Not for use outside of BusinessLayer</remarks>
        XElement? GetXElement(ScriptingWork scripting, IAttributeIndex index);
    }

    class AttributeData : DomainAttributeCollection<AttributeValue>, IAttributeData,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDataTableFile, INamedScopeSourceData
    {
        public required DomainModel Model { get; init; }

        /// <inheritdoc/>
        public IAttributeAliasData Aliases { get { return aliasValues; } }
        private readonly AttributeAliasData aliasValues;

        /// <inheritdoc/>
        public IAttributePropertyData Properties { get { return propertyValues; } }
        private readonly AttributePropertyData propertyValues;

        /// <inheritdoc/>
        public IAttributeDefinitionData Definitions { get { return definitionValues; } }
        private readonly AttributeDefinitionData definitionValues;

        /// <inheritdoc/>
        public IAttributeSubjectAreaData SubjectArea { get { return subjectAreaValues; } }
        private readonly AttributeSubjectAreaData subjectAreaValues;

        public AttributeData() : base()
        {
            aliasValues = new AttributeAliasData();
            propertyValues = new AttributePropertyData() { Attributes = this };
            definitionValues = new AttributeDefinitionData();
            subjectAreaValues = new AttributeSubjectAreaData();
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public override void Remove(IDomainAttributeKey attributeItem)
        {
            base.Remove(attributeItem);
            DomainAttributeKey key = new DomainAttributeKey(attributeItem);
            aliasValues.Remove(key);
            propertyValues.Remove(key);
            definitionValues.Remove(key);
            subjectAreaValues.Remove(key);
            Model.Entities.Attributes.Remove(key);
        }

        #region ILoadData, ISaveData
        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDomainAttributeKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, dataKey));
            work.Add(factory.CreateLoad(aliasValues, dataKey));
            work.Add(factory.CreateLoad(propertyValues, dataKey));
            work.Add(factory.CreateLoad(definitionValues, dataKey));
            work.Add(factory.CreateLoad(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IAttributeIndex dataKey)
        { return Save(factory, (IDomainAttributeKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, dataKey));
            work.Add(factory.CreateLoad(aliasValues, dataKey));
            work.Add(factory.CreateLoad(propertyValues, dataKey));
            work.Add(factory.CreateLoad(definitionValues, dataKey));
            work.Add(factory.CreateLoad(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainAttributeKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, dataKey));
            work.Add(factory.CreateSave(aliasValues, dataKey));
            work.Add(factory.CreateSave(propertyValues, dataKey));
            work.Add(factory.CreateSave(definitionValues, dataKey));
            work.Add(factory.CreateSave(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex dataKey)
        { return Load(factory, (IDomainAttributeKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, dataKey));
            work.Add(factory.CreateSave(aliasValues, dataKey));
            work.Add(factory.CreateSave(propertyValues, dataKey));
            work.Add(factory.CreateSave(definitionValues, dataKey));
            work.Add(factory.CreateSave(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Remove Attribute", DoWork = () => { this.Clear(); } });
            work.AddRange(aliasValues.Delete());
            work.AddRange(propertyValues.Delete());
            work.AddRange(definitionValues.Delete());
            work.AddRange(subjectAreaValues.Delete());

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Delete(IAttributeIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Attribute", DoWork = () => { Remove((IDomainAttributeKey)dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }
        #endregion

        #region IDataTableFile

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(this.ToDataTable());
            result.Add(aliasValues.ToDataTable());
            result.Add(propertyValues.ToDataTable());
            result.Add(definitionValues.ToDataTable());
            result.Add(subjectAreaValues.ToDataTable());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public void Import(System.Data.DataSet source)
        {
            this.Load(source);
            aliasValues.Load(source);
            propertyValues.Load(source);
            definitionValues.Load(source);
            subjectAreaValues.Load(source);
        }

        /// <inheritdoc/>
        /// <remarks>Attribute by Catalog</remarks>
        [Obsolete]
        public void Import_Old(IDatabaseModel source, IPropertyData propertyDefinition, ICatalogIndex key)
        {
            CatalogIndex catalogKey = new CatalogIndex(key);
            if (source.DbCatalogs.FirstOrDefault(w => catalogKey.Equals(w)) is CatalogValue catalog)
            {
                CatalogIndexName catalogName = new CatalogIndexName(catalog);

                foreach (TableValue item in source.DbTables.Where(w => catalogName.Equals(w)))
                {
                    TableIndex tableKey = new TableIndex(item);
                    Import(source, propertyDefinition, tableKey);
                }
            }
        }

        [Obsolete]
        public void Import(IDatabaseModel source, IPropertyData propertyDefinition, ICatalogIndex key)
        {
            //var stuff = new DatabaseImport(source, Model);
            //stuff.Import(key);

        }

        [Obsolete]
        public void Import(IDatabaseModel source, IPropertyData propertyDefinition, ITableIndex key)
        {
            TableIndex tableKey = new TableIndex(key);

            //var stuff = new DatabaseImport(source, Model);
            //if (source.DbTables.FirstOrDefault(w => tableKey.Equals(w)) is TableValue table)
            //{
            //    CatalogIndex catalog = new CatalogIndex(table);

            //    stuff.Import(catalog, table);
            //}

        }

        /// <inheritdoc/>
        /// <remarks>Attribute by Table</remarks>
        [Obsolete]
        public void Import_Old(IDatabaseModel source, IPropertyData propertyDefinition, ITableIndex key)
        {
            TableIndex tableKey = new TableIndex(key);
            if (source.DbTables.FirstOrDefault(w => tableKey.Equals(w)) is TableValue table)
            {
                TableIndexName tableName = new TableIndexName(table);
                foreach (TableColumnValue item in source.DbTableColumns.Where(w => tableName.Equals(w)))
                {
                    TableColumnIndex columKey = new TableColumnIndex(item);
                    Import(source, propertyDefinition, columKey);
                }
            }
        }


        /// <inheritdoc/>
        /// <remarks>Attribute by Column</remarks>
        [Obsolete]
        public void Import(IDatabaseModel source, IPropertyData propertyDefinition, ITableColumnIndex key)
        {
            TableColumnIndex colunKey = new TableColumnIndex(key);

            if (source.DbTableColumns.FirstOrDefault(w => colunKey.Equals(w)) is TableColumnValue item)
            {
                TableColumnIndexName columnKey = new TableColumnIndexName(item);
                AliasIndex aliasKey = new AliasIndex(item);
                AttributeIndexName attributeName = new AttributeIndexName(item);
                AttributeIndex attributeKey;

                // Create Attribute or get existing
                if (aliasValues.FirstOrDefault(w => aliasKey.Equals(w)) is AttributeAliasValue existingAlias)
                { attributeKey = new AttributeIndex(existingAlias); }
                else if (this.FirstOrDefault(w => attributeName.Equals(w)) is AttributeValue existing)
                { attributeKey = new AttributeIndex(existing); }
                else
                {


                    // See if there is a Primary Key for this column. Use that for Path, if possible.
                    // TODO: Debug this. It is not picking up the right values (SampleParentId)
                    // Issue: If the column name is defined elsewhere, the code does not reach this point.
                    var constraint = source.DbConstraintColumns.
                        Where(w => columnKey.Equals(w)).
                        Join(source.DbConstraints,
                            left => new ConstraintIndexName(left),
                            right => new ConstraintIndexName(right),
                            (left, right) => new
                            {
                                left.SchemaName,
                                left.TableName,
                                left.ColumnName,
                                right.ConstraintType,
                                MemberPath = new NamedScopePath(left.SchemaName, left.TableName, left.ColumnName),
                                Count = source.DbConstraintColumns.Count(w => new ConstraintIndexName(right).Equals(w))
                            }).
                        OrderBy(o => o.Count). //Ideally Count == 1 but it may not.
                        FirstOrDefault(w => w.ConstraintType is DbConstraintType.PrimaryKey);

                    var x = source.DbConstraints.
                        Join(source.DbConstraintColumns,
                            left => new ConstraintIndexName(left),
                            right => new ConstraintIndexName(right),
                            (left, right) => new
                            {
                                left.ConstraintName,
                                left.ConstraintType,
                                left.SchemaName,
                                left.TableName,
                                right.ColumnName,
                                Key = new TableColumnIndexName(right)
                            }
                        ).ToList();

                    var y = x.Where(w => columnKey.Equals(w));

                    NamedScopePath MemberPath;
                    if (constraint is null)
                    { MemberPath = new NamedScopePath(item.SchemaName, item.TableName, item.ColumnName); }
                    else { MemberPath = constraint.MemberPath; }

                    AttributeValue newItem = new AttributeValue()
                    {
                        AttributeTitle = item.ColumnName,
                        MemberName = new NamedScopePath(item.SchemaName, item.TableName, item.ColumnName).MemberFullPath,
                        IsDerived = item.IsComputed ?? false,
                        IsIntegral = !item.IsComputed ?? false,
                        IsNullable = item.IsNullable ?? false,
                        IsValued = !item.IsNullable ?? false,
                    };
                    this.Add(newItem);
                    attributeKey = new AttributeIndex(newItem);
                }

                // Create Alias
                if (aliasValues.Count(w => aliasKey.Equals(w) && attributeKey.Equals(w)) == 0)
                {
                    AttributeAliasValue newAlias = new AttributeAliasValue(attributeKey, new AliasIndex(item));

                    aliasValues.Add(newAlias);

                    // Look for related Entities
                    foreach (EntityAliasValue entity in Model.Entities.Aliases.
                        Where(w => w.AliasPath.Equals(newAlias.AliasPath.ParentPath)))
                    {
                        EntityAttributeValue entityAttribute = new EntityAttributeValue(entity, attributeKey);
                        EntityAttributeIndex entityAttributeKey = new EntityAttributeIndex(entityAttribute);
                        if (Model.Entities.Attributes.FirstOrDefault(w => entityAttributeKey.Equals(w)) is null)
                        { Model.Entities.Attributes.Add(entityAttribute); }
                    }
                }

                // Create Properties
                ExtendedPropertyIndexName propertyKey = new ExtendedPropertyIndexName(item);
                foreach (ExtendedPropertyValue property in source.DbExtendedProperties.Where(w => propertyKey.Equals(w)))
                {
                    PropertyIndexValue appKey = new PropertyIndexValue(property);

                    if (propertyDefinition.FirstOrDefault(w =>
                        appKey.Equals(w)) is IPropertyValue appProperty
                        && propertyValues.Count(w =>
                            attributeKey.Equals(w)
                            && new PropertyIndexValue(appProperty).Equals(w)) == 0)
                    { propertyValues.Add(new AttributePropertyValue(attributeKey, appProperty, property)); }
                }


            }
            else { throw new Exception("Column Not Found."); } // Should never occur.
        }
        #endregion

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progressChanged = (completed, total) => { };

            WorkItem newWork = new WorkItem(ref progressChanged)
            {
                WorkName = "Adding NamedScopes (Attribute)",
                DoWork = () =>
                {
                    Int32 completed = 0;
                    Int32 total = this.Count();

                    ModelValue? model = Model.Models.FirstOrDefault();

                    foreach (AttributeValue attribute in this)
                    {
                        //NamedScopeValue newItem = new NamedScopeValue(attribute);
                        Boolean hasParent = false;

                        foreach (EntityValue entityParent in ParentEntites(attribute))
                        {
                            NamedScopeValue newItem = new NamedScopeValue(attribute)
                            {
                                GetPath = () => new NamedScopePath(
                                    entityParent.GetPath(),
                                    attribute.GetPath())
                            };
                            addNamedScope(entityParent, newItem);
                            hasParent = true;
                        }

                        foreach (SubjectAreaValue subjectParent in ParentSubjects(attribute))
                        {
                            NamedScopeValue newItem = new NamedScopeValue(attribute)
                            {
                                GetPath = () => new NamedScopePath(
                                    subjectParent.GetPath(),
                                    attribute.GetPath())
                            };
                            addNamedScope(subjectParent, newItem);
                            hasParent = true; 
                        }

                        if (!hasParent) // No Parents found
                        {
                            NamedScopeValue newItem = new NamedScopeValue(attribute);
                            addNamedScope(model, newItem); 
                        }

                        progressChanged(completed++, total);
                    }
                }
            };

            work.Add(newWork);

            return work;

            IEnumerable<EntityValue> ParentEntites(AttributeValue attribute)
            {
                AttributeIndex key = new AttributeIndex(attribute);

                return this.
                    Where(w => key.Equals(w)).
                    Join(Model.Entities.Attributes,
                        attribute => new AttributeIndex(attribute),
                        entity => new AttributeIndex(entity),
                        (attribute, entity) => new EntityIndex(entity)).
                    Join(Model.Entities,
                        entityKey => entityKey,
                        entity => new EntityIndex(entity),
                        (key, entity) => entity).
                    ToList();
            }

            IEnumerable<SubjectAreaValue> ParentSubjects(AttributeValue attribute)
            {

                AttributeIndex key = new AttributeIndex(attribute);

                return this.
                    Where(w => key.Equals(w)).
                    Join(SubjectArea,
                        attribute => new AttributeIndex(attribute),
                        subject => new AttributeIndex(subject),
                        (attribute, subject) => new SubjectAreaIndex(subject)).
                    Join(Model.SubjectAreas,
                        subjectKey => subjectKey,
                        subject => new SubjectAreaIndex(subject),
                        (key, subject) => subject).
                    ToList();
            }
        }

        #region XML Scripting

        /// <inheritdoc/>
        public XElement? GetXElement(ScriptingWork scripting, IAttributeIndex index)
        {
            XElement? result = null;
            AttributeIndex key = new AttributeIndex(index);
            if (this.FirstOrDefault(w => key.Equals(w)) is AttributeValue attribute)
            {
                foreach (TemplateNodeValue node in scripting.Nodes.Where(w => w.PropertyScope == attribute.Scope))
                {
                    XObject? value = null;

                    switch (node.PropertyName)
                    {
                        case nameof(attribute.AttributeTitle): value = node.BuildXObject(attribute.AttributeTitle); break;
                        case nameof(attribute.AttributeDescription): value = node.BuildXObject(attribute.AttributeDescription); break;
                        case nameof(attribute.IsCompositeType): value = node.BuildXObject(attribute.IsCompositeType); break;
                        case nameof(attribute.IsDerived): value = node.BuildXObject(attribute.IsDerived); break; ;
                        case nameof(attribute.IsIntegral): value = node.BuildXObject(attribute.IsIntegral); break; ;
                        case nameof(attribute.IsKey): value = node.BuildXObject(attribute.IsKey); break; ;
                        case nameof(attribute.IsMultiValue): value = node.BuildXObject(attribute.IsMultiValue); break; ;
                        case nameof(attribute.IsNonKey): value = node.BuildXObject(attribute.IsNonKey); break; ;
                        case nameof(attribute.IsNullable): value = node.BuildXObject(attribute.IsNullable); break; ;
                        case nameof(attribute.IsSimpleType): value = node.BuildXObject(attribute.IsSimpleType); break; ;
                        case nameof(attribute.IsSingleValue): value = node.BuildXObject(attribute.IsSingleValue); break; ;
                        case nameof(attribute.IsValued): value = node.BuildXObject(attribute.IsValued); break; ;
                        default:
                            break;
                    }

                    if (value is XObject)
                    {
                        if (result is null) { result = new XElement(ScopeEnumeration.Cast(attribute.Scope).Name); }
                        result.Add(value);

                        IReadOnlyList<XAttribute> attributes = Model.Properties.GetXAttributes(scripting, node, Properties);

                        if (value is XElement element) { element.Add(attributes.ToArray()); }
                        else if (value.Parent is XElement) { value.Parent.Add(attributes.ToArray()); }
                    }
                }

                foreach (AttributeAliasValue alias in Aliases.Where(w => key.Equals(w)))
                {
                    XElement? aliasNode = alias.GetXElement(scripting, (node) => Model.Properties.GetXAttributes(scripting, node, Properties));
                    if (aliasNode is not null && result is null)
                    {
                        result = new XElement(ScopeEnumeration.Cast(attribute.Scope).Name);
                        result.Add(aliasNode);
                    }
                    else if (aliasNode is not null && result is XElement)
                    { result.Add(aliasNode); }
                }
            }

            return result;
        }

        #endregion
    }
}
