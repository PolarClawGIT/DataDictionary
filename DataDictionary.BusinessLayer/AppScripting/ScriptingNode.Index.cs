using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface IScriptingNodeIndex : IScriptingNodeKey
    { }

    /// <inheritdoc/>
    public class ScriptingNodeIndex : ScriptingNodeKey, IScriptingNodeIndex,
        IKeyEquality<IScriptingNodeIndex>, IKeyEquality<ScriptingNodeIndex>
    {
        /// <inheritdoc cref="ScriptingNodeKey(IScriptingNodeKey)"/>
        public ScriptingNodeIndex(IScriptingNodeIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(IScriptingNodeIndex? other)
        { return other is IScriptingNodeKey key && Equals(new ScriptingNodeKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(ScriptingNodeIndex? other)
        { return other is IScriptingNodeKey key && Equals(new ScriptingNodeKey(key)); }
    }
}
