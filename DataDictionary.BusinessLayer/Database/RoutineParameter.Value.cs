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
<<<<<<< HEAD
        /// <inheritdoc cref="DbRoutineItem()"/>
        public RoutineParameterValue() : base()
        { PropertyChanged += OnPropertyChanged; }

        /// <inheritdoc/>
        public NamedScopeKey GetSystemId()
=======
        /// <inheritdoc cref="DbRoutineParameterItem()"/>
        public RoutineParameterValue() : base()
        { PropertyChanged += CatalogValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeKey GetSystemId()
>>>>>>> RenameIndexValue
        { return new NamedScopeKey(ParameterId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName, SchemaName, RoutineName, ParameterName); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return ParameterName ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
<<<<<<< HEAD
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(SchemaName) or nameof(DatabaseName) or nameof(RoutineName) or nameof(ParameterName)
=======
        private void CatalogValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(RoutineName) or nameof(ParameterName)
>>>>>>> RenameIndexValue
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
