using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IRoutineParameterValue : IDbRoutineParameterItem, IRoutineParameterIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class RoutineParameterValue : DbRoutineParameterItem, IRoutineParameterValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="DbRoutineParameterItem()"/>
        public RoutineParameterValue() : base()
        {  }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new RoutineParameterIndex(this); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName, SchemaName, RoutineName, ParameterName); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return ParameterName ?? ScopeEnumeration.Cast(Scope).Name; }
    }
}
