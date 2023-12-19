using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DomainData;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Schema
{
    /// <summary>
    /// Interface for the Database Schema Item
    /// </summary>
    public interface IDbSchemaItem : IDbSchemaKeyName, IDbSchemaKey, IDbCatalogKey, IDbCatalogScopeKey, IDbIsSystem, IDbScopeType, IDataItem
    { }

    /// <summary>
    /// Implementation for the Database Schema Item
    /// </summary>
    [Serializable]
    public class DbSchemaItem : BindingTableRow, IDbSchemaItem, INotifyPropertyChanged, IDbExtendedProperty, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }

        /// <inheritdoc/>
        public Guid? SchemaId { get { return GetValue<Guid>("SchemaId"); } }

        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue("DatabaseName"); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue("SchemaName"); } }

        /// <inheritdoc/>
        public string? ScopeName { get { return GetValue("ScopeName"); } }

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
        public DbCatalogScope CatalogScope { get; } = DbCatalogScope.Schema;

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(string)){ AllowDBNull = true},
            new DataColumn("SchemaId", typeof(string)){ AllowDBNull = true},
            new DataColumn("DatabaseName", typeof(string)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(string)){ AllowDBNull = false},
            new DataColumn("ScopeName", typeof(string)){ AllowDBNull = false},
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
            if (this.ToScopeType().TryScope() is IDbCatalogScopeKey scopeKey)
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
                ex.Data.Add(nameof(ScopeName), ScopeName);
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
