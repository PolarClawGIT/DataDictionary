﻿using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ModelData.Entity;
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
    public interface IEntityData :
        IBindingData<DomainEntityItem>,
        ILoadData<IDomainEntityKey>, ISaveData<IDomainEntityKey>
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

        void Import(IDatabaseModel source, IDbCatalogKeyName key);

        void Import(IDatabaseModel source, IDbTableKeyName key);
    }

    class EntityData : DomainEntityCollection, IEntityData,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDataTableFile, INameScopeData<IModelKey>
    {
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
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainEntityKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

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

        public void Import(IDatabaseModel source, IDbCatalogKeyName key)
        {
            throw new NotImplementedException();
        }

        public void Import(IDatabaseModel source, IDbTableKeyName key)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<WorkItem> Export(IList<NameScopeItem> target, Func<IModelKey?> parent)
        {
            return new WorkItem()
            {
                WorkName = "Load NameScope, Entities",
                DoWork = () =>
                {
                    if (parent() is IModelKey modelKey)
                    {
                        foreach (DomainEntityItem entity in this)
                        {
                            DomainEntityKey entityKey = new DomainEntityKey(entity);
                            List<ModelEntityItem> subjects = subjectAreaValues.Where(w => entityKey.Equals(w)).ToList();

                            foreach (ModelEntityItem subject in subjects)
                            { target.Add(new NameScopeItem(subject, entity)); }

                            if (subjects.Count == 0)
                            { target.Add(new NameScopeItem(modelKey, entity)); }
                        }
                    }
                }
            }.ToList();
        }
    }
}
