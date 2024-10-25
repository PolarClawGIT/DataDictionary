// Ignore Spelling: Securables securable

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
    /// Interface component for the Authorization data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IAuthorizationData :
        IBindingData<AuthorizationValue>, ILoadData
    {
        /// <summary>
        /// Creates an instance of the Authorization and returns the interface.
        /// </summary>
        /// <returns></returns>
        static public IAuthorizationData Create()
        { return new AuthorizationData(); }

        /// <inheritdoc cref="AuthorizationSecurableItem.IsOwner"/>
        Boolean IsOwner(ISecurableIndex securable);

        /// <inheritdoc cref="AuthorizationSecurableItem.IsGrant"/>
        Boolean IsGrant(ISecurableIndex securable);

        /// <inheritdoc cref="AuthorizationSecurableItem.IsDeny"/>
        Boolean IsDeny(ISecurableIndex securable);
    }


    /// <summary>
    /// Wrapper Class for Security Authorization.
    /// </summary>
    class AuthorizationData : AuthorizationCollection<AuthorizationValue>,
        IAuthorizationData
    {
        class SecurableData : AuthorizationSecurableCollection<AuthorizationSecurableItem>
        { }

        SecurableData Securables = new SecurableData();

        /// <summary>
        /// Load the Authorization.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(Delete());
            work.Add(factory.CreateLoad(this));
            work.Add(factory.CreateLoad(Securables));
            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { DoWork = Clear });
            work.Add(new WorkItem() { DoWork = Securables.Clear });            
            return work;
        }

        /// <inheritdoc/>
        public Boolean IsOwner(ISecurableIndex securable)
        {
            SecurableIndex key = new SecurableIndex(securable);
            if (Securables.FirstOrDefault(w => key.Equals(w)) is AuthorizationSecurableItem value)
            { return value.IsOwner; }
            else { return false; }
        }

        /// <inheritdoc/>
        public Boolean IsGrant(ISecurableIndex securable)
        {
            SecurableIndex key = new SecurableIndex(securable);
            if (Securables.FirstOrDefault(w => key.Equals(w)) is AuthorizationSecurableItem value)
            { return value.IsGrant; }
            else { return false; }
        }

        /// <inheritdoc/>
        public Boolean IsDeny(ISecurableIndex securable)
        {
            SecurableIndex key = new SecurableIndex(securable);
            if (Securables.FirstOrDefault(w => key.Equals(w)) is AuthorizationSecurableItem value)
            { return value.IsDeny; }
            else { return false; }
        }
    }
}
