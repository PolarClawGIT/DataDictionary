using DataDictionary.DataLayer.ScriptingData.Schema;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ISchemaIndex : ISchemaKey
    { }

    /// <inheritdoc/>
    public class SchemaIndex : SchemaKey, ISchemaIndex
    {
        /// <inheritdoc cref="SchemaKey(ISchemaKey)"/>
        public SchemaIndex(ISchemaIndex source) : base(source) { }

        /// <summary>
        /// Convert SchemaIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(SchemaIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.SchemaId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface ISchemaIndexName : ISchemaKeyName
    { }

    /// <inheritdoc/>
    public class SchemaIndexName : SchemaKeyName, ISchemaIndexName
    {
        /// <inheritdoc cref="SchemaKeyName(ISchemaKeyName)"/>
        public SchemaIndexName(ISchemaIndexName source) : base(source) { }
    }
}
