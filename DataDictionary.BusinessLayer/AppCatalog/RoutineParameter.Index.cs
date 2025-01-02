using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IRoutineParameterIndex : IRoutineParameterKey
    { }

    /// <inheritdoc/>
    public class RoutineParameterIndex : RoutineParameterKey, IRoutineParameterIndex,
        IKeyEquality<IRoutineParameterIndex>, IKeyEquality<RoutineParameterIndex>
    {
        /// <inheritdoc cref="RoutineParameterKey(IRoutineParameterKey)"/>
        public RoutineParameterIndex(IRoutineParameterKey source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(IRoutineParameterIndex? other)
        { return other is IRoutineParameterKey value && Equals(new RoutineParameterKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(RoutineParameterIndex? other)
        { return other is IRoutineParameterKey value && Equals(new RoutineParameterKey(value)); }

        /// <summary>
        /// Convert RoutineParameterIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(RoutineParameterIndex source)
        { return new DataIndex() { SystemId = source.RoutineParameterId ?? Guid.Empty }; }
    }


}
