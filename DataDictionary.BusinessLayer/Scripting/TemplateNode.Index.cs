using DataDictionary.DataLayer.ScriptingData.Template;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplateNodeIndex : IScriptingNodeKeyComposite
    { }

    /// <inheritdoc/>
    public class TemplateNodeIndex : ScriptingNodeKeyComposite
    {
        /// <inheritdoc cref="ScriptingNodeKeyComposite(IScriptingNodeKeyComposite)"/>
        public TemplateNodeIndex(ITemplateNodeIndex source) : base(source)
        { }
    }

    /// <inheritdoc/>
    public interface ITemplateNodeIndexName : IScriptingNodeKeyName
    { }

    /// <inheritdoc/>
    public class TemplateNodeIndexName : ScriptingNodeKeyName
    {
        /// <inheritdoc cref="ScriptingNodeKeyName(IScriptingNodeKeyName)"/>
        public TemplateNodeIndexName(ITemplateNodeIndexName source) : base(source)
        { }
    }
}
