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
    public class TableColumnIndexName : DbTableColumnKeyName, ITableColumnIndexName,
        IKeyEquality<ITableColumnIndexName>, IKeyEquality<TableColumnIndexName>
    {
        /// <inheritdoc cref="DbTableColumnKeyName(IDbTableColumnKeyName)"/>
        public TableColumnIndexName(ITableColumnIndexName source) : base(source) { }

        /// <inheritdoc cref="DbTableColumnKeyName(IDbTableColumnKeyName)"/>
        public TableColumnIndexName(IDbTableColumnKeyName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ITableColumnIndexName? other)
        { return other is IDbTableColumnKeyName value && Equals(new DbTableColumnKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(TableColumnIndexName? other)
        { return other is IDbTableColumnKeyName value && Equals(new DbTableColumnKeyName(value)); }
    }
}
