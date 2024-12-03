using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IRoutineColumnIndex : IRoutineColumnKey
    { }

    /// <inheritdoc/>
    public class RoutineColumnIndex : RoutineColumnKey, IRoutineColumnIndex,
        IKeyEquality<IRoutineColumnIndex>, IKeyEquality<RoutineColumnIndex>
    {
        /// <inheritdoc cref="RoutineColumnKey(IRoutineColumnKey)"/>
        public RoutineColumnIndex(IRoutineColumnKey source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(IRoutineColumnIndex? other)
        { return other is IRoutineColumnKey value && Equals(new RoutineColumnKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(RoutineColumnIndex? other)
        { return other is IRoutineColumnKey value && Equals(new RoutineColumnKey(value)); }

        /// <summary>
        /// Convert RoutineColumnIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(RoutineColumnIndex source)
        { return new DataIndex() { SystemId = source.RoutineColumnId ?? Guid.Empty }; }
    }
}
