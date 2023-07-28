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
    class ExecuteNonQuery : WorkItem, IDbWorkItem
    {
        public required Func<IConnection, Command> Command { get; init; }
        readonly IConnection connection;

        public ExecuteNonQuery(OpenConnection conn) : base()
        {
            this.connection = conn.Connection;
            conn.Dependency(this);
        }

        protected override void Work()
        {
            Command command = Command(connection);
            connection.ExecuteNonQuery(command);
        }
    }
}
