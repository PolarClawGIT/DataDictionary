using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Template;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplateValue : IScriptingTemplateItem, ITemplateIndex, ITemplateIndexName
    { }

    /// <inheritdoc/>
    public class TemplateValue: ScriptingTemplateItem, ITemplateValue, INamedScopeSourceValue
    {
        /// <inheritdoc/>
        public TemplateValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new TemplateIndex(this); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(Scope); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return this.TemplateTitle ?? Scope.ToName(); }
    }
}
