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
using DataDictionary.DataLayer.ModelData.Attribute;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using System.Xml.Linq;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface component for the Model Attribute
    /// </summary>
    public interface IAttributeData :
        IBindingData<AttributeItem>,
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
    }

    class AttributeData : DomainAttributeCollection<AttributeItem>, IAttributeData,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDataTableFile
    {
        public required DomainModel Model { get; init; }

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
                AttributeKey attributeKey;
                AttributeKeyName uniqueKey = new AttributeKeyName(item);
                AliasKeyName aliasKey = new AliasKeyName(item);

                // Create Attribute or get existing
                if (aliasValues.FirstOrDefault(w => aliasKey.Equals(w)) is DomainAttributeAliasItem existingAlias)
                { attributeKey = new AttributeKey(existingAlias); }
                else if (this.FirstOrDefault(w => uniqueKey.Equals(w)) is DomainAttributeItem existing)
                { attributeKey = new AttributeKey(existing); }
                else
                {
                    AttributeItem newItem = new AttributeItem()
                    {
                        AttributeTitle = item.ColumnName,
                        IsDerived = item.IsComputed ?? false,
                        IsIntegral = !item.IsComputed ?? false,
                        IsNullable = item.IsNullable ?? false,
                        IsValued = !item.IsNullable ?? false,
                    };
                    this.Add(newItem);
                    attributeKey = new AttributeKey(newItem);
                }

                // Create Alias
                if (aliasValues.Count(w => alaisKey.Equals(w) && attributeKey.Equals(w)) == 0)
                {
                    aliasValues.Add(new AttributeAliasItem(attributeKey)
                    {
                        AliasName = item.ToAliasName(),
                        Scope = item.Scope
                    });
                }

                // Create Properties
                DbExtendedPropertyKeyName propertyKey = new DbExtendedPropertyKeyName(item);
                foreach (DbExtendedPropertyItem property in source.DbExtendedProperties.Where(w => propertyKey.Equals(w)))
                {
                    PropertyKeyExtended appKey = new PropertyKeyExtended(property);

                    if (propertyDefinition.FirstOrDefault(w =>
                        appKey.Equals(w)) is Application.IPropertyItem appProperty
                        && propertyValues.Count(w =>
                            attributeKey.Equals(w)
                            && new Application.PropertyKey(appProperty).Equals(w)) == 0)
                    { propertyValues.Add(new AttributePropertyItem(attributeKey, appProperty, property)); }
                }
            }
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Build(INamedScopeDictionary target)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Build NamedScope Attributes",
                DoWork = () =>
                {
                    if (Model.Models.FirstOrDefault() is IModelItem model)
                    {
                        ModelKey key = new ModelKey(model);
                        List<IDomainAttributeItem> unhandled = this.Select(s => s as IDomainAttributeItem).Cast<IDomainAttributeItem>().ToList();

                        foreach (IDomainAttributeItem item in unhandled)
                        { target.Remove(new NamedScopeKey(item)); }

                        foreach (IGrouping<ModelSubjectAreaKey, ModelAttributeItem> subject in SubjectAreas.GroupBy(g => new ModelSubjectAreaKey(g)))
                        {
                            NamedScopeKey subjectKey = new NamedScopeKey(subject.Key);
                            ModelSubjectAreaKey subjectModelKey = new ModelSubjectAreaKey(subject.Key);

                            if (target.ContainsKey(new NamedScopeKey(subjectKey)))
                            {
                                foreach (IDomainAttributeItem attribute in subject.Select(s => s as IDomainAttributeItem).Cast<IDomainAttributeItem>())
                                {
                                    //TODO: Possible Error if the attribute is in multiple subject areas
                                    target.Add(new NamedScopeItem(subjectModelKey, attribute));
                                    unhandled.Remove(attribute);
                                }
                            }
                        }

                        foreach (IDomainAttributeItem item in unhandled)
                        {
                            target.Remove(new NamedScopeKey(item));
                            target.Add(new NamedScopeItem(key, item));
                        }
                    }
                }
            });

            return work;
        }

        public XElement? GetXElement(IAttributeKey key, IEnumerable<ElementItem>? options = null)
        {
            XElement? result = null;

            AttributeKey attributeKey = new AttributeKey(key);
            if (this.FirstOrDefault(w => attributeKey.Equals(w)) is AttributeItem attribute)
            {
                if (attribute.GetXElement(options) is XElement xAttribute)
                {
                    result = xAttribute;

                    if (Properties.FirstOrDefault(w => attributeKey.Equals(w)) is AttributePropertyItem property)
                    {
                        Application.PropertyKey propertyKey = new Application.PropertyKey(property);
                        if (Model.ModelProperty.FirstOrDefault((Object w) => propertyKey.Equals(w)) is Application.PropertyItem item)
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
