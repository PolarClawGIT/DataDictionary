using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource.Enumerations;
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
        { return new TableIndex(this); }

        /// <inheritdoc/>
        public NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName, SchemaName, TableName); }

        /// <inheritdoc/>
        public String GetTitle()
        { return TableName ?? ScopeEnumeration.Cast(Scope).Name; }
    }
}
