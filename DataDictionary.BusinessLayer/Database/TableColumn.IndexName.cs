using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ITableColumnIndexName : ITableColumnKeyName, ITableIndexName
    { }

    /// <inheritdoc/>
    public class TableColumnIndexName : TableColumnKeyName, ITableColumnIndexName,
        IKeyEquality<ITableColumnIndexName>, IKeyEquality<TableColumnIndexName>
    {
        /// <inheritdoc cref="TableColumnKeyName(ITableColumnKeyName)"/>
        public TableColumnIndexName(ITableColumnIndexName source) : base(source) { }

        /// <inheritdoc cref="TableColumnKeyName(ITableColumnKeyName)"/>
        public TableColumnIndexName(ITableColumnKeyName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ITableColumnIndexName? other)
        { return other is ITableColumnKeyName value && Equals(new TableColumnKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(TableColumnIndexName? other)
        { return other is ITableColumnKeyName value && Equals(new TableColumnKeyName(value)); }

        /// <summary>
        /// Convert TableColumnIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(TableColumnIndexName source)
        { return new DataIndexName() { Title = source.ColumnName ?? String.Empty }; }
    }
}
