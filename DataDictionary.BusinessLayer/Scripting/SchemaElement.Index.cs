using DataDictionary.DataLayer.ScriptingData.Schema;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ISchemaElementIndex : IElementKey
    { }

    /// <inheritdoc/>
    public class SchemaElementIndex : ElementKey, ISchemaElementIndex
    {
        /// <inheritdoc/>
        public SchemaElementIndex(ISchemaElementIndex source) : base(source) { }
    }
}
