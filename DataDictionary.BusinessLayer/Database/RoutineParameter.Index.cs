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
        /// Convert RoutineParameterIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(RoutineParameterIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.ParameterId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface IRoutineParameterIndexName : IDbRoutineParameterKeyName, IRoutineIndexName
    { }

    /// <inheritdoc/>
    public class RoutineParameterKeyName : DbRoutineParameterKeyName, IRoutineParameterIndexName,
        IKeyEquality<IRoutineParameterIndexName>, IKeyEquality<RoutineParameterKeyName>
    {
        /// <inheritdoc cref="DbRoutineParameterKeyName(IDbRoutineParameterKeyName)"/>
        public RoutineParameterKeyName(IRoutineParameterIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IRoutineParameterIndexName? other)
        { return other is IDbRoutineParameterKeyName value && Equals(new DbRoutineParameterKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(RoutineParameterKeyName? other)
        { return other is IDbRoutineParameterKeyName value && Equals(new DbRoutineParameterKeyName(value)); }
    }
}
