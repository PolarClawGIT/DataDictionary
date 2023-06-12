using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Toolbox.DbContext
{
    public interface IConnection : IDisposable
    {
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

        public Context DbContext { get; init; } = null!;

        private Boolean disposedValue;
        private SqlConnection connection = new SqlConnection();
        private SqlTransaction transaction = null!;

        public Connection() { }

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
                ex.Data.Add("ConnectionString", DbContext.ConnectionBuilder.ConnectionString);
                throw;
            }
        }

        public void Commit()
        {
            try
            {
                if (transaction != null) { transaction.Commit(); }
                connection.Close();
            }
            catch (Exception)
            { throw; }
        }

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

        public SqlCommand GetCommand()
        { return connection.CreateCommand(); }

        public IDataReader GetReader(Schema.Collection collection)
        {
            IDataReader result;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = DbContext.ConnectionBuilder.ConnectionString;
            connection.Open();

            if (collection == Schema.Collection.Schemas)
            { // Handling for Db Schema objects.
                SqlCommand getCommand = connection.CreateCommand();
                getCommand.CommandText = "Select Db_Name() As [SCHEMA_CATALOG], [name] As [SCHEMA_NAME] From [Sys].[Schemas]";
                getCommand.CommandType = CommandType.Text;
                return getCommand.ExecuteReader();
            }
            else
            {// Everything else is handled by GetSchema method.
                DataTable data = connection.GetSchema(collection.ToString());
                result = data.CreateDataReader();
            }

            connection.Close();
            return result;
        }

        public IDataReader GetReader(SqlCommand command)
        { return command.ExecuteReader(); }


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
    }
}
