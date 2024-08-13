using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface component for the Model Entity
    /// </summary>
    public interface IEntityData :
        IBindingData<EntityValue>,
        ILoadData<IEntityIndex>, ISaveData<IEntityIndex>,
        ITableImport
    {
        /// <summary>
        /// List of Domain Aliases for the Entities within the Model.
        /// </summary>
        IEntityAliasData Aliases { get; }

        /// <summary>
        /// List of Domain Properties for the Entities within the Model.
        /// </summary>
        IEntityPropertyData Properties { get; }

        /// <summary>
        /// List of Domain Definitions for the Entities within the Model.
        /// </summary>
        IEntityDefinitionData Definitions { get; }

        /// <summary>
        /// List of Attributes for the Entities within the Model.
        /// </summary>
        IEntityAttributeData Attributes { get; }

        /// <summary>
        /// List of Subject Areas for the Entities within the Model.
        /// </summary>
        IEntitySubjectAreaData SubjectArea { get; }
    }

    class EntityData : DomainEntityCollection<EntityValue>, IEntityData,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDataTableFile, INamedScopeSource
    {
        public required DomainModel Model { get; init; }

        /// <inheritdoc/>
        public IEntityAliasData Aliases { get { return aliasValues; } }
        private readonly EntityAliasData aliasValues;


        /// <inheritdoc/>
        public IEntityDefinitionData Definitions { get { return definitionValues; } }
        private readonly EntityDefinitionData definitionValues;

        /// <inheritdoc/>
        public IEntityPropertyData Properties { get { return propertyValues; } }
        private readonly EntityPropertyData propertyValues;

        /// <inheritdoc/>
        public IEntityAttributeData Attributes { get { return attributeValues; } }
        private readonly EntityAttributeData attributeValues;

        /// <inheritdoc/>
        public IEntitySubjectAreaData SubjectArea { get { return subjectAreaValues; } }
        private readonly EntitySubjectAreaData subjectAreaValues;

        public EntityData() : base()
        {
            aliasValues = new EntityAliasData();
            propertyValues = new EntityPropertyData();
            definitionValues = new EntityDefinitionData();
            attributeValues = new EntityAttributeData();
            subjectAreaValues = new EntitySubjectAreaData();
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDomainEntityKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, dataKey));
            work.Add(factory.CreateLoad(aliasValues, dataKey));
            work.Add(factory.CreateLoad(propertyValues, dataKey));
            work.Add(factory.CreateLoad(definitionValues, dataKey));
            work.Add(factory.CreateLoad(attributeValues, dataKey));
            work.Add(factory.CreateLoad(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, dataKey));
            work.Add(factory.CreateLoad(aliasValues, dataKey));
            work.Add(factory.CreateLoad(propertyValues, dataKey));
            work.Add(factory.CreateLoad(definitionValues, dataKey));
            work.Add(factory.CreateLoad(attributeValues, dataKey));
            work.Add(factory.CreateLoad(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityIndex dataKey)
        { return Load(factory, (IDomainEntityKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainEntityKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, dataKey));
            work.Add(factory.CreateSave(aliasValues, dataKey));
            work.Add(factory.CreateSave(propertyValues, dataKey));
            work.Add(factory.CreateSave(definitionValues, dataKey));
            work.Add(factory.CreateSave(attributeValues, dataKey));
            work.Add(factory.CreateSave(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, dataKey));
            work.Add(factory.CreateSave(aliasValues, dataKey));
            work.Add(factory.CreateSave(propertyValues, dataKey));
            work.Add(factory.CreateSave(definitionValues, dataKey));
            work.Add(factory.CreateSave(attributeValues, dataKey));
            work.Add(factory.CreateSave(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IEntityIndex dataKey)
        { return Save(factory, (IDomainEntityKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Remove Entity", DoWork = () => { this.Clear(); } });
            work.AddRange(aliasValues.Delete());
            work.AddRange(propertyValues.Delete());
            work.AddRange(definitionValues.Delete());
            work.AddRange(attributeValues.Delete());
            work.AddRange(subjectAreaValues.Delete());

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public override void Remove(IDomainEntityKey entityItem)
        {
            base.Remove(entityItem);
            DomainEntityKey key = new DomainEntityKey(entityItem);
            aliasValues.Remove(key);
            propertyValues.Remove(key);
            definitionValues.Remove(key);
            attributeValues.Remove(key);
            subjectAreaValues.Remove(key);
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Delete(IEntityIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Entity", DoWork = () => { this.Remove((IDomainEntityKey)dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(this.ToDataTable());
            result.Add(aliasValues.ToDataTable());
            result.Add(propertyValues.ToDataTable());
            result.Add(definitionValues.ToDataTable());
            result.Add(attributeValues.ToDataTable());
            result.Add(subjectAreaValues.ToDataTable());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public void Import(System.Data.DataSet source)
        {
            this.Load(source);
            aliasValues.Load(source);
            propertyValues.Load(source);
            definitionValues.Load(source);
            attributeValues.Load(source);
            subjectAreaValues.Load(source);
        }


        /// <inheritdoc/>
        /// <remarks>Entity by Catalog</remarks>
        public void Import(IDatabaseModel source, IPropertyData propertyDefinition, ICatalogIndex key)
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

        /// <inheritdoc/>
        /// <remarks>Entity by Table</remarks>
        public void Import(IDatabaseModel source, IPropertyData propertyDefinition, ITableIndex key)
        {
            TableIndex tableKey = new TableIndex(key);
            foreach (TableValue item in source.DbTables.Where(w => tableKey.Equals(w)))
            {
                AliasIndex aliasKey = new AliasIndex(item);
                EntityIndexName entityName = new EntityIndexName(item);
                EntityIndex entityKey;


                // Create Entity or get existing
                if (aliasValues.FirstOrDefault(w => aliasKey.Equals(w)) is EntityAliasValue existingAlias)
                { entityKey = new EntityIndex(existingAlias); }
                else if (this.FirstOrDefault(w => entityName.Equals(w)) is EntityValue existing)
                { entityKey = new EntityIndex(existing); }
                else
                {
                    EntityValue newItem = new EntityValue()
                    { 
                        EntityTitle = item.TableName,
                        MemberName = new NamedScopePath(item.SchemaName, item.TableName).MemberFullPath,
                    };
                    this.Add(newItem);
                    entityKey = new EntityIndex(newItem);
                }

                // Create Alias, if they do not exist
                if (aliasValues.Count(w => aliasKey.Equals(w) && entityKey.Equals(w)) == 0)
                {
                    var newAlias = new EntityAliasValue(entityKey, new AliasIndex(item));

                    aliasValues.Add(newAlias);

                    // Look for related attributes
                    foreach (AttributeAliasValue attribute in Model.Attributes.Aliases.
                        Where(w => w.AliasPath.ParentPath is NamedScopePath
                            && w.AliasPath.ParentPath.Equals(newAlias.AliasPath)))
                    {
                        EntityAttributeValue entityAttribute = new EntityAttributeValue(entityKey, attribute);
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
                            entityKey.Equals(w)
                            && new PropertyIndexValue(appProperty).Equals(w)) == 0)
                    { propertyValues.Add(new EntityPropertyValue(entityKey, appProperty, property)); }
                }
            }
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            List<NamedScopePair> result = new List<NamedScopePair>();

            DataLayerIndex parentIndex;
            if (Model.Models.FirstOrDefault() is ModelValue model)
            { parentIndex = model.GetIndex(); }
            else { throw new InvalidOperationException("Could not find the Model"); }

            var values = this.GroupJoin(SubjectArea.Join(Model.SubjectAreas,
                entity => new SubjectAreaIndex(entity),
                subject => new SubjectAreaIndex(subject),
                (entity, subject) => new
                {
                    entityIndex = new EntityIndex(entity),
                    subjectIndex = subject.GetIndex(),
                    subjectPath = subject.GetPath()
                }),
                entity => new EntityIndex(entity),
                subject => subject.entityIndex,
                (entity, subjects) => new { entity, subjects }).
                ToList();


            foreach (var item in values)
            {
                NamedScopeValue value = new NamedScopeValue(item.entity);

                if (item.subjects.Count() == 0)
                { result.Add(new NamedScopePair(parentIndex, value)); }
                else
                {
                    foreach (var subject in item.subjects)
                    {
                        result.Add(new NamedScopePair(
                            subject.subjectIndex,
                            new NamedScopeValue(item.entity)
                            { // Create Full Path, including subject
                                GetPath = () => new NamedScopePath(
                                    subject.subjectPath,
                                    item.entity.GetPath())
                            }));
                    }
                }
            }

            return result;
        }
    }
}
