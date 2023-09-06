using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DatabaseData;
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
        public static IReadOnlyList<WorkItem> GetDatabaseSchema(this DbSchemaContext context, IBindingTable<DbDatabaseItem> target)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            workItems.Add(
                new GetInformationSchema<DbDatabaseItem>(context)
                {
                    Collection = DbDatabaseItem.Schema,
                    Target = target,
                    WorkName = "Get Databases"
                });
            return workItems;
        }
    }
}
