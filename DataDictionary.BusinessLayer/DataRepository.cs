using DataDictionary.DataLayer.DbMetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.BusinessLayer
{
    public class DataRepository
    {
        BindingTable<DbSchemaItem> dbSchemas = new BindingTable<DbSchemaItem>();
        BindingTable<DbTableItem> dbTables = new BindingTable<DbTableItem>();
        BindingTable<DbColumnItem> dbColumns = new BindingTable<DbColumnItem>();


        public void Load()
        {
            using (IConnection connection = BusinessContext.Instance.DbContext.CreateConnection())
            {
                dbSchemas.Load(connection);
                dbTables.Load(connection);
                dbColumns.Load(connection);
            }
        }
    }
}
