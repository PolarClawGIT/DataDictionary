using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
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

        /// <summary>
        /// Convert RoutineIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(RoutineIndexName source)
        { return new DataIndexName() { Title = source.RoutineName ?? String.Empty }; }
    }
}
