using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IDefinitionIndex : IDefinitionKey
    { }

    /// <inheritdoc/>
    public class DefinitionIndex : DefinitionKey, IDefinitionIndex,
        IKeyEquality<IDefinitionIndex>, IKeyEquality<DefinitionIndex>
    {
        /// <inheritdoc cref="DefinitionKey(IDefinitionKey)"/>
        public DefinitionIndex(IDefinitionIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IDefinitionIndex? other)
        { return other is IDefinitionKey key && Equals(new DefinitionKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(DefinitionIndex? other)
        { return other is IDefinitionKey key && Equals(new DefinitionKey(key)); }

        /// <summary>
        /// Convert DefinitionIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(DefinitionIndex source)
        { return new DataIndex() { SystemId = source.DefinitionId ?? Guid.Empty }; }
    }
}
