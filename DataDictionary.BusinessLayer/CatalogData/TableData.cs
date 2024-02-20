using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.CatalogData
{
    /// <summary>
    /// Interface representing Catalog Table data
    /// </summary>
    public interface ITableData: IBindingData<DbTableItem>
    {
        internal IReadOnlyList<WorkItem> Load(IDbSchemaItem parent, NameScopeDictionary target);
    }

    class TableData : DbTableCollection, ITableData,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        public IReadOnlyList<WorkItem> Load(IDbSchemaItem parent, NameScopeDictionary target)
        {
            List<WorkItem> result = new List<WorkItem>();
            DbSchemaKeyName nameKey = new DbSchemaKeyName(parent);
            DbSchemaKey key = new DbSchemaKey(parent);

            foreach (DbTableItem item in this.Where(w => nameKey.Equals(w)))
            {
                result.Add(new WorkItem()
                {
                    WorkName = "Building Name Tree",
                    DoWork = () => { target.Add(new NameScopeItem(key, item)); }
                });
                //result.AddRange(Catalog.DbTableColumns.Load(item, target));
            }

            return result;
        }

    }
}
