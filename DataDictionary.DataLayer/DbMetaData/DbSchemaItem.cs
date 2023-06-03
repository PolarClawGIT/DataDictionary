using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DbMetaData
{
    public class DbSchemaItem : BindingTableRow
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
                    "db_denydatawriter";
            }
        }

        public static BindingTable<DbSchemaItem> Create(Func<IDataReader> dataReader)
        {
            if (dataReader is null) { throw new ArgumentNullException(nameof(dataReader)); }

            BindingTable<DbSchemaItem> result = new BindingTable<DbSchemaItem>();
            result.Load(dataReader());

            return result;
        }
    }
}
