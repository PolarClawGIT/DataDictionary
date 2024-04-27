using DataDictionary.DataLayer.DatabaseData.Routine;
<<<<<<< HEAD
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> RenameIndexValue

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IRoutineParameterIndex : IDbRoutineParameterKey
    { }

    /// <inheritdoc/>
    public class RoutineParameterIndex : DbRoutineParameterKey, IRoutineParameterIndex
    {
        /// <inheritdoc cref="DbRoutineParameterKey(IDbRoutineParameterKey)"/>
        public RoutineParameterIndex(IDbRoutineParameterKey source) : base(source)
        { }
    }

    /// <inheritdoc/>
    public interface IRoutineParameterIndexName : IDbRoutineParameterKeyName, IRoutineIndexName
    { }

    /// <inheritdoc/>
    public class RoutineParameterKeyName : DbRoutineParameterKeyName, IRoutineParameterIndexName
    {
        /// <inheritdoc cref="DbRoutineParameterKeyName(IDbRoutineParameterKeyName)"/>
        public RoutineParameterKeyName(IRoutineParameterIndexName source) : base(source) { }
    }
}
