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
    public class TemplateIndexName : ScriptingTemplateKeyName, ITemplateIndexName,
        IKeyEquality<ITemplateIndexName>, IKeyEquality<TemplateIndexName>
    {
        /// <inheritdoc cref="ScriptingTemplateKeyName(IScriptingTemplateKeyName)"/>
        public TemplateIndexName(ITemplateIndexName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateIndexName? other)
        { return other is IScriptingTemplateKeyName key && Equals(new ScriptingTemplateKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateIndexName? other)
        { return other is IScriptingTemplateKeyName key && Equals(new ScriptingTemplateKeyName(key)); }
    }
}
