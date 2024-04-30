using DataDictionary.DataLayer.DatabaseData.Schema;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ISchemaIndex : IDbSchemaKey
    { }

    /// <inheritdoc/>
    public class SchemaIndex : DbSchemaKey, ISchemaIndex
    {
        /// <inheritdoc cref="DbSchemaKey(IDbSchemaKey)"/>
        public SchemaIndex(IDbSchemaKey source) : base(source)
        { }
    }

    /// <inheritdoc/>
    public interface ISchemaIndexName : IDbSchemaKeyName, ICatalogIndexName
    { }

    /// <inheritdoc/>
    public class SchemaIndexName : DbSchemaKeyName, ISchemaIndexName
    {
        /// <inheritdoc cref="DbSchemaKeyName(IDbSchemaKeyName)"/>
        public SchemaIndexName(ISchemaIndexName source) : base(source) { }
    }
}
