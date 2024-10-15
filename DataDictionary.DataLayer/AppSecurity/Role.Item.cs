// Ignore Spelling: Admin

using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the Security Role Item.
    /// </summary>
    public interface IRoleItem :
        IRoleKey, IRoleKeyName,
        IObjectAuthorization, IRolePermissions
    {
        /// <summary>
        /// Description of the Role.
        /// </summary>
        String? RoleDescription { get; }
    }

    /// <summary>
    /// Implementation of the Security Role Item.
    /// </summary>
    [Serializable]
    public class RoleItem : BindingTableRow, IRoleItem, ISerializable
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
        public RoleItem() : base()
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
        protected RoleItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new RoleKeyName(this).ToString(); }
    }
}
