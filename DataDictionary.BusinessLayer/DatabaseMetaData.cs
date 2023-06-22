using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.WorkDbItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading.WorkItem;

namespace DataDictionary.BusinessLayer
{
    public class DatabaseMetaData
    {
        public BindingList<Context> DbConnections { get; } = new BindingList<Context>();
        public BindingTable<DbCatalogItem> DbCatalogs { get; } = new BindingTable<DbCatalogItem>();
        public BindingTable<DbSchemaItem> DbSchemas { get; } = new BindingTable<DbSchemaItem>();
        public BindingTable<DbTableItem> DbTables { get; } = new BindingTable<DbTableItem>();
        public BindingTable<DbColumnItem> DbColumns { get; } = new BindingTable<DbColumnItem>();
        public BindingTable<DbExtendedPropertyItem> DbExtendedProperties = new BindingTable<DbExtendedPropertyItem>();

        public IWorkItem ImportDb(Context connection, Action? onComplete = null)
        {
            raiseListChanged = false;

            if (DbConnections.FirstOrDefault(w => w.ServerName == connection.ServerName && w.DatabaseName == connection.DatabaseName) is Context item)
            {
                //results.Add(new BackgroundWork() { WorkName = "Remove Database", OnDoWork = ClearData });
            }
            else { DbConnections.Add(connection); }


            List<IWorkItem> workItems = new List<IWorkItem>();

            DbConnection dbData = new DbConnection(connection)
            {
                WorkName = "Open Connection",
                WorkItems = workItems.Select(s => s)
            };
            dbData.WorkCompleting += WorkCompleting;

            workItems.Add(new DbLoad()
            {
                WorkName = "Load Catalogs",
                Connection = dbData.Connection,
                Load = (conn) => DbCatalogs.Load(DbCatalogItem.GetDataReader(conn, connection.DatabaseName)),
            });

            workItems.Add(new DbLoad()
            {
                WorkName = "Load Schemas",
                Connection = dbData.Connection,
                Load = (conn) => DbSchemas.Load(DbSchemaItem.GetDataReader(conn)),
            });

            workItems.Add(new DbLoad()
            {
                WorkName = "Load Tables",
                Connection = dbData.Connection,
                Load = (conn) => DbTables.Load(DbTableItem.GetDataReader(conn)),
            });

            workItems.Add(new DbLoad()
            {
                WorkName = "Load Columns",
                Connection = dbData.Connection,
                Load = (conn) => DbColumns.Load(DbColumnItem.GetDataReader(conn)),
            });

            workItems.Add(new BatchWork()
            {
                WorkName = "Load Extended Properties, Schema",
                WorkItems = DbSchemas.Select(s => new DbPropertiesLoad()
                {
                    WorkName = String.Format("Load Extended Properties, Schema: {0}", s.SchemaName),
                    Connection = dbData.Connection,
                    GetCommand = s.GetProperties,
                    Target = DbExtendedProperties
                })
            });

            workItems.Add(new BatchWork()
            {
                WorkName = "Load Extended Properties, Table",
                WorkItems = DbTables.Select(s => new DbPropertiesLoad()
                {
                    WorkName = String.Format("Load Extended Properties, Table: {0}.{1}", s.SchemaName, s.TableName),
                    Connection = dbData.Connection,
                    GetCommand = s.GetProperties,
                    Target = DbExtendedProperties
                })
            });

            workItems.Add(new BatchWork()
            {
                WorkName = "Load Extended Properties, Column",
                WorkItems = DbColumns.Select(s => new DbPropertiesLoad()
                {
                    WorkName = String.Format("Load Extended Properties, Column: {0}.{1}.{2}", s.SchemaName, s.TableName, s.ColumnName),
                    Connection = dbData.Connection,
                    GetCommand = s.GetProperties,
                    Target = DbExtendedProperties
                })
            });

            return dbData;

            void WorkCompleting(object? sender, EventArgs e)
            {
                dbData.WorkCompleting -= WorkCompleting;
                raiseListChanged = true;
                if (onComplete is Action) { onComplete(); }
                DbData_ListChanged(this, new ListChangedEventArgs(ListChangedType.Reset, -1));

                var x = DbExtendedProperties;
            }
        }

        public IWorkItem GetDatabases(Context connection, Action<IEnumerable<IDbCatalogItem>>? onComplete = null)
        {
            BindingTable<DbCatalogItem> resultData = new BindingTable<DbCatalogItem>();

            List<IWorkItem> workItems = new List<IWorkItem>();

            DbConnection dbData = new DbConnection(connection)
            {
                WorkName = "Open Connection",
                WorkItems = workItems.Select(s => s)
            };
            dbData.WorkCompleting += WorkCompleting;

            workItems.Add(new DbLoad()
            {
                WorkName = "Load Databases",
                Connection = dbData.Connection,
                Load = (conn) => resultData.Load(DbCatalogItem.GetDataReader(conn)),
            });
            return dbData;

            void WorkCompleting(object? sender, EventArgs e)
            {
                dbData.WorkCompleting -= WorkCompleting;
                if (onComplete is Action<IEnumerable<IDbCatalogItem>>) { onComplete(resultData); }
            }
        }

        public DatabaseMetaData() : base()
        {
            DbSchemas.ListChanged += DbData_ListChanged;
            DbTables.ListChanged += DbData_ListChanged;
            DbColumns.ListChanged += DbData_ListChanged;
        }

        public event EventHandler<ListChangedEventArgs>? ListChanged;
        private Boolean raiseListChanged = true;

        private void DbData_ListChanged(object? sender, ListChangedEventArgs e)
        {
            if (ListChanged is EventHandler<ListChangedEventArgs> handler && raiseListChanged)
            { handler(sender, e); }
        }
    }
}
