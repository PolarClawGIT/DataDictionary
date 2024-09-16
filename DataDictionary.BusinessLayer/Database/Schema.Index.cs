using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ISchemaIndex : IDbSchemaKey
    { }

    /// <inheritdoc/>
    public class SchemaIndex : DbSchemaKey, ISchemaIndex,
        IKeyEquality<ISchemaIndex>, IKeyEquality<SchemaIndex>
    {
        /// <inheritdoc cref="DbSchemaKey(IDbSchemaKey)"/>
        public SchemaIndex(IDbSchemaKey source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaIndex? other)
        { return other is IDbSchemaKey value && Equals(new DbSchemaKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(SchemaIndex? other)
        { return other is IDbSchemaKey value && Equals(new DbSchemaKey(value)); }

        /// <summary>
        /// Convert SchemaIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(SchemaIndex source)
        { return new DataLayerIndex() { DataLayerId = source.SchemaId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface ISchemaIndexName : IDbSchemaKeyName, ICatalogIndexName
    { }

    /// <inheritdoc/>
    public class SchemaIndexName : DbSchemaKeyName, ISchemaIndexName,
        IKeyEquality<ISchemaIndexName>, IKeyEquality<SchemaIndexName>
    {
        /// <inheritdoc cref="DbSchemaKeyName(IDbSchemaKeyName)"/>
        public SchemaIndexName(ISchemaIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaIndexName? other)
        { return other is IDbSchemaKeyName value && Equals(new DbSchemaKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(SchemaIndexName? other)
        { return other is IDbSchemaKeyName value && Equals(new DbSchemaKeyName(value)); }
    }
}
