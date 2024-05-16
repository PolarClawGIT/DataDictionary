using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Table;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ITableValue : IDbTableItem, ITableIndex, ITableIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class TableValue : DbTableItem, ITableValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="DbTableItem()"/>
        public TableValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new DataLayerIndex(TableId); }

        /// <inheritdoc/>
        public NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName, SchemaName, TableName); }

        /// <inheritdoc/>
        public String GetTitle()
        { return TableName ?? Scope.ToName(); }
    }
}
