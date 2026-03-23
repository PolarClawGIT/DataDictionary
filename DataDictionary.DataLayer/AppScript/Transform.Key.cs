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
        Guid? SchemaId { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Transform Key
    /// </summary>
    public class TransformKey : ITransformKey,
        IKeyEquality<TransformKey>,
        IKeyEquality<ITransformKey>
    {
        /// <inheritdoc/>
        public Guid? SchemaId { get; init; } = Guid.Empty;

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
            if (source.SchemaId is Guid) { SchemaId = source.SchemaId; }
            else { SchemaId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(TransformKey? other)
        { return other is TransformKey key && key.SchemaId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(SchemaId, key.SchemaId); }

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
        { return HashCode.Combine(SchemaId); }


        #endregion
    }
}
