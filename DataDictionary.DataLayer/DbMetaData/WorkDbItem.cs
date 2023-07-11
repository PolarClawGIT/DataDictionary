using DataDictionary.DataLayer.DbMetaData;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading.WorkItem;

namespace DataDictionary.DataLayer
{
    namespace WorkDbItem
    {
        public class DbConnection : BatchWork
        {
            public IConnection Connection { get; private set; }

            public DbConnection(Context dbContext)
            { Connection = dbContext.CreateConnection(); }

            public override void OnStarting()
            {
                base.OnStarting();
                Connection.Open();
            }

            public override void OnCompleting()
            {
                if (Connection is IConnection)
                {
                    if (Connection.HasException) { Connection.Rollback(); }
                    else { Connection.Commit(); }

                    Connection.Dispose();
                }

                base.OnCompleting();
            }
        }

        public class DbVerifyConnection : BackgroundWork
        {
            Context dbContext;

            public DbVerifyConnection(Context context)
            { dbContext = context; }

            public override void DoWork()
            {
                base.DoWork();
                using (IConnection Connection = dbContext.CreateConnection())
                { Connection.Open(); }
            }
        }

        public class DbLoad : BackgroundWork
        {
            public required IConnection Connection { get; init; }
            public required Action<IConnection> Load { get; init; }

            public override void DoWork()
            {
                base.DoWork();

                if (Connection.HasException) { }
                else { Load(Connection); }
            }
        }

        public class DbPropertiesLoad : BackgroundWork
        {
            public required IConnection Connection { get; init; }
            public required Func<IConnection, SqlCommand> GetCommand { get; init; }
            public required BindingTable<DbExtendedPropertyItem> Target { get; init; }

            public override void DoWork()
            {
                base.DoWork();
                Target.Load(Connection.ExecuteReader(GetCommand(Connection)));
            }
        }

        [Obsolete()]
        public class DbReader : BackgroundWork
        {
            public required IConnection Connection { get; init; }
            public required Func<IConnection, IDataReader> GetReader { get; init; }
            public required Action<IDataReader> LoadReader { get; init; }
            public required Action<Exception> ReportError { get; init; }

            public override void DoWork()
            {
                base.DoWork();

                try
                {
                    IDataReader reader = GetReader(Connection);
                    LoadReader(reader);
                }
                catch (Exception ex)
                { ReportError(ex); }
            }
        }
    }
}
