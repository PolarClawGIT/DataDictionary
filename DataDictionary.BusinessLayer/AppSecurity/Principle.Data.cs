using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.AppSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <summary>
    /// Interface component for the Security Principle data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IPrincipleData :
        IBindingData<PrincipleValue>
    { }

    /// <summary>
    /// Wrapper Class for Security Principle.
    /// </summary>
    class PrincipleData : PrincipleCollection<PrincipleValue>, IPrincipleData,
        ILoadData, ILoadData<IPrincipleIndex>,
        ISaveData, ISaveData<IPrincipleIndex>
    {
        /// <inheritdoc/>
        /// <remarks>PrincipleData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PrincipleData</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IPrincipleIndex dataKey)
        { return factory.CreateLoad(this, (IPrincipleKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PrincipleData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PrincipleData</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IPrincipleIndex dataKey)
        { return factory.CreateSave(this, (IPrincipleKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>PrincipleData</remarks>
        public IReadOnlyList<WorkItem> Delete(IPrincipleIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Principle", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>PrincipleData</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Principle", DoWork = () => { this.Clear(); } }.ToList(); }

    }
}
