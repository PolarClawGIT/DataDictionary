using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ISchemaValue : IDbSchemaItem,
        ISchemaIndex, ISchemaIndexName, ICatalogIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class SchemaValue : DbSchemaItem, ISchemaValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public SchemaValue() : base()
        {
            pathValue = new PathValue(this)
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
