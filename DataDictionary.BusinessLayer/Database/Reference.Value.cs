using DataDictionary.BusinessLayer.NamedScope;
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
public class ReferenceValue : DbReferenceItem, IReferenceValue, INamedScopeSourceValue
{
    /// <inheritdoc/>
    public ScopeType Scope { get { return ScopeType.DatabaseDependency; } }

    /// <inheritdoc/>
    public DataLayerIndex GetIndex()
    { return new ReferenceIndex(this); }

    /// <inheritdoc/>
    public NamedScopePath GetPath()
    { return new NamedScopePath(ReferencedDatabaseName, ReferencedSchemaName, ReferencedObjectName, ReferencedColumnName); }

    /// <inheritdoc/>
    public String GetTitle()
    { return ReferencedColumnName ?? ReferencedObjectName ?? ScopeEnumeration.Cast(Scope).Name; }

    /// <inheritdoc/>
    public Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs)
    {
        return eventArgs.PropertyName is
            nameof(ReferencedDatabaseName) or
            nameof(ReferencedSchemaName) or
            nameof(ReferencedObjectName) or
            nameof(ReferencedColumnName);
    }
}
