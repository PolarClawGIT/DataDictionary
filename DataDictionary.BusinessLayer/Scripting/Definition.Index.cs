using DataDictionary.DataLayer.ScriptingData.Schema;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    [Obsolete("To be removed", true)]
    public interface IDefinitionIndex : ISchemaKey
    { }

    /// <inheritdoc/>
    [Obsolete("To be removed", true)]
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
    [Obsolete("To be removed", true)]
    public interface IDefinitionIndexName : ISchemaKeyName
    { }

    /// <inheritdoc/>
    [Obsolete("To be removed", true)]
    public class DefinitionIndexName : SchemaKeyName, IDefinitionIndexName
    {
        /// <inheritdoc cref="SchemaKeyName(ISchemaKeyName)"/>
        public DefinitionIndexName(IDefinitionIndexName source) : base(source) { }
    }
}
