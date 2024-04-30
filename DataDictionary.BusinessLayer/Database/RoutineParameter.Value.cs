using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Routine;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IRoutineParameterValue : IDbRoutineParameterItem, IRoutineParameterIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class RoutineParameterValue : DbRoutineParameterItem, IRoutineParameterValue, INamedScopeValue
    {
        /// <inheritdoc cref="DbRoutineParameterItem()"/>
        public RoutineParameterValue() : base()
        { PropertyChanged += CatalogValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeKey GetSystemId()
        { return new NamedScopeKey(ParameterId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName, SchemaName, RoutineName, ParameterName); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return ParameterName ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
        private void CatalogValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(RoutineName) or nameof(ParameterName)
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
