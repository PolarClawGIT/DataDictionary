using DataDictionary.DataLayer.ScriptingData.Schema;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ISchemaIndex : ISchemaKey
    { }

    /// <inheritdoc/>
    public class SchemaIndex : SchemaKey, ISchemaIndex
    {
        /// <inheritdoc/>
        public SchemaIndex(ISchemaKey source) : base(source) { }
    }
}
