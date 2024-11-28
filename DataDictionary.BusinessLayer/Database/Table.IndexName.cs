using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ITableIndexName : ITableKeyName, ISchemaIndexName
    { }

    /// <inheritdoc/>
    public class TableIndexName : TableKeyName, ITableIndexName,
        IKeyEquality<ITableIndexName>, IKeyEquality<TableIndexName>
    {
        /// <inheritdoc cref="TableKeyName(ITableKeyName)"/>
        public TableIndexName(ITableIndexName source) : base(source) { }

        /// <inheritdoc cref="TableKeyName(ITableKeyName)"/>
        public TableIndexName(ITableKeyName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ITableIndexName? other)
        { return other is ITableKeyName value && Equals(new TableKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(TableIndexName? other)
        { return other is ITableKeyName value && Equals(new TableKeyName(value)); }

        /// <summary>
        /// Convert DomainIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(TableIndexName source)
        { return new DataIndexName() { Title = source.TableName ?? String.Empty }; }
    }
}
