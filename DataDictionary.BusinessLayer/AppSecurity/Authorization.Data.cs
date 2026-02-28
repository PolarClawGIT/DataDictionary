// Ignore Spelling: Securables securable Admin

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
        IBindingData<AuthorizationValue>, ILoadData, IAuthorizationItem
    {
        /// <summary>
        /// Creates an instance of the Authorization and returns the interface.
        /// </summary>
        /// <returns></returns>
        static public IAuthorizationData Create()
        { return new AuthorizationData(); }

        /// <inheritdoc cref="AuthorizationSecurableItem.IsOwner"/>
        Boolean IsOwner(ISecurableIndex? securable);

        /// <inheritdoc cref="AuthorizationSecurableItem.IsGrant"/>
        Boolean IsGrant(ISecurableIndex? securable);

        /// <inheritdoc cref="AuthorizationSecurableItem.IsDeny"/>
        Boolean IsDeny(ISecurableIndex? securable);
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

        AuthorizationValue? Authorization
        { get { return this.FirstOrDefault(); } }

        /// <inheritdoc/>
        public Guid? PrincipalId
        {
            get
            {
                if (Authorization is not null) { return Authorization.PrincipalId; }
                else { return null; }
            }
        }

        /// <inheritdoc/>
        public String? PrincipalLogin
        {
            get
            {
                if (Authorization is not null) { return Authorization.PrincipalLogin; }
                else { return null; }
            }
        }

        /// <inheritdoc/>
        public String? PrincipalName
        {
            get
            {
                if (Authorization is not null) { return Authorization.PrincipalName; }
                else { return null; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsApplicationUser
        {
            get
            {
                if (Authorization is not null) { return Authorization.IsApplicationUser; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsSecurityAdmin
        {
            get
            {
                if (Authorization is not null) { return Authorization.IsSecurityAdmin; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsHelpAdmin
        {
            get
            {
                if (Authorization is not null) { return Authorization.IsHelpAdmin; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsHelpOwner
        {
            get
            {
                if (Authorization is not null) { return Authorization.IsHelpOwner; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsCatalogAdmin
        {
            get
            {
                if (Authorization is not null) { return Authorization.IsCatalogAdmin; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsCatalogOwner
        {
            get
            {
                if (Authorization is not null) { return Authorization.IsCatalogOwner; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsLibraryAdmin
        {
            get
            {
                if (Authorization is not null) { return Authorization.IsLibraryAdmin; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsLibraryOwner
        {
            get
            {
                if (Authorization is not null) { return Authorization.IsLibraryOwner; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsModelAdmin
        {
            get
            {
                if (Authorization is not null) { return Authorization.IsModelAdmin; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsModelOwner
        {
            get
            {
                if (Authorization is not null) { return Authorization.IsModelOwner; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsScriptAdmin
        {
            get
            {
                if (Authorization is not null) { return Authorization.IsScriptAdmin; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsScriptOwner
        {
            get
            {
                if (Authorization is not null) { return Authorization.IsScriptOwner; }
                else { return false; }
            }
        }

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
        public Boolean IsOwner(ISecurableIndex? securable)
        {
            if(securable is null) { return false; }

            SecurableIndex key = new SecurableIndex(securable);
            if (Securables.FirstOrDefault(w => key.Equals(w)) is AuthorizationSecurableItem value)
            { return value.IsOwner; }
            else { return false; }
        }

        /// <inheritdoc/>
        public Boolean IsGrant(ISecurableIndex? securable)
        {
            if (securable is null) { return false; }

            SecurableIndex key = new SecurableIndex(securable);
            if (Securables.FirstOrDefault(w => key.Equals(w)) is AuthorizationSecurableItem value)
            { return value.IsGrant; }
            else { return false; }
        }

        /// <inheritdoc/>
        public Boolean IsDeny(ISecurableIndex? securable)
        {
            if (securable is null) { return false; }

            SecurableIndex key = new SecurableIndex(securable);
            if (Securables.FirstOrDefault(w => key.Equals(w)) is AuthorizationSecurableItem value)
            { return value.IsDeny; }
            else { return false; }
        }
    }
}
