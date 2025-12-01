// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <summary>
    /// Interface representing Catalog sourceColumn data
    /// </summary>
    public interface ITableColumnData : IBindingData<TableColumnValue>
    {
        /// <summary>
        /// Finds all the Columns that are Aliased to the column provided (includes itself).
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<ITableColumnValue> GetAlias(ITableColumnIndexName source);
    }

    class TableColumnData : TableColumnCollection<TableColumnValue>, ITableColumnData,
        ILoadData<ICatalogIndex>, ISaveData<ICatalogIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        ICatalogReference
    {
        /// <inheritdoc/>
        public required ICatalog Catalog { get; init; }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogIndex dataKey)
        { return factory.CreateLoad(this, (ICatalogKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (ICatalogKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ICatalogIndex dataKey)
        { return factory.CreateSave(this, (ICatalogKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove TableColumn", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Delete(ICatalogIndex dataKey)
        { return new WorkItem() { WorkName = "Remove TableColumn", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IEnumerable<ITableColumnValue> GetAlias(ITableColumnIndexName tableColumn)
        {
            List<TableColumnIndexName> keys = new List<TableColumnIndexName>();
            TableColumnIndexName key = new TableColumnIndexName(tableColumn);

            var constraints = Catalog.DbConstraints.
                Where(w => w.ConstraintType is DbConstraintType.ForeignKey).
                Join(Catalog.DbConstraintColumns,
                constraint => new ConstraintIndexName(constraint),
                columns => new ConstraintIndexName(columns),
                (constraint, column) => new
                {
                    constraint,
                    parentKey = new ConstraintColumnIndexReferenced(column).AsColumnName(),
                    childKey = new TableColumnIndexName(column),
                }).Where(w => key.Equals(w.parentKey) || key.Equals(w.childKey)).
                ToList();

            keys.AddRange(
                this.Where(w => key.Equals(w)).Select(s => new TableColumnIndexName(s)).
                Union(constraints.Select(s => s.parentKey)).
                Union(constraints.Select(s => s.childKey))
                );

            return this.Join(keys,
                column => new TableColumnIndexName(column),
                key => key,
                (column, key) => column
                ).ToList();
        }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public void Remove(ICatalogIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }
    }
}
