using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database
{
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

        /// <summary>
        /// Convert TableColumnIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(TableColumnIndexName source)
        { return new DataIndexName() { Title = source.ColumnName ?? String.Empty }; }
    }
}
