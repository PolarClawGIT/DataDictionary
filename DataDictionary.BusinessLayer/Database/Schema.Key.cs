using DataDictionary.DataLayer.DatabaseData.Catalog;
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

    /// <inheritdoc/>
    public interface ISchemaKeyName : IDbSchemaKeyName, IDbCatalogKeyName, ICatalogKeyName
    { }

    /// <inheritdoc/>
    public class SchemaKeyName : DbSchemaKeyName, ISchemaKeyName
    {
        /// <inheritdoc cref="DbSchemaKeyName(IDbSchemaKeyName)"/>
        public SchemaKeyName(ISchemaKeyName source) : base(source) { }
    }
}
