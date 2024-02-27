using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData;
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
        IEntityAliasData DomainEntityAliases { get; }

        /// <summary>
        /// List of Domain Properties for the Entities within the Model.
        /// </summary>
        IEntityPropertyData DomainEntityProperties { get; }

        void Import(IDatabaseModel source, IDbCatalogKeyName key);

        void Import(IDatabaseModel source, IDbTableKeyName key);
    }

    class EntityData: DomainEntityCollection, IEntityData,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDataTableFile
    {
        /// <inheritdoc/>
        public IEntityAliasData DomainEntityAliases { get { return entityAlias; } }
        private readonly EntityAliasData entityAlias;

        /// <inheritdoc/>
        public IEntityPropertyData DomainEntityProperties { get { return entityProperty; } }
        private readonly EntityPropertyData entityProperty;

        public EntityData() : base ()
        {
            entityAlias = new EntityAliasData();
            entityProperty = new EntityPropertyData();
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
                    entityAlias.Clear();
                    entityProperty.Clear();
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
            entityAlias.Remove(key);
            entityProperty.Remove(key);
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(this.ToDataTable());
            result.Add(entityAlias.ToDataTable());
            result.Add(entityProperty.ToDataTable());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public void Import(System.Data.DataSet source)
        {
            this.Load(source);
            entityAlias.Load(source);
            entityProperty.Load(source);
        }

        public void Import(IDatabaseModel source, IDbCatalogKeyName key)
        {
            throw new NotImplementedException();
        }

        public void Import(IDatabaseModel source, IDbTableKeyName key)
        {
            throw new NotImplementedException();
        }
    }
}
