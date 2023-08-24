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
    public interface IDbTableItem : IDbTableKey, IDbCatalogKey, IDbObjectScope, IDbIsSystem, IBindingTableRow
    {
        String? TableType { get; }
    }

    public class DbTableItem : BindingTableRow, IDbTableItem, INotifyPropertyChanged, IDbExtendedProperties
    {
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } }
        public String? TableName { get { return GetValue("TableName"); } }
        public String? TableType { get { return GetValue("TableType"); } }
        public Boolean IsSystem { get { return TableName is "__RefactorLog" or "sysdiagrams"; } }
        public DbObjectScope ObjectScope
        {
            get
            {
                if (Enum.TryParse(TableType, true, out DbObjectScope value))
                { return value; }
                else if (TableType is "BASE TABLE" or "HISTORY TABLE" or "TEMPORAL TABLE") { return DbObjectScope.Table; }
                else { return DbObjectScope.NULL; }
            }
        }

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

        public static Command GetData(IConnection connection, IModelKey modelId)
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

        public static Command SetData(IConnection connection, IModelKey modelId, IBindingTable<DbTableItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseTable]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseTable]", source);
            return command;
        }

        public override String ToString()
        { return new DbTableKey(this).ToString(); }
    }

    public static class DbTableItemExtension
    {

        public static DbTableItem? GetTable(this IEnumerable<DbTableItem> source, IDbTableKey item)
        { return source.FirstOrDefault(w => new DbTableKey(item) == new DbTableKey(w)); }

        public static DbTableItem? GetTable(this IDbTableKey item, IEnumerable<DbTableItem> source)
        { return source.FirstOrDefault(w => new DbTableKey(item) == new DbTableKey(w)); }

        public static IEnumerable<DbTableItem> GetTables(this IEnumerable<DbTableItem> source, IDbSchemaKey item)
        { return source.Where(w => new DbSchemaKey(item) == new DbSchemaKey(w)); }

        public static IEnumerable<DbTableItem> GetTables(this IDbSchemaKey item, IEnumerable<DbTableItem> source)
        { return source.Where(w => new DbSchemaKey(item) == new DbSchemaKey(w)); }

        public static IEnumerable<DbTableItem> GetTables(this IEnumerable<DbTableItem> source, IDbCatalogKeyUnique item)
        { return source.Where(w => new DbCatalogKeyUnique(item) == new DbCatalogKeyUnique(w)); }

        public static IEnumerable<DbTableItem> GetTables(this IDbCatalogKeyUnique item, IEnumerable<DbTableItem> source)
        { return source.Where(w => new DbCatalogKeyUnique(item) == new DbCatalogKeyUnique(w)); }
    }
}
