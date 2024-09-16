using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ITableIndex : IDbTableKey
    { }

    /// <inheritdoc/>
    public class TableIndex : DbTableKey, ITableIndex,
        IKeyEquality<ITableIndex>, IKeyEquality<TableIndex>
    {
        /// <inheritdoc cref="DbTableKey(IDbTableKey)"/>
        public TableIndex(IDbTableKey source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITableIndex? other)
        { return other is IDbTableKey value && Equals(new DbTableKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(TableIndex? other)
        { return other is IDbTableKey value && Equals(new DbTableKey(value)); }

        /// <summary>
        /// Convert TableIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(TableIndex source)
        { return new DataLayerIndex() { DataLayerId = source.TableId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface ITableIndexName : IDbTableKeyName, ISchemaIndexName
    { }

    /// <inheritdoc/>
    public class TableIndexName : DbTableKeyName, ITableIndexName,
        IKeyEquality<ITableIndexName>, IKeyEquality<TableIndexName>
    {
        /// <inheritdoc cref="DbTableKeyName(IDbTableKeyName)"/>
        public TableIndexName(ITableIndexName source) : base(source) { }

        /// <inheritdoc cref="DbTableKeyName(IDbTableKeyName)"/>
        public TableIndexName(IDbTableKeyName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ITableIndexName? other)
        { return other is IDbTableKeyName value && Equals(new DbTableKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(TableIndexName? other)
        { return other is IDbTableKeyName value && Equals(new DbTableKeyName(value)); }
    }
}
