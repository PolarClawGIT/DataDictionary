using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Routine;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IRoutineValue : IDbRoutineItem, IRoutineIndex, IRoutineIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class RoutineValue : DbRoutineItem, IRoutineValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="DbRoutineItem()"/>
        public RoutineValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new DataLayerIndex(RoutineId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName, SchemaName, RoutineName); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return RoutineName ?? Scope.ToName(); }
    }
}
