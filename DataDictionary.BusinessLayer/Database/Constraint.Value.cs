using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IConstraintValue : IDbConstraintItem,
        IConstraintIndex, IConstraintIndexName, ICatalogIndex, ITableIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class ConstraintValue : DbConstraintItem, IConstraintValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

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
