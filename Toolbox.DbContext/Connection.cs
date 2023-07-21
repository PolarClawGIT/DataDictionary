using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Toolbox.DbContext
{
    public interface IConnection : IDisposable
    {
        /// <summary>
        /// Has an Exception occurred within the scope of this connection.
        /// </summary>
        Boolean HasException { get; }
        IReadOnlyList<Exception> Errors { get; }

        String? ServerName { get; }
        String? DatabaseName { get; }

        /// <summary>
        /// Opens a new connection to the database and starts a transaction.
        /// </summary>
        void Open();

        /// <summary>
        /// Returns the Schema data for the specified SQL Collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        IDataReader GetReader(InformationSchema.Collection collection);
        IDataReader GetReader(InformationSchema.Collection collection, String catalogName);
        IDataReader GetReader(InformationSchema.Collection collection, String catalogName, String schemaName);
        IDataReader GetReader(InformationSchema.Collection collection, String catalogName, String schemaName, String objectName);

        void ExecuteNonQuery(Command command);

        /// <summary>
        /// Expose the Create Command function.
        /// </summary>
        /// <returns></returns>
        Command CreateCommand();

        /// <summary>
        /// Wrappers the ExecuteReader and returns the Data Reader.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(Command command);

        /// <summary>
        /// Commits the transaction on the Open Connection. The Connection is then closed.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollback the transaction on an Open Connection. The Connection is then closed.
        /// </summary>
        void Rollback();
    }

    class Connection : IConnection
    {
        public required Context DbContext { get; init; }
        public Boolean HasException { get { return (Errors.Count > 0); } }

        List<Exception> exceptions = new List<Exception>();
        public IReadOnlyList<Exception> Errors { get { return exceptions.AsReadOnly(); } }

        public String ServerName { get { return connection.DataSource; } }
        public String DatabaseName { get { return connection.Database; } }

        private Boolean disposedValue = false;
        private SqlConnection connection = new SqlConnection();
        private SqlTransaction transaction = null!;
        private Byte[]? applicationRoleCookie = null;

        public Connection() { }

        /// <summary>
        /// Wrappers the Open and Begin Transaction of the SQL Connection.
        /// </summary>
        public void Open()
        {
            try
            {
                connection.ConnectionString = DbContext.ConnectionBuilder.ConnectionString;
                connection.Open();

                // Activate the Application role
                if (!String.IsNullOrEmpty(DbContext.ApplicationRole))
                {
                    // This is the only way I could get the sp_setapprole to work.
                    // On-line documentation was filled with incorrect solutions.
                    // @cookie needs to come out as a Byte[50]
                    // Using a CommandType.StoredProcedure, @cookie was returned as Int32 and retrieved only the first 4 bytes
                    // Using Parameters with CommandType.Text, SQL generates an error because it the call uses sp_executeSql which is not allowed with sp_setapprole.
                    SqlCommand roleCommand = connection.CreateCommand();

                    roleCommand.CommandType = CommandType.Text;
                    roleCommand.CommandText = String.Format("Declare @cookie VarBinary(8000); Exec sp_setapprole '{0}', N'{1}', @fCreateCookie = true, @cookie = @cookie OUTPUT; Select @cookie", DbContext.ApplicationRole, DbContext.ApplicationRolePassword);

                    using (DataTable data = new DataTable())
                    {
                        data.Load(roleCommand.ExecuteReader());
                        applicationRoleCookie = (Byte[])data.Rows[0][0];
                    }
                }

                transaction = connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                ex.Data.Add(nameof(DbContext.ConnectionBuilder.ConnectionString), DbContext.ConnectionBuilder.ConnectionString);
                if (!String.IsNullOrEmpty(DbContext.ApplicationRole))
                { ex.Data.Add(nameof(DbContext.ApplicationRole), DbContext.ApplicationRole); }
                exceptions.Add(ex);

                throw;
            }
        }

        /// <summary>
        /// Wrappers the Commit and Close of the SQL Connection.
        /// </summary>
        public void Commit()
        {
            try
            {
                if (transaction != null) { transaction.Commit(); }

                if (applicationRoleCookie is not null)
                {
                    SqlCommand roleCommand = connection.CreateCommand();
                    roleCommand.CommandType = CommandType.StoredProcedure;
                    roleCommand.CommandText = "sys.sp_unsetapprole";
                    SqlParameter parameter = new SqlParameter("@cookie", applicationRoleCookie);
                    roleCommand.Parameters.Add(parameter);

                    roleCommand.ExecuteNonQuery();
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
                throw;
            }
        }

        /// <summary>
        /// Wrappers the Rollback and Close of the SQL Connection.
        /// </summary>
        public void Rollback()
        {
            try
            {
                if (transaction != null) { transaction.Rollback(); }

                if (applicationRoleCookie is not null)
                {
                    SqlCommand roleCommand = connection.CreateCommand();
                    roleCommand.CommandType = CommandType.StoredProcedure;
                    roleCommand.CommandText = "sys.sp_unsetapprole";
                    roleCommand.Parameters.Add(new SqlParameter("@cookie", applicationRoleCookie));
                    roleCommand.ExecuteNonQuery();
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
                throw;
            }
        }

        /// <summary>
        /// Exposes the CreateCommand.
        /// The Command created is already associated with the Transaction.
        /// </summary>
        /// <returns></returns>
        public Command CreateCommand()
        {
            Command result = new Command(connection.CreateCommand());
            result.BaseCommand.Transaction = transaction;
            return result;
        }

        /// <summary>
        /// Exposes the ExecuteReader.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(Command command)
        {
            //if (HasException) { throw new InvalidOperationException("Connection has an Exception"); }

            try
            { return command.BaseCommand.ExecuteReader(); }
            catch (Exception ex)
            {
                AddExceptionData(command.BaseCommand, ex);
                exceptions.Add(ex);
                throw;
            }
        }

        public void ExecuteNonQuery(Command command)
        {
            //if (HasException) { throw new InvalidOperationException("Connection has an Exception"); }

            try
            { command.BaseCommand.ExecuteNonQuery(); }
            catch (Exception ex)
            {
                AddExceptionData(command.BaseCommand, ex);
                exceptions.Add(ex);
                throw;
            }
        }

        /// <summary>
        /// Specialized GetReader for the GetSchema method.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        /// <remarks>
        /// This method does not honor transactions.
        /// This is because the GetSchema method provided by MS does not support
        /// a command/data reader that uses a transaction. Instead this method
        /// creates its own connection without a transaction and executes
        /// the GetSchema method.
        /// </remarks>
        public IDataReader GetReader(InformationSchema.Collection collection)
        {
            if (HasException) { throw new InvalidOperationException("Connection has an Exception"); }

            IDataReader result;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = DbContext.ConnectionBuilder.ConnectionString;

            try
            {
                connection.Open();
                DataTable data = connection.GetSchema(collection.ToString());
                result = data.CreateDataReader();
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Add(nameof(collection), collection.ToString());
                exceptions.Add(ex);
                throw;
            }

            return result;
        }

        public IDataReader GetReader(InformationSchema.Collection collection, String catalogName)
        {
            if (HasException) { throw new InvalidOperationException("Connection has an Exception"); }

            IDataReader result;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = DbContext.ConnectionBuilder.ConnectionString;

            try
            {
                connection.Open();
                DataTable data = connection.GetSchema(collection.ToString(), new String[1] { catalogName });
                result = data.CreateDataReader();
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Add(nameof(collection), collection.ToString());
                exceptions.Add(ex);
                throw;
            }

            return result;
        }

        public IDataReader GetReader(InformationSchema.Collection collection, String catalogName, String schemaName)
        {
            if (HasException) { throw new InvalidOperationException("Connection has an Exception"); }

            IDataReader result;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = DbContext.ConnectionBuilder.ConnectionString;

            try
            {
                connection.Open();
                DataTable data = connection.GetSchema(collection.ToString(), new String[2] { catalogName, schemaName });
                result = data.CreateDataReader();
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Add(nameof(collection), collection.ToString());
                exceptions.Add(ex);
                throw;
            }

            return result;
        }

        public IDataReader GetReader(InformationSchema.Collection collection, String catalogName, String schemaName, String objectName)
        {
            if (HasException) { throw new InvalidOperationException("Connection has an Exception"); }

            IDataReader result;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = DbContext.ConnectionBuilder.ConnectionString;

            try
            {
                connection.Open();
                DataTable data = connection.GetSchema(collection.ToString(), new String[3] { catalogName, schemaName, objectName });
                result = data.CreateDataReader();
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Data.Add(nameof(collection), collection.ToString());
                exceptions.Add(ex);
                throw;
            }

            return result;
        }

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (connection is SqlConnection && connection.State != ConnectionState.Closed) { Rollback(); }
                    if (transaction is SqlTransaction) { transaction.Dispose(); }
                    if (connection is SqlConnection) { connection.Dispose(); }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Exception handing
        protected void AddExceptionData(SqlCommand command, Exception ex)
        {
            ex.Data.Add(nameof(command.CommandText), command.CommandText);
            foreach (SqlParameter item in command.Parameters)
            { ex.Data.Add(String.Format("Parameter: {0}", item.ParameterName), item.Value.ToString()); }
        }

        #endregion
    }
}
