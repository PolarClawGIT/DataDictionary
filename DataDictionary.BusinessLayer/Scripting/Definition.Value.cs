using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Schema;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    [Obsolete("To be removed", true)]
    public interface IDefinitionValue : ISchemaItem, IDefinitionIndex, IDefinitionIndexName, ISchemaKeyName
    { }

    /// <inheritdoc/>
    [Obsolete("To be removed", true)]
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
