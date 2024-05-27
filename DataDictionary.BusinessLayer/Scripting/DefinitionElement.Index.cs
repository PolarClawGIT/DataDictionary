using DataDictionary.DataLayer.ScriptingData.Schema;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface IDefinitionElementIndex : IElementKey
    { }

    /// <inheritdoc/>
    public class DefinitionElementIndex : ElementKey, IDefinitionElementIndex
    {
        /// <inheritdoc cref="ElementKey(IElementKey)"/>
        public DefinitionElementIndex(IDefinitionElementIndex source) : base(source) { }
    }
}
