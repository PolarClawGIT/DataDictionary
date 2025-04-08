using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppGeneral
{
    /// <summary>
    /// Interface representing Application data
    /// </summary>
    public interface IApplicationData :
        ILoadData, ISaveData,
        IBindData
    {
        /// <summary>
        /// List of Application Help.
        /// </summary>
        IHelpSubjectData HelpSubjects { get; }

        /// <summary>
        /// List of Application (Common) Properties.
        /// </summary>
        IPropertyData Properties { get; }

        /// <summary>
        /// List of Application (Common) Definitions.
        /// </summary>
        IDefinitionData Definitions { get; }

        /// <summary>
        /// Loads/Import the Application Data from file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Load(FileInfo file);

        /// <summary>
        /// Saves/Export the Application Data to file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Save(FileInfo file);
    }

    /// <summary>
    /// Implementation for Application data
    /// </summary>
    public class ApplicationData : IApplicationData,
        ILoadData, ISaveData//, IDataTableFile
    {
        /// <inheritdoc/>
        public IHelpSubjectData HelpSubjects { get { return helpSubjectValues; } }
        private readonly HelpSubjectData helpSubjectValues = new HelpSubjectData();

        /// <inheritdoc/>
        public IPropertyData Properties { get { return propertyValues; } }
        private readonly PropertyData propertyValues = new PropertyData();

        /// <inheritdoc/>
        public IDefinitionData Definitions { get { return definitionValues; } }
        private readonly DefinitionData definitionValues = new DefinitionData();

        /// <inheritdoc/>
        public Boolean RaiseListChangedEvents
        {
            get
            {
                return helpSubjectValues.RaiseListChangedEvents
                    && propertyValues.RaiseListChangedEvents
                    && definitionValues.RaiseListChangedEvents;
            }
            set
            {
                helpSubjectValues.RaiseListChangedEvents = value;
                propertyValues.RaiseListChangedEvents = value;
                definitionValues.RaiseListChangedEvents = value;
            }
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(HelpSubjects.Load(factory));
            work.AddRange(Properties.Load(factory));
            work.AddRange(Definitions.Load(factory));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(FileInfo file)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(Delete());
            work.Add(new WorkItem() { WorkName = "Load Application Data", DoWork = DoWork });
            return work;

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    workSet.ReadXml(file.FullName, System.Data.XmlReadMode.ReadSchema);
                    helpSubjectValues.Load(workSet);
                    propertyValues.Load(workSet);
                    definitionValues.Load(workSet);
                }
            }
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(HelpSubjects.Save(factory));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(FileInfo file)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Save Application Data", DoWork = DoWork });

            return work;

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    workSet.Tables.Add(helpSubjectValues.ToDataTable());
                    workSet.Tables.Add(propertyValues.ToDataTable());
                    workSet.Tables.Add(definitionValues.ToDataTable());
                    workSet.WriteXml(file.FullName, System.Data.XmlWriteMode.WriteSchema);
                }
            }
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(HelpSubjects.Delete());
            work.AddRange(Properties.Delete());
            work.AddRange(Definitions.Delete());
            return work;
        }

        /// <inheritdoc/>
        public void Clear()
        {
            helpSubjectValues.Clear();
            propertyValues.Clear();
            definitionValues.Clear();
        }

        /// <inheritdoc/>
        public void ResetBindings()
        {
            helpSubjectValues.ResetBindings();
            propertyValues.ResetBindings();
            definitionValues.ResetBindings();
        }
    }
}
