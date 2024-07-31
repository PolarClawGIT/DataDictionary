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
        /// Convert DefinitionIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(DefinitionIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.DefinitionId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface IDefinitionIndexName : IDomainDefinitionKeyName
    { }

    /// <inheritdoc/>
    public class DefinitionIndexName : DomainDefinitionKeyName, IDefinitionIndexName,
        IKeyEquality<IDefinitionIndexName>, IKeyEquality<DefinitionIndexName>
    {
        /// <inheritdoc cref="DomainDefinitionKeyName(IDomainDefinitionKeyName)"/>
        public DefinitionIndexName(IDefinitionIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IDefinitionIndexName? other)
        { return other is IDomainDefinitionKeyName key && Equals(new DomainDefinitionKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(DefinitionIndexName? other)
        { return other is IDomainDefinitionKeyName key && Equals(new DomainDefinitionKeyName(key)); }
    }
}
