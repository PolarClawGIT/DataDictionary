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
    class SaveBindingTable : WorkItem, IDbWorkItem
    {
        public required Func<IConnection,SqlCommand> Command { get; init; }
        readonly IConnection connection;

        public SaveBindingTable(OpenConnection conn) : base()
        {
            this.connection = conn.Connection;
            conn.Dependency(this);
        }

        protected override void Work()
        {
            SqlCommand command = Command(connection);
            command.ExecuteNonQuery();
        }
    }
}
