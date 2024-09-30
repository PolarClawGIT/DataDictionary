using DataDictionary.BusinessLayer.ToolSet;
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
        /// Convert TableIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(TableIndex source)
        { return new DataIndex() { SystemId = source.TableId ?? Guid.Empty }; }
    }


}
