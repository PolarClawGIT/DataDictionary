using DataDictionary.DataLayer.DatabaseData.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ISchemaKey : IDbSchemaKey
    { }

    /// <inheritdoc/>
    public class SchemaKey : DbSchemaKey, ISchemaKey
    {
        /// <inheritdoc cref="DbSchemaKey(IDbSchemaKey)"/>
        public SchemaKey(IDbSchemaKey source) : base(source)
        { }
    }
}
