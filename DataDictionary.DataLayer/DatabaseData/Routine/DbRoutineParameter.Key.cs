using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Routine
{
    /// <summary>
    /// Interface for the Database Routine Parameter Key.
    /// </summary>
    public interface IDbRoutineParameterKey : IKey
    {
        /// <summary>
        /// Application ID for the Routine Parameter.
        /// </summary>
        Guid? ParameterId { get; }
    }

    /// <summary>
    /// Implementation for the Database Routine Parameter Key.
    /// </summary>
    public class DbRoutineParameterKey : IDbRoutineParameterKey, IKeyEquality<IDbRoutineParameterKey>
    {
        /// <inheritdoc/>
        public Guid? ParameterId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the RoutineParameter Key.
        /// </summary>
        /// <param name="source"></param>
        public DbRoutineParameterKey(IDbRoutineParameterKey source) : base()
        {
            if (source.ParameterId is Guid value) { ParameterId = value; }
            else { ParameterId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IDbRoutineParameterKey? other)
        { return other is IDbRoutineParameterKey && EqualityComparer<Guid?>.Default.Equals(ParameterId, other.ParameterId); }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbRoutineParameterKey value && Equals(new DbRoutineParameterKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbRoutineParameterKey left, DbRoutineParameterKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbRoutineParameterKey left, DbRoutineParameterKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(ParameterId); }
        #endregion
    }
}
