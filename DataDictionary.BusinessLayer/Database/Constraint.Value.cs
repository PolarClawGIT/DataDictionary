using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IConstraintValue : IDbConstraintItem, IConstraintIndex, IConstraintIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class ConstraintValue : DbConstraintItem, IConstraintValue, INamedScopeValue
    {
        /// <inheritdoc cref="DbConstraintItem()"/>
        public ConstraintValue() : base()
<<<<<<< HEAD
        { PropertyChanged += OnPropertyChanged; }

        /// <inheritdoc/>
        public NamedScopeKey GetSystemId()
=======
        { PropertyChanged += CatalogValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeKey GetSystemId()
>>>>>>> RenameIndexValue
        { return new NamedScopeKey(ConstraintId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
<<<<<<< HEAD
        { return new NamedScopePath(DatabaseName, SchemaName, TableName, ConstraintName); }
=======
        { return new NamedScopePath(DatabaseName, SchemaName, ConstraintName); }
>>>>>>> RenameIndexValue

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return ConstraintName ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
<<<<<<< HEAD
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(SchemaName) or nameof(DatabaseName) or nameof(TableName) or nameof(ConstraintName)
=======
        private void CatalogValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(ConstraintName)
>>>>>>> RenameIndexValue
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
