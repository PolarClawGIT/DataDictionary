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

        /// <summary>
        /// Convert TableIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(TableIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.TableId ?? Guid.Empty }; }
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
