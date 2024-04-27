using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Interface representing Catalog TableColumn data
    /// </summary>
<<<<<<< HEAD
    public interface ITableColumnData<TValue> : IBindingData<TValue>
        where TValue : TableColumnValue, ITableColumnValue
=======
    public interface ITableColumnData: IBindingData<TableColumnValue>
>>>>>>> RenameIndexValue
    { }

    class TableColumnData : DbTableColumnCollection<TableColumnValue>, ITableColumnData,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseModelItem, IGetNamedScopes
<<<<<<< HEAD
        where TValue : TableColumnValue, new()
=======
>>>>>>> RenameIndexValue
    {
        /// <inheritdoc/>
        public required IDatabaseModel Database { get; init; }

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

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            List<NamedScopePair> result = new List<NamedScopePair>();
<<<<<<< HEAD

            foreach (TValue item in this)
            {
                TableIndexName keyName = new TableIndexName(item);

                if (Database.DbTables.FirstOrDefault(w => keyName.Equals(w)) is TableValue table)
                { result.Add(new NamedScopePair(table.GetSystemId(), item)); }
            }

=======
            foreach (TableColumnValue item in this)
            {
                DbTableKeyName nameKey = new DbTableKeyName(item);
                if (Database.DbTables.FirstOrDefault(w => nameKey.Equals(w)) is TableValue parent)
                { result.Add(new NamedScopePair(parent.GetSystemId(), item)); }
            }

>>>>>>> RenameIndexValue
            return result;
        }
    }
}
