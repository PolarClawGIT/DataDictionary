using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ICatalogValue : IDbCatalogItem, ICatalogIndex, ICatalogIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class CatalogValue : DbCatalogItem, ICatalogValue, IPathValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="DbCatalogItem()"/>
        public CatalogValue() : base() { }

        /// <inheritdoc/>
        public IPathValue AsPathValue()
        {
            if (pathValue is null)
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

            return pathValue;
        }
        IPathValue? pathValue; // Backing field for AsPathValue
    }
}
