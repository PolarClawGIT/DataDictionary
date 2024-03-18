using DataDictionary.BusinessLayer.DbWorkItem;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Application
{
    /// <summary>
    /// Interface representing Application data
    /// </summary>
    public interface IApplicationData:
        ILoadData, ISaveData, IFileData
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
    public class ApplicationData : IApplicationData,
        ILoadData, ISaveData, IFileData
    {
        /// <inheritdoc/>
        public IHelpSubjectData HelpSubjects { get { return helpSubjectValues; } }
        private readonly HelpSubjectData helpSubjectValues = new HelpSubjectData();

        /// <inheritdoc/>
        public IPropertyData Properties { get { return propertyValues; } }
        private readonly PropertyData propertyValues = new PropertyData();

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(HelpSubjects.Load(factory));
            work.AddRange(Properties.Load(factory));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Import(FileInfo file)
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
                    helpSubjectValues.Load(workSet);
                    propertyValues.Load(workSet);
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
        public IReadOnlyList<WorkItem> Export(FileInfo file)
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
