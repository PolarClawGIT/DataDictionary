using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.DbContext
{
    /// <summary>
    /// Class containing the data needed to create a Database Connection.
    /// </summary>
    /// <remarks>This wrappers a Connection String.</remarks>
    public class Context
    {
        internal SqlConnectionStringBuilder ConnectionBuilder { get; set; } = new SqlConnectionStringBuilder();

        public String ServerName { get { return ConnectionBuilder.DataSource; } init { ConnectionBuilder.DataSource = value; } }
        public String DatabaseName { get { return ConnectionBuilder.InitialCatalog; } init { ConnectionBuilder.InitialCatalog = value; } }


        /// <summary>
        /// Constructor.
        /// </summary>
        public Context() { }

        public IConnection CreateConnection()
        {
            if (ConnectionBuilder == null || String.IsNullOrWhiteSpace(ConnectionBuilder.ConnectionString))
            { throw new ArgumentException("ConnectionString has not been defined"); }

            return new Connection() { DbContext = this };
        }

        public override string ToString()
        {   return ConnectionBuilder.ConnectionString; }
    }
}
