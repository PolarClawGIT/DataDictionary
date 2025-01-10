using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Interface for DbType
    /// </summary>
    public interface IDbType
    {
        /// <summary>
        /// DataType translated to System.Data.DbType
        /// </summary>
        DbType? DatabaseType { get; }
    }
}
