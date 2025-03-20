using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IConstraintValue : IConstraintItem,
        IConstraintIndex, IConstraintIndexName, ICatalogIndex, ITableIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class ConstraintValue : ConstraintItem, IConstraintValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }
        
        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.DatabaseConstraint;

        /// <inheritdoc/>
        public ConstraintValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new ConstraintIndex(this),
                GetPath = () => new PathIndex(DatabaseName, SchemaName, ConstraintName),
                GetScope = () => Scope,
                GetTitle = () => ConstraintName ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(ConstraintName),
                IsTitleChanged = (e) => e.PropertyName is nameof(ConstraintName)
            };
        }
    }
}
