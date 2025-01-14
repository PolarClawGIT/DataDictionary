using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.DbWorkItem;
using Toolbox.Threading;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.AppModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <summary>
    /// Interface representing Catalog Table data
    /// </summary>
    public interface ITableData : IBindingData<TableValue>
    {
        /// <summary>
        /// Finds all the Columns that are Aliased to the column provided (includes itself).
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<ITableValue> GetAlias(ITableIndexName source);

        /// <summary>
        /// Finds all the Columns for the Table specified.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        BindingView<TableColumnValue> GetColumns(ITableIndexName table);
    }

    class TableData : TableCollection<TableValue>,
        ILoadData<ICatalogKey>, ISaveData<ICatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        ICatalogModel, ITableData, INamedScopeSourceData
    {
        /// <inheritdoc/>
        public required ICatalog Model { get; init; }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ICatalogKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            return INamedScopeSourceData.LoadNamedScope<TableData, TableValue>
                (this, addNamedScope,
                (value) => Model.DbSchemta.
                    FirstOrDefault(w => new SchemaKeyName(value).Equals(w)));
        }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Table", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Delete(ICatalogKey dataKey)
        { return new WorkItem() { WorkName = "Remove Table", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        public IEnumerable<ITableValue> GetAlias(ITableIndexName table)
        {
            List<TableIndexName> keys = new List<TableIndexName>();
            TableIndexName key = new TableIndexName(table);

            keys.AddRange(
                this.Where(w => key.Equals(w)).Select(s => new TableIndexName(s))
                );

            return this.Join(keys,
                table => new TableIndexName(table),
                key => key,
                (table, key) => table
                ).ToList();
        }

        /// <inheritdoc/>
        public BindingView<TableColumnValue> GetColumns(ITableIndexName table)
        {
            TableIndexName key = new TableIndexName(table);
            return new BindingView<TableColumnValue>(Model.DbTableColumns, w => key.Equals(w));
        }
    }
}
