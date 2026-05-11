using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface ICatalogValue : ICatalogItem, ICatalogIndex, ICatalogIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged,
        IScopeType, ITemporal, IAuthorization
    { }

    /// <inheritdoc/>
    public class CatalogValue : CatalogItem, ICatalogValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.Database;

        /// <inheritdoc/>
        public CatalogValue() : base()
        { pathValue = CreatePath(); }

        PathValue CreatePath()
        {
            return new PathValue(this)
            {
                GetIndex = () => new CatalogIndex(this),
                GetPath = () => new PathIndex(DatabaseName),
                GetScope = () => Scope,
                GetTitle = () => CatalogTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DatabaseName),
                IsTitleChanged = (e) => e.PropertyName is nameof(CatalogTitle)
            };
        }

        /// <inheritdoc/>
        public (Boolean IsAdmin, Boolean IsOwner, Boolean IsGrant) GetAuthorization(IAuthorizationData authorizations)
        { return new CatalogIndex(this).GetAuthorization(authorizations); }
    }
}
