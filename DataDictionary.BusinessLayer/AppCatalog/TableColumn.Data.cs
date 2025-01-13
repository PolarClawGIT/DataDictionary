using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <summary>
    /// Interface representing Catalog TableColumn data
    /// </summary>
    public interface ITableColumnData : IBindingData<TableColumnValue>
    {
        /// <summary>
        /// Finds all the Columns that are Aliased to the column provided (includes itself).
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IEnumerable<ITableColumnValue> FindAliases(ITableColumnIndexName source);
    }

    class TableColumnData : TableColumnCollection<TableColumnValue>, ITableColumnData,
        ILoadData<ICatalogKey>, ISaveData<ICatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        ICatalogModel, INamedScopeSourceData
    {
        /// <inheritdoc/>
        public required ICatalog Model { get; init; }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ICatalogKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            return INamedScopeSourceData.LoadNamedScope<TableColumnData, TableColumnValue>
                (this, addNamedScope,
                (value) => Model.DbTables.
                    FirstOrDefault(w => new TableKeyName(value).Equals(w)));
        }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove TableColumn", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Delete(ICatalogKey dataKey)
        { return new WorkItem() { WorkName = "Remove TableColumn", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        public IEnumerable<ITableColumnValue> FindAliases(ITableColumnIndexName tableColumn)
        {
            List<TableColumnIndexName> keys = new List<TableColumnIndexName>();
            TableColumnIndexName key = new TableColumnIndexName(tableColumn);

            var constraints = Model.DbConstraints.
                Where(w => w.ConstraintType is DbConstraintType.ForeignKey).
                Join(Model.DbConstraintColumns,
                constraint => new ConstraintIndexName(constraint),
                columns => new ConstraintIndexName(columns),
                (constraint, column) => new
                {
                    constraint,
                    parentKey = new ConstraintColumnIndexReferenced(column).AsColumnName(),
                    childKey = new TableColumnIndexName(column),
                }).Where(w => key.Equals(w.parentKey) || key.Equals(w.childKey)).
                ToList();

            //return this.Where(w => key.Equals(w)).Select(s => new TableColumnIndexName(s)).
            //    Union(constraints.Select(s => s.parentKey)).
            //    Union(constraints.Select(s => s.childKey)).
            //    ToList();

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
    }
}
