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
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged, IScopeType
    { }

    /// <inheritdoc/>
    public class CatalogValue : CatalogItem, ICatalogValue, IPathValue, INamedScopeSourceValue
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
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new CatalogIndex(this),
                GetPath = () => new PathIndex(DatabaseName),
                GetScope = () => Scope,
                GetTitle = () => CatalogTitle ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DatabaseName),
                IsTitleChanged = (e) => e.PropertyName is nameof(CatalogTitle)
            };
        }

        /// <inheritdoc/>
        internal CatalogValue(IConnection connection) : base (connection)
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new CatalogIndex(this),
                GetPath = () => new PathIndex(DatabaseName),
                GetScope = () => Scope,
                GetTitle = () => CatalogTitle ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DatabaseName),
                IsTitleChanged = (e) => e.PropertyName is nameof(CatalogTitle)
            };
        }
    }
}
