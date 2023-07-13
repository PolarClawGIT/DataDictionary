using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.DbContext;
using Toolbox.Threading.WorkItem;

namespace DataDictionary.BusinessLayer.WorkItem
{
    class DbVerifyConnection : BackgroundWork
    {
        Context dbContext;
        public Action? OnStart { get; init; }
        public Action? OnComplete { get; init; }

        public DbVerifyConnection(Context context) : base()
        {
            dbContext = context;
            WorkStarting += DbVerifyConnection_WorkStarting;
            WorkCompleting += DbVerifyConnection_WorkCompleting;
        }

        private void DbVerifyConnection_WorkStarting(object? sender, EventArgs e)
        { if (OnStart is not null) { OnStart(); } }

        private void DbVerifyConnection_WorkCompleting(object? sender, EventArgs e)
        { if (OnComplete is not null) { OnComplete(); } }

        public override void DoWork()
        {
            base.DoWork();
            using (IConnection Connection = dbContext.CreateConnection())
            { Connection.Open(); }
        }
    }
}
