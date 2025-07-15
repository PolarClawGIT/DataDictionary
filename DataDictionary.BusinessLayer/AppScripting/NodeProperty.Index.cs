using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface for the Scripting Node Property key.
    /// </summary>
    [Obsolete]
    public interface INodePropertyIndex : IScriptingNodeKeyName
    { }

    /// <summary>
    /// Implementation for the Scripting Schema Column key.
    /// </summary>
    [Obsolete]
    public class NodePropertyIndex : ScriptingNodeKeyName, INodePropertyIndex,
        IKeyEquality<INodePropertyIndex>, IKeyEquality<NodePropertyIndex>
    {
        /// <inheritdoc cref="ScriptingNodeKeyName(IScriptingNodeKeyName)"/>
        public NodePropertyIndex(INodePropertyIndex source) : base(source)
        { }

        /// <inheritdoc cref="ScriptingNodeKeyName(IScriptingNodeKeyName)"/>
        public NodePropertyIndex(IScriptingNodeKeyName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(INodePropertyIndex? other)
        { return other is IScriptingNodeKeyName key && Equals(new ScriptingNodeKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(NodePropertyIndex? other)
        { return other is IScriptingNodeKeyName key && Equals(new ScriptingNodeKeyName(key)); }
    }
}
