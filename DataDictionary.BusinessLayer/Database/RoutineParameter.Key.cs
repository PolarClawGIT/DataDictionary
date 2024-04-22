using DataDictionary.DataLayer.DatabaseData.Routine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IRoutineParameterKey : IDbRoutineParameterKey
    { }

    /// <inheritdoc/>
    public class RoutineParameterKey : DbRoutineParameterKey, IRoutineParameterKey
    {
        /// <inheritdoc cref="DbRoutineParameterKey(IDbRoutineParameterKey)"/>
        public RoutineParameterKey(IDbRoutineParameterKey source) : base(source)
        { }
    }

    /// <inheritdoc/>
    public interface IRoutineParameterKeyName : IDbRoutineParameterKeyName
    { }

    /// <inheritdoc/>
    public class RoutineParameterKeyName : DbRoutineParameterKeyName, IRoutineParameterKeyName
    {
        /// <inheritdoc cref="DbRoutineParameterKeyName(IDbRoutineParameterKeyName)"/>
        public RoutineParameterKeyName(IRoutineParameterKeyName source) : base(source) { }
    }
}
