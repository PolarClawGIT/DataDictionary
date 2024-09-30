using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Reference;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database;

/// <inheritdoc/>
public interface IReferenceValue : IDbReferenceItem,
    IReferenceIndex, IReferencedIndexObject, IReferencedIndexColumn, ICatalogIndex,
    IBindingTableRow, IBindingRowState, IBindingPropertyChanged
{ }

/// <inheritdoc/>
public class ReferenceValue : DbReferenceItem, IReferenceValue, IPathValue, INamedScopeSourceValue
{
    /// <inheritdoc/>
    public ScopeType Scope { get { return ScopeType.DatabaseDependency; } }

    IPathValue pathValue; // Backing field for IPathValue

    /// <inheritdoc/>
    PathIndex IPathIndex.Path { get { return pathValue.Path; } }

    /// <inheritdoc/>
    DataIndex IDataValue.Index { get { return pathValue.Index; } }

    /// <inheritdoc/>
    String IDataValue.Title { get { return pathValue.Title; } }

    /// <inheritdoc/>
    public ReferenceValue() : base ()
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
