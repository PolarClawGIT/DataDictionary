using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Routine
{

    /// <summary>
    /// Interface for the Database Routine Dependency Key.
    /// </summary>
    public interface IDbRoutineDependencyKey : IKey
    {
        /// <summary>
        /// Application ID for the Routine Dependency.
        /// </summary>
        Guid? DependencyId { get; }
    }

    /// <summary>
    /// Implementation for the Database Routine Dependency Key.
    /// </summary>
    public class DbRoutineDependencyKey : IDbRoutineDependencyKey,
        IKeyEquality<IDbRoutineDependencyKey>, IKeyEquality<DbRoutineDependencyKey>
    {
        /// <inheritdoc/>
        public Guid? DependencyId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Routine Dependency Key.
        /// </summary>
        /// <param name="source"></param>
        public DbRoutineDependencyKey(IDbRoutineDependencyKey source) : base()
        {
            if (source.DependencyId is Guid value) { DependencyId = value; }
            else { DependencyId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(DbRoutineDependencyKey? other)
        { return other is DbRoutineDependencyKey && EqualityComparer<Guid?>.Default.Equals(DependencyId, other.DependencyId); }

        /// <inheritdoc/>
        public virtual Boolean Equals(IDbRoutineDependencyKey? other)
        { return other is IDbRoutineDependencyKey value && Equals(new DbRoutineDependencyKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IDbRoutineDependencyKey value && Equals(new DbRoutineDependencyKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(DbRoutineDependencyKey left, DbRoutineDependencyKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DbRoutineDependencyKey left, DbRoutineDependencyKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(DependencyId); }

        #endregion
    }
}
