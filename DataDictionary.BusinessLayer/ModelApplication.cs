using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Help;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Interface component for the Application data
    /// </summary>
    /// <remarks>When combined with the Extension class, this implements multi-inheritance.</remarks>
    public interface IModelApplication
    {
        /// <summary>
        /// List of Help Subjects for the Application (the help system).
        /// </summary>
        HelpCollection HelpSubjects { get; }

        /// <summary>
        /// List Properties defined for the Application.
        /// </summary>
        PropertyCollection Properties { get; }

        /// <summary>
        /// List of Scopes defined for the Application.
        /// </summary>
        ScopeCollection Scopes { get; }

    }

    /// <summary>
    /// Implementation component for the Application data
    /// </summary>
    /// <remarks>When combined with the Extension class, this implements multi-inheritance.</remarks>
    public static class ModelApplication
    {
        /// <summary>
        /// Loads the Application Data from the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadApplicationData(this IModelApplication data, IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Clear Help",
                DoWork = data.HelpSubjects.Clear
            });

            work.Add(new WorkItem()
            {
                WorkName = "Clear Properties",
                DoWork = data.Properties.Clear
            });

            work.Add(factory.CreateWork(
                workName: "Load HelpSubjects",
                target: data.HelpSubjects,
                command: data.HelpSubjects.LoadCommand));

            work.Add(factory.CreateWork(
                workName: "Load Properties",
                target: data.Properties,
                command: data.Properties.LoadCommand));

            work.Add(factory.CreateWork(
                workName: "Load Scopes",
                target: data.Scopes,
                command: data.Scopes.LoadCommand));

            return work;
        }

        /// <summary>
        /// Loads the Application Data from a File
        /// </summary>
        /// <param name="data"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadApplicationData(this IModelApplication data, FileInfo file)
        {
            List<WorkItem> workItems = new List<WorkItem>
            {
                new WorkItem() { WorkName = "Load Application Data", DoWork = DoWork }
            };

            return workItems.AsReadOnly();

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    workSet.ReadXml(file.FullName, System.Data.XmlReadMode.ReadSchema);

                    if (workSet.Tables.Contains(data.HelpSubjects.BindingName) &&
                        workSet.Tables[data.HelpSubjects.BindingName] is System.Data.DataTable helpData)
                    {
                        data.HelpSubjects.Clear();
                        data.HelpSubjects.Load(helpData.CreateDataReader());
                    }

                    if (workSet.Tables.Contains(data.Properties.BindingName) &&
                        workSet.Tables[data.Properties.BindingName] is System.Data.DataTable propertiesData)
                    {
                        data.Properties.Clear();
                        data.Properties.Load(propertiesData.CreateDataReader());
                    }

                    if (workSet.Tables.Contains(data.Scopes.BindingName) &&
                        workSet.Tables[data.Scopes.BindingName] is System.Data.DataTable scopesData)
                    {
                        data.Scopes.Clear();
                        data.Scopes.Load(scopesData.CreateDataReader());
                    }
                }
            }
        }

        /// <summary>
        /// Saves the Application Data to the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveApplicationData(this IModelApplication data, IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(factory.CreateWork(
                workName: "Save HelpSubjects",
                command: data.HelpSubjects.SaveCommand));

            work.Add(factory.CreateWork(
                workName: "Save Properties",
                command: data.Properties.SaveCommand));

            work.Add(factory.CreateWork(
                workName: "Save Scopes",
                command: data.Scopes.SaveCommand));

            return work;
        }

        /// <summary>
        /// Saves the Application Data to a file.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveApplicationData(this IModelApplication data, FileInfo file)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            workItems.Add(new WorkItem() { WorkName = "Save Application Data", DoWork = DoWork });

            return workItems.AsReadOnly();

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    workSet.Tables.Add(data.HelpSubjects.ToDataTable());
                    workSet.Tables.Add(data.Properties.ToDataTable());
                    workSet.Tables.Add(data.Scopes.ToDataTable());

                    workSet.WriteXml(file.FullName, System.Data.XmlWriteMode.WriteSchema);
                }
            }
        }
    }
}
