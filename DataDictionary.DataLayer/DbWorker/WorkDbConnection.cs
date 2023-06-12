using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.DbContext;
using Toolbox.Threading.WorkItem;

namespace DataDictionary.DataLayer.DbWorker
{
    internal class WorkDbConnection: BatchWork
    {
        public required IConnection Connection { get; init; }
    }
}
