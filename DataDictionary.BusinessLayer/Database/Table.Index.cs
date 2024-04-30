using DataDictionary.DataLayer.DatabaseData.Table;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ITableIndex : IDbTableKey
    { }

    /// <inheritdoc/>
    public class TableIndex : DbTableKey, ITableIndex
    {
        /// <inheritdoc cref="DbTableKey(IDbTableKey)"/>
        public TableIndex(IDbTableKey source) : base(source)
        { }
    }

    /// <inheritdoc/>
    public interface ITableIndexName : IDbTableKeyName, ISchemaIndexName
    { }

    /// <inheritdoc/>
    public class TableIndexName : DbTableKeyName, ITableIndexName
    {
        /// <inheritdoc cref="DbTableKeyName(IDbTableKeyName)"/>
        public TableIndexName(ITableIndexName source) : base(source) { }
    }
}
