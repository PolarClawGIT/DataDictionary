using DataDictionary.BusinessLayer.AppProperty;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IPropertyValue : IPropertyItem,
        IPropertyIndex, IPropertyIndexObject, ICatalogIndex, IScopeType,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class PropertyValue : PropertyItem, IPropertyValue, IPathValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.DatabaseProperty;

        /// <inheritdoc/>
        public PropertyValue() : base()
        { pathValue = CreatePath(); }

        PathValue CreatePath()
        {
            return new PathValue(this)
            {
                GetIndex = () => new PropertyIndex(this),
                GetPath = () => new PathIndex(DatabaseName, PropertyName),
                GetScope = () => Scope,
                GetTitle = () => PropertyName ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DatabaseName) or nameof(PropertyName),
                IsTitleChanged = (e) => e.PropertyName is nameof(PropertyName)
            };
        }
    }
}
