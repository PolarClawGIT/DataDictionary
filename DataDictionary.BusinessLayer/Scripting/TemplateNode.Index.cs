using DataDictionary.DataLayer.ScriptingData;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Scripting
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

    /// <inheritdoc/>
    public interface ITemplateNodeIndexName : IScriptingNodeKeyName
    { }

    /// <inheritdoc/>
    public class TemplateNodeIndexName : ScriptingNodeKeyName, ITemplateNodeIndexName,
        IKeyEquality<ITemplateNodeIndexName>, IKeyEquality<TemplateNodeIndexName>
    {
        /// <inheritdoc cref="ScriptingNodeKeyName(IScriptingNodeKeyName)"/>
        public TemplateNodeIndexName(ITemplateNodeIndexName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateNodeIndexName? other)
        { return other is IScriptingNodeKeyName key && Equals(new ScriptingNodeKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateNodeIndexName? other)
        { return other is IScriptingNodeKeyName key && Equals(new ScriptingNodeKeyName(key)); }
    }
}
