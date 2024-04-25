using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ModelData.Entity;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface component for the Model Entity
    /// </summary>
    public interface IEntityData<TValue> : IBindingData<TValue>,
        ILoadData<IDomainEntityKey>, ISaveData<IDomainEntityKey>,
        ITableImport
        where TValue : EntityValue, IEntityValue
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
        /// List of Model Entity Subject Areas within the Model.
        /// </summary>
        IEntitySubjectAreaData SubjectAreas { get; }
    }

    class EntityData<TValue> : DomainEntityCollection<TValue>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IEntityData<TValue>, IDataTableFile, IGetNamedScopes
        where TValue : EntityValue, IEntityValue, new()
    {
        public required DomainModel DomainModel { get; init; }

        /// <inheritdoc/>
        public IEntityAliasData Aliases { get { return aliasValues; } }
        private readonly EntityAliasData aliasValues;

        /// <inheritdoc/>
        public IEntityPropertyData Properties { get { return propertyValues; } }
        private readonly EntityPropertyData propertyValues;

        /// <inheritdoc/>
        public IEntitySubjectAreaData SubjectAreas { get { return subjectAreaValues; } }
        private readonly EntitySubjectAreaData subjectAreaValues;

        public EntityData() : base()
        {
            aliasValues = new EntityAliasData();
            propertyValues = new EntityPropertyData();
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
            work.Add(factory.CreateLoad(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainEntityKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, dataKey));
            work.Add(factory.CreateSave(aliasValues, dataKey));
            work.Add(factory.CreateSave(propertyValues, dataKey));
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
            work.Add(factory.CreateSave(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Remove()
        {
            List<WorkItem> result = new List<WorkItem>();

            result.Add(new WorkItem()
            {
                WorkName = "Remove Entities",
                DoWork = () =>
                {
                    aliasValues.Clear();
                    propertyValues.Clear();
                    subjectAreaValues.Clear();
                    this.Clear();
                }
            });

            return result;
        }

        /// <inheritdoc/>
        public override void Remove(IDomainEntityKey entityItem)
        {
            base.Remove(entityItem);
            DomainEntityKey key = new DomainEntityKey(entityItem);
            aliasValues.Remove(key);
            propertyValues.Remove(key);
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
            subjectAreaValues.Load(source);
        }

        /// <inheritdoc/>
        /// <remarks>Entity by Catalog</remarks>
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
        /// <remarks>Entity by Table</remarks>
        public void Import(IDatabaseModel source, IPropertyData propertyDefinition, IDbTableKeyName key)
        {
            DbTableKeyName nameKey = new DbTableKeyName(key);
            foreach (IDbTableItem item in source.DbTables.Where(w => nameKey.Equals(w)))
            {
                AliasKeyName alaisKey = new AliasKeyName(item);
                DomainEntityKey entityKey;
                DomainEntityUniqueKey uniqueKey = new DomainEntityUniqueKey(item);
                AliasKeyName aliasKey = new AliasKeyName(item);

                // Create Entity or get existing
                if (aliasValues.FirstOrDefault(w => aliasKey.Equals(w)) is DomainEntityAliasItem existingAlias)
                { entityKey = new DomainEntityKey(existingAlias); }
                else if (this.FirstOrDefault(w => uniqueKey.Equals(w)) is DomainEntityItem existing)
                { entityKey = new DomainEntityKey(existing); }
                else
                {
                    TValue newItem = new TValue()
                    { EntityTitle = item.TableName, };
                    this.Add(newItem);
                    entityKey = new DomainEntityKey(newItem);
                }

                // Create Alias, if they do not exist
                if (aliasValues.Count(w => alaisKey.Equals(w) && entityKey.Equals(w)) == 0)
                {
                    aliasValues.Add(new DomainEntityAliasItem(entityKey)
                    {
                        AliasName = item.ToAliasName(),
                        Scope = item.Scope,
                    });
                }

                // Create Properties, if they do not exist
                DbExtendedPropertyKeyName propertyKey = new DbExtendedPropertyKeyName(item);
                foreach (DbExtendedPropertyItem property in source.DbExtendedProperties.Where(w => propertyKey.Equals(w)))
                {
                    PropertyKeyExtended appKey = new PropertyKeyExtended(property);

                    if (propertyDefinition.FirstOrDefault(w =>
                        appKey.Equals(w)) is Application.IPropertyValue appProperty
                        && propertyValues.Count(w =>
                            entityKey.Equals(w)
                            && new Application.PropertyIndex(appProperty).Equals(w)) == 0)
                    { propertyValues.Add(new DomainEntityPropertyItem(entityKey, appProperty, property)); }
                }
            }
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        { return this.Select(s => new NamedScopePair(s)); }
    }
}
