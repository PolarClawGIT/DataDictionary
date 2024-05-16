using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Schema;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ISchemaValue : ISchemaItem, ISchemaIndex
    { }

    /// <inheritdoc/>
    public class SchemaValue : SchemaItem, ISchemaValue, INamedScopeSourceValue
    {
        /// <inheritdoc/>
        public SchemaValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new DataLayerIndex(SchemaId); }


        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(Scope); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return SchemaTitle ?? Scope.ToName(); }
    }
}
