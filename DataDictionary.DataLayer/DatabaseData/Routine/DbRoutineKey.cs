using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Routine
{
    /// <summary>
    /// Interface for the Database Routine Key.
    /// </summary>
    public interface IDbRoutineKey : IKey
    {
        /// <summary>
        /// Application ID for the Routine.
        /// </summary>
        Guid? RoutineId { get; }
    }

    /// <summary>
    /// Implementation for the Database Routine Key.
    /// </summary>
    public class DbRoutineKey : IDbRoutineKey, IKeyEquality<IDbRoutineKey>
    {
        /// <inheritdoc/>
        public Guid? RoutineId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Routine Key.
        /// </summary>
        /// <param name="source"></param>
        public DbRoutineKey(IDbRoutineKey source) : base()
        {
            if (source.RoutineId is Guid value) { RoutineId = value; }
            else { RoutineId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IDbRoutineKey? other)
        { return other is IDbRoutineKey && EqualityComparer<Guid?>.Default.Equals(RoutineId, other.RoutineId); }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbRoutineKey value && Equals(new DbRoutineKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbRoutineKey left, DbRoutineKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbRoutineKey left, DbRoutineKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(RoutineId); }
        #endregion
    }
}
