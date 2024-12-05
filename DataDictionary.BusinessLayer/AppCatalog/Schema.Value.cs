using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface ISchemaValue : ISchemaItem,
        ISchemaIndex, ISchemaIndexName, ICatalogIndex, 
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged,
        IScopeType, ITemporalValue
    { }

    /// <inheritdoc/>
    public class SchemaValue : SchemaItem, ISchemaValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.DatabaseSchema;

        /// <inheritdoc/>
        public SchemaValue() : base()
        { pathValue = CreatePath(); }

        PathValue CreatePath()
        {
            return new PathValue(this)
            {
                GetIndex = () => new SchemaIndex(this),
                GetPath = () => new PathIndex(DatabaseName, SchemaName),
                GetScope = () => Scope,
                GetTitle = () => SchemaName ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DatabaseName) or nameof(SchemaName),
                IsTitleChanged = (e) => e.PropertyName is nameof(SchemaName)
            };
        }

    }
}
