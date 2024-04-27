using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
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
<<<<<<< HEAD
    public interface IAttributeData<TValue> : IBindingData<TValue>,
=======
    public interface IAttributeData :
        IBindingData<AttributeValue>,
>>>>>>> RenameIndexValue
        ILoadData<IAttributeIndex>, ISaveData<IAttributeIndex>,
        ITableColumnImport
        where TValue : AttributeValue, IAttributeValue
    {
        /// <summary>
        /// List of Domain Aliases for the Attributes within the Model.
        /// </summary>
        IAttributeAliasData Aliases { get; }

        /// <summary>
        /// List of Domain Properties for the Attributes within the Model.
        /// </summary>
<<<<<<< HEAD
        IAttributePropertyData<AttributePropertyValue> Properties { get; }

        /// <summary>
        /// List of Model Attribute Subject Areas within the Model.
        /// </summary>
        IAttributeSubjectAreaData SubjectAreas { get; }
    }

    class AttributeData<TValue> : DomainAttributeCollection<TValue>, 
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        ILoadData<IDomainAttributeKey>, ISaveData<IDomainAttributeKey>,
        IAttributeData<TValue>, IDataTableFile, IGetNamedScopes
        where TValue : AttributeValue, IAttributeValue, new()
=======
        IAttributePropertyData Properties { get; }
    }

    class AttributeData : DomainAttributeCollection<AttributeValue>, IAttributeData,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDataTableFile, IGetNamedScopes
>>>>>>> RenameIndexValue
    {
        public required DomainModel Model { get; init; }

        /// <inheritdoc/>
        public IAttributeAliasData Aliases { get { return aliasValues; } }
        private readonly AttributeAliasData aliasValues;

        /// <inheritdoc/>
        public IAttributePropertyData<AttributePropertyValue> Properties { get { return propertyValues; } }
        private readonly AttributePropertyData<AttributePropertyValue> propertyValues;

        public AttributeData() : base()
        {
            aliasValues = new AttributeAliasData();
<<<<<<< HEAD
            propertyValues = new AttributePropertyData<AttributePropertyValue>();
            subjectAreaValues = new AttributeSubjectAreaData();
=======
            propertyValues = new AttributePropertyData() { Attributes = this };
>>>>>>> RenameIndexValue
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDomainAttributeKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, dataKey));
            work.Add(factory.CreateLoad(aliasValues, dataKey));
            work.Add(factory.CreateLoad(propertyValues, dataKey));
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
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex dataKey)
        {
            IDomainAttributeKey key = new AttributeIndex(dataKey);
            return Load(factory, key);
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainAttributeKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, dataKey));
            work.Add(factory.CreateSave(aliasValues, dataKey));
            work.Add(factory.CreateSave(propertyValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
<<<<<<< HEAD
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IAttributeIndex dataKey)
        {
            IDomainAttributeKey key = new AttributeIndex(dataKey);
            return Save(factory, key);
        }
=======
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex dataKey)
        { return Load(factory, (IDomainAttributeKey)dataKey); }
>>>>>>> RenameIndexValue

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, dataKey));
            work.Add(factory.CreateSave(aliasValues, dataKey));
            work.Add(factory.CreateSave(propertyValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public override void Remove(IDomainAttributeKey attributeItem)
        {
            base.Remove(attributeItem);
            DomainAttributeKey key = new DomainAttributeKey(attributeItem);
            aliasValues.Remove(key);
            propertyValues.Remove(key);
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
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public void Import(System.Data.DataSet source)
        {
            this.Load(source);
            aliasValues.Load(source);
            propertyValues.Load(source);
        }

        /// <inheritdoc/>
        /// <remarks>Attribute by Catalog</remarks>
        public void Import(IDatabaseModel source, IPropertyData propertyDefinition, ICatalogIndex key)
        {
            CatalogIndex catalogKey = new CatalogIndex(key);
            if(source.DbCatalogs.Where(w => catalogKey.Equals(w)) is CatalogValue catalog)
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
            if(source.DbTables.Where(w => tableKey.Equals(w)) is TableValue table)
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
<<<<<<< HEAD
                AliasKeyName alaisKey = new AliasKeyName(item);
                AttributeIndex attributeKey;
                DomainAttributeKeyName uniqueKey = new DomainAttributeKeyName(item);
=======
>>>>>>> RenameIndexValue
                AliasKeyName aliasKey = new AliasKeyName(item);
                AttributeIndexName attributeName = new AttributeIndexName(item);
                AttributeIndex attributeKey;

                // Create Attribute or get existing
<<<<<<< HEAD
                if (aliasValues.FirstOrDefault(w => aliasKey.Equals(w)) is DomainAttributeAliasItem existingAlias)
                { attributeKey = new AttributeIndex(existingAlias); }
                else if (this.FirstOrDefault(w => uniqueKey.Equals(w)) is DomainAttributeItem existing)
                { attributeKey = new AttributeIndex(existing); }
                else
                {
                    TValue newItem = new TValue()
=======
                if (aliasValues.FirstOrDefault(w => aliasKey.Equals(w)) is AttributeAliasValue existingAlias)
                { attributeKey = new AttributeIndex(existingAlias); }
                else if (this.FirstOrDefault(w => attributeName.Equals(w)) is AttributeValue existing)
                { attributeKey = new AttributeIndex(existing); }
                else
                {
                    AttributeValue newItem = new AttributeValue()
>>>>>>> RenameIndexValue
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

<<<<<<< HEAD
        public IEnumerable<NamedScopePair> GetNamedScopes()
        { return this.Select(s => new NamedScopePair(s)); }

        public XElement? GetXElement(IAttributeIndex key, IEnumerable<ElementValue>? options = null)
=======
        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        { return this.Select(s => new NamedScopePair(s)); }

        public XElement? GetXElement(IAttributeIndex key, IEnumerable<SchemaElementValue>? options = null)
>>>>>>> RenameIndexValue
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
<<<<<<< HEAD
                        Application.PropertyIndex propertyKey = new Application.PropertyIndex(property);
                        if (Model.ModelProperty.FirstOrDefault((Object w) => propertyKey.Equals(w)) is Application.PropertyValue item)
=======
                        Application.PropertyIndex propertyKey = new PropertyIndex(property);
                        if (Model.ModelProperty.FirstOrDefault((Object w) => propertyKey.Equals(w)) is PropertyValue item)
>>>>>>> RenameIndexValue
                        {
                            if (property.GetXElement(item, options) is XElement xProperty)
                            { result.Add(xProperty); }
                        }
                    }
                }

            }

            return result;
        }

<<<<<<< HEAD
       
=======
>>>>>>> RenameIndexValue
    }
}
