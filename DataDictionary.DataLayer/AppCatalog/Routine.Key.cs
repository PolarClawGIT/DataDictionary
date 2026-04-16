using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Routine Key.
    /// </summary>
    public interface IRoutineKey : IKey
    {
        /// <summary>
        /// Application ID for the Routine.
        /// </summary>
        Guid? RoutineId { get; }
    }

    /// <summary>
    /// Implementation for the Database Routine Key.
    /// </summary>
    public class RoutineKey : IRoutineKey,
        IKeyEquality<IRoutineKey>, IKeyEquality<RoutineKey>
    {
        /// <inheritdoc/>
        public Guid? RoutineId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return RoutineId.HasValue && RoutineId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Routine Key.
        /// </summary>
        /// <param name="source"></param>
        public RoutineKey(IRoutineKey source) : base()
        {
            if (source.RoutineId is Guid value) { RoutineId = value; }
            else { RoutineId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(RoutineKey? other)
        {
            return other is RoutineKey key
                && RoutineId.HasValue && RoutineId != Guid.Empty
                && key.RoutineId.HasValue && key.RoutineId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(RoutineId, other.RoutineId);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IRoutineKey? other)
        { return other is IRoutineKey value && Equals(new RoutineKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IRoutineKey value && Equals(new RoutineKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(RoutineKey left, RoutineKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(RoutineKey left, RoutineKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(RoutineId); }


        #endregion
    }
}
