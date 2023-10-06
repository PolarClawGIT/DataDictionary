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
            base.Work();
            Command command = Command(connection);

            try { connection.ExecuteNonQuery(command); }
            catch (Exception ex) { ex.Data.Add("Command", command.CommandText); throw; }


        }
    }
}
