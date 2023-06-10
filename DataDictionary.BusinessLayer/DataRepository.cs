using DataDictionary.DataLayer.DbMetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading;

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

                BusinessContext.Instance.AddWork(
                    new WorkBackgroundItem()
                    {
                        WorkName = "Load Schemas",
                        OnDoWork = () => dbSchemas.Load(connection)
                    });

                BusinessContext.Instance.AddWork(
                    new WorkBackgroundItem()
                    {
                        WorkName = "Load Tables",
                        OnDoWork = () => dbTables.Load(connection)
                    });

                BusinessContext.Instance.AddWork(
                    new WorkBackgroundItem()
                    {
                        WorkName = "Load Columns",
                        OnDoWork = () => dbColumns.Load(connection)
                    });

            }
        }
    }
}
