using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Definition Key
    /// </summary>
    public interface IDefinitionKey : IKey
    {
        /// <summary>
        /// Application ID for the Model Definition.
        /// </summary>
        Guid? DefinitionId { get; }
    }

    /// <summary>
    /// Implementation for the Model Definition Key
    /// </summary>
    public class DefinitionKey : IDefinitionKey,
        IKeyEquality<IDefinitionKey>, IKeyEquality<DefinitionKey>
    {
        /// <inheritdoc/>
        public Guid? DefinitionId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return DefinitionId.HasValue && DefinitionId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Model Definition Key
        /// </summary>
        /// <param name="source"></param>
        public DefinitionKey(IDefinitionKey source) : base()
        {
            if (source.DefinitionId is Guid) { DefinitionId = source.DefinitionId; }
            else { DefinitionId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(DefinitionKey? other)
        {
            return other is DefinitionKey key
                && DefinitionId.HasValue && DefinitionId != Guid.Empty
                && key.DefinitionId.HasValue && key.DefinitionId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(DefinitionId, other.DefinitionId);
        }

        /// <inheritdoc/>
        public Boolean Equals(IDefinitionKey? other)
        { return other is IDefinitionKey value && Equals(new DefinitionKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDefinitionKey value && Equals(new DefinitionKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(DefinitionKey left, DefinitionKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DefinitionKey left, DefinitionKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(DefinitionId); }

        #endregion
    }
}