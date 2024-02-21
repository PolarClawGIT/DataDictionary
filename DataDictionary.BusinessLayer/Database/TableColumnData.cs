using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Interface representing Catalog TableColumn data
    /// </summary>
    public interface ITableColumnData : IBindingData<DbTableColumnItem>
    {
        internal IReadOnlyList<WorkItem> Load(IDbTableItem parent, NameScopeDictionary target);
    }

    class TableColumnData : DbTableColumnCollection, ITableColumnData,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        public IReadOnlyList<WorkItem> Load(IDbTableItem parent, NameScopeDictionary target)
        {
            List<WorkItem> result = new List<WorkItem>();
            DbTableKeyName nameKey = new DbTableKeyName(parent);
            DbTableKey key = new DbTableKey(parent);

            result.Add(new WorkItem()
            {
                WorkName = "Building Name Tree",
                DoWork = () =>
                {
                    foreach (DbTableColumnItem item in this.Where(w => nameKey.Equals(w)))
                    { target.Add(new NameScopeItem(key, item)); }
                }
            });

            return result;
        }
    }
}
