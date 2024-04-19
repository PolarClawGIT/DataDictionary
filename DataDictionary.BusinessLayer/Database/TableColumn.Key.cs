using DataDictionary.DataLayer.DatabaseData.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ITableColumnKey : IDbTableColumnKey
    { }

    /// <inheritdoc/>
    public class TableColumnKey : DbTableColumnKey, ITableColumnKey
    {
        /// <inheritdoc cref="DbTableColumnKey(IDbTableColumnKey)"/>
        public TableColumnKey(IDbTableColumnKey source) : base(source)
        { }
    }
}
