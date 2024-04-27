using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Interface representing Catalog Schema data
    /// </summary>
<<<<<<< HEAD
    public interface ISchemaData<TValue> : IBindingData<TValue>
        where TValue : SchemaValue, ISchemaValue
=======
    public interface ISchemaData : IBindingData<SchemaValue>
>>>>>>> RenameIndexValue
    { }

    class SchemaData : DbSchemaCollection<SchemaValue>, ISchemaData,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseModelItem, IGetNamedScopes
<<<<<<< HEAD
        where TValue : SchemaValue, ISchemaValue, new()
=======
>>>>>>> RenameIndexValue
    {
        /// <inheritdoc/>
        public required IDatabaseModel Database { get; init; }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            List<NamedScopePair> result = new List<NamedScopePair>();
<<<<<<< HEAD

            foreach (TValue item in this.Where(w => w.IsSystem == false))
            {
                CatalogIndexName keyName = new CatalogIndexName(item);

                if (Database.DbCatalogs.FirstOrDefault(w => keyName.Equals(w)) is CatalogValue catalog)
                { result.Add(new NamedScopePair(catalog.GetSystemId(), item)); }
            }

=======
            foreach (SchemaValue item in this.Where(w => w.IsSystem == false))
            {
                DbCatalogKeyName nameKey = new DbCatalogKeyName(item);
                if (Database.DbCatalogs.FirstOrDefault(w => nameKey.Equals(w)) is CatalogValue parent)
                { result.Add(new NamedScopePair(parent.GetSystemId(), item)); }
            }

>>>>>>> RenameIndexValue
            return result;
        }
    }
}
