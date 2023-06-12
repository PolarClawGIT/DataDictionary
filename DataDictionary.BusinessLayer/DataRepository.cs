using DataDictionary.DataLayer.DbMetaData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading;
using Toolbox.Threading.WorkItem;

namespace DataDictionary.BusinessLayer
{
    
    
    public class DataRepository
    {
        BindingTable<DbSchemaItem> dbSchemas = new BindingTable<DbSchemaItem>();
        BindingTable<DbTableItem> dbTables = new BindingTable<DbTableItem>();
        BindingTable<DbColumnItem> dbColumns = new BindingTable<DbColumnItem>();

        public String ServerName { get { return BusinessContext.Instance.DbContext.ServerName; } }
        public String DatabaseName { get { return BusinessContext.Instance.DbContext.DatabaseName; } }

        public IEnumerable<IDbSchemaItem> DbSchema { get { return dbSchemas; } }
        public IEnumerable<IDbTableItem> DbTable { get { return dbTables; } }
        public IEnumerable<IDbColumnItem> DbColumn { get { return dbColumns; } }

        public WorkBase Load()
        {
            BatchWork result = new BatchWork() { WorkName = "Load DataRepository" };

            result.WorkItems.Add(new BackgroundWork()
            {
                WorkName = "Load Schemas",
                OnDoWork = () => dbSchemas.Load(BusinessContext.Instance.DbContext.CreateConnection())
            });

            result.WorkItems.Add(new BackgroundWork()
            {
                WorkName = "Load Tables",
                OnDoWork = () => dbTables.Load(BusinessContext.Instance.DbContext.CreateConnection())
            });

            result.WorkItems.Add(new BackgroundWork()
            {
                WorkName = "Load Columns",
                OnDoWork = () => dbColumns.Load(BusinessContext.Instance.DbContext.CreateConnection())
            });

            return result;
        }
    }
}
