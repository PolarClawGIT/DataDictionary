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
        /// <inheritdoc cref="DbDomainItem()"/>
        public DomainValue() : base()
        { }

        /// <inheritdoc/>
        public IPathValue AsPathValue()
        {
            if (pathValue is null)
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

            return pathValue;
        }
        IPathValue? pathValue; // Backing field for AsPathValue
    }
}
