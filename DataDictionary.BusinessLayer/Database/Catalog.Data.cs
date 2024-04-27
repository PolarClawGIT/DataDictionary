using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Wrapper of Catalog Data (The Database)
    /// </summary>
<<<<<<< HEAD
    public interface ICatalogData<TValue> :
        IBindingData<TValue>
        where TValue : CatalogValue, ICatalogValue
=======
    public interface ICatalogData : IBindingData<CatalogValue>
>>>>>>> RenameIndexValue
    { }

    class CatalogData : DbCatalogCollection<CatalogValue>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
<<<<<<< HEAD
        IDatabaseModelItem, ICatalogData<TValue>,
        IGetNamedScopes
        where TValue : CatalogValue, ICatalogValue, new()
=======
        IDatabaseModelItem, ICatalogData, IGetNamedScopes
>>>>>>> RenameIndexValue
    {
        /// <inheritdoc/>
        public required IDatabaseModel Database { get; init; }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        { return this.Where(w => w.IsSystem == false).Select(s => new NamedScopePair(s)); }
    }
}
