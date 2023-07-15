using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.DbContext;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    /// <summary>
    /// Worker that calls SqlConnection.GetSchema
    /// </summary>
    class GetInformationSchema<TResult> : WorkItem
        where TResult : class
    {
        public IConnection Connection { get; private set; }
        public required InformationSchema.Collection Collection { get; init; }
        public required Func<DataRow, TResult> Transform { get; init; }
        public required IList<TResult> Target { get; init; }
        public Func<TResult, Boolean>? Filter { get; init; }

        public GetInformationSchema(IContext context) : base()
        { Connection = context.CreateConnection(); }

        protected override void Work()
        {
            List<TResult> results = new List<TResult>();

            base.Work();
            using (DataTable data = new DataTable())
            {
                data.Load(Connection.GetReader(Collection));

                foreach (DataRow item in data.Rows)
                {
                    if (Filter is not null)
                    { if (Filter(Transform(item))) { Target.Add(Transform(item)); } }
                    else { Target.Add(Transform(item)); }
                }
            }
        }
    }
}
