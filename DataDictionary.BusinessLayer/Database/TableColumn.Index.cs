using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ITableColumnIndex : IDbTableColumnKey
    { }

    /// <inheritdoc/>
    public class TableColumnIndex : DbTableColumnKey, ITableColumnIndex,
        IKeyEquality<ITableColumnIndex>, IKeyEquality<TableColumnIndex>
    {
        /// <inheritdoc cref="DbTableColumnKey(IDbTableColumnKey)"/>
        public TableColumnIndex(IDbTableColumnKey source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITableColumnIndex? other)
        { return other is IDbTableColumnKey value && Equals(new DbTableColumnKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(TableColumnIndex? other)
        { return other is IDbTableColumnKey value && Equals(new DbTableColumnKey(value)); }

        /// <summary>
        /// Convert TableColumnIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(TableColumnIndex source)
        { return new DataIndex() { SystemId = source.ColumnId ?? Guid.Empty }; }
    }
}
