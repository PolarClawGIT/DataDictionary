using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
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

        /// <summary>
        /// Convert DomainIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(TableIndexName source)
        { return new DataIndexName() { Title = source.TableName ?? String.Empty }; }
    }
}
