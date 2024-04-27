using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Help;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Application
{
    /// <summary>
    /// Wrapper for the HelpSubject data
    /// </summary>
<<<<<<< HEAD
    public interface IHelpSubjectData :
=======
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IHelpSubjectData:
>>>>>>> RenameIndexValue
        IBindingData<HelpSubjectValue>,
        ILoadData, ILoadData<IHelpSubjectIndex>,
        ISaveData, ISaveData<IHelpSubjectIndex>
    { }

<<<<<<< HEAD
    /// <inheritdoc/>
    class HelpSubjectData : HelpCollection<HelpSubjectValue>,
        IHelpSubjectData,
        ILoadData<IHelpKey>, ISaveData<IHelpKey>
=======
    /// <summary>
    /// Wrapper Class for Application Help.
    /// </summary>
    class HelpSubjectData : HelpCollection<HelpSubjectValue>, IHelpSubjectData
>>>>>>> RenameIndexValue
    {
        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IHelpSubjectIndex helpKey)
        { return factory.CreateLoad(this, (IHelpKey)helpKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IHelpSubjectIndex dataKey)
        {
            IHelpKey key = new HelpSubjectIndex(dataKey);
            return Load(factory, key);
        }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IHelpSubjectIndex helpKey)
        { return factory.CreateSave(this, (IHelpKey)helpKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IHelpSubjectIndex dataKey)
        {
            IHelpKey key = new HelpSubjectIndex(dataKey);
            return Save(factory, key);
        }
    }
}
