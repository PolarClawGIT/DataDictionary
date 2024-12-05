using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface ITableValue : ITableItem,
        ITableIndex, ITableIndexName, ICatalogIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged,
        IScopeType, ITemporalValue
    { }

    /// <inheritdoc/>
    public class TableValue : TableItem, ITableValue, INamedScopeSourceValue
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
                    case DbTableType.Table: return ScopeType.DatabaseTable;
                    case DbTableType.TemporalTable: return ScopeType.DatabaseTable;
                    case DbTableType.HistoryTable: return ScopeType.DatabaseTable;
                    case DbTableType.View: return ScopeType.DatabaseView;
                    default: return ScopeType.Null;
                }
            }
        }

        /// <inheritdoc/>
        public TableValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new TableIndex(this),
                GetPath = () => new PathIndex(DatabaseName, SchemaName, TableName),
                GetScope = () => Scope,
                GetTitle = () => TableName ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(TableName),
                IsTitleChanged = (e) => e.PropertyName is nameof(TableName)
            };
        }
    }
}
