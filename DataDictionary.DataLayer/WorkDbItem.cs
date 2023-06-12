using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.DbContext;
using Toolbox.Threading.WorkItem;

namespace DataDictionary.DataLayer
{
    namespace WorkDbItem
    {
        public class DbConnection : BatchWork
        {
            public required IConnection Connection { get; init; }
            public List<Exception> Errors { get; } = new List<Exception>();

            public void AddError(Exception ex) { Errors.Add(ex); }

            public bool CommentTransaction()
            {
                if (Errors.Count > 0) { return false; }
                return true;
            }

            public override void OnCompleting()
            {
                if (Errors.FirstOrDefault() is Exception ex) { OnException(ex); }

                base.OnCompleting();
            }
        }

        public class DbOpen : BackgroundWork
        {
            public required IConnection Connection { get; init; }
            public required Action<Exception> ReportError { get; init; }

            public override void DoWork()
            {
                base.DoWork();

                try
                { Connection.Open(); }
                catch (Exception ex)
                { ReportError(ex); }
            }
        }

        public class DbClose : BackgroundWork
        {
            public required IConnection Connection { get; init; }
            public required Func<bool> CommentTransaction { get; init; }
            public required Action<Exception> ReportError { get; init; }

            public override void DoWork()
            {
                try
                {
                    if (CommentTransaction()) { Connection.Commit(); }
                    else { Connection.Rollback(); }
                }
                catch (Exception ex)
                { ReportError(ex); }

                base.DoWork();
            }
        }

        public class DbLoad : BackgroundWork
        {
            public required IConnection Connection { get; init; }
            public required Action<Exception> ReportError { get; init; }
            public required Action<IConnection> Load { get; init; }

            public override void DoWork()
            {
                base.DoWork();

                try
                { Load(Connection); }
                catch (Exception ex)
                { ReportError(ex); }
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
