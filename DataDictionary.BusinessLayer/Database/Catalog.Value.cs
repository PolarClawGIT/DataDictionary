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
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

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

    }
}
