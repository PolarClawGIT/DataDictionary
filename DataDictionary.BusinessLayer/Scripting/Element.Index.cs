using DataDictionary.DataLayer.ScriptingData.Schema;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface IElementIndex : IElementKey
    { }

    /// <inheritdoc/>
    public class ElementIndex : ElementKey, IElementIndex
    {
        /// <inheritdoc cref="ElementKey(IElementKey)"/>
        public ElementIndex(IElementIndex source) : base(source) { }
    }
}
