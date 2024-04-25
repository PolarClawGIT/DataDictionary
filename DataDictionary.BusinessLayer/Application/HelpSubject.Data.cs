using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Help;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Application
{
    /// <summary>
    /// Wrapper for the HelpSubject data
    /// </summary>
    public interface IHelpSubjectData :
        IBindingData<HelpSubjectValue>,
        ILoadData, ILoadData<IHelpSubjectIndex>,
        ISaveData, ISaveData<IHelpSubjectIndex>
    { }

    /// <inheritdoc/>
    class HelpSubjectData : HelpCollection<HelpSubjectValue>,
        IHelpSubjectData,
        ILoadData<IHelpKey>, ISaveData<IHelpKey>
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
        public virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IHelpKey helpKey)
        { return factory.CreateSave(this, helpKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IHelpSubjectIndex dataKey)
        {
            IHelpKey key = new HelpSubjectIndex(dataKey);
            return Save(factory, key);
        }
    }
}
