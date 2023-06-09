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
    public interface IDbSchemaItem
    {
        String? CatalogName { get; }
        String? SchemaName { get; }
        Boolean IsSystem { get; }
    }

    public class DbSchemaItem : BindingTableRow, IDbSchemaItem
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
        { return connection.GetSchema(Schema.Collection.Schemas); }
    }
}
