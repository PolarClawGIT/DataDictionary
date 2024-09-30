using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.ScriptingData;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplateIndex : IScriptingTemplateKey
    { }

    /// <inheritdoc/>
    public class TemplateIndex : ScriptingTemplateKey, ITemplateIndex,
        IKeyEquality<ITemplateIndex>, IKeyEquality<TemplateIndex>
    {
        /// <inheritdoc cref="ScriptingTemplateKey(IScriptingTemplateKey)"/>
        public TemplateIndex(ITemplateIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateIndex? other)
        { return other is IScriptingTemplateKey key && Equals(new ScriptingTemplateKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateIndex? other)
        { return other is IScriptingTemplateKey key && Equals(new ScriptingTemplateKey(key)); }

        /// <summary>
        /// Convert TemplateIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(TemplateIndex source)
        { return new DataIndex() { SystemId = source.TemplateId ?? Guid.Empty }; }
    }

}
