using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IConstraintColumnValue : IConstraintColumnItem,
        IConstraintIndexName, ITableColumnIndexName, IConstraintColumnIndexName, IConstraintColumnIndexReferenced,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class ConstraintColumnValue : ConstraintColumnItem, IConstraintColumnValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.DatabaseConstraintColumn;

        /// <inheritdoc/>
        public ConstraintColumnValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new ConstraintColumnIndex(this),
                GetPath = () => new PathIndex(DatabaseName, SchemaName, ConstraintName, ColumnName),
                GetScope = () => Scope,
                GetTitle = () => ColumnName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(ConstraintName) or nameof(ColumnName),
                IsTitleChanged = (e) => e.PropertyName is nameof(ColumnName)
            };
        }
    }
}
