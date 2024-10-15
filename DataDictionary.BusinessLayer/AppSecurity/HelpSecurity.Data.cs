using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.AppGeneral;
using DataDictionary.DataLayer.AppSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <summary>
    /// Interface component for the Help Security data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IHelpSecurityData :
        IBindingData<HelpSecurityValue>
    { }

    /// <inheritdoc/>
    class HelpSecurityData : HelpSecurityCollection<HelpSecurityValue>, IHelpSecurityData,
        ILoadData, ILoadData<IHelpSubjectIndex>,
        ISaveData, ISaveData<IHelpSubjectIndex>
    {
        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IHelpSubjectIndex dataKey)
        { return factory.CreateLoad(this, (IHelpKey)dataKey).ToList(); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IHelpSubjectIndex dataKey)
        { return factory.CreateSave(this, (IHelpKey)dataKey).ToList(); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove HelpSecurity", DoWork = () => { this.Clear(); } }.ToList(); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IHelpSubjectIndex dataKey)
        { return new WorkItem() { WorkName = "Remove HelpSecurity", DoWork = () => { this.Remove(dataKey); } }.ToList(); }
    }
}
