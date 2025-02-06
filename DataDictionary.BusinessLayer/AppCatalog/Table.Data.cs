// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.DbWorkItem;
using Toolbox.Threading;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.AppModel;
using Toolbox.BindingTable;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;

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
        ILoadData<ICatalogIndex>, ISaveData<ICatalogIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        ICatalogModel, ITableData, INamedScopeSourceData
    {
        /// <inheritdoc/>
        public required ICatalog Model { get; init; }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogIndex dataKey)
        { return factory.CreateLoad(this, (ICatalogKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (ICatalogKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ICatalogIndex dataKey)
        { return factory.CreateSave(this, (ICatalogKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
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
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Table", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Table</remarks>
        public IReadOnlyList<WorkItem> Delete(ICatalogIndex dataKey)
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
