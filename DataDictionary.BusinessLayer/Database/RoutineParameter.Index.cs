using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IRoutineParameterIndex : IDbRoutineParameterKey
    { }

    /// <inheritdoc/>
    public class RoutineParameterIndex : DbRoutineParameterKey, IRoutineParameterIndex,
        IKeyEquality<IRoutineParameterIndex>, IKeyEquality<RoutineParameterIndex>
    {
        /// <inheritdoc cref="DbRoutineParameterKey(IDbRoutineParameterKey)"/>
        public RoutineParameterIndex(IDbRoutineParameterKey source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(IRoutineParameterIndex? other)
        { return other is IDbRoutineParameterKey value && Equals(new DbRoutineParameterKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(RoutineParameterIndex? other)
        { return other is IDbRoutineParameterKey value && Equals(new DbRoutineParameterKey(value)); }

        /// <summary>
        /// Convert RoutineParameterIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(RoutineParameterIndex source)
        { return new DataIndex() { SystemId = source.ParameterId ?? Guid.Empty }; }
    }


}
