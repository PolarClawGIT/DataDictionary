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
        /// Convert RoutineIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(RoutineIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.RoutineId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface IRoutineIndexName : IDbRoutineKeyName, ISchemaIndexName
    { }

    /// <inheritdoc/>
    public class RoutineIndexName : DbRoutineKeyName, IRoutineIndexName,
        IKeyEquality<IRoutineIndexName>, IKeyEquality<RoutineIndexName>
    {
        /// <inheritdoc cref="DbRoutineKeyName(IDbRoutineKeyName)"/>
        public RoutineIndexName(IRoutineIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IRoutineIndexName? other)
        { return other is IDbRoutineKeyName value && Equals(new DbRoutineKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(RoutineIndexName? other)
        { return other is IDbRoutineKeyName value && Equals(new DbRoutineKeyName(value)); }
    }
}
