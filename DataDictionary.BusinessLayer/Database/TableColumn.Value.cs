using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Table;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ITableColumnValue : IDbTableColumnItem, ITableColumnIndex, ITableColumnIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class TableColumnValue : DbTableColumnItem, ITableColumnValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="DbTableItem()"/>
        public TableColumnValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new DataLayerIndex(ColumnId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName, SchemaName, TableName, ColumnName); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return ColumnName ?? Scope.ToName(); }
    }
}
