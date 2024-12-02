using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface ITableIndex : ITableKey
    { }

    /// <inheritdoc/>
    public class TableIndex : TableKey, ITableIndex,
        IKeyEquality<ITableIndex>, IKeyEquality<TableIndex>
    {
        /// <inheritdoc cref="TableKey(ITableKey)"/>
        public TableIndex(ITableKey source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITableIndex? other)
        { return other is ITableKey value && Equals(new TableKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(TableIndex? other)
        { return other is ITableKey value && Equals(new TableKey(value)); }

        /// <summary>
        /// Convert TableIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(TableIndex source)
        { return new DataIndex() { SystemId = source.TableId ?? Guid.Empty }; }
    }


}
