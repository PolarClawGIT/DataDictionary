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

        /// <summary>
        /// Convert SchemaIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(SchemaIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.SchemaId ?? Guid.Empty }; }
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
