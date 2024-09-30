using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ITableColumnValue : IDbTableColumnItem,
        ITableColumnIndex, ITableColumnIndexName, ICatalogIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class TableColumnValue : DbTableColumnItem, ITableColumnValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public TableColumnValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new TableColumnIndex(this),
                GetPath = () => new PathIndex(DatabaseName, SchemaName, TableName, ColumnName),
                GetScope = () => Scope,
                GetTitle = () => ColumnName ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(TableName) or nameof(ColumnName),
                IsTitleChanged = (e) => e.PropertyName is nameof(ColumnName)
            };
        }
    }
}
