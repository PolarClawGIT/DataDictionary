using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{

    /// <summary>
    /// Interface for the Database RoutineColumn Key.
    /// </summary>
    public interface IRoutineColumnKey : IKey
    {
        /// <summary>
        /// Application ID for the RoutineColumn.
        /// </summary>
        Guid? RoutineColumnId { get; }
    }

    /// <summary>
    /// Implementation for the Database RoutineColumn Key.
    /// </summary>
    public class RoutineColumnKey : IRoutineColumnKey,
        IKeyEquality<IRoutineColumnKey>, IKeyEquality<RoutineColumnKey>
    {
        /// <inheritdoc/>
        public Guid? RoutineColumnId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return RoutineColumnId.HasValue && RoutineColumnId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the RoutineColumn Key.
        /// </summary>
        /// <param name="source"></param>
        public RoutineColumnKey(IRoutineColumnKey source) : base()
        {
            if (source.RoutineColumnId is Guid value) { RoutineColumnId = value; }
            else { RoutineColumnId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public virtual Boolean Equals(RoutineColumnKey? other)
        {
            return other is RoutineColumnKey key
                && RoutineColumnId.HasValue && RoutineColumnId != Guid.Empty
                && key.RoutineColumnId.HasValue && key.RoutineColumnId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(RoutineColumnId, other.RoutineColumnId);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IRoutineColumnKey? other)
        { return other is IRoutineColumnKey value && Equals(new RoutineColumnKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IRoutineColumnKey value && Equals(new RoutineColumnKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(RoutineColumnKey left, RoutineColumnKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(RoutineColumnKey left, RoutineColumnKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(RoutineColumnId); }
        #endregion
    }
}
