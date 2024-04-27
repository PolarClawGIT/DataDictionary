using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Table;
<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
using System.ComponentModel;
>>>>>>> RenameIndexValue
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ITableColumnValue : IDbTableColumnItem, ITableColumnIndex, ITableColumnIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class TableColumnValue : DbTableColumnItem, ITableColumnValue, INamedScopeValue
    {
<<<<<<< HEAD
        /// <inheritdoc cref="DbTableColumnItem()"/>
        public TableColumnValue() : base()
        { PropertyChanged += OnPropertyChanged; }

        /// <inheritdoc/>
        public NamedScopeKey GetSystemId()
=======
        /// <inheritdoc cref="DbTableItem()"/>
        public TableColumnValue() : base()
        { PropertyChanged += CatalogValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeKey GetSystemId()
>>>>>>> RenameIndexValue
        { return new NamedScopeKey(ColumnId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName, SchemaName, TableName, ColumnName); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return TableName ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
<<<<<<< HEAD
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(SchemaName) or nameof(DatabaseName) or nameof(TableName) or nameof(ColumnName)
=======
        private void CatalogValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(TableName) or nameof(ColumnName)
>>>>>>> RenameIndexValue
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }
    }
}
