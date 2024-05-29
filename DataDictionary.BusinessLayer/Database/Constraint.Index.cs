using DataDictionary.DataLayer.DatabaseData.Constraint;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IConstraintIndex : IDbConstraintKey { }

    /// <inheritdoc/>
    public class ConstraintIndex : DbConstraintKey
    {
        /// <inheritdoc cref="DbConstraintKey(IDbConstraintKey)"/>
        public ConstraintIndex(IConstraintIndex source) : base(source) { }

        /// <summary>
        /// Convert ConstraintIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(ConstraintIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.ConstraintId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface IConstraintIndexName : IDbConstraintKeyName, ISchemaIndexName
    { }

    /// <inheritdoc/>
    public class ConstraintIndexName : DbConstraintKeyName, IConstraintIndexName
    {
        /// <inheritdoc cref="DbConstraintKeyName(IDbConstraintKeyName)"/>
        public ConstraintIndexName(IConstraintIndexName source) : base(source) { }
    }
}
