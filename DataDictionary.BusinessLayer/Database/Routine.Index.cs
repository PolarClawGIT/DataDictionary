using DataDictionary.DataLayer.DatabaseData.Routine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IRoutineIndex : IDbRoutineKey
    { }

    /// <inheritdoc/>
    public class RoutineIndex : DbRoutineKey, IRoutineIndex
    {
        /// <inheritdoc cref="DbRoutineKey(IDbRoutineKey)"/>
        public RoutineIndex(IDbRoutineKey source) : base(source)
        { }

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
    public class RoutineIndexName : DbRoutineKeyName, IRoutineIndexName
    {
        /// <inheritdoc cref="DbRoutineKeyName(IDbRoutineKeyName)"/>
        public RoutineIndexName(IRoutineIndexName source) : base(source) { }
    }
}
