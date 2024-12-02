using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IRoutineParameterValue : IRoutineParameterItem, IRoutineParameterIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class RoutineParameterValue : RoutineParameterItem, IRoutineParameterValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public RoutineParameterValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new RoutineParameterIndex(this),
                GetPath = () => new PathIndex(DatabaseName, SchemaName, RoutineName, ParameterName),
                GetScope = () => Scope,
                GetTitle = () => ParameterName ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(RoutineName) or nameof(ParameterName),
                IsTitleChanged = (e) => e.PropertyName is nameof(ParameterName)
            };
        }
    }
}
