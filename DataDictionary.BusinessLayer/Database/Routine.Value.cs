using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
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
        { return new RoutineIndex(this); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName, SchemaName, RoutineName); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return RoutineName ?? ScopeEnumeration.Cast(Scope).Name; }

        /// <inheritdoc/>
        public Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs)
        { return eventArgs.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(RoutineName); }
    }
}
