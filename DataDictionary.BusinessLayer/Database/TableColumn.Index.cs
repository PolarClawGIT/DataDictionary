using DataDictionary.DataLayer.DatabaseData.Table;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ITableColumnIndex : IDbTableColumnKey
    { }

    /// <inheritdoc/>
    public class TableColumnIndex : DbTableColumnKey, ITableColumnIndex
    {
        /// <inheritdoc cref="DbTableColumnKey(IDbTableColumnKey)"/>
        public TableColumnIndex(IDbTableColumnKey source) : base(source)
        { }

        /// <summary>
        /// Convert TableColumnIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(TableColumnIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.ColumnId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface ITableColumnIndexName : IDbTableColumnKeyName, ITableIndexName
    { }

    /// <inheritdoc/>
    public class TableColumnIndexName : DbTableColumnKeyName, ITableColumnIndexName
    {
        /// <inheritdoc cref="DbTableColumnKeyName(IDbTableColumnKeyName)"/>
        public TableColumnIndexName(ITableColumnIndexName source) : base(source) { }
    }
}
