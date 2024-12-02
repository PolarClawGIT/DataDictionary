using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IRoutineParameterIndexName : IRoutineParameterKeyName, IRoutineIndexName
    { }

    /// <inheritdoc/>
    public class RoutineParameterIndexName : RoutineParameterKeyName, IRoutineParameterIndexName,
        IKeyEquality<IRoutineParameterIndexName>, IKeyEquality<RoutineParameterIndexName>
    {
        /// <inheritdoc cref="RoutineParameterKeyName(IRoutineParameterKeyName)"/>
        public RoutineParameterIndexName(IRoutineParameterIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IRoutineParameterIndexName? other)
        { return other is IRoutineParameterKeyName value && Equals(new RoutineParameterKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(RoutineParameterIndexName? other)
        { return other is IRoutineParameterKeyName value && Equals(new RoutineParameterKeyName(value)); }

        /// <summary>
        /// Convert DomainIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(RoutineParameterIndexName source)
        { return new DataIndexName() { Title = source.ParameterName ?? String.Empty }; }
    }
}
