using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Schema;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface IDefinitionValue : ISchemaItem, IDefinitionIndex, IDefinitionIndexName, ISchemaKeyName
    { }

    /// <inheritdoc/>
    public class DefinitionValue : SchemaItem, IDefinitionValue, INamedScopeSourceValue
    {
        /// <inheritdoc/>
        public DefinitionValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new DefinitionIndex(this); }


        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(Scope); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return SchemaTitle ?? Scope.ToName(); }
    }
}
