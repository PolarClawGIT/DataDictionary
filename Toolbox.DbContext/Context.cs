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
        internal SqlConnectionStringBuilder ConnectionBuilder { get; set; } = new SqlConnectionStringBuilder()
        {
            ApplicationName = "Data Dictionary Manager (C) 2023 William Howard" // TODO: pull this from the Assebmly?
        };

        public String ServerName { get { return ConnectionBuilder.DataSource; } init { ConnectionBuilder.DataSource = value; } }
        public String DatabaseName
        {
            get { return ConnectionBuilder.InitialCatalog; }

            init
            {
                if (String.IsNullOrWhiteSpace(value)) { ConnectionBuilder.InitialCatalog = "master"; }
                else { ConnectionBuilder.InitialCatalog = value; }
            }
        }
        public SqlAuthenticationMethod? Authentication { get { return ConnectionBuilder.Authentication; } }
        public String ServerUserName { get { return ConnectionBuilder.UserID; } init { ConnectionBuilder.UserID = value; } }
        public String ServerUserPassword { get { return ConnectionBuilder.Password; } init { ConnectionBuilder.Password = value; } }

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
        { return ConnectionBuilder.ConnectionString; }
    }
}
