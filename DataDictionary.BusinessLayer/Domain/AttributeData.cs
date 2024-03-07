using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
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
    public interface IAttributeData :
        IBindingData<DomainAttributeItem>,
        ILoadData<IDomainAttributeKey>, ISaveData<IDomainAttributeKey>,
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
        /// List of Model Attribute Subject Areas within the Model.
        /// </summary>
        IAttributeSubjectAreaData SubjectAreas { get; }

        /// <summary>
        /// Returns the Attribute as XML.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        XDocument ToXml(IDomainAttributeKey key);
    }

    class AttributeData : DomainAttributeCollection, IAttributeData,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDataTableFile, INamedScopeData<IModelKey>
    {
        public required IDomainModel DomainModel { get; init; }

        /// <inheritdoc/>
        public IAttributeAliasData Aliases { get { return aliasValues; } }
        private readonly AttributeAliasData aliasValues;

        /// <inheritdoc/>
        public IAttributePropertyData Properties { get { return propertyValues; } }
        private readonly AttributePropertyData propertyValues;

        /// <inheritdoc/>
        public IAttributeSubjectAreaData SubjectAreas { get { return subjectAreaValues; } }
        private readonly AttributeSubjectAreaData subjectAreaValues;

        public AttributeData() : base()
        {
            aliasValues = new AttributeAliasData();
            propertyValues = new AttributePropertyData();
            subjectAreaValues = new AttributeSubjectAreaData();
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDomainAttributeKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainAttributeKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

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
            List<WorkItem> result = new List<WorkItem>();

            result.Add(new WorkItem()
            {
                WorkName = "Remove Attributes",
                DoWork = () =>
                {
                    aliasValues.Clear();
                    propertyValues.Clear();
                    this.Clear();
                }
            });

            return result;
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
        public void Import(IDatabaseModel source, IPropertyData propertyDefinition, IDbCatalogKeyName key)
        {
            DbCatalogKeyName nameKey = new DbCatalogKeyName(key);
            foreach (DbTableItem item in source.DbTables.Where(w => nameKey.Equals(w)))
            {
                DbTableKeyName tableKey = new DbTableKeyName(item);
                Import(source, propertyDefinition, tableKey);
            }
        }

        /// <inheritdoc/>
        /// <remarks>Attribute by Table</remarks>
        public void Import(IDatabaseModel source, IPropertyData propertyDefinition, IDbTableKeyName key)
        {
            DbTableKeyName nameKey = new DbTableKeyName(key);
            foreach (DbTableColumnItem item in source.DbTableColumns.Where(w => nameKey.Equals(w)))
            {
                DbTableColumnKeyName columKey = new DbTableColumnKeyName(item);
                Import(source, propertyDefinition, columKey);
            }
        }

        /// <inheritdoc/>
        /// <remarks>Attribute by Column</remarks>
        public void Import(IDatabaseModel source, IPropertyData propertyDefinition, IDbTableColumnKeyName key)
        {
            DbTableColumnKeyName nameKey = new DbTableColumnKeyName(key);
            foreach (DbTableColumnItem item in source.DbTableColumns.Where(w => nameKey.Equals(w)))
            {
                AliasKeyName alaisKey = new AliasKeyName(item);
                DomainAttributeKey attributeKey;
                DomainAttributeUniqueKey uniqueKey = new DomainAttributeUniqueKey(item);
                AliasKeyName aliasKey = new AliasKeyName(item);

                // Create Attribute or get existing
                if (aliasValues.FirstOrDefault(w => aliasKey.Equals(w)) is DomainAttributeAliasItem existingAlias)
                { attributeKey = new DomainAttributeKey(existingAlias); }
                else if (this.FirstOrDefault(w => uniqueKey.Equals(w)) is DomainAttributeItem existing)
                { attributeKey = new DomainAttributeKey(existing); }
                else
                {
                    DomainAttributeItem newItem = new DomainAttributeItem()
                    {
                        AttributeTitle = item.ColumnName,
                        IsDerived = item.IsComputed ?? false,
                        IsIntegral = !item.IsComputed ?? false,
                        IsNullable = item.IsNullable ?? false,
                        IsValued = !item.IsNullable ?? false,
                    };
                    this.Add(newItem);
                    attributeKey = new DomainAttributeKey(newItem);
                }

                // Create Alias
                if (aliasValues.Count(w => alaisKey.Equals(w) && attributeKey.Equals(w)) == 0)
                {
                    aliasValues.Add(new DomainAttributeAliasItem(attributeKey)
                    {
                        AliasName = item.ToAliasName(),
                        ScopeName = item.ScopeName
                    });
                }

                // Create Properties
                DbExtendedPropertyKeyName propertyKey = new DbExtendedPropertyKeyName(item);
                foreach (DbExtendedPropertyItem property in source.DbExtendedProperties.Where(w => propertyKey.Equals(w)))
                {
                    PropertyKeyExtended appKey = new PropertyKeyExtended(property);

                    if (propertyDefinition.FirstOrDefault(w =>
                        appKey.Equals(w)) is IPropertyItem appProperty
                        && propertyValues.Count(w =>
                            attributeKey.Equals(w)
                            && new PropertyKey(appProperty).Equals(w)) == 0)
                    { propertyValues.Add(new DomainAttributePropertyItem(attributeKey, appProperty, property)); }
                }
            }
        }

        public IReadOnlyList<WorkItem> Export(IList<NamedScopeItem> target, Func<IModelKey?> parent)
        {
            return new WorkItem()
            {
                WorkName = "Load NameScope, Attributes",
                DoWork = () =>
                {
                    if (parent() is IModelKey key)
                    {
                        foreach (DomainAttributeItem item in this)
                        { target.Add(new NamedScopeItem(key, item)); }
                    }
                }
            }.ToList();
        }

        /// <inheritdoc/>
        public XDocument ToXml(IDomainAttributeKey key)
        {
            DomainAttributeKey attributeKey = new DomainAttributeKey(key);
            XDocument result = new XDocument();

            if (this.FirstOrDefault(w => attributeKey.Equals(w)) is DomainAttributeItem attributeItem)
            {
                XElement xAttribute = new XElement(attributeItem.GetType().Name);

                if (attributeItem.AttributeId is not null)
                { xAttribute.Add(new XAttribute(nameof(attributeItem.AttributeId), attributeItem.AttributeId)); }

                if(attributeItem.AttributeTitle is not null) 
                { xAttribute.Add(new XElement(nameof(attributeItem.AttributeTitle), attributeItem.AttributeTitle)); }

                if (attributeItem.AttributeDescription is not null)
                { xAttribute.Add(new XElement(nameof(attributeItem.AttributeDescription), attributeItem.AttributeDescription)); }

                result.Add(xAttribute);
            }

            return result;
        }
    }
}
