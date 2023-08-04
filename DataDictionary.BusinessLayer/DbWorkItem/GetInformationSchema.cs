using DataDictionary.DataLayer.DbGetSchema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    /// <summary>
    /// Worker that calls SqlConnection.GetSchema
    /// </summary>
    class GetInformationSchema<TResult> : WorkItem
        where TResult : class, IBindingTableRow
    {
        public IConnection Connection { get; private set; }
        public required InformationSchema.Collection Collection { get; init; }
        public required IBindingTable<TResult> Target { get; init; }
        public override string WorkName { get; init; } = "Get InformationSchema";

        public GetInformationSchema(IContext context) : base()
        { Connection = context.CreateConnection(); }

        protected override void Work()
        {
            base.Work();
            Target.Load(Connection.GetReader(Collection));
        }
    }
}
