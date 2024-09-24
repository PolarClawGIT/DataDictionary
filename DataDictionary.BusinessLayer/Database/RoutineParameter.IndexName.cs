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
    public interface IRoutineParameterIndexName : IDbRoutineParameterKeyName, IRoutineIndexName
    { }

    /// <inheritdoc/>
    public class RoutineParameterIndexName : DbRoutineParameterKeyName, IRoutineParameterIndexName,
        IKeyEquality<IRoutineParameterIndexName>, IKeyEquality<RoutineParameterIndexName>
    {
        /// <inheritdoc cref="DbRoutineParameterKeyName(IDbRoutineParameterKeyName)"/>
        public RoutineParameterIndexName(IRoutineParameterIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IRoutineParameterIndexName? other)
        { return other is IDbRoutineParameterKeyName value && Equals(new DbRoutineParameterKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(RoutineParameterIndexName? other)
        { return other is IDbRoutineParameterKeyName value && Equals(new DbRoutineParameterKeyName(value)); }

        /// <summary>
        /// Convert DomainIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(RoutineParameterIndexName source)
        { return new DataIndexName() { Title = source.ParameterName ?? String.Empty }; }
    }
}
