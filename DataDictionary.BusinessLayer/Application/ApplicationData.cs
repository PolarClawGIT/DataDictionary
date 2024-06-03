using DataDictionary.BusinessLayer.DbWorkItem;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Application
{
    /// <summary>
    /// Interface representing Application data
    /// </summary>
    public interface IApplicationData :
        ILoadData, ISaveData
    {
        /// <summary>
        /// Wrapper for Application Help.
        /// </summary>
        IHelpSubjectData HelpSubjects { get; }

        /// <summary>
        /// Wrapper for Application (Common) Properties.
        /// </summary>
        Domain.IPropertyData Properties  { get;}

        /// <summary>
        /// Wrapper for Application (Common) Definitions.
        /// </summary>
        Domain.IDefinitionData Definitions { get; } 
    }

    /// <summary>
    /// Implementation for Application data
    /// </summary>
    public class ApplicationData : IApplicationData,
        ILoadData, ISaveData, IDataTableFile
    {
        /// <inheritdoc/>
        public IHelpSubjectData HelpSubjects { get { return helpSubjectValues; } }
        private readonly HelpSubjectData helpSubjectValues = new HelpSubjectData();

        /// <inheritdoc/>
        public Domain.IPropertyData Properties { get { return propertyValues; } }
        private readonly Domain.PropertyData propertyValues = new Domain.PropertyData();

        /// <inheritdoc/>
        public Domain.IDefinitionData Definitions { get { return definitionValues; } }
        private readonly Domain.DefinitionData definitionValues = new Domain.DefinitionData();

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
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(HelpSubjects.Save(factory));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<DataTable> Export()
        {
            List<System.Data.DataTable> work = new List<System.Data.DataTable>();
            work.Add(helpSubjectValues.ToDataTable());
            work.Add(propertyValues.ToDataTable());
            work.Add(definitionValues.ToDataTable());
            return work;
        }

        /// <inheritdoc/>
        public void Import(DataSet source)
        {
            helpSubjectValues.Load(source);
            propertyValues.Load(source);
            definitionValues.Load(source);
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
    }
}
