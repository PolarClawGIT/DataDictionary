using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IRoutineColumnValue : IRoutineColumnItem,
        IRoutineColumnIndex, IRoutineColumnIndexName, ICatalogIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class RoutineColumnValue : RoutineColumnItem, IRoutineColumnValue, IPathValue, INamedScopeSourceValue
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
                switch (RoutineType)
                {
                    case DbRoutineType.Null: return ScopeType.Null;
                    case DbRoutineType.Function: return ScopeType.DatabaseFunctionParameter;
                    case DbRoutineType.Procedure: return ScopeType.DatabaseProcedureParameter;
                    default: return ScopeType.Null;
                }
            }
        }

        /// <inheritdoc/>
        public RoutineColumnValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new RoutineColumnIndex(this),
                GetPath = () => new PathIndex(DatabaseName, SchemaName, RoutineName, ColumnName),
                GetScope = () => Scope,
                GetTitle = () => ColumnName ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(RoutineName) or nameof(ColumnName),
                IsTitleChanged = (e) => e.PropertyName is nameof(ColumnName)
            };
        }
    }
}
