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
    public class ApplicationData: IApplicationData, ILoadData, ISaveData, ILoadFile, ISaveFile 
    {
        /// <inheritdoc/>
        public IHelpSubjectData HelpSubjects { get { return helpSubjects; } }
        private readonly HelpSubjectData helpSubjects = new HelpSubjectData();

        /// <inheritdoc/>
        public IPropertyData Properties { get { return Properties; } }
        private readonly PropertyData properties = new PropertyData();

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(HelpSubjects.Load(factory));
            work.AddRange(Properties.Load(factory));
            return work;
        }

        /// <inheritdoc/>
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
                    helpSubjects.Load(workSet);
                    properties.Load(workSet);
                }
            }
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(HelpSubjects.Save(factory));
            work.AddRange(Properties.Save(factory));
            return work;
        }

        /// <inheritdoc/>
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
