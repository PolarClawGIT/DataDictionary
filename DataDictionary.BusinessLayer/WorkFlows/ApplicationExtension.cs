using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Help;
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
    /// <summary>
    /// Methods dealing with working with the Application Data. (Help Subjects and Properties)
    /// </summary>
    public static class ApplicationExtension
    {
        /// <summary>
        /// Loads the Application Data from the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadApplicationData(this ModelData data)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(ModelData.ModelContext);
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

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Help",
                Command = data.HelpSubjects.LoadCommand,
                Target = data.HelpSubjects
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Properties",
                Command = data.Properties.LoadCommand,
                Target = data.Properties
            });

            return workItems;
        }

        /// <summary>
        /// Saves the Application Data to the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveApplicationData(this ModelData data)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(ModelData.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Help",
                Command = data.HelpSubjects.SaveCommand
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Properties",
                Command = data.Properties.SaveCommand
            });

            return workItems;
        }

        /// <summary>
        /// Loads the Application Data from a File
        /// </summary>
        /// <param name="data"></param>
        /// <param name="file"></param>
        /// <returns></returns>
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
                }
            }
        }

        /// <summary>
        /// Saves the Application Data to a file.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="file"></param>
        /// <returns></returns>
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

                    workSet.WriteXml(file.FullName, XmlWriteMode.WriteSchema);
                }
            }
        }


    }
}
