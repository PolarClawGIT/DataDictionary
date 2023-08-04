using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DbGetSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.WorkFlows
{
    public static class GetSchemaExtension
    {
        public static IReadOnlyList<WorkItem> GetDatabaseSchema(this DbSchemaContext context, IBindingTable<GetSchemaDatabase> target)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            workItems.Add(
                new GetInformationSchema<GetSchemaDatabase>(context)
                {
                    Collection = GetSchemaDatabase.Schema,
                    Target = target,
                    WorkName = "Get Databases"
                });
            return workItems;
        }
    }
}
