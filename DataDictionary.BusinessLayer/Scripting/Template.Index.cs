using DataDictionary.DataLayer.ScriptingData;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplateIndex : IScriptingTemplateKey
    { }

    /// <inheritdoc/>
    public class TemplateIndex : ScriptingTemplateKey
    {
        /// <inheritdoc cref="ScriptingTemplateKey(IScriptingTemplateKey)"/>
        public TemplateIndex(ITemplateIndex source) : base(source)
        { }

        /// <summary>
        /// Convert TemplateIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(TemplateIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.TemplateId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface ITemplateIndexName : IScriptingTemplateKeyName
    { }

    /// <inheritdoc/>
    public class TemplateIndexName : ScriptingTemplateKeyName
    {
        /// <inheritdoc cref="ScriptingTemplateKeyName(IScriptingTemplateKeyName)"/>
        public TemplateIndexName(ITemplateIndexName source) : base(source)
        { }
    }
}
