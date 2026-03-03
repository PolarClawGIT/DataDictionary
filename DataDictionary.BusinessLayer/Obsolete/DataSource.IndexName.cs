using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.Obsolete;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Obsolete
{
    /// <inheritdoc/>
    [Obsolete]
    public interface IDataSourceIndexName : IDataSourceKeyName
    { }

    /// <inheritdoc/>
    [Obsolete]
    public class DataSourceIndexName : DataSourceKeyName, IDataSourceIndexName,
        IKeyEquality<IDataSourceIndexName>, IKeyEquality<DataSourceIndexName>
    {
        /// <inheritdoc cref="DataSourceKeyName(IDataSourceKeyName)"/>
        public DataSourceIndexName(IDataSourceIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IDataSourceIndexName? other)
        { return other is IDataSourceKeyName value && Equals(new DataSourceKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(DataSourceIndexName? other)
        { return other is IDataSourceKeyName value && Equals(new DataSourceKeyName(value)); }

        /// <summary>
        /// Convert DataSourceIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(DataSourceIndexName source)
        { return new DataIndexName() { Title = source.DataSourceTitle ?? String.Empty }; }
    }
}
