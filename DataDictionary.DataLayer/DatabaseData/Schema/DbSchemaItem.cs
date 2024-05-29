using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Schema
{
    /// <summary>
    /// Interface for the Database Schema Item
    /// </summary>
    public interface IDbSchemaItem : IDbSchemaKeyName, IDbSchemaKey, IDbCatalogKey, IDbLevelCatalogKey, IDbIsSystem, IScopeKey
    { }

    /// <summary>
    /// Implementation for the Database Schema Item
    /// </summary>
    [Serializable]
    public class DbSchemaItem : BindingTableRow, IDbSchemaItem, INotifyPropertyChanged, IDbExtendedProperty, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>(nameof(CatalogId)); } }

        /// <inheritdoc/>
        public Guid? SchemaId { get { return GetValue<Guid>(nameof(SchemaId)); } }

        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public bool IsSystem
        {
            get
            {
                return SchemaName is "sys" or
                    "db_owner" or
                    "db_accessadmin" or
                    "db_securityadmin" or
                    "db_ddladmin" or
                    "db_backupoperator" or
                    "db_datareader" or
                    "db_datawriter" or
                    "db_denydatareader" or
                    "db_denydatawriter" or
                    "INFORMATION_SCHEMA" or
                    "guest";
            }
        }

        /// <inheritdoc/>
        public DbLevelCatalog CatalogScope { get; } = DbLevelCatalog.Schema;

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.DatabaseSchema;

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(CatalogId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(SchemaId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
        };

        /// <summary>
        /// Constructor for the Database Schema Item
        /// </summary>
        public DbSchemaItem() : base() { }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public virtual Command PropertyCommand(IConnection connection)
        {
            if (this.Scope.ToDbLevel() is IDbLevelCatalogKey scopeKey)
            {
                return new DbExtendedPropertyGetCommand(connection)
                {
                    CatalogId = CatalogId,
                    Level0Name = SchemaName,
                    Level0Type = scopeKey.CatalogScope.ToString(),
                    Level1Name = String.Empty,
                    Level1Type = String.Empty,
                    Level2Name = String.Empty,
                    Level2Type = String.Empty,
                }.GetCommand();
            }
            else
            {
                Exception ex = new InvalidOperationException("Could not determine LevelType");
                ex.Data.Add(nameof(DatabaseName), DatabaseName);
                ex.Data.Add(nameof(SchemaName), SchemaName);
                throw ex;
            }
        }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Schema Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DbSchemaItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new DbSchemaKeyName(this).ToString(); }
    }
}
