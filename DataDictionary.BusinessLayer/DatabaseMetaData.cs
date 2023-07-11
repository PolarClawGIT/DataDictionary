// Ignore Spelling: Schemas

using DataDictionary.DataLayer;
using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.WorkDbItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading.WorkItem;

namespace DataDictionary.BusinessLayer
{
    public class DatabaseMetaData
    {
        public BindingTable<DbCatalogItem> DbCatalogs { get; } = ModelFactory.Create<DbCatalogItem>();
        public BindingTable<DbSchemaItem> DbSchemas { get; } = ModelFactory.Create<DbSchemaItem>();
        public BindingTable<DbTableItem> DbTables { get; } = ModelFactory.Create<DbTableItem>();
        public BindingTable<DbColumnItem> DbColumns { get; } = ModelFactory.Create<DbColumnItem>();
        public BindingTable<DbExtendedPropertyItem> DbExtendedProperties = ModelFactory.Create<DbExtendedPropertyItem>();

        public IWorkItem ImportDb(DbSchemaContext connection, Action? onComplete = null)
        {
            List<IWorkItem> workItems = new List<IWorkItem>();

            DbConnection result = new DbConnection(connection)
            {
                WorkName = "Import Db",
                WorkItems = workItems.Select(s => s)
            };
            result.WorkCompleting += WorkCompleting;
            result.WorkStarting += WorkStarting;

            workItems.Add(new DbLoad()
            {
                WorkName = "Load Catalogs",
                Connection = result.Connection,
                Load = (conn) => DbCatalogs.Load(DbCatalogItem.GetSchema(conn)),
            });

            workItems.Add(new DbLoad()
            {
                WorkName = "Load Schemas",
                Connection = result.Connection,
                Load = (conn) => DbSchemas.Load(DbSchemaItem.GetSchema(conn)),
            });

            workItems.Add(new DbLoad()
            {
                WorkName = "Load Tables",
                Connection = result.Connection,
                Load = (conn) => DbTables.Load(DbTableItem.GetSchema(conn)),
            });

            workItems.Add(new DbLoad()
            {
                WorkName = "Load Columns",
                Connection = result.Connection,
                Load = (conn) => DbColumns.Load(DbColumnItem.GetSchema(conn)),
            });

            workItems.Add(new BatchWork()
            {
                WorkName = "Load Extended Properties, Schemas",
                WorkItems = DbSchemas.Select(s => new DbPropertiesLoad()
                {
                    WorkName = "Load Extended Properties, Schemas",
                    Connection = result.Connection,
                    GetCommand = s.GetProperties,
                    Target = DbExtendedProperties
                })
            });

            workItems.Add(new BatchWork()
            {
                WorkName = "Load Extended Properties, Tables",
                WorkItems = DbTables.Select(s => new DbPropertiesLoad()
                {
                    WorkName = "Load Extended Properties, Tables",
                    Connection = result.Connection,
                    GetCommand = s.GetProperties,
                    Target = DbExtendedProperties
                })
            });

            workItems.Add(new BatchWork()
            {
                WorkName = "Load Extended Properties, Columns",
                WorkItems = DbColumns.Select(s => new DbPropertiesLoad()
                {
                    WorkName = "Load Extended Properties, Columns",
                    Connection = result.Connection,
                    GetCommand = s.GetProperties,
                    Target = DbExtendedProperties
                }),
            });

            return result;

            void WorkStarting(object? sender, EventArgs e)
            {
                result.WorkStarting -= WorkStarting;
                raiseListChanged = false;
            }

            void WorkCompleting(object? sender, EventArgs e)
            {
                result.WorkCompleting -= WorkCompleting;

                raiseListChanged = true;
                DbData_ListChanged(this, new ListChangedEventArgs(ListChangedType.Reset, -1));

                if (onComplete is Action) { onComplete(); }
            }

        }

        public IWorkItem GetDatabases(Context connection, Action<IEnumerable<String?>>? onComplete = null)
        {
            List<String?> result = new List<String?>();

            List<IWorkItem> workItems = new List<IWorkItem>();

            IWorkItem dbData = new DbLoad()
            {
                WorkName = "Load Databases",
                Connection = connection.CreateConnection(),
                Load = (conn) => LoadData(conn),
            };
            dbData.WorkCompleting += WorkCompleting;

            return dbData;

            void WorkCompleting(object? sender, EventArgs e)
            {
                dbData.WorkCompleting -= WorkCompleting;
                if (onComplete is Action<IEnumerable<String?>>) { onComplete(result); }
            }

            void LoadData(IConnection conn)
            {
                using (DataTable data = new DataTable())
                {
                    data.Load(conn.GetReader(Schema.Collection.Databases));
                    result.AddRange(
                    data.Rows.Cast<DataRow>().
                        Select(s => s[0].ToString()));
                }
            }
        }

        public IWorkItem RemoveDb(DbSchemaContext connection, Action? onComplete = null)
        {
            List<IWorkItem> workItems = new List<IWorkItem>();

            IWorkItem result = new BatchWork()
            {
                WorkName = String.Format("Remove Db {0}", connection.DatabaseName),
                WorkItems = workItems.Select(s => s)
            };
            result.WorkCompleting += WorkCompleting;
            result.WorkStarting += WorkStarting;

            workItems.Add(new BatchWork()
            {
                WorkName = "Removing Catalog Items",
                WorkItems = DbCatalogs.
                    Where(w => w.CatalogName == connection.DatabaseName).
                    Select(s => new BackgroundWork()
                    {
                        WorkName = "Removing Catalog Items",
                        OnDoWork = () => DbCatalogs.Remove(s)
                    })
            });

            workItems.Add(new BatchWork()
            {
                WorkName = "Removing Schemas",
                WorkItems = DbSchemas.
                    Where(w => w.CatalogName == connection.DatabaseName).
                    Select(s => new BackgroundWork()
                    {
                        WorkName = "Removing Schemas",
                        OnDoWork = () => DbSchemas.Remove(s)
                    })
            });

            workItems.Add(new BatchWork()
            {
                WorkName = "Removing Tables",
                WorkItems = DbTables.
                    Where(w => w.CatalogName == connection.DatabaseName).
                    Select(s => new BackgroundWork()
                    {
                        WorkName = "Removing Tables",
                        OnDoWork = () => DbTables.Remove(s)
                    })
            });

            workItems.Add(new BatchWork()
            {
                WorkName = "Removing Column Items",
                WorkItems = DbColumns.
                    Where(w => w.CatalogName == connection.DatabaseName).
                    Select(s => new BackgroundWork()
                    {
                        WorkName = "Removing Columns",
                        OnDoWork = () => DbColumns.Remove(s)
                    })
            });

            workItems.Add(new BatchWork()
            {
                WorkName = "Removing Extended Properties",
                WorkItems = DbExtendedProperties.
                    Where(w => w.CatalogName == connection.DatabaseName).
                    Select(s => new BackgroundWork()
                    {
                        WorkName = "Removing Extended Properties",
                        OnDoWork = () => DbExtendedProperties.Remove(s)
                    })
            });

            return result;

            void WorkCompleting(object? sender, EventArgs e)
            {
                result.WorkCompleting -= WorkCompleting;
                raiseListChanged = true;
                if (onComplete is Action) { onComplete(); }
                DbData_ListChanged(this, new ListChangedEventArgs(ListChangedType.Reset, -1));
            }

            void WorkStarting(object? sender, EventArgs e)
            {
                result.WorkStarting -= WorkStarting;
                raiseListChanged = false;
            }
        }

        public DatabaseMetaData() : base()
        {
            DbCatalogs.ListChanged += DbData_ListChanged;
            DbSchemas.ListChanged += DbData_ListChanged;
            DbTables.ListChanged += DbData_ListChanged;
            DbColumns.ListChanged += DbData_ListChanged;
            DbExtendedProperties.ListChanged += DbData_ListChanged;
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
