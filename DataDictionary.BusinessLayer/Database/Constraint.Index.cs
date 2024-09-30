using DataDictionary.BusinessLayer.ToolSet;
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
        /// Convert ConstraintIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(ConstraintIndex source)
        { return new DataIndex() { SystemId = source.ConstraintId ?? Guid.Empty }; }
    }
}
