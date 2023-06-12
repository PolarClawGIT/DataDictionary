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
    public interface IDbSchemaName: IDbCatalogName
    {
        String? SchemaName { get; }
    }

    public interface IDbSchemaItem : IDbSchemaName, IDbIsSystem
    {
    }

    public class DbSchemaItem : BindingTableRow, IDbSchemaItem, INotifyPropertyChanged
    {
        public String? CatalogName { get { return GetValue("SCHEMA_CATALOG"); } }
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
                    "INFORMATION_SCHEMA";
            }
        }

        internal static IDataReader GetDataReader(IConnection connection)
        {
            SqlCommand getCommand = connection.CreateCommand();
            getCommand.CommandText = "Select Db_Name() As [SCHEMA_CATALOG], [name] As [SCHEMA_NAME] From [Sys].[Schemas]";
            getCommand.CommandType = CommandType.Text;

            return connection.GetReader(getCommand); }
    }
}
