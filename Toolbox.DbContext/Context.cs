﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.DbContext
{
    public interface IContext
    {
        String ServerName { get; }
        String DatabaseName { get; }
        IConnection CreateConnection();
        Boolean ValidateCommand { get; set; }
    }

    /// <summary>
    /// Class containing the data needed to create a Database Connection.
    /// </summary>
    /// <remarks>This wrappers a Connection String.</remarks>
    public class Context: IContext
    {
        internal SqlConnectionStringBuilder ConnectionBuilder { get; set; } = new SqlConnectionStringBuilder()
        {
            ApplicationName = "Data Dictionary Manager (C) 2023 William Howard" // TODO: pull this from the Assembly?
        };

        /// <summary>
        /// Name of the Database Server of the Connection
        /// </summary>
        public String ServerName { get { return ConnectionBuilder.DataSource; } init { ConnectionBuilder.DataSource = value; } }

        /// <summary>
        /// Name of the Database of the Connection
        /// </summary>
        public String DatabaseName
        {
            get { return ConnectionBuilder.InitialCatalog; }

            init
            {
                if (String.IsNullOrWhiteSpace(value)) { ConnectionBuilder.InitialCatalog = "master"; }
                else { ConnectionBuilder.InitialCatalog = value; }
            }
        }
        
        public Boolean IntegratedSecurity { get { return ConnectionBuilder.IntegratedSecurity; } set { ConnectionBuilder.IntegratedSecurity = value; } }

        public String ServerUserName { get { return ConnectionBuilder.UserID; } init { ConnectionBuilder.UserID = value; } }
        public String ServerUserPassword { get { return ConnectionBuilder.Password; } init { ConnectionBuilder.Password = value; } }

        public String? ApplicationRole { get; init; }
        public String? ApplicationRolePassword { get; init; }

        /// <summary>
        /// Attempt to validate the command before executing it. Any issues are thrown as exceptions.
        /// </summary>
        public Boolean ValidateCommand { get; set; } = false;

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
