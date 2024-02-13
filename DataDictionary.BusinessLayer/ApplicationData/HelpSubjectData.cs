using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Help;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.ApplicationData
{
    /// <summary>
    /// Interface component for the HelpSubject data
    /// </summary>
    /// <remarks>Overrides: IDatabaseData</remarks>
    public interface IHelpSubjectData
    {
        /// <summary>
        /// List of Help Subjects for the Application (the help system).
        /// </summary>
        HelpCollection HelpSubjects { get; }

        /// <summary>
        /// Casts inherited type into the interface type.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Provides mechanism to map the "this" into the Interface</remarks>
        IHelpSubjectData AsHelpSubjectData { get; }

        /// <inheritdoc cref="DbWorkItem.ILoadData.Load(IDatabaseWork)"/>
        /// <remarks>HelpSubject</remarks>
        virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad("Load Help", this.HelpSubjects).ToList(); }

        /// <inheritdoc cref="DbWorkItem.ILoadData{TKey}.Load(IDatabaseWork, TKey)"/>
        /// <remarks>HelpSubject</remarks>
        virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IHelpKey helpKey)
        { return factory.CreateLoad("Load Help", this.HelpSubjects, helpKey).ToList(); }

        /// <inheritdoc cref="DbWorkItem.ILoadData.Load(System.Data.DataSet)"/>
        /// <remarks>HelpSubject</remarks>
        virtual IReadOnlyList<WorkItem> Load(System.Data.DataSet source)
        { return source.CreateLoad("Load Help", this.HelpSubjects).ToList(); }

        /// <inheritdoc cref="DbWorkItem.ISaveData.Save(IDatabaseWork)"/>
        /// <remarks>HelpSubject</remarks>
        virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave("Save Help", this.HelpSubjects).ToList(); }

        /// <inheritdoc cref="DbWorkItem.ISaveData{TKey}.Save(IDatabaseWork, TKey)"/>
        /// <remarks>HelpSubject</remarks>
        virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IHelpKey helpKey)
        { return factory.CreateSave("Save Help", this.HelpSubjects, helpKey).ToList(); }

        /// <inheritdoc cref="DbWorkItem.IRemoveData{TKey}.Remove(TKey)"/>
        /// <remarks>HelpSubject</remarks>
        virtual IReadOnlyList<WorkItem> Remove(IHelpKey helpKey)
        { return new WorkItem() { WorkName = "Remove Help", DoWork = () => { this.HelpSubjects.Remove(new HelpKey(helpKey)); } }.ToList(); }
    }

    partial class ApplicationData : IHelpSubjectData
    {
        /// <inheritdoc/>
        public HelpCollection HelpSubjects { get; } = new HelpCollection();

        /// <inheritdoc/>
        public IHelpSubjectData AsHelpSubjectData { get { return this; } }
    }
}
