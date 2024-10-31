using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
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
    }

    class EntityData : DomainEntityCollection<EntityValue>, IEntityData,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDataTableFile, INamedScopeSourceData
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
                            addNamedScope(model, newItem); }

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
    }
}
