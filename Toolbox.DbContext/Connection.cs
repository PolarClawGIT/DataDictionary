using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Toolbox.DbContext
{
    public interface IConnection : IDisposable
    {
        void Open();
        void Commit();
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


        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (connection.State != System.Data.ConnectionState.Closed) { Rollback(); }

                    transaction.Dispose();
                    connection.Dispose();
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
