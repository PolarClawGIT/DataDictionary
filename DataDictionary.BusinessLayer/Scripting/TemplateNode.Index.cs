using DataDictionary.DataLayer.ScriptingData.Template;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplateNodeIndex : IScriptingNodeKey
    { }

    /// <inheritdoc/>
    public class TemplateNodeIndex : ScriptingNodeKey
    {
        /// <inheritdoc cref="ScriptingNodeKey(IScriptingNodeKey)"/>
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
