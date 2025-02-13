// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface representing Model Entity data
    /// </summary>
    public interface IEntity :
        ILoadData<IEntityIndex>, ISaveData<IEntityIndex>, IDeleteData<IEntityIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        /// <summary>
        /// List of Entities within the Model.
        /// </summary>
        IEntityData Values { get; }

        /// <summary>
        /// List of Aliases for the Entities within the Model.
        /// </summary>
        IEntityAliasData Aliases { get; }

        /// <summary>
        /// List of Properties for the Entities within the Model.
        /// </summary>
        IEntityPropertyData Properties { get; }

        /// <summary>
        /// List of Definitions for the Entities within the Model.
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

    class Entity : IEntity, IDataTableFile
    {
        /// <inheritdoc/>
        public IEntityData Values { get { return entityValues; } }
        private readonly EntityData entityValues;

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

        public Entity() : base()
        {
            entityValues = new EntityData();
            aliasValues = new EntityAliasData();
            definitionValues = new EntityDefinitionData();
            propertyValues = new EntityPropertyData();
            attributeValues = new EntityAttributeData();
            subjectAreaValues = new EntitySubjectAreaData();
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(entityValues.Load(factory, dataKey));
            work.AddRange(aliasValues.Load(factory, dataKey));
            work.AddRange(propertyValues.Load(factory, dataKey));
            work.AddRange(definitionValues.Load(factory, dataKey));
            work.AddRange(attributeValues.Load(factory, dataKey));
            work.AddRange(subjectAreaValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(entityValues.Load(factory, dataKey, asOfUtcDate));
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
            work.AddRange(entityValues.Load(factory, dataKey));
            work.AddRange(aliasValues.Load(factory, dataKey));
            work.AddRange(propertyValues.Load(factory, dataKey));
            work.AddRange(definitionValues.Load(factory, dataKey));
            work.AddRange(attributeValues.Load(factory, dataKey));
            work.AddRange(subjectAreaValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(entityValues.Load(factory, dataKey, asOfUtcDate));
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
            work.AddRange(entityValues.Save(factory, dataKey));
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
            work.AddRange(entityValues.Save(factory, dataKey));
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

            work.AddRange(entityValues.Delete());
            work.AddRange(aliasValues.Delete());
            work.AddRange(propertyValues.Delete());
            work.AddRange(definitionValues.Delete());
            work.AddRange(attributeValues.Delete());
            work.AddRange(subjectAreaValues.Delete());

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Delete(IEntityIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(entityValues.Delete(dataKey));
            work.AddRange(aliasValues.Delete(dataKey));
            work.AddRange(propertyValues.Delete(dataKey));
            work.AddRange(definitionValues.Delete(dataKey));
            work.AddRange(attributeValues.Delete(dataKey));
            work.AddRange(subjectAreaValues.Delete(dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(entityValues.ToDataTable());
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
            entityValues.Load(source);
            aliasValues.Load(source);
            propertyValues.Load(source);
            definitionValues.Load(source);
            attributeValues.Load(source);
            subjectAreaValues.Load(source);
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityValue> FindEntity(IAliasIndex aliasIndex)
        {
            AliasIndex key = new AliasIndex(aliasIndex);
            return
                entityValues.Join(
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
            if (entityValues.FirstOrDefault(w => entityIndex.Equals(w)) is EntityValue value)
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
                entityValues.Add(source.Entity);

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
