using DataDictionary.DataLayer;
using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.WorkDbItem;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading;
using Toolbox.Threading.WorkItem;

namespace DataDictionary.BusinessLayer
{

    [Obsolete()]
    public class DataRepository
    {
        BindingTable<DbSchemaItem> dbSchemas = new BindingTable<DbSchemaItem>();
        BindingTable<DbTableItem> dbTables = new BindingTable<DbTableItem>();
        BindingTable<DbColumnItem> dbColumns = new BindingTable<DbColumnItem>();
        BindingTable<DbExtendedPropertyItem> dbExtendedProperties = new BindingTable<DbExtendedPropertyItem>();

        public String ServerName { get { return BusinessContext.Instance.DbContext.ServerName; } }
        public String DatabaseName { get { return BusinessContext.Instance.DbContext.DatabaseName; } }

        public IEnumerable<IDbSchemaItem> DbSchema { get { return dbSchemas; } }
        public IEnumerable<IDbTableItem> DbTable { get { return dbTables; } }
        public IEnumerable<IDbColumnItem> DbColumn { get { return dbColumns; } }
        public IEnumerable<IDbExtendedPropertyItem> DbExtendedProperties { get { return dbExtendedProperties; } }

        public WorkBase Load()
        {
            DbConnection result = new DbConnection()
            {
                WorkName = "Load DataRepository",
                Connection = BusinessContext.Instance.DbContext.CreateConnection()
            };

            result.WorkItems.Add(new DbOpen()
            {
                WorkName = "Open Connection",
                Connection = result.Connection,
                ReportError = result.AddError,
            });

            result.WorkItems.Add(new DbLoad()
            {
                WorkName = "Load Schema",
                Connection = result.Connection,
                Load = dbSchemas.Load,
                ReportError = result.AddError
            });

            result.WorkItems.Add(new DbLoad()
            {
                WorkName = "Load Tables",
                Connection = result.Connection,
                Load = dbTables.Load,
                ReportError = result.AddError
            });

            result.WorkItems.Add(new DbLoad()
            {
                WorkName = "Load Columns",
                Connection = result.Connection,
                Load = dbColumns.Load,
                ReportError = result.AddError
            });
            
            result.WorkItems.Add(new DbParellel()
            {
                WorkName = "Load Extended Properties, Schema",
                Connection = BusinessContext.Instance.DbContext.CreateConnection,
                MaxDegreeOfParallelism = 1,
                Tasks = dbSchemas.Select<DbSchemaItem, Action>(s => () => dbExtendedProperties.Load(result.Connection.GetReader(s.GetProperties(result.Connection)))),
                ReportError = result.AddError
            });
            
            result.WorkItems.Add(new DbParellel()
            {
                WorkName = "Load Extended Properties, Table",
                Connection = BusinessContext.Instance.DbContext.CreateConnection,
                MaxDegreeOfParallelism = 1,
                Tasks = dbTables.Select<DbTableItem, Action>(s => () => dbExtendedProperties.Load(result.Connection.GetReader(s.GetProperties(result.Connection)))),
                ReportError = result.AddError
            });
            
            result.WorkItems.Add(new DbParellel()
            {
                WorkName = "Load Extended Properties, Column",
                Connection = BusinessContext.Instance.DbContext.CreateConnection,
                MaxDegreeOfParallelism = 1,
                Tasks = dbColumns.Select<DbColumnItem, Action>(s => () => dbExtendedProperties.Load(result.Connection.GetReader(s.GetProperties(result.Connection)))),
                ReportError = result.AddError
            });
            
            result.WorkItems.Add(new DbClose()
            {
                WorkName = "Close Connection",
                Connection = result.Connection,
                CommentTransaction = result.CommentTransaction,
                ReportError = result.AddError
            });

            return result;
        }
    }
}
