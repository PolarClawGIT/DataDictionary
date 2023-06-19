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
        List<Context> connections { get; } = new List<Context>();
        public BindingTable<DbSchemaItem> DbSchemas { get; } = new BindingTable<DbSchemaItem>();
        public BindingTable<DbTableItem> DbTables { get; } = new BindingTable<DbTableItem>();
        public BindingTable<DbColumnItem> DbColumns { get; } = new BindingTable<DbColumnItem>();
        public BindingTable<DbExtendedPropertyItem> DbExtendedProperties = new BindingTable<DbExtendedPropertyItem>();

        public IReadOnlyList<Context> Connections { get { return connections.AsReadOnly(); } }

        public IEnumerable<IWorkItem> ImportDb(Context connection)
        {
            raiseListChanged = false;
            List<IWorkItem> results = new List<IWorkItem>();

            if (connections.FirstOrDefault(w => w.ServerName == connection.ServerName && w.DatabaseName == connection.DatabaseName) is Context item)
            {
                //results.Add(new BackgroundWork() { WorkName = "Remove Database", OnDoWork = ClearData });
            }
            else
            { connections.Add(connection); }

            DbConnection dbData = new DbConnection()
            {
                WorkName = "Load DataRepository",
                Connection = connection.CreateConnection()
            };

            dbData.WorkItems.Add(new DbOpen()
            {
                WorkName = "Open Connection",
                Connection = dbData.Connection,
                ReportError = dbData.AddError,
            });

            dbData.WorkItems.Add(new DbLoad()
            {
                WorkName = "Load Schema",
                Connection = dbData.Connection,
                Load = DbSchemas.Load,
                ReportError = dbData.AddError
            });

            dbData.WorkItems.Add(new DbLoad()
            {
                WorkName = "Load Tables",
                Connection = dbData.Connection,
                Load = DbTables.Load,
                ReportError = dbData.AddError
            });

            dbData.WorkItems.Add(new DbLoad()
            {
                WorkName = "Load Columns",
                Connection = dbData.Connection,
                Load = DbColumns.Load,
                ReportError = dbData.AddError
            });

            dbData.WorkItems.Add(new DbClose()
            {
                WorkName = "Close Connection",
                Connection = dbData.Connection,
                CommentTransaction = dbData.CommentTransaction,
                ReportError = dbData.AddError
            });

            results.Add(dbData);

            results.Add(new DbParellel()
            {
                WorkName = "Load Extended Properties, Schema",
                Connection = connection.CreateConnection,
                MaxDegreeOfParallelism = 1,
                Tasks = DbSchemas.Select<DbSchemaItem, Action>(s => () => DbExtendedProperties.Load(dbData.Connection.GetReader(s.GetProperties(dbData.Connection)))),
                ReportError = dbData.AddError
            });

            results.Add(new DbParellel()
            {
                WorkName = "Load Extended Properties, Table",
                Connection = connection.CreateConnection,
                MaxDegreeOfParallelism = 1,
                Tasks = DbTables.Select<DbTableItem, Action>(s => () => DbExtendedProperties.Load(dbData.Connection.GetReader(s.GetProperties(dbData.Connection)))),
                ReportError = dbData.AddError
            });

            results.Add(new DbParellel()
            {
                WorkName = "Load Extended Properties, Column",
                Connection = connection.CreateConnection,
                MaxDegreeOfParallelism = 1,
                Tasks = DbColumns.Select<DbColumnItem, Action>(s => () => DbExtendedProperties.Load(dbData.Connection.GetReader(s.GetProperties(dbData.Connection)))),
                ReportError = dbData.AddError
            });

            results.Last().WorkCompleting += DatabaseMetaData_WorkCompleting;

            return results;

            void DatabaseMetaData_WorkCompleting(object? sender, EventArgs e)
            {
                results.Last().WorkCompleting -= DatabaseMetaData_WorkCompleting;
                raiseListChanged = true;
                DbData_ListChanged(this, new ListChangedEventArgs(ListChangedType.Reset, -1));
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
