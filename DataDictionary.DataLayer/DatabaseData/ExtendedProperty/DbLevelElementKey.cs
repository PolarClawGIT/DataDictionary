using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.DatabaseData.ExtendedProperty
{
  
    /// <summary>
    /// Interface for Level2 MS Extended Property Type.
    /// </summary>
    public interface IDbLevelElementKey: IDbLevelObjectKey, IDbLevelElementType
    { }

    /// <summary>
    /// Implementation of the Key for Level2 MS Extended Property Type.
    /// </summary>
    /// <remarks>
    /// Currently not used.
    /// </remarks>
    public class DbLevelElementKey : DbLevelObjectKey, IDbLevelElementKey, IKeyEquality<IDbLevelElementKey>
    {
        /// <inheritdoc/>
        public DbLevelElementType ElementScope { get; init; } = DbLevelElementType.Null;

        /// <summary>
        /// Constructor for a Element Scope.
        /// </summary>
        internal protected DbLevelElementKey() : base() { }

        /// <summary>
        /// Constructor for a Element Scope.
        /// </summary>
        public DbLevelElementKey(IDbLevelElementKey source) : base(source)
        { ElementScope = source.ElementScope; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual bool Equals(IDbLevelElementKey? other)
        {
            return
                other is IDbLevelObjectKey
                && new DbLevelObjectKey(this).Equals(other)
                && ObjectScope != DbLevelObjectType.Null
                && other.ObjectScope != DbLevelObjectType.Null
                && ObjectScope == other.ObjectScope;
        }

        /// <inheritdoc/>
        public override bool Equals(object? other)
        { return other is IDbLevelElementKey value && Equals(new DbLevelElementKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DbLevelElementKey left, DbLevelElementKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbLevelElementKey left, DbLevelElementKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ElementScope); }
        #endregion

    }
}
