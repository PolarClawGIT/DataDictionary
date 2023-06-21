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
            public required IConnection Connection { get; init; }
        }

        public class DbOpen : BackgroundWork
        {
            public required IConnection Connection { get; init; }

            public override void DoWork()
            {
                base.DoWork();
                Connection.Open();
            }
        }

        public class DbClose : BackgroundWork
        {
            public required IConnection Connection { get; init; }

            public override void DoWork()
            {
                base.DoWork();

                if (Connection.HasException) { Connection.Rollback(); }
                else { Connection.Commit(); }
            }
        }

        public class DbLoad : BackgroundWork
        {
            public required IConnection Connection { get; init; }
            public required Action<IConnection> Load { get; init; }

            public override void DoWork()
            {
                if (Connection.HasException) { }
                else { Load(Connection); }

                base.DoWork();
            }
        }

        public class DbPropertiesLoad : BackgroundWork
        {
            public required Func<IConnection> Connection { get; init; }
            public required Func<IConnection, SqlCommand> GetCommand { get; init; }
            public required BindingTable<DbExtendedPropertyItem> Target { get; init; }

            public override void DoWork()
            {
                using (IConnection connection = Connection())
                {
                    connection.Open();
                    Target.Load(connection.GetReader(GetCommand(connection)));
                    connection.Commit();
                }
            }
        }


        public class DbParellel : BackgroundWork
        {
            public required Func<IConnection> Connection { get; init; }
            public required Func<IConnection, SqlCommand> GetCommand { get; init; }
            public required BindingTable<DbExtendedPropertyItem> Target { get; init; }

            public override void DoWork()
            {
                using (IConnection connection = Connection())
                {
                    connection.Open();
                    Target.Load(connection.GetReader(GetCommand(connection)));
                    connection.Commit();
                }
            }
        }

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
