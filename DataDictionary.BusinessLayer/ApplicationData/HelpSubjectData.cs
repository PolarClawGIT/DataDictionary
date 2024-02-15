using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Help;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.ApplicationData
{
    /// <summary>
    /// Interface component for the HelpSubject data
    /// </summary>
    public interface IHelpSubjectData :
        IBindingList<HelpItem>,
        ILoadData, ILoadData<IHelpKey>,
        ISaveData, ISaveData<IHelpKey>
    { }

    /// <summary>
    /// Wrapper Class for Application Help.
    /// </summary>
    public class HelpSubjectData : HelpCollection, IHelpSubjectData
    {
        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad("Load Help", this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IHelpKey helpKey)
        { return factory.CreateLoad("Load Help", this, helpKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave("Save Help", this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IHelpKey helpKey)
        { return factory.CreateSave("Save Help", this, helpKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>HelpSubject</remarks>
        public virtual new IReadOnlyList<WorkItem> Load(DataSet source)
        { return new WorkItem() { WorkName = "Load Help", DoWork = () => base.Load(source) }.ToList(); }
    }
}
