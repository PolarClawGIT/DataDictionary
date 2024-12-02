using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IRoutineValue : IRoutineItem, 
        IRoutineIndex, IRoutineIndexName, ICatalogIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class RoutineValue : RoutineItem, IRoutineValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

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
