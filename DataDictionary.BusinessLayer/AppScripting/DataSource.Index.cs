using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface IDataSourceIndex : IDataSourceKey
    { }

    /// <inheritdoc/>
    public class DataSourceIndex : DataSourceKey, IDataSourceIndex,
        IKeyEquality<IDataSourceIndex>, IKeyEquality<DataSourceIndex>
    {
        /// <inheritdoc cref="DataSourceKey()"/>
        public DataSourceIndex() : base()
        { }

        /// <inheritdoc cref="DataSourceKey(IDataSourceKey)"/>
        public DataSourceIndex(IDataSourceIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(IDataSourceIndex? other)
        { return other is IDataSourceKey key && Equals(new DataSourceKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(DataSourceIndex? other)
        { return other is IDataSourceKey key && Equals(new DataSourceKey(key)); }

        /// <summary>
        /// Convert TemplateIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(DataSourceIndex source)
        { return new DataIndex() { SystemId = source.DataSourceId ?? Guid.Empty }; }
    }
}
