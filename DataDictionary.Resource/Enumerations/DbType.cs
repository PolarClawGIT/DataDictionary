using System.Data;

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
