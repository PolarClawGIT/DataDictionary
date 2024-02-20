using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.CatalogData
{
    /// <summary>
    /// Interface representing Catalog Schema data
    /// </summary>
    public interface ISchemaData : IBindingData<DbSchemaItem>
    {

    }

    class SchemaData : DbSchemaCollection, ISchemaData,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
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

        public IReadOnlyList<WorkItem> Load(IDbCatalogItem parent, NameScopeDictionary target)
        {
            List<WorkItem> result = new List<WorkItem>();
            DbCatalogKeyName nameKey = new DbCatalogKeyName(parent);
            DbCatalogKey key = new DbCatalogKey(parent);

            foreach (DbSchemaItem item in this.Where(w => nameKey.Equals(w)))
            {
                result.Add(new WorkItem()
                {
                    WorkName = "Building Name Tree",
                    DoWork = () => { target.Add(new NameScopeItem(key, item)); }
                });
                //result.AddRange(Catalog.DbTables.Load(item, target));
                //result.AddRange(Catalog.DbConstraints.Load(item, target));
                //result.AddRange(Catalog.DbDomains.Load(item, target));
                //result.AddRange(Catalog.DbRoutines.Load(item, target));
            }

            return result;
        }
    }
}
