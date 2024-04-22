using DataDictionary.DataLayer.DatabaseData.Routine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IRoutineKey : IDbRoutineKey
    { }

    /// <inheritdoc/>
    public class RoutineKey : DbRoutineKey, IRoutineKey
    {
        /// <inheritdoc cref="DbRoutineKey(IDbRoutineKey)"/>
        public RoutineKey(IDbRoutineKey source) : base(source)
        { }
    }

    /// <inheritdoc/>
    public interface IRoutineKeyName : IDbRoutineKeyName
    { }

    /// <inheritdoc/>
    public class RoutineKeyName : DbRoutineKeyName, IRoutineKeyName
    {
        /// <inheritdoc cref="DbRoutineKeyName(IDbRoutineKeyName)"/>
        public RoutineKeyName(IRoutineKeyName source) : base(source) { }
    }
}
