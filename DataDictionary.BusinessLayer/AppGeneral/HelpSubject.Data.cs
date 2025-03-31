// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppGeneral;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppGeneral
{
    /// <summary>
    /// Interface component for the HelpSubject data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IHelpSubjectData :
        IBindingData<HelpSubjectValue>,
        ILoadData, ILoadData<IHelpSubjectIndex>,
        ISaveData, ISaveData<IHelpSubjectIndex>,
        IGetTemporal
    {
        /// <summary>
        /// Returns a new IHelpSubjectData.
        /// </summary>
        /// <returns></returns>
        public static IHelpSubjectData Create()
        { return new HelpSubjectData(); }
    }

    /// <summary>
    /// Wrapper Class for Application Help.
    /// </summary>
    class HelpSubjectData : HelpSubjectCollection<HelpSubjectValue>, IHelpSubjectData
    {
        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IHelpSubjectIndex helpKey)
        { return factory.CreateLoad(this, (IHelpSubjectKey)helpKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IHelpSubjectIndex helpKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IHelpSubjectKey)helpKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IHelpSubjectIndex helpKey)
        { return factory.CreateSave(this, (IHelpSubjectKey)helpKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public IReadOnlyList<WorkItem> Delete(IHelpSubjectIndex dataKey)
        { return new WorkItem() { WorkName = "Remove HelpSubject", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove HelpSubject", DoWork = () => { this.Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public ITemporalData GetTemporal()
        {
            return new TemporalData<HelpSubjectData, HelpSubjectValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data) };
        }
    }
}
