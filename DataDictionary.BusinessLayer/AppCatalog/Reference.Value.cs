using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IReferenceValue : IReferenceItem,
        IReferenceIndex, IReferencedIndexObject, IReferencedIndexColumn, ICatalogIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class ReferenceValue : ReferenceItem, IReferenceValue, INamedScopeSourceValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.DatabaseReference; } }

        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ReferenceValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new ReferenceIndex(this),
                GetPath = () => new PathIndex(ReferencedDatabaseName, ReferencedSchemaName, ReferencedObjectName, ReferencedColumnName),
                GetScope = () => Scope,
                GetTitle = () => ReferencedColumnName ?? ReferencedObjectName ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(ReferencedDatabaseName) or nameof(ReferencedSchemaName) or nameof(ReferencedObjectName) or nameof(ReferencedColumnName),
                IsTitleChanged = (e) => e.PropertyName is nameof(ReferencedObjectName) or nameof(ReferencedColumnName)
            };
        }
    }
}
