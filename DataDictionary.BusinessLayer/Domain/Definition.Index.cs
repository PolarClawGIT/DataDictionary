using DataDictionary.DataLayer.DomainData.Definition;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IDefinitionIndex : IDomainDefinitionKey
    { }

    /// <inheritdoc/>
    public class DefinitionIndex : DomainDefinitionKey, IDefinitionIndex
    {
        /// <inheritdoc cref="DomainDefinitionKey(IDomainDefinitionKey)"/>
        public DefinitionIndex(IDefinitionIndex source) : base(source) { }

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
    public class DefinitionIndexName : DomainDefinitionKeyName
    {
        /// <inheritdoc cref="DomainDefinitionKeyName(IDomainDefinitionKeyName)"/>
        public DefinitionIndexName(IDefinitionIndexName source) : base(source) { }
    }
}
