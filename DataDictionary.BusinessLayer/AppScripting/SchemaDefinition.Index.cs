using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ISchemaDefinitionIndex : ISchemaDefinitionKey
    { }

    /// <summary>
    /// Composite Index for the Template/Schema
    /// </summary>
    public interface ISchemaComposite : ITemplateIndex, ISchemaDefinitionIndex
    { }

    /// <inheritdoc/>
    public class SchemaDefinitionIndex : SchemaDefinitionKey, ISchemaDefinitionIndex,
        IKeyEquality<ISchemaDefinitionIndex>, IKeyEquality<SchemaDefinitionIndex>
    {
        /// <inheritdoc cref="SchemaDefinitionKey()"/>
        public SchemaDefinitionIndex() : base() { }

        /// <inheritdoc cref="SchemaDefinitionKey(ISchemaDefinitionKey)"/>
        public SchemaDefinitionIndex(ISchemaDefinitionIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaDefinitionIndex? other)
        { return other is ISchemaDefinitionKey key && Equals(new SchemaDefinitionKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(SchemaDefinitionIndex? other)
        { return other is ISchemaDefinitionKey key && Equals(new SchemaDefinitionKey(key)); }

        /// <summary>
        /// Convert SchemaDefinitionIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(SchemaDefinitionIndex source)
        { return new DataIndex() { SystemId = source.SchemaId ?? Guid.Empty }; }
    }
}
