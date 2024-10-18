// Ignore Spelling: Admin

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the Logins with Application Principal and Role Permissions.
    /// </summary>
    public interface IAuthorizationItem :
        IPrincipalKey, IPrincipalKeyName,
        IPrincipalName, IRolePermissions
    {
        /// <summary>
        /// Is the current user an Application User.
        /// </summary>
        Boolean IsApplicationUser { get; }
    }

    /// <summary>
    /// Implementation of the Logins with Application Principal and Role Permissions.
    /// </summary>
    [Serializable]
    public class AuthorizationItem : BindingTableRow, IAuthorizationItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? PrincipalId { get { return GetValue<Guid>(nameof(PrincipalId)); } protected set { SetValue(nameof(PrincipalId), value); } }

        /// <inheritdoc/>
        public String? PrincipalLogin { get { return GetValue(nameof(PrincipalLogin)); } protected set { SetValue(nameof(PrincipalLogin), value); } }

        /// <inheritdoc/>
        public String? PrincipalName { get { return GetValue(nameof(PrincipalName)); } protected set { SetValue(nameof(PrincipalName), value); } }

        /// <inheritdoc/>
        public Boolean IsApplicationUser { get { return GetValue<Guid>(nameof(PrincipalId)) is Guid value && !value.Equals(Guid.Empty); } }

        /// <inheritdoc/>
        public Boolean IsSecurityAdmin
        {
            get
            {
                if (GetValue<Boolean>(nameof(IsSecurityAdmin), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsHelpAdmin
        {
            get
            {
                if (GetValue<Boolean>(nameof(IsHelpAdmin), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsHelpOwner
        {
            get
            {
                if (GetValue<Boolean>(nameof(IsHelpOwner), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsCatalogAdmin
        {
            get
            {
                if (GetValue<Boolean>(nameof(IsCatalogAdmin), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsCatalogOwner
        {
            get
            {
                if (GetValue<Boolean>(nameof(IsCatalogOwner), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsLibraryAdmin
        {
            get
            {
                if (GetValue<Boolean>(nameof(IsLibraryAdmin), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsLibraryOwner
        {
            get
            {
                if (GetValue<Boolean>(nameof(IsLibraryOwner), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsModelAdmin
        {
            get
            {
                if (GetValue<Boolean>(nameof(IsModelAdmin), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsModelOwner
        {
            get
            {
                if (GetValue<Boolean>(nameof(IsModelOwner), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsScriptAdmin
        {
            get
            {
                if (GetValue<Boolean>(nameof(IsScriptAdmin), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsScriptOwner
        {
            get
            {
                if (GetValue<Boolean>(nameof(IsScriptOwner), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }


        /// <summary>
        /// Constructor for Authorization.
        /// </summary>
        /// <exception cref="NotImplementedException">Use AuthorizationItem(IIdentity)</exception>
        public AuthorizationItem() : base()
        { }

        /// <summary>
        /// Constructor for Authorization.
        /// </summary>
        public AuthorizationItem(IIdentity identity) : base()
        {
            PrincipalId = Guid.NewGuid();

            if (String.IsNullOrWhiteSpace(identity.Name))
            {
                PrincipalLogin = "(unknown)";
                PrincipalName = "(unknown user)";
            }
            else
            {
                PrincipalLogin = identity.Name;
                PrincipalName = identity.Name.Split('\\').Last();
            }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(PrincipalLogin), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(PrincipalId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(PrincipalName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(IsSecurityAdmin), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsHelpAdmin), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsHelpOwner), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsCatalogAdmin), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsCatalogOwner), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsLibraryAdmin), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsLibraryOwner), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsModelAdmin), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsModelOwner), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsScriptAdmin), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsScriptOwner), typeof(Boolean)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public override String ToString()
        { return PrincipalName ?? String.Empty; }
    }
}
