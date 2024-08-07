using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
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

        /// <inheritdoc/>
        public Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs)
        { return eventArgs.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(RoutineName) or nameof(ParameterName); }
    }
}
