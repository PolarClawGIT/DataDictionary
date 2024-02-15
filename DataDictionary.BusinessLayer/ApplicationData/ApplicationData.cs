using DataDictionary.BusinessLayer.DbWorkItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.ApplicationData
{
    /// <summary>
    /// Interface representing Application data
    /// </summary>
    public interface IApplicationData
    {
        /// <summary>
        /// Wrapper for Application Help.
        /// </summary>
        IHelpSubjectData HelpSubjects { get; }

        /// <summary>
        /// Wrapper for Application Properties.
        /// </summary>
        IPropertyData Properties { get; }

    }

    /// <summary>
    /// Implementation for Application data
    /// </summary>
    public class ApplicationData: IApplicationData
    {
        /// <inheritdoc/>
        public IHelpSubjectData HelpSubjects { get; } = new HelpSubjectData();

        /// <inheritdoc/>
        public IPropertyData Properties { get; } = new PropertyData();

        /// <summary>
        /// Loads the Application Data from the Database
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(HelpSubjects.Load(factory));
            work.AddRange(Properties.Load(factory));
            return work;
        }

        /// <summary>
        /// Loads the Application Data from a File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load(FileInfo file)
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
                    HelpSubjects.Load(workSet);
                    Properties.Load(workSet);
                }
            }
        }

        /// <summary>
        /// Saves the Application Data to the Database
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(HelpSubjects.Save(factory));
            work.AddRange(Properties.Save(factory));
            return work;
        }

        /// <summary>
        /// Saves the Application Data to a File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Save(FileInfo file)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            workItems.Add(new WorkItem() { WorkName = "Save Application Data", DoWork = DoWork });

            return workItems.AsReadOnly();

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    workSet.Tables.Add(HelpSubjects.ToDataTable());
                    workSet.Tables.Add(Properties.ToDataTable());

                    workSet.WriteXml(file.FullName, System.Data.XmlWriteMode.WriteSchema);
                }
            }
        }

    }
}
