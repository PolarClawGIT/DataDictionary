using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.WorkFlows
{
    public static class ApplicationExtension
    {
        public static IReadOnlyList<WorkItem> LoadApplication(this ModelData data)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(data.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new WorkItem()
            {
                WorkName = "Clear Help",
                DoWork = data.HelpSubjects.Clear
            });

            workItems.Add(new WorkItem()
            {
                WorkName = "Clear Properties",
                DoWork = data.HelpSubjects.Clear
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Help",
                Command = HelpItem.GetData,
                Target = data.HelpSubjects
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Properties",
                Command = PropertyItem.GetData,
                Target = data.Properties
            });

            return workItems;
        }

        public static IReadOnlyList<WorkItem> SaveHelp(this ModelData data)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(data.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Help",
                Command = (conn) => HelpItem.SetData(conn, data.HelpSubjects)
            });

            workItems.Add(new WorkItem()
            {
                WorkName = "Clear Help",
                DoWork = data.HelpSubjects.Clear
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Help",
                Command = HelpItem.GetData,
                Target = data.HelpSubjects
            });

            return workItems;
        }


    }
}
