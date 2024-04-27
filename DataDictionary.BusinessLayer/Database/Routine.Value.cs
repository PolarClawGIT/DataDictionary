using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Routine;
<<<<<<< HEAD
using System.ComponentModel;
=======
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> RenameIndexValue
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IRoutineValue : IDbRoutineItem, IRoutineIndex, IRoutineIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class RoutineValue : DbRoutineItem, IRoutineValue, INamedScopeValue
    {
        /// <inheritdoc cref="DbRoutineItem()"/>
        public RoutineValue() : base()
<<<<<<< HEAD
        { PropertyChanged += OnPropertyChanged; }

        /// <inheritdoc/>
        public NamedScopeKey GetSystemId()
=======
        { PropertyChanged += CatalogValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeKey GetSystemId()
>>>>>>> RenameIndexValue
        { return new NamedScopeKey(RoutineId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName, SchemaName, RoutineName); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return RoutineName ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
<<<<<<< HEAD
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(SchemaName) or nameof(DatabaseName) or nameof(RoutineName)
=======
        private void CatalogValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(RoutineName)
>>>>>>> RenameIndexValue
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
