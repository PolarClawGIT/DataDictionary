using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.DataLayer.DatabaseData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.WorkFlows
{
    public static class ApplicationExtension
    {
        public static IReadOnlyList<WorkItem> LoadApplicationData(this ModelData data)
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
                DoWork = data.Properties.Clear
            });

            workItems.Add(new WorkItem()
            {
                WorkName = "Clear Definitions",
                DoWork = data.Definitions.Clear
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

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Definitions",
                Command = DefinitionItem.GetData,
                Target = data.Definitions
            });

            return workItems;
        }

        public static IReadOnlyList<WorkItem> SaveApplicationData(this ModelData data)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(data.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Help",
                Command = (conn) => HelpItem.SetData(conn, data.HelpSubjects)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Properties",
                Command = (conn) => PropertyItem.SetData(conn, data.Properties)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Definitions",
                Command = (conn) => DefinitionItem.SetData(conn, data.Definitions)
            });

            return workItems;
        }

        public static IReadOnlyList<WorkItem> SaveApplicationData(this ModelData data, FileInfo file)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            workItems.Add(new WorkItem() { WorkName = "Save Application Data", DoWork = DoWork });

            return workItems.AsReadOnly();

            void DoWork()
            {
                using (DataSet workSet = new DataSet())
                {
                    workSet.Tables.Add(data.HelpSubjects.ToDataTable());
                    workSet.Tables.Add(data.Properties.ToDataTable());
                    workSet.Tables.Add(data.Definitions.ToDataTable());

                    workSet.WriteXml(file.FullName, XmlWriteMode.WriteSchema);
                }
            }
        }

        public static IReadOnlyList<WorkItem> LoadApplicationData(this ModelData data, FileInfo file)
        {
            List<WorkItem> workItems = new List<WorkItem>
            {
                new WorkItem() { WorkName = "Load Application Data", DoWork = DoWork }
            };

            return workItems.AsReadOnly();

            void DoWork()
            {
                using (DataSet workSet = new DataSet())
                {
                    workSet.ReadXml(file.FullName, XmlReadMode.ReadSchema);

                    if (workSet.Tables.Contains(data.HelpSubjects.BindingName) &&
                        workSet.Tables[data.HelpSubjects.BindingName] is DataTable helpData)
                    {
                        data.HelpSubjects.Clear();
                        data.HelpSubjects.Load(helpData.CreateDataReader());
                    }

                    if (workSet.Tables.Contains(data.Properties.BindingName) &&
                        workSet.Tables[data.Properties.BindingName] is DataTable propertiesData)
                    {
                        data.Properties.Clear();
                        data.Properties.Load(propertiesData.CreateDataReader());
                    }

                    if (workSet.Tables.Contains(data.Definitions.BindingName) &&
                        workSet.Tables[data.Definitions.BindingName] is DataTable definitionsData)
                    {
                        data.Definitions.Clear();
                        data.Definitions.Load(definitionsData.CreateDataReader());
                    }

                }
            }
        }
    }
}
