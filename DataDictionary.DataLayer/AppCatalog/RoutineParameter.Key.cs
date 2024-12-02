using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Routine Parameter Key.
    /// </summary>
    public interface IRoutineParameterKey : IKey
    {
        /// <summary>
        /// Application ID for the Routine Parameter.
        /// </summary>
        Guid? ParameterId { get; }
    }

    /// <summary>
    /// Implementation for the Database Routine Parameter Key.
    /// </summary>
    public class RoutineParameterKey : IRoutineParameterKey, IKeyEquality<IRoutineParameterKey>
    {
        /// <inheritdoc/>
        public Guid? ParameterId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the RoutineParameter Key.
        /// </summary>
        /// <param name="source"></param>
        public RoutineParameterKey(IRoutineParameterKey source) : base()
        {
            if (source.ParameterId is Guid value) { ParameterId = value; }
            else { ParameterId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IRoutineParameterKey? other)
        { return other is IRoutineParameterKey && EqualityComparer<Guid?>.Default.Equals(ParameterId, other.ParameterId); }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IRoutineParameterKey value && Equals(new RoutineParameterKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(RoutineParameterKey left, RoutineParameterKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(RoutineParameterKey left, RoutineParameterKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(ParameterId); }
        #endregion
    }
}
