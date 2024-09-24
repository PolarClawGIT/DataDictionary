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
        /// <inheritdoc cref="DbSchemaItem()"/>
        public SchemaValue() : base()
        { }

        /// <inheritdoc/>
        public IPathValue AsPathValue()
        {
            if (pathValue is null)
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

            return pathValue;
        }
        IPathValue? pathValue; // Backing field for AsPathValue
    }
}
