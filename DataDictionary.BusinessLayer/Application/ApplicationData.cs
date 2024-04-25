using DataDictionary.BusinessLayer.DbWorkItem;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Application
{
    /// <summary>
    /// Interface representing Application data
    /// </summary>
    public interface IApplicationData:
        ILoadData, ISaveData
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
        ILoadData, ISaveData, IDataTableFile
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
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(HelpSubjects.Save(factory));
            work.AddRange(Properties.Save(factory));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(helpSubjectValues.ToDataTable());
            result.Add(propertyValues.ToDataTable());;
            return result;
        }

        /// <inheritdoc/>
        public void Import(DataSet source)
        {
            helpSubjectValues.Load(source);
            propertyValues.Load(source);
        }
    }
}
