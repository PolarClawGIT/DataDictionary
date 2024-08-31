using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IConstraintIndex : IDbConstraintKey { }

    /// <inheritdoc/>
    public class ConstraintIndex : DbConstraintKey, IConstraintIndex,
        IKeyEquality<IConstraintIndex>, IKeyEquality<ConstraintIndex>
    {
        /// <inheritdoc cref="DbConstraintKey(IDbConstraintKey)"/>
        public ConstraintIndex(IConstraintIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IConstraintIndex? other)
        { return other is IDbConstraintKey value && Equals(new DbConstraintKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ConstraintIndex? other)
        { return other is IDbConstraintKey value && Equals(new DbConstraintKey(value)); }

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
    public class ConstraintIndexName : DbConstraintKeyName, IConstraintIndexName,
        IKeyEquality<IConstraintIndexName>, IKeyEquality<ConstraintIndexName>
    {
        /// <inheritdoc cref="DbConstraintKeyName(IDbConstraintKeyName)"/>
        public ConstraintIndexName(IConstraintIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IConstraintIndexName? other)
        { return other is IDbConstraintKeyName value && Equals(new DbConstraintKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ConstraintIndexName? other)
        { return other is IDbConstraintKeyName value && Equals(new DbConstraintKeyName(value)); }
    }

    /// <inheritdoc/>
    public interface IConstraintIndexReferenced : IDbConstraintKeyReferenced
    { }

    /// <inheritdoc/>
    public class ConstraintIndexReferenced : DbConstraintKeyReferenced, IConstraintIndexReferenced,
        IKeyEquality<IConstraintIndexReferenced>, IKeyEquality<ConstraintIndexName>
    {
        /// <inheritdoc cref="DbConstraintKeyReferenced(IDbConstraintKeyReferenced)"/>
        public ConstraintIndexReferenced(IConstraintIndexReferenced source) : base(source) { }

        /// <inheritdoc/>
        public override TableIndexName AsTableName()
        { return new TableIndexName(base.AsTableName()); }

        /// <inheritdoc/>
        public Boolean Equals(IConstraintIndexReferenced? other)
        { return other is IConstraintIndexReferenced value && Equals(new DbConstraintKeyReferenced(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ConstraintIndexName? other)
        { return other is IConstraintIndexReferenced value && Equals(new DbConstraintKeyReferenced(value)); }
    }

}
