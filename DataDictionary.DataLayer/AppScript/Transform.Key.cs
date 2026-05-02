using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Transform Key
    /// </summary>
    public interface ITransformKey : IKey
    {
        /// <summary>
        /// Transform ID for the Scripting Transform.
        /// </summary>
        Guid? TransformId { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Transform Key
    /// </summary>
    public class TransformKey : ITransformKey,
        IKeyEquality<TransformKey>,
        IKeyEquality<ITransformKey>
    {
        /// <inheritdoc/>
        public Guid? TransformId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return TransformId.HasValue && TransformId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Blank/Empty Transform Key
        /// </summary>
        /// <remarks>Empty Key is never equal to anything.</remarks>
        public TransformKey()
        { }

        /// <summary>
        /// Constructor for the Transform Key
        /// </summary>
        /// <param name="source"></param>
        public TransformKey(ITransformKey source) : base()
        {
            if (source.TransformId is Guid) { TransformId = source.TransformId; }
            else { TransformId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(TransformKey? other)
        {
            return other is TransformKey key
                && TransformId.HasValue && TransformId != Guid.Empty
                && key.TransformId.HasValue && key.TransformId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(TransformId, other.TransformId);
        }

        /// <inheritdoc/>
        public Boolean Equals(ITransformKey? other)
        { return other is ITransformKey key && Equals(new TransformKey(key)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITransformKey key && Equals(new TransformKey(key)); }

        /// <inheritdoc/>
        public static Boolean operator ==(TransformKey left, TransformKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TransformKey left, TransformKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(TransformId); }


        #endregion
    }
}
