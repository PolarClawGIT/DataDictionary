using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IDomainValue : IDomainItem,
        IDomainIndex, IDomainIndexName, ICatalogIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class DomainValue : DomainItem, IDomainValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.DatabaseDomain;

        /// <inheritdoc/>
        public DomainValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DomainIndex(this),
                GetPath = () => new PathIndex(DatabaseName, SchemaName, DomainName),
                GetScope = () => Scope,
                GetTitle = () => DomainName ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DatabaseName) or nameof(SchemaName) or nameof(DomainName),
                IsTitleChanged = (e) => e.PropertyName is nameof(DomainName)
            };
        }
    }
}
