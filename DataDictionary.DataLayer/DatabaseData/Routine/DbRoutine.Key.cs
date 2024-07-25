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
    public class DbRoutineKey : IDbRoutineKey,
        IKeyEquality<IDbRoutineKey>, IKeyEquality<DbRoutineKey>
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
        public Boolean Equals(DbRoutineKey? other)
        { return other is DbRoutineKey && EqualityComparer<Guid?>.Default.Equals(RoutineId, other.RoutineId); }

        /// <inheritdoc/>
        public virtual Boolean Equals(IDbRoutineKey? other)
        { return other is IDbRoutineKey value && Equals(new DbRoutineKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IDbRoutineKey value && Equals(new DbRoutineKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(DbRoutineKey left, DbRoutineKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DbRoutineKey left, DbRoutineKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(RoutineId); }


        #endregion
    }
}
