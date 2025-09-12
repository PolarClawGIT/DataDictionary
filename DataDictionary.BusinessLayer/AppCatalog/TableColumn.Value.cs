using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface ITableColumnValue : ITableColumnItem,
        ITableColumnIndex, ITableColumnIndexName, ICatalogIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class TableColumnValue : TableColumnItem, ITableColumnValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope
        {
            get
            {
                switch (TableType)
                {
                    case DbTableType.Null: return ScopeType.Null;
                    case DbTableType.Table: return ScopeType.DatabaseTableColumn;
                    case DbTableType.TemporalTable: return ScopeType.DatabaseTableColumn;
                    case DbTableType.HistoryTable: return ScopeType.DatabaseTableColumn;
                    case DbTableType.View: return ScopeType.DatabaseViewColumn;
                    default: return ScopeType.Null;
                }
            }
        }

        /// <inheritdoc/>
        public TableColumnValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new TableColumnIndex(this),
                GetPath = () => new PathIndex(DatabaseName, SchemaName, TableName, ColumnName),
                GetScope = () => Scope,
                GetTitle = () => ColumnName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(TableName) or nameof(ColumnName),
                IsTitleChanged = (e) => e.PropertyName is nameof(ColumnName)
            };
        }
    }
}
