using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IRoutineColumnIndexName : IRoutineColumnKeyName, IRoutineIndexName
    { }

    /// <inheritdoc/>
    public class RoutineColumnIndexName : RoutineColumnKeyName, IRoutineColumnIndexName,
        IKeyEquality<IRoutineColumnIndexName>, IKeyEquality<RoutineColumnIndexName>
    {
        /// <inheritdoc cref="RoutineColumnKeyName(IRoutineColumnKeyName)"/>
        public RoutineColumnIndexName(IRoutineColumnIndexName source) : base(source) { }

        /// <inheritdoc cref="RoutineColumnKeyName(IRoutineColumnKeyName)"/>
        public RoutineColumnIndexName(IRoutineColumnKeyName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IRoutineColumnIndexName? other)
        { return other is IRoutineColumnKeyName value && Equals(new RoutineColumnKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(RoutineColumnIndexName? other)
        { return other is IRoutineColumnKeyName value && Equals(new RoutineColumnKeyName(value)); }

        /// <summary>
        /// Convert RoutineColumnIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(RoutineColumnIndexName source)
        { return new DataIndexName() { Title = source.ColumnName ?? String.Empty }; }
    }
}
