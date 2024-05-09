using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.ModelData;
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
        ILoadData<IAttributeIndex>, ISaveData<IAttributeIndex>,
        ITableColumnImport
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
        /// List of Subject Areas for the Attributes within the Model.
        /// </summary>
        IAttributeSubjectAreaData SubjectArea { get; }
    }

    class AttributeData : DomainAttributeCollection<AttributeValue>, IAttributeData,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDataTableFile, IGetNamedScopes
    {
        public required DomainModel Model { get; init; }

        /// <inheritdoc/>
        public IAttributeAliasData Aliases { get { return aliasValues; } }
        private readonly AttributeAliasData aliasValues;

        /// <inheritdoc/>
        public IAttributePropertyData Properties { get { return propertyValues; } }
        private readonly AttributePropertyData propertyValues;

        /// <inheritdoc/>
        public IAttributeSubjectAreaData SubjectArea { get { return subjectAreaValues; } }
        private readonly AttributeSubjectAreaData subjectAreaValues;

        public AttributeData() : base()
        {
            aliasValues = new AttributeAliasData();
            propertyValues = new AttributePropertyData() { Attributes = this };
            subjectAreaValues = new AttributeSubjectAreaData();
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDomainAttributeKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, dataKey));
            work.Add(factory.CreateLoad(aliasValues, dataKey));
            work.Add(factory.CreateLoad(propertyValues, dataKey));
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
            work.Add(factory.CreateSave(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public override void Remove(IDomainAttributeKey attributeItem)
        {
            base.Remove(attributeItem);
            DomainAttributeKey key = new DomainAttributeKey(attributeItem);
            aliasValues.Remove(key);
            propertyValues.Remove(key);
            subjectAreaValues.Remove(key);
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Remove()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Remove Attributes",
                DoWork = () =>
                {
                    aliasValues.Clear();
                    propertyValues.Clear();
                    subjectAreaValues.Clear();
                    this.Clear();
                }
            });

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(this.ToDataTable());
            result.Add(aliasValues.ToDataTable());
            result.Add(propertyValues.ToDataTable());
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
            subjectAreaValues.Load(source);
        }

        /// <inheritdoc/>
        /// <remarks>Attribute by Catalog</remarks>
        public void Import(IDatabaseModel source, IPropertyData propertyDefinition, ICatalogIndex key)
        {
            CatalogIndex catalogKey = new CatalogIndex(key);
            if (source.DbCatalogs.Where(w => catalogKey.Equals(w)) is CatalogValue catalog)
            {
                CatalogIndexName catalogName = new CatalogIndexName(catalog);

                foreach (TableValue item in source.DbTables.Where(w => catalogName.Equals(w)))
                {
                    TableIndex tableKey = new TableIndex(item);
                    Import(source, propertyDefinition, tableKey);
                }
            }
        }

        /// <inheritdoc/>
        /// <remarks>Attribute by Table</remarks>
        public void Import(IDatabaseModel source, IPropertyData propertyDefinition, ITableIndex key)
        {
            TableIndex tableKey = new TableIndex(key);
            if (source.DbTables.Where(w => tableKey.Equals(w)) is TableValue table)
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
        public void Import(IDatabaseModel source, IPropertyData propertyDefinition, ITableColumnIndex key)
        {
            TableColumnIndex colunKey = new TableColumnIndex(key);
            foreach (TableColumnValue item in source.DbTableColumns.Where(w => colunKey.Equals(w)))
            {
                AliasKeyName aliasKey = new AliasKeyName(item);
                AttributeIndexName attributeName = new AttributeIndexName(item);
                AttributeIndex attributeKey;

                // Create Attribute or get existing
                if (aliasValues.FirstOrDefault(w => aliasKey.Equals(w)) is AttributeAliasValue existingAlias)
                { attributeKey = new AttributeIndex(existingAlias); }
                else if (this.FirstOrDefault(w => attributeName.Equals(w)) is AttributeValue existing)
                { attributeKey = new AttributeIndex(existing); }
                else
                {
                    AttributeValue newItem = new AttributeValue()
                    {
                        AttributeTitle = item.ColumnName,
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
                    aliasValues.Add(new AttributeAliasValue(attributeKey)
                    {
                        AliasName = item.ToAliasName(),
                        Scope = item.Scope
                    });
                }

                // Create Properties
                ExtendedPropertyIndexName propertyKey = new ExtendedPropertyIndexName(item);
                foreach (ExtendedPropertyValue property in source.DbExtendedProperties.Where(w => propertyKey.Equals(w)))
                {
                    PropertyKeyExtended appKey = new PropertyKeyExtended(property);

                    if (propertyDefinition.FirstOrDefault(w =>
                        appKey.Equals(w)) is Application.IPropertyValue appProperty
                        && propertyValues.Count(w =>
                            attributeKey.Equals(w)
                            && new Application.PropertyIndex(appProperty).Equals(w)) == 0)
                    { propertyValues.Add(new AttributePropertyValue(attributeKey, appProperty, property)); }
                }
            }
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            List<NamedScopePair> result = new List<NamedScopePair>();

            ModelValue? model = Model.Models.FirstOrDefault();

            foreach (AttributeValue attribute in this)
            {
                AttributeIndex attributeKey = new AttributeIndex(attribute);
                Boolean hasSubjectArea = false;

                foreach (AttributeSubjectAreaValue subjectArea in subjectAreaValues.Where(w => attributeKey.Equals(w)))
                {
                    hasSubjectArea = true;
                    SubjectAreaIndex subjectKey = new SubjectAreaIndex(subjectArea);

                    if (Model.SubjectAreas.FirstOrDefault(w => subjectKey.Equals(w)) is SubjectAreaValue subject)
                    {
                        // TODO: The GetPath = () => does not actually work.
                        // An Attribute does not have a path. The Attribute/Subject has a full path.
                        // This tries to assign the Attribute/Subject path to each copy of the attribute.
                        // Consider creating a wrapper node around Attribute that holds the path for that Attribute/Subject.

                        result.Add(new NamedScopePair(subject.GetKey(), attribute)
                        { GetPath = () => { return new NamedScopePath(subject.GetPath(), attribute.GetPath()); } });
                    }
                }

                if (!hasSubjectArea && model is not null)
                {
                    result.Add(new NamedScopePair(model.GetKey(), attribute)
                    { GetPath = () => { return new NamedScopePath(model.GetPath(), attribute.GetPath()); } });
                }
                else if (!hasSubjectArea && model is null)
                { new NamedScopePair(attribute); }

            }

            return result;
        }



        public XElement? GetXElement(IAttributeIndex key, IEnumerable<SchemaElementValue>? options = null)
        {
            XElement? result = null;

            AttributeIndex attributeKey = new AttributeIndex(key);
            if (this.FirstOrDefault(w => attributeKey.Equals(w)) is AttributeValue attribute)
            {
                if (attribute.GetXElement(options) is XElement xAttribute)
                {
                    result = xAttribute;

                    if (Properties.FirstOrDefault(w => attributeKey.Equals(w)) is AttributePropertyValue property)
                    {
                        Application.PropertyIndex propertyKey = new PropertyIndex(property);
                        if (Model.ModelProperty.FirstOrDefault((Object w) => propertyKey.Equals(w)) is PropertyValue item)
                        {
                            if (property.GetXElement(item, options) is XElement xProperty)
                            { result.Add(xProperty); }
                        }
                    }
                }

            }

            return result;
        }

    }
}
