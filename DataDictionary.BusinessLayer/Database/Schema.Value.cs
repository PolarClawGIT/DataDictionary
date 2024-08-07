using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ISchemaValue : IDbSchemaItem, ISchemaIndex, ISchemaIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class SchemaValue : DbSchemaItem, ISchemaValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="DbSchemaItem()"/>
        public SchemaValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new SchemaIndex(this); }

        /// <inheritdoc/>
        public NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName, SchemaName); }

        /// <inheritdoc/>
        public String GetTitle()
        { return SchemaName ?? ScopeEnumeration.Cast(Scope).Name; }

        /// <inheritdoc/>
        public Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs)
        { return eventArgs.PropertyName is nameof(DatabaseName) or nameof(SchemaName); }

    }
}
