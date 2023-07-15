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
    public interface IDbSchemaItem : IDbSchemaName, IDbIsSystem
    {
        //Guid? CatalogId { get; }
    }
    
    public class DbSchemaItem : BindingTableRow, IDbSchemaItem, INotifyPropertyChanged, IDbExtendedProperties
    {
        //public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("CATALOG_NAME"); } }
        public String? SchemaName { get { return GetValue("SCHEMA_NAME"); } }
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

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(String)){ AllowDBNull = true},
            new DataColumn("CATALOG_NAME", typeof(String)){ AllowDBNull = false},
            new DataColumn("SCHEMA_NAME", typeof(String)){ AllowDBNull = false},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions ()
        { return columnDefinitions; }

        public static IDataReader GetSchema(IConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbSchemaItem;

            return connection.ExecuteReader(command);
        }

        public virtual SqlCommand GetProperties(IConnection connection)
        {
            return (new DbExtendedPropertyGetCommand(connection)
            { Level0Name = SchemaName, Level0Type = "SCHEMA" }).
            GetCommand();
        }
    }

    public static class DbSchemaItemExtension
    {
        public static DbSchemaItem? GetSchema(this IEnumerable<DbSchemaItem> source, IDbSchemaName item)
        {
            DbSchemaName itemName = new DbSchemaName(item);
            return source.FirstOrDefault(w => itemName == w);
        }

        public static DbSchemaItem? GetSchema(this IDbSchemaName item, IEnumerable<DbSchemaItem> source)
        {
            DbSchemaName itemName = new DbSchemaName(item);
            return source.FirstOrDefault(w => itemName == w);
        }

        public static IEnumerable<DbSchemaItem> GetSchemta(this IEnumerable<DbSchemaItem> source, IDbCatalogName item)
        {
            DbCatalogName itemName = new DbCatalogName(item);
            return source.Where(w => itemName == w);
        }

        public static IEnumerable<DbSchemaItem> GetSchemta(this IDbCatalogName item, IEnumerable<DbSchemaItem> source)
        {
            DbCatalogName itemName = new DbCatalogName(item);
            return source.Where(w => itemName == w);
        }
    }
}
