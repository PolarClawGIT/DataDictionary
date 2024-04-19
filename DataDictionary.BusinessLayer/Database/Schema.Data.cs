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
    public interface ISchemaData<TValue> : IBindingData<TValue>
        where TValue : SchemaValue
    { }

    class SchemaData<TValue> : DbSchemaCollection<TValue>, ISchemaData<TValue>,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseModelItem, INamedScopeData
        where TValue : SchemaValue, new()
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
        public IReadOnlyList<WorkItem> Build(INamedScopeDictionary target)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Build NamedScope Schema",
                DoWork = () =>
                {
                    foreach (DbSchemaItem item in this.Where(w => w.IsSystem == false))
                    {
                        //target.Remove(new NamedScopeKey(item)); Done by Catalog

                        DbCatalogKeyName nameKey = new DbCatalogKeyName(item);
                        if (Database.DbCatalogs.FirstOrDefault(w => nameKey.Equals(w)) is IDbCatalogItem parent)
                        { target.Add(new NamedScopeItem(parent, item)); }
                    }
                }
            });

            return work;
        }
    }
}
