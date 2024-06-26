using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Property;
using DataDictionary.DataLayer.ModelData;
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
        INamedScopeSource, IDataTableFile
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

        public required IPropertyData DomainProperties { get; init; }
        public required IDefinitionData DomainDefinitions { get; init; }

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
                        AliasScope = item.Scope
                    });
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
        }
        #endregion

        #region INamedScopeSource
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
                    { result.Add(new NamedScopePair(subject.GetIndex(), GetSubjectValue(attribute, subject))); }
                }

                if (!hasSubjectArea && model is not null)
                { result.Add(new NamedScopePair(model.GetIndex(), GetModelValue(attribute, model))); }
                else if (!hasSubjectArea && model is null)
                { result.Add(new NamedScopePair(GetValue(attribute))); } // Should not occur.

            }

            return result;

            NamedScopeValueCore GetValue(AttributeValue source)
            {
                NamedScopeValueCore result = new NamedScopeValueCore(source);
                source.PropertyChanged += Source_PropertyChanged;

                return result;

                void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName is nameof(source.AttributeTitle))
                    { result.TitleChanged(); }
                }
            }

            NamedScopeValueCore GetSubjectValue(AttributeValue source, SubjectAreaValue subject)
            {
                NamedScopeValueCore result = new NamedScopeValueCore(source)
                { GetPath = () => new NamedScopePath(subject.GetPath(), source.GetPath()) };
                source.PropertyChanged += Source_PropertyChanged;
                subject.PropertyChanged += Source_PropertyChanged;

                return result;

                void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName is nameof(source.AttributeTitle) or nameof(subject.SubjectAreaNameSpace))
                    { result.TitleChanged(); }
                }
            }

            NamedScopeValueCore GetModelValue(AttributeValue source, ModelValue model)
            {
                NamedScopeValueCore result = new NamedScopeValueCore(source)
                { GetPath = () => new NamedScopePath(model.GetPath(), source.GetPath()) };
                source.PropertyChanged += Source_PropertyChanged;
                model.PropertyChanged += Source_PropertyChanged;

                return result;

                void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName is nameof(source.AttributeTitle) or nameof(model.ModelTitle))
                    { result.TitleChanged(); }
                }
            }
        }
        #endregion

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
                    TemplateNodeIndex nodeKey = new TemplateNodeIndex(node);
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
                        if (result is null) { result = new XElement(attribute.Scope.ToName()); }
                        result.Add(value);

                        foreach (TemplateAttributeValue templateAttrib in scripting.Attributes.Where(w => nodeKey.Equals(w)))
                        {
                            XAttribute? attrib = null;
                            PropertyIndex propertyKey = new PropertyIndex(templateAttrib);
                            PropertyValue? propertyValue = DomainProperties.FirstOrDefault(w => propertyKey.Equals(w));
                            IDomainProperty? property = Properties.FirstOrDefault(w => propertyKey.Equals(w));

                            String newTitle = String.Empty;
                            String newValue = String.Empty;

                            if (!String.IsNullOrWhiteSpace(templateAttrib.AttributeName))
                            { newTitle = templateAttrib.AttributeName; }
                            else if (propertyValue is PropertyValue && !String.IsNullOrWhiteSpace(propertyValue.PropertyTitle))
                            { { newTitle = propertyValue.PropertyTitle; } }

                            if (property is IDomainProperty && !String.IsNullOrWhiteSpace(property.PropertyValue))
                            { newValue = property.PropertyValue; }
                            else if (!String.IsNullOrWhiteSpace(templateAttrib.AttributeValue))
                            { newValue = templateAttrib.AttributeValue; }

                            attrib = templateAttrib.BuildXAttribute(newTitle, newValue);

                            if (attrib is XAttribute)
                            {
                                if (value is XElement element) { element.Add(attrib); }
                                else if (value.Parent is XElement) { value.Parent.Add(attrib); }
                            }
                        }
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
