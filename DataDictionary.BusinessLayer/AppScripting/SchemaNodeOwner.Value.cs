using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ISchemaNodeOwnerValue : ISchemaNodeOwnerItem,
        ISchemaNodeOwnerIndex, ISchemaComposite,
        IScopeType
    { }

    /// <inheritdoc/>
    public class SchemaNodeOwnerValue : SchemaNodeOwnerItem, ISchemaNodeOwnerValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingNodeOwner; } }

        /// <inheritdoc/>
        public SchemaNodeOwnerValue() : base()
        { }

        /// <inheritdoc cref="SchemaNodeOwnerItem.SchemaNodeOwnerItem(ITemplateKey, ISchemaDefinitionKey)"/>
        public SchemaNodeOwnerValue(ISchemaComposite schema) : base(schema, schema)
        { }
    }
}
