using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DomainData.Definition;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IDefinitionIndex : IDomainDefinitionKey
    { }

    /// <inheritdoc/>
    public class DefinitionIndex : DomainDefinitionKey, IDefinitionIndex,
        IKeyEquality<IDefinitionIndex>, IKeyEquality<DefinitionIndex>
    {
        /// <inheritdoc cref="DomainDefinitionKey(IDomainDefinitionKey)"/>
        public DefinitionIndex(IDefinitionIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IDefinitionIndex? other)
        { return other is IDomainDefinitionKey key && Equals(new DomainDefinitionKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(DefinitionIndex? other)
        { return other is IDomainDefinitionKey key && Equals(new DomainDefinitionKey(key)); }

        /// <summary>
        /// Convert DefinitionIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(DefinitionIndex source)
        { return new DataIndex() { SystemId = source.DefinitionId ?? Guid.Empty }; }
    }
}
