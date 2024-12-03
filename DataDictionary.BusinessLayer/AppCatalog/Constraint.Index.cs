using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IConstraintIndex : IConstraintKey { }

    /// <inheritdoc/>
    public class ConstraintIndex : ConstraintKey, IConstraintIndex,
        IKeyEquality<IConstraintIndex>, IKeyEquality<ConstraintIndex>
    {
        /// <inheritdoc cref="ConstraintKey(IConstraintKey)"/>
        public ConstraintIndex(IConstraintIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IConstraintIndex? other)
        { return other is IConstraintKey value && Equals(new ConstraintKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ConstraintIndex? other)
        { return other is IConstraintKey value && Equals(new ConstraintKey(value)); }

        /// <summary>
        /// Convert ConstraintIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(ConstraintIndex source)
        { return new DataIndex() { SystemId = source.ConstraintId ?? Guid.Empty }; }
    }
}
