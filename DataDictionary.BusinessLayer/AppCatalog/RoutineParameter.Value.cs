using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IRoutineParameterValue : IRoutineParameterItem, IRoutineParameterIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class RoutineParameterValue : RoutineParameterItem, IRoutineParameterValue, INamedScopeSourceValue
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
        public RoutineParameterValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new RoutineParameterIndex(this),
                GetPath = () => new PathIndex(DatabaseName, SchemaName, RoutineName, ParameterName),
                GetScope = () => Scope,
                GetTitle = () => ParameterName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(RoutineName) or nameof(ParameterName),
                IsTitleChanged = (e) => e.PropertyName is nameof(ParameterName)
            };
        }
    }
}
