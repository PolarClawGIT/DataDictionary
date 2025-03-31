using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IRoutineValue : IRoutineItem,
        IRoutineIndex, IRoutineIndexName, ICatalogIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class RoutineValue : RoutineItem, IRoutineValue, INamedScopeSourceValue
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
                    case DbRoutineType.Function: return ScopeType.DatabaseFunction;
                    case DbRoutineType.Procedure: return ScopeType.DatabaseProcedure;
                    default: return ScopeType.Null;
                }
            }
        }

        /// <inheritdoc/>
        public RoutineValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new RoutineIndex(this),
                GetPath = () => new PathIndex(DatabaseName, SchemaName, RoutineName),
                GetScope = () => Scope,
                GetTitle = () => RoutineName ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(RoutineName),
                IsTitleChanged = (e) => e.PropertyName is nameof(RoutineName)
            };
        }
    }
}
