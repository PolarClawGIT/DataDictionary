using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface ITableColumnIndex : ITableColumnKey
    { }

    /// <inheritdoc/>
    public class TableColumnIndex : TableColumnKey, ITableColumnIndex,
        IKeyEquality<ITableColumnIndex>, IKeyEquality<TableColumnIndex>
    {
        /// <inheritdoc cref="TableColumnKey(ITableColumnKey)"/>
        public TableColumnIndex(ITableColumnKey source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITableColumnIndex? other)
        { return other is ITableColumnKey value && Equals(new TableColumnKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(TableColumnIndex? other)
        { return other is ITableColumnKey value && Equals(new TableColumnKey(value)); }

        /// <summary>
        /// Convert TableColumnIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(TableColumnIndex source)
        { return new DataIndex() { SystemId = source.ColumnId ?? Guid.Empty }; }
    }
}
