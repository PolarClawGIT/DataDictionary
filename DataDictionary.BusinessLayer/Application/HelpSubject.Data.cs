using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Modification;
using DataDictionary.DataLayer.ApplicationData;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Application
{
    /// <summary>
    /// Interface component for the HelpSubject data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IHelpSubjectData :
        IBindingData<HelpSubjectValue>,
        ILoadData, ILoadData<IHelpSubjectIndex>,
        ISaveData, ISaveData<IHelpSubjectIndex>,
        IModificationData
    {
        /// <summary>
        /// Returns a ModificationData holding HelpSubjects.
        /// </summary>
        /// <returns></returns>
        public IModificationData Create() { return new HelpSubjectData(); }
    }

    /// <summary>
    /// Wrapper Class for Application Help.
    /// </summary>
    class HelpSubjectData : HelpCollection<HelpSubjectValue>, IHelpSubjectData
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
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, Boolean includeHistory)
        {
            List<WorkItem> work = new List<WorkItem>();
            
            work.Add(factory.CreateWork(
                workName: "Load HelpSubject History",
                target: this,
                command: (conn) => LoadCommand(conn, includeHistory)));

            return work;
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
        public IReadOnlyList<WorkItem> Delete(IHelpSubjectIndex dataKey)
        { return new WorkItem() { WorkName = "Remove HelpSubject", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove HelpSubject", DoWork = () => { this.Clear(); } }.ToList(); }

    }
}
