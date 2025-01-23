// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface component for the Model Entity
    /// </summary>
    public interface IEntityData :
        IBindingData<EntityValue>,
        ILoadData<IEntityIndex>, ISaveData<IEntityIndex>
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

        /// <summary>
        /// Finds the Entity that match the Alias Index.
        /// </summary>
        /// <param name="aliasIndex"></param>
        /// <returns></returns>
        IEnumerable<IEntityValue> FindEntity(IAliasIndex aliasIndex);

        /// <summary>
        /// Imports a TableEntity into the list of Entities
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEntityValue Import(AppCatalog.TableEntity source);
    }

    class EntityData : EntityCollection<EntityValue>, IEntityData,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        IDataTableFile, INamedScopeSourceData
    {
        public required Model Model { get; init; }

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
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, (IModelKey)dataKey));
            work.AddRange(aliasValues.Load(factory, dataKey));
            work.AddRange(propertyValues.Load(factory, dataKey));
            work.AddRange(definitionValues.Load(factory, dataKey));
            work.AddRange(attributeValues.Load(factory, dataKey));
            work.AddRange(subjectAreaValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, DateTime asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate));
            work.AddRange(aliasValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(propertyValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(definitionValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(attributeValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(subjectAreaValues.Load(factory, dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, (IEntityKey)dataKey));
            work.AddRange(aliasValues.Load(factory, dataKey));
            work.AddRange(propertyValues.Load(factory, dataKey));
            work.AddRange(definitionValues.Load(factory, dataKey));
            work.AddRange(attributeValues.Load(factory, dataKey));
            work.AddRange(subjectAreaValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityIndex dataKey, DateTime asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, (IEntityKey)dataKey, asOfUtcDate));
            work.AddRange(aliasValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(propertyValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(definitionValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(attributeValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(subjectAreaValues.Load(factory, dataKey, asOfUtcDate));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IEntityIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, (IEntityKey)dataKey));
            work.AddRange(aliasValues.Save(factory, dataKey));
            work.AddRange(propertyValues.Save(factory, dataKey));
            work.AddRange(definitionValues.Save(factory, dataKey));
            work.AddRange(attributeValues.Save(factory, dataKey));
            work.AddRange(subjectAreaValues.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, (IModelKey)dataKey));
            work.AddRange(aliasValues.Save(factory, dataKey));
            work.AddRange(propertyValues.Save(factory, dataKey));
            work.AddRange(definitionValues.Save(factory, dataKey));
            work.AddRange(attributeValues.Save(factory, dataKey));
            work.AddRange(subjectAreaValues.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Remove Entity", DoWork = () => { Clear(); } });
            work.AddRange(aliasValues.Delete());
            work.AddRange(propertyValues.Delete());
            work.AddRange(definitionValues.Delete());
            work.AddRange(attributeValues.Delete());
            work.AddRange(subjectAreaValues.Delete());

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public void Remove(IEntityIndex entityItem)
        {
            base.Remove(entityItem);
            EntityKey key = new EntityKey(entityItem);
            aliasValues.Remove(key);
            propertyValues.Remove(key);
            definitionValues.Remove(key);
            attributeValues.Remove(key);
            subjectAreaValues.Remove(key);
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Delete(IEntityIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Entity", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
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
            Load(source);
            aliasValues.Load(source);
            propertyValues.Load(source);
            definitionValues.Load(source);
            attributeValues.Load(source);
            subjectAreaValues.Load(source);
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progressChanged = (completed, total) => { };

            WorkItem newWork = new WorkItem(ref progressChanged)
            {
                WorkName = "Adding NamedScopes (Entities)",
                DoWork = () =>
                {
                    Int32 completed = 0;
                    Int32 total = this.Count();

                    ModelValue? model = Model.Models.FirstOrDefault();

                    foreach (EntityValue entity in this)
                    {
                        Boolean hasParent = false;

                        foreach (SubjectAreaValue subjectParent in ParentSubjects(entity))
                        {
                            NamedScopeValue newItem = new NamedScopeValue(entity)
                            {
                                GetPath = () => new PathIndex(
                                    ((IPathValue)subjectParent).Path,
                                    ((IPathValue)entity).Path)
                            };

                            addNamedScope(subjectParent, newItem);
                            hasParent = true;
                        }

                        if (!hasParent) // No Parents found
                        {
                            NamedScopeValue newItem = new NamedScopeValue(entity);
                            addNamedScope(model, newItem);
                        }

                        progressChanged(completed++, total);
                    }
                }
            };

            work.Add(newWork);

            return work;

            IEnumerable<SubjectAreaValue> ParentSubjects(EntityValue entity)
            {
                EntityIndex key = new EntityIndex(entity);

                return this.
                    Where(w => key.Equals(w)).
                    Join(SubjectArea,
                        entity => new EntityIndex(entity),
                        subject => new EntityIndex(subject),
                        (entity, subject) => new SubjectAreaIndex(subject)).
                    Join(Model.SubjectAreas,
                        subjectKey => subjectKey,
                        subject => new SubjectAreaIndex(subject),
                        (key, subject) => subject).
                    ToList();
            }

        }

        /// <inheritdoc/>
        public IEnumerable<IEntityValue> FindEntity(IAliasIndex aliasIndex)
        {
            AliasIndex key = new AliasIndex(aliasIndex);
            return
                this.Join(
                    Aliases.Where(w => key.Equals(w)),
                    entity => new EntityIndex(entity),
                    alias => new EntityIndex(alias),
                    (entity, alias) => entity).
                ToList();
        }

        public IEntityValue Import(TableEntity source)
        {
            // Find the Entity by Alias
            IEntityValue entity = source.Aliases.
                SelectMany(s => FindEntity(new AliasIndex(s))).
                FirstOrDefault() ??
                source.Entity;

            EntityIndex entityIndex = new EntityIndex(entity);

            // Entity already exists, copy the source information into the existing Entity
            if (this.FirstOrDefault(w => entityIndex.Equals(w)) is EntityValue value)
            {

                if (String.IsNullOrEmpty(value.EntityDescription))
                { value.EntityDescription = source.Entity.EntityDescription; }

                foreach (var property in source.Properties)
                {
                    PropertyIndex propertyIndex = new PropertyIndex(property);
                    if (Properties.FirstOrDefault(w => entityIndex.Equals(w) && propertyIndex.Equals(w)) is not EntityPropertyValue)
                    { Properties.Add(new EntityPropertyValue(value, property) { PropertyValue = property.PropertyValue }); };
                }

                foreach (var alias in source.Aliases)
                {
                    AliasIndex aliasIndex = new AliasIndex(alias);
                    if (Aliases.FirstOrDefault(w => entityIndex.Equals(w) && aliasIndex.Equals(w)) is not EntityAliasValue)
                    { Aliases.Add(new EntityAliasValue(value, aliasIndex)); }
                }

                // Attributes get replaced
                attributeValues.Delete(entityIndex);
                foreach (IEntityAttributeValue item in source.Attributes)
                { Attributes.Add(item); }
            }
            else // Entity does not exist, add everything
            {
                Add(source.Entity);

                foreach (var item in source.Properties)
                { Properties.Add(item); }

                foreach (var item in source.Aliases)
                { Aliases.Add(item); }

                foreach (var item in source.Attributes)
                { Attributes.Add(item); }

            }

            return entity;
        }


    }
}
