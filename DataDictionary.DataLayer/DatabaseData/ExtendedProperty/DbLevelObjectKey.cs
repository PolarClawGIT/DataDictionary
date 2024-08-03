using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.ExtendedProperty
{


    /// <summary>
    /// Interface for Level1 MS Extended Property Type.
    /// </summary>
    public interface IDbLevelObjectKey : IDbLevelCatalogKey, IDbLevelObjectType
    { }

    /// <summary>
    /// Implementation of the Key for Level1 MS Extended Property Type.
    /// </summary>
    /// <remarks>
    /// Currently not used.
    /// </remarks>
    public class DbLevelObjectKey : DbLevelCatalogKey, IDbLevelObjectKey, IKeyEquality<IDbLevelObjectKey>
    {
        /// <inheritdoc/>
        public DbLevelObjectType ObjectScope { get; init; } = DbLevelObjectType.Null;

        /// <summary>
        /// Constructor for a Object Scope.
        /// </summary>
        internal protected DbLevelObjectKey() : base() { }

        /// <summary>
        /// Constructor for a Object Scope.
        /// </summary>
        public DbLevelObjectKey(IDbLevelObjectKey source) : base(source)
        { ObjectScope = source.ObjectScope; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IDbLevelObjectKey? other)
        {
            return
                other is IDbLevelObjectKey
                && new DbLevelCatalogKey(this).Equals(other)
                && ObjectScope != DbLevelObjectType.Null
                && other.ObjectScope != DbLevelObjectType.Null
                && ObjectScope == other.ObjectScope;
        }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbLevelObjectKey value && Equals(new DbLevelObjectKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbLevelObjectKey left, DbLevelObjectKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbLevelObjectKey left, DbLevelObjectKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ObjectScope); }
        #endregion
    }
}
