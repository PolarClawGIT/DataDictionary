using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.DataLayer.DomainData;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbTableItem : IDbTableName, IDbIsSystem, IBindingTableRow
    {
        //Guid? CatalogId { get; }
        String? TableType { get; }
    }

    public class DbTableItem : BindingTableRow, IDbTableItem, INotifyPropertyChanged, IDbExtendedProperties
    {
        //public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } }
        public String? TableName { get { return GetValue("TableName"); } }
        public String? TableType { get { return GetValue("TableType"); } }
        public Boolean IsSystem { get { return TableName is "__RefactorLog" or "sysdiagrams"; } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(String)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(String)){ AllowDBNull = false},
            new DataColumn("TableName", typeof(String)){ AllowDBNull = false},
            new DataColumn("TableType", typeof(String)){ AllowDBNull = false},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetSchema(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbTableItem;
            return command;
        }

        public virtual Command GetProperties(IConnection connection)
        {
            return (new DbExtendedPropertyGetCommand(connection)
            { Level0Name = SchemaName, Level0Type = "SCHEMA", Level1Name = TableName, Level1Type = "TABLE" }).
            GetCommand();
        }

        public static Command GetData(IConnection connection, IModelIdentifier modelId)
        { return GetData(connection, (modelId.ModelId, null, null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, String? catalogName, String? schemaName, String? tableName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseTable]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@TableName", parameters.tableName);
            return command;
        }

        public static Command SetData(IConnection connection, IModelIdentifier modelId, IBindingTable<DbTableItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseTable]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseTable]", source);
            return command;
        }

        public override String ToString()
        { return new DbTableName(this).ToString(); }
    }

    public static class DbTableItemExtension
    {

        public static DbTableItem? GetTable(this IEnumerable<DbTableItem> source, IDbTableName item)
        {
            DbTableName itemName = new DbTableName(item);
            return source.FirstOrDefault(w => itemName == w);
        }

        public static DbTableItem? GetTable(this IDbTableName item, IEnumerable<DbTableItem> source)
        {
            DbTableName itemName = new DbTableName(item);
            return source.FirstOrDefault(w => itemName == w);
        }

        public static IEnumerable<DbTableItem> GetTables(this IEnumerable<DbTableItem> source, IDbSchemaName item)
        {
            DbSchemaName itemName = new DbSchemaName(item);
            return source.Where(w => itemName == w);
        }

        public static IEnumerable<DbTableItem> GetTables(this IDbSchemaName item, IEnumerable<DbTableItem> source)
        {
            DbSchemaName itemName = new DbSchemaName(item);
            return source.Where(w => itemName == w);
        }

        public static IEnumerable<DbTableItem> GetTables(this IEnumerable<DbTableItem> source, IDbCatalogName item)
        {
            DbCatalogName itemName = new DbCatalogName(item);
            return source.Where(w => itemName == w);
        }

        public static IEnumerable<DbTableItem> GetTables(this IDbCatalogName item, IEnumerable<DbTableItem> source)
        {
            DbCatalogName itemName = new DbCatalogName(item);
            return source.Where(w => itemName == w);
        }
    }
}
