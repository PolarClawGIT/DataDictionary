using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Interface representing Catalog Table data
    /// </summary>
    public interface ITableData: IBindingData<DbTableItem>
    { }

    class TableData : DbTableCollection, ITableData,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseModelItem, INameScopeData
    {
        /// <inheritdoc/>
        public required IDatabaseModel Database { get; init; }

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

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Export(IList<NameScopeItem> target)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Load NameScope, Table",
                DoWork = () =>
                {
                    foreach (DbTableItem item in this.Where(w => w.IsSystem == false))
                    {
                        DbSchemaKeyName nameKey = new DbSchemaKeyName(item);
                        if (Database.DbSchemta.FirstOrDefault(w => nameKey.Equals(w)) is IDbSchemaItem parent)
                        { target.Add(new NameScopeItem(parent, item)); }
                    }
                }
            });

            return work;
        }

    }
}
