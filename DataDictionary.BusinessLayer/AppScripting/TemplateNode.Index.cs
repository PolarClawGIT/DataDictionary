using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITemplateNodeIndex : IScriptingNodeKey
    { }

    /// <inheritdoc/>
    public class TemplateNodeIndex : ScriptingNodeKey, ITemplateNodeIndex,
        IKeyEquality<ITemplateNodeIndex>, IKeyEquality<TemplateNodeIndex>
    {
        /// <inheritdoc cref="ScriptingNodeKey(IScriptingNodeKey)"/>
        public TemplateNodeIndex(ITemplateNodeIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateNodeIndex? other)
        { return other is IScriptingNodeKey key && Equals(new ScriptingNodeKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateNodeIndex? other)
        { return other is IScriptingNodeKey key && Equals(new ScriptingNodeKey(key)); }
    }
}
