using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface IScriptingTemplateIndex : IScriptingTemplateKey
    { }

    /// <inheritdoc/>
    public class ScriptingTemplateIndex : ScriptingTemplateKey, IScriptingTemplateIndex,
        IKeyEquality<IScriptingTemplateIndex>, IKeyEquality<ScriptingTemplateIndex>
    {
        /// <inheritdoc cref="ScriptingTemplateKey(IScriptingTemplateKey)"/>
        public ScriptingTemplateIndex(IScriptingTemplateIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(IScriptingTemplateIndex? other)
        { return other is IScriptingTemplateKey key && Equals(new ScriptingTemplateKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(ScriptingTemplateIndex? other)
        { return other is IScriptingTemplateKey key && Equals(new ScriptingTemplateKey(key)); }

        /// <summary>
        /// Convert TemplateIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(ScriptingTemplateIndex source)
        { return new DataIndex() { SystemId = source.TemplateId ?? Guid.Empty }; }
    }

}
