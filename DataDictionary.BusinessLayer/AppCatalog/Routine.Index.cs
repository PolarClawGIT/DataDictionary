using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IRoutineIndex : IRoutineKey
    { }

    /// <inheritdoc/>
    public class RoutineIndex : RoutineKey, IRoutineIndex,
        IKeyEquality<IRoutineIndex>, IKeyEquality<RoutineIndex>
    {
        /// <inheritdoc cref="RoutineKey(IRoutineKey)"/>
        public RoutineIndex(IRoutineKey source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(IRoutineIndex? other)
        { return other is IRoutineKey value && Equals(new RoutineKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(RoutineIndex? other)
        { return other is IRoutineKey value && Equals(new RoutineKey(value)); }

        /// <summary>
        /// Convert RoutineIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(RoutineIndex source)
        { return new DataIndex() { SystemId = source.RoutineId ?? Guid.Empty }; }
    }
}
