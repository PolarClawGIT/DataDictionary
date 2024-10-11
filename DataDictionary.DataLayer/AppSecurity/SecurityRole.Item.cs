// Ignore Spelling: Admin

using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the Security Role Item.
    /// </summary>
    public interface ISecurityRoleItem :
        ISecurityRoleKey, ISecurityRoleKeyName,
        IObjectSecurity
    {
        /// <summary>
        /// Description of the Role.
        /// </summary>
        String? RoleDescription { get; }

        /// <summary>
        /// Permission: Security Administrators can insert/update/delete any Security Table.
        /// </summary>
        Boolean IsSecurityAdmin { get; }

        /// <summary>
        /// Permission: Help Administrators can insert/update/delete any Help Table.
        /// </summary>
        Boolean IsHelpAdmin { get; }

        /// <summary>
        /// Permission: Help Owner can insert/update/delete Help item they own.
        /// </summary>
        Boolean IsHelpOwner { get; }

        /// <summary>
        /// Permission: Catalog Administrators can insert/delete any Catalog (database schema) Table.
        /// </summary>
        Boolean IsCatalogAdmin { get; }

        /// <summary>
        /// Permission: Catalog Owner can insert/delete Catalog item they own.
        /// </summary>
        Boolean IsCatalogOwner { get; }

        /// <summary>
        /// Permission: Library Administrators can insert/delete any Library (code library) Table.
        /// </summary>
        Boolean IsLibraryAdmin { get; }

        /// <summary>
        /// Permission: Library Owner can insert/delete Library item they own.
        /// </summary>
        Boolean IsLibraryOwner { get; }

        /// <summary>
        /// Permission: Model Administrators can insert/update/delete any Model (Entity, Attribute, Relationship, Process Table.
        /// </summary>
        Boolean IsModelAdmin { get; }

        /// <summary>
        /// Permission: Model Owner can insert/update/delete Model item they own.
        /// </summary>
        Boolean IsModelOwner { get; }

        /// <summary>
        /// Permission: Scripting Administrators can insert/update/delete any Scripting Table.
        /// </summary>
        Boolean IsScriptAdmin { get; }

        /// <summary>
        /// Permission: Scripting Owner can insert/update/delete Scripting item they own.
        /// </summary>
        Boolean IsScriptOwner { get; }


    }

    /// <summary>
    /// Implementation of the Security Role Item.
    /// </summary>
    [Serializable]
    public class SecurityRoleItem : BindingTableRow, ISecurityRoleItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? RoleId { get { return GetValue<Guid>(nameof(RoleId)); } protected set { SetValue(nameof(RoleId), value); } }

        /// <inheritdoc/>
        public String? RoleName { get { return GetValue(nameof(RoleName)); } set { SetValue(nameof(RoleName), value); } }

        /// <inheritdoc/>
        public String? RoleDescription { get { return GetValue(nameof(RoleDescription)); } set { SetValue(nameof(RoleDescription), value); } }

        /// <inheritdoc/>
        public Boolean IsSecurityAdmin
        {
            get
            {
                if (GetValue<Boolean>(nameof(IsSecurityAdmin), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsSecurityAdmin), value);
                if (value == true) { SetValue<Boolean>(nameof(IsSecurityAdmin), !value); }
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
            set
            {
                SetValue<Boolean>(nameof(IsHelpAdmin), value);
                if (value == true) { SetValue<Boolean>(nameof(IsHelpAdmin), !value); }
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
            set
            {
                SetValue<Boolean>(nameof(IsHelpOwner), value);
                if (value == true) { SetValue<Boolean>(nameof(IsHelpOwner), !value); }
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
            set
            {
                SetValue<Boolean>(nameof(IsCatalogAdmin), value);
                if (value == true) { SetValue<Boolean>(nameof(IsCatalogAdmin), !value); }
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
            set
            {
                SetValue<Boolean>(nameof(IsCatalogOwner), value);
                if (value == true) { SetValue<Boolean>(nameof(IsCatalogOwner), !value); }
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
            set
            {
                SetValue<Boolean>(nameof(IsLibraryAdmin), value);
                if (value == true) { SetValue<Boolean>(nameof(IsLibraryAdmin), !value); }
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
            set
            {
                SetValue<Boolean>(nameof(IsLibraryOwner), value);
                if (value == true) { SetValue<Boolean>(nameof(IsLibraryOwner), !value); }
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
            set
            {
                SetValue<Boolean>(nameof(IsModelAdmin), value);
                if (value == true) { SetValue<Boolean>(nameof(IsModelAdmin), !value); }
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
            set
            {
                SetValue<Boolean>(nameof(IsModelOwner), value);
                if (value == true) { SetValue<Boolean>(nameof(IsModelOwner), !value); }
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
            set
            {
                SetValue<Boolean>(nameof(IsScriptAdmin), value);
                if (value == true) { SetValue<Boolean>(nameof(IsScriptAdmin), !value); }
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
            set
            {
                SetValue<Boolean>(nameof(IsScriptOwner), value);
                if (value == true) { SetValue<Boolean>(nameof(IsScriptOwner), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean AlterValue
        {
            get
            {
                if (GetValue<bool>(nameof(AlterValue), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean AlterSecurity
        {
            get
            {
                if (GetValue<bool>(nameof(AlterSecurity), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <summary>
        /// Constructor for SecurityRoleItem.
        /// </summary>
        public SecurityRoleItem() : base()
        { RoleId = Guid.NewGuid(); }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(RoleId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(RoleName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RoleDescription), typeof(String)){ AllowDBNull = true},

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

            new DataColumn(nameof(AlterValue), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(AlterSecurity), typeof(Boolean)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for SecurityPrincipleItem.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected SecurityRoleItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new SecurityRoleKeyName(this).ToString(); }
    }
}
