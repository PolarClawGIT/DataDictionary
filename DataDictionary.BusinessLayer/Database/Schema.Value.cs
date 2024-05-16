using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Schema;
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
        { return new DataLayerIndex(SchemaId); }

        /// <inheritdoc/>
        public NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName, SchemaName); }

        /// <inheritdoc/>
        public String GetTitle()
        { return SchemaName ?? Scope.ToName(); }
    }
}
