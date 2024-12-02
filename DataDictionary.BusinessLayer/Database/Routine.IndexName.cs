using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IRoutineIndexName : IRoutineKeyName, ISchemaIndexName
    { }

    /// <inheritdoc/>
    public class RoutineIndexName : RoutineKeyName, IRoutineIndexName,
        IKeyEquality<IRoutineIndexName>, IKeyEquality<RoutineIndexName>
    {
        /// <inheritdoc cref="RoutineKeyName(IRoutineKeyName)"/>
        public RoutineIndexName(IRoutineIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IRoutineIndexName? other)
        { return other is IRoutineKeyName value && Equals(new RoutineKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(RoutineIndexName? other)
        { return other is IRoutineKeyName value && Equals(new RoutineKeyName(value)); }

        /// <summary>
        /// Convert RoutineIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(RoutineIndexName source)
        { return new DataIndexName() { Title = source.RoutineName ?? String.Empty }; }
    }
}
