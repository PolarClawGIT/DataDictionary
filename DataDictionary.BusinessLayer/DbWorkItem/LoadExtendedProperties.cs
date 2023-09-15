using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    class LoadExtendedProperties<TDbItem> : WorkItem, IDbWorkItem
        where TDbItem : class, IBindingTableRow, IDbExtendedProperties
    {
        public required BindingTable<DbExtendedPropertyItem> Target { get; init; }
        public required IBindingTable<TDbItem> Source { get; init; }
        readonly IConnection connection;

        public LoadExtendedProperties(OpenConnection conn) : base()
        {
            this.connection = conn.Connection;
            conn.Dependency(this);
        }

        protected override void Work()
        {
            Int32 toDo = Source.Count();
            Int32 complete = 0;

            foreach (TDbItem item in Source)
            {
                Target.Load(connection.ExecuteReader(item.GetProperties(connection)));
                complete++;
                Double progress = ((Double)complete / (Double)toDo) * (Double)100.0;
                this.OnProgressChanged((Int32)progress);
            }
        }
    }
}
