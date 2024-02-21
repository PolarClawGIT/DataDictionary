using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Help;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Application
{
    /// <summary>
    /// Interface component for the HelpSubject data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IHelpSubjectData:
        IBindingData<HelpItem>,
        ILoadData, ILoadData<IHelpKey>,
        ISaveData, ISaveData<IHelpKey>
    { }

    /// <summary>
    /// Wrapper Class for Application Help.
    /// </summary>
    class HelpSubjectData : HelpCollection, IHelpSubjectData
    {
        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IHelpKey helpKey)
        { return factory.CreateLoad(this, helpKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IHelpKey helpKey)
        { return factory.CreateSave(this, helpKey).ToList(); }


    }
}
