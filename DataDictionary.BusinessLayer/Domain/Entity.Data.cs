﻿using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Alias;
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
        public IEntitySubjectAreaData SubjectArea { get { return subjectAreaValues; } }
        private readonly EntitySubjectAreaData subjectAreaValues;

        public EntityData() : base()
        {
            aliasValues = new EntityAliasData();
            propertyValues = new EntityPropertyData();
            definitionValues = new EntityDefinitionData();
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
            work.Add(factory.CreateSave(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IEntityIndex dataKey)
        { return Save(factory, (IDomainEntityKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Remove()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Remove Entity", DoWork = () => { this.Clear(); } });
            work.AddRange(aliasValues.Remove());
            work.AddRange(propertyValues.Remove());
            work.AddRange(definitionValues.Remove());
            work.AddRange(subjectAreaValues.Remove());

            return work;
        }

        /// <inheritdoc/>
        public override void Remove(IDomainEntityKey entityItem)
        {
            base.Remove(entityItem);
            DomainEntityKey key = new DomainEntityKey(entityItem);
            aliasValues.Remove(key);
            propertyValues.Remove(key);
            definitionValues.Remove(key);
            subjectAreaValues.Remove(key);
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
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
        /// <remarks>Entity</remarks>
        public void Import(System.Data.DataSet source)
        {
            this.Load(source);
            aliasValues.Load(source);
            propertyValues.Load(source);
            definitionValues.Load(source);
            subjectAreaValues.Load(source);
        }

        /// <inheritdoc/>
        /// <remarks>Entity by Catalog</remarks>
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
        /// <remarks>Entity by Table</remarks>
        public void Import(IDatabaseModel source, IPropertyData propertyDefinition, ITableIndex key)
        {
            TableIndex tableKey = new TableIndex(key);
            foreach (TableValue item in source.DbTables.Where(w => tableKey.Equals(w)))
            {
                AliasKeyName aliasKey = new AliasKeyName(item);
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
                    { EntityTitle = item.TableName, };
                    this.Add(newItem);
                    entityKey = new EntityIndex(newItem);
                }

                // Create Alias, if they do not exist
                if (aliasValues.Count(w => aliasKey.Equals(w) && entityKey.Equals(w)) == 0)
                {
                    aliasValues.Add(new EntityAliasValue(entityKey)
                    {
                        AliasName = item.ToAliasName(),
                        Scope = item.Scope,
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
                            entityKey.Equals(w)
                            && new Application.PropertyIndex(appProperty).Equals(w)) == 0)
                    { propertyValues.Add(new EntityPropertyValue(entityKey, appProperty, property)); }
                }
            }
        }


        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            List<NamedScopePair> result = new List<NamedScopePair>();

            ModelValue? model = Model.Models.FirstOrDefault();

            foreach (EntityValue entity in this)
            {
                EntityIndex EntityKey = new EntityIndex(entity);
                Boolean hasSubjectArea = false;

                foreach (EntitySubjectAreaValue subjectArea in subjectAreaValues.Where(w => EntityKey.Equals(w)))
                {
                    hasSubjectArea = true;
                    SubjectAreaIndex subjectKey = new SubjectAreaIndex(subjectArea);

                    if (Model.SubjectAreas.FirstOrDefault(w => subjectKey.Equals(w)) is SubjectAreaValue subject)
                    { result.Add(new NamedScopePair(subject.GetIndex(), GetSubjectValue(entity, subject))); }
                }

                if (!hasSubjectArea && model is not null)
                { result.Add(new NamedScopePair(model.GetIndex(), GetModelValue(entity, model))); }
                else if (!hasSubjectArea && model is null)
                { result.Add(new NamedScopePair(GetValue(entity))); } // Should not occur.

            }

            return result;

            NamedScopeValueCore GetValue(EntityValue source)
            {
                NamedScopeValueCore result = new NamedScopeValueCore(source);
                source.PropertyChanged += Source_PropertyChanged;

                return result;

                void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName is nameof(source.EntityTitle))
                    { result.TitleChanged(); }
                }
            }

            NamedScopeValueCore GetSubjectValue(EntityValue source, SubjectAreaValue subject)
            {
                NamedScopeValueCore result = new NamedScopeValueCore(source)
                { GetPath = () => new NamedScopePath(subject.GetPath(), source.GetPath()) };
                source.PropertyChanged += Source_PropertyChanged;
                subject.PropertyChanged += Source_PropertyChanged;

                return result;

                void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName is nameof(source.EntityTitle) or nameof(subject.SubjectAreaNameSpace))
                    { result.TitleChanged(); }
                }
            }

            NamedScopeValueCore GetModelValue(EntityValue source, ModelValue model)
            {
                NamedScopeValueCore result = new NamedScopeValueCore(source)
                { GetPath = () => new NamedScopePath(model.GetPath(), source.GetPath()) };
                source.PropertyChanged += Source_PropertyChanged;
                model.PropertyChanged += Source_PropertyChanged;

                return result;

                void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName is nameof(source.EntityTitle) or nameof(model.ModelTitle))
                    { result.TitleChanged(); }
                }
            }
        }
    }
}
