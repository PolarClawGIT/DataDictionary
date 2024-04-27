﻿using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Interface representing Catalog Domain data
    /// </summary>
<<<<<<< HEAD
    public interface IDomainData<TValue> : IBindingData<TValue>
        where TValue: DomainValue, IDomainValue
=======
    public interface IDomainData : IBindingData<DomainValue>
>>>>>>> RenameIndexValue
    { }

    class DomainData : DbDomainCollection<DomainValue>, IDomainData,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseModelItem, IGetNamedScopes
<<<<<<< HEAD
        where TValue: DomainValue, new()
=======
>>>>>>> RenameIndexValue
    {
        /// <inheritdoc/>
        public required IDatabaseModel Database { get; init; }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            List<NamedScopePair> result = new List<NamedScopePair>();
<<<<<<< HEAD

            foreach (TValue item in this)
            {
                SchemaIndexName keyName = new SchemaIndexName(item);

                if (Database.DbSchemta.FirstOrDefault(w => keyName.Equals(w)) is SchemaValue schema)
                { result.Add(new NamedScopePair(schema.GetSystemId(), item)); }
            }

=======
            foreach (DomainValue item in this)
            {
                DbSchemaKeyName nameKey = new DbSchemaKeyName(item);
                if (Database.DbSchemta.FirstOrDefault(w => nameKey.Equals(w)) is SchemaValue parent)
                { result.Add(new NamedScopePair(parent.GetSystemId(), item)); }
            }

>>>>>>> RenameIndexValue
            return result;
        }

    }
}
