using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IDomainValue : IDbDomainItem,
        IDomainIndex, IDomainIndexName, ICatalogIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class DomainValue : DbDomainItem, IDomainValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

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
