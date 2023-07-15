using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.DbContext;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    class VerifyConnection : WorkItem
    {
        public required IContext Context { get; init; }
        public Boolean IsSuccessful { get; private set; } = false;
        public override string WorkName { get; init; } = "Verify Connection";

        public VerifyConnection() { }

        protected override void Work()
        {
            using (IConnection conn = Context.CreateConnection())
            {
                try
                {
                    conn.Open();
                    conn.Commit();
                    IsSuccessful = true;
                }
                catch (Exception)
                { throw; }

            }
        }
    }
}
