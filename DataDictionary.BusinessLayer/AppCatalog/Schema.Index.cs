using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface ISchemaIndex : ISchemaKey
    { }

    /// <inheritdoc/>
    public class SchemaIndex : SchemaKey, ISchemaIndex,
        IKeyEquality<ISchemaIndex>, IKeyEquality<SchemaIndex>
    {
        /// <inheritdoc cref="SchemaKey(ISchemaKey)"/>
        public SchemaIndex(ISchemaKey source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaIndex? other)
        { return other is ISchemaKey value && Equals(new SchemaKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(SchemaIndex? other)
        { return other is ISchemaKey value && Equals(new SchemaKey(value)); }

        /// <summary>
        /// Convert SchemaIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(SchemaIndex source)
        { return new DataIndex() { SystemId = source.SchemaId ?? Guid.Empty }; }
    }


}
