// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppSecurity;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <summary>
    /// Interface component for the Security Principal data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IPrincipalData :
        IBindingData<PrincipalValue>
    { }

    /// <summary>
    /// Wrapper Class for Security Principal.
    /// </summary>
    class PrincipalData : PrincipalCollection<PrincipalValue>, IPrincipalData,
        ILoadData, ILoadData<IPrincipalIndex>,
        ISaveData, ISaveData<IPrincipalIndex>
    {
        /// <inheritdoc/>
        /// <remarks>PrincipalData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PrincipalData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IPrincipalIndex dataKey)
        { return factory.CreateLoad(this, (IPrincipalKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PrincipalData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IPrincipalIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IPrincipalKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PrincipalData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PrincipalData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IPrincipalIndex dataKey)
        { return factory.CreateSave(this, (IPrincipalKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PrincipalData</remarks>
        public IReadOnlyList<WorkItem> Delete(IPrincipalIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Principal", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>PrincipalData</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Principal", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>PrincipalData</remarks>
        public void Remove(IPrincipalIndex dataKey)
        { base.Remove(dataKey); }
    }
}
