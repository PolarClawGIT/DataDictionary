using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DomainData.Definition
{
    /// <summary>
    /// Interface for the Domain Definition Key
    /// </summary>
    public interface IDomainDefinitionKey : IKey
    {
        /// <summary>
        /// Application ID for the Domain Definition.
        /// </summary>
        Guid? DefinitionId { get; }
    }

    /// <summary>
    /// Implementation for the Domain Definition Key
    /// </summary>
    public class DomainDefinitionKey : IDomainDefinitionKey, IKeyEquality<IDomainDefinitionKey>
    {
        /// <inheritdoc/>
        public Guid? DefinitionId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Definition Key
        /// </summary>
        /// <param name="source"></param>
        public DomainDefinitionKey(IDomainDefinitionKey source) : base()
        {
            if (source.DefinitionId is Guid) { DefinitionId = source.DefinitionId; }
            else { DefinitionId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDomainDefinitionKey? other)
        { return other is IDomainDefinitionKey key && EqualityComparer<Guid?>.Default.Equals(DefinitionId, key.DefinitionId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainDefinitionKey value && Equals(new DomainDefinitionKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DomainDefinitionKey left, DomainDefinitionKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainDefinitionKey left, DomainDefinitionKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(DefinitionId); }
        #endregion
    }
}