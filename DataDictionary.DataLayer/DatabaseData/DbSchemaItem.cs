using DataDictionary.DataLayer.ApplicationData;
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

namespace DataDictionary.DataLayer.DatabaseData
{
    public interface IDbSchemaItem : IDbSchemaKey, IDbCatalogKey, IDbCatalogScope, IDbIsSystem, IBindingTableRow
    { }

    [Serializable]
    public class DbSchemaItem : BindingTableRow, IDbSchemaItem, INotifyPropertyChanged, IDbExtendedProperties, ISerializable
    {
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } }
        public Boolean IsSystem
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

        public DbCatalogScope CatalogScope { get; } = DbCatalogScope.Schema;

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(String)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(String)){ AllowDBNull = false},
        };

        public DbSchemaItem() : base() { }

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetSchema(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbSchemaItem;
            return command;
        }

        public virtual Command GetProperties(IConnection connection)
        {
            return (new DbExtendedPropertyGetCommand(connection)
            { Level0Name = SchemaName, Level0Type = "SCHEMA" }).
            GetCommand();
        }

        public static Command GetData(IConnection connection, IModelKey modelId)
        { return GetData(connection, (modelId.ModelId, null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, String? catalogName, String? schemaName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseSchema]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            return command;
        }

        public static Command SetData(IConnection connection, IModelKey modelId, IBindingTable<DbSchemaItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseSchema]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseSchema]", source);
            return command;
        }

        #region ISerializable
        protected DbSchemaItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        public override String ToString()
        { return new DbSchemaKey(this).ToString(); }
    }

    public static class DbSchemaItemExtension
    {
        public static DbSchemaItem? GetSchema(this IEnumerable<DbSchemaItem> source, IDbSchemaKey item)
        { return source.FirstOrDefault(w => new DbSchemaKey(item) == new DbSchemaKey(w)); }

        public static DbSchemaItem? GetSchema(this IDbSchemaKey item, IEnumerable<DbSchemaItem> source)
        { return source.FirstOrDefault(w => new DbSchemaKey(item) == new DbSchemaKey(w)); }

        public static IEnumerable<DbSchemaItem> GetSchemta(this IEnumerable<DbSchemaItem> source, IDbCatalogKeyUnique item)
        { return source.Where(w => new DbCatalogKeyUnique(item) == new DbCatalogKeyUnique(w)); }

        public static IEnumerable<DbSchemaItem> GetSchemta(this IDbCatalogKeyUnique item, IEnumerable<DbSchemaItem> source)
        { return source.Where(w => new DbCatalogKeyUnique(item) == new DbCatalogKeyUnique(w)); }
    }
}
