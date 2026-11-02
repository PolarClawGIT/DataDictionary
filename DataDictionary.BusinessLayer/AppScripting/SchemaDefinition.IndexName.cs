using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ISchemaDefinitionIndexName : ISchemaDefinitionKeyName
    { }

    /// <inheritdoc/>
    public class SchemaDefinitionIndexName : SchemaDefinitionKeyName, ISchemaDefinitionIndexName,
        IKeyEquality<ISchemaDefinitionIndexName>, IKeyEquality<SchemaDefinitionIndexName>
    {
        /// <inheritdoc cref="SchemaDefinitionKeyName(ISchemaDefinitionKeyName)"/>
        public SchemaDefinitionIndexName(ISchemaDefinitionIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaDefinitionIndexName? other)
        { return other is ISchemaDefinitionKeyName key && Equals(new SchemaDefinitionKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(SchemaDefinitionIndexName? other)
        { return other is ISchemaDefinitionKeyName key && Equals(new SchemaDefinitionKeyName(key)); }

        /// <summary>
        /// Convert SchemaDefinitionIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(SchemaDefinitionIndexName source)
        { return new DataIndexName() { Title = source.SchemaTitle ?? String.Empty }; }
    }
}
