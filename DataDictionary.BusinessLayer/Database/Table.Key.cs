using DataDictionary.DataLayer.DatabaseData.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ITableKey : IDbTableKey
    { }

    /// <inheritdoc/>
    public class TableKey : DbTableKey, ITableKey
    {
        /// <inheritdoc cref="DbTableKey(IDbTableKey)"/>
        public TableKey(IDbTableKey source) : base(source)
        { }
    }

    /// <inheritdoc/>
    public interface ITableKeyName : IDbTableKeyName
    { }

    /// <inheritdoc/>
    public class TableKeyName : DbTableKeyName, ITableKeyName
    {
        /// <inheritdoc cref="DbTableKeyName(IDbTableKeyName)"/>
        public TableKeyName(ITableKeyName source) : base(source) { }
    }
}
