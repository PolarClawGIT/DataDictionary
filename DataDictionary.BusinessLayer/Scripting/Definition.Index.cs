using DataDictionary.DataLayer.ScriptingData.Schema;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface IDefinitionIndex : ISchemaKey
    { }

    /// <inheritdoc/>
    public class DefinitionIndex : SchemaKey, IDefinitionIndex
    {
        /// <inheritdoc cref="SchemaKey(ISchemaKey)"/>
        public DefinitionIndex(IDefinitionIndex source) : base(source) { }

        /// <summary>
        /// Convert DefinitionIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(DefinitionIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.SchemaId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface IDefinitionIndexName : ISchemaKeyName
    { }

    /// <inheritdoc/>
    public class DefinitionIndexName : SchemaKeyName, IDefinitionIndexName
    {
        /// <inheritdoc cref="SchemaKeyName(ISchemaKeyName)"/>
        public DefinitionIndexName(IDefinitionIndexName source) : base(source) { }
    }
}
