// Ignore Spelling: Securable Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.AppSecurity;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppSecurity
{

    /// <summary>
    /// Interface component for the Security Securable data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface ISecurableData :
        IBindingData<SecurableValue>
    { }

    /// <summary>
    /// Wrapper Class for Security Securable.
    /// </summary>
    class SecurableData : SecurableCollection<SecurableValue>, ISecurableData,
        ILoadData, ILoadData<ISecurableIndex>
    {
        /// <inheritdoc/>
        /// <remarks>SecurableData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>SecurableData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ISecurableIndex dataKey)
        { return factory.CreateLoad(this, (ISecurableKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>SecurableData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ISecurableIndex dataKey, DateTime asOfUtcDate)
        { return factory.CreateLoad(this, (ISecurableKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>SecurableData</remarks>
        public IReadOnlyList<WorkItem> Delete(ISecurableIndex dataKey)
        { throw new InvalidOperationException("Not Supported"); }

        /// <inheritdoc/>
        /// <remarks>SecurableData</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { throw new InvalidOperationException("Not Supported"); }

    }
}
