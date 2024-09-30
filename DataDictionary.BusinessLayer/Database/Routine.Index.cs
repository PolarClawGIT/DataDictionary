using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IRoutineIndex : IDbRoutineKey
    { }

    /// <inheritdoc/>
    public class RoutineIndex : DbRoutineKey, IRoutineIndex,
        IKeyEquality<IRoutineIndex>, IKeyEquality<RoutineIndex>
    {
        /// <inheritdoc cref="DbRoutineKey(IDbRoutineKey)"/>
        public RoutineIndex(IDbRoutineKey source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(IRoutineIndex? other)
        { return other is IDbRoutineKey value && Equals(new DbRoutineKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(RoutineIndex? other)
        { return other is IDbRoutineKey value && Equals(new DbRoutineKey(value)); }

        /// <summary>
        /// Convert RoutineIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(RoutineIndex source)
        { return new DataIndex() { SystemId = source.RoutineId ?? Guid.Empty }; }
    }
}
