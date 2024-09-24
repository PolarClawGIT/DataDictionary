using DataDictionary.BusinessLayer.ToolSet;
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
        /// Convert SchemaIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(SchemaIndex source)
        { return new DataIndex() { SystemId = source.SchemaId ?? Guid.Empty }; }
    }


}
