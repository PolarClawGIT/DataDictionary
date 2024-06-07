using DataDictionary.DataLayer.ScriptingData.Schema;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    [Obsolete("To be removed", true)]
    public interface IDefinitionElementIndex : IElementKey
    { }

    /// <inheritdoc/>
    [Obsolete("To be removed", true)]
    public class DefinitionElementIndex : ElementKey, IDefinitionElementIndex
    {
        /// <inheritdoc cref="ElementKey(IElementKey)"/>
        public DefinitionElementIndex(IDefinitionElementIndex source) : base(source) { }
    }
}
