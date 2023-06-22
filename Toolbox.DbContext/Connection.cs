using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Toolbox.DbContext
{
    public interface IConnection : IDisposable
    {
        /// <summary>
        /// Has an Exception occured within the scope of this connection.
        /// </summary>
        Boolean HasException { get; }

        /// <summary>
        /// Opens a new connection to the database and starts a transaction.
        /// </summary>
        void Open();

        /// <summary>
        /// Returns the Schema data for the spcefied Sql Collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        IDataReader GetReader(Schema.Collection collection);
        IDataReader GetReader(Schema.Collection collection, String catalogName);
        IDataReader GetReader(Schema.Collection collection, String catalogName, String schemaName);
        IDataReader GetReader(Schema.Collection collection, String catalogName, String schemaName, String objectName);

        SqlCommand CreateCommand();
        IDataReader GetReader(SqlCommand command);

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

        private Boolean disposedValue = false;
        public Boolean HasException { get; private set; } = false;
        private SqlConnection connection = new SqlConnection();
        private SqlTransaction transaction = null!;

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
                transaction = connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                HasException = true;
                ex.Data.Add("ConnectionString", DbContext.ConnectionBuilder.ConnectionString);
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
                connection.Close();
            }
            catch (Exception)
            {
                HasException = true;
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
                connection.Close();
            }
            catch (Exception)
            { throw; }
        }

        /// <summary>
        /// Exposes the CreateCommand.
        /// The Command created is already assocated with the Transaction.
        /// </summary>
        /// <returns></returns>
        public SqlCommand CreateCommand()
        {
            SqlCommand result = connection.CreateCommand();
            result.Transaction = transaction;
            return result;
        }

        /// <summary>
        /// Exposes the ExecuteReader.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IDataReader GetReader(SqlCommand command)
        {
            if (HasException) { throw new InvalidOperationException("Connection has an Exception"); }

            try
            { return command.ExecuteReader(); }
            catch (Exception ex)
            {
                AddExceptionData(command, ex);
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
        public IDataReader GetReader(Schema.Collection collection)
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
                throw;
            }

            return result;
        }

        public IDataReader GetReader(Schema.Collection collection, String catalogName)
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
                throw;
            }

            return result;
        }

        public IDataReader GetReader(Schema.Collection collection, String catalogName, String schemaName)
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
                throw;
            }

            return result;
        }

        public IDataReader GetReader(Schema.Collection collection, String catalogName, String schemaName, String objectName)
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
