// Ignore Spelling: Securable

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the Securable (Security Object) Permission Item defined for a Role.
    /// </summary>
    public interface ISecurablePermissionItem : IRoleKey, ISecurableKey,
        ISecurableKeyName
    {
        /// <summary>
        /// Grant permission for the object
        /// </summary>
        Boolean IsGrant { get; }

        /// <summary>
        /// Deny all permission for the object
        /// </summary>
        Boolean IsDeny { get; }
    }

    /// <summary>
    /// Implementation of the Securable (Security Object) Permission Item defined for a Role.
    /// </summary>
    [Serializable]
    public class SecurablePermissionItem : BindingTableRow, ISecurablePermissionItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? RoleId { get { return GetValue<Guid>(nameof(RoleId)); } set { SetValue(nameof(RoleId), value); } }

        /// <inheritdoc/>
        public Guid? SecurableId { get { return GetValue<Guid>(nameof(SecurableId)); } set { SetValue(nameof(SecurableId), value); } }

        /// <inheritdoc/>
        public String? SecurableTitle { get { return GetValue(nameof(SecurableTitle)); } set { SetValue(nameof(SecurableTitle), value); } }

        /// <inheritdoc/>
        public Boolean IsGrant
        {
            get
            {
                if (GetValue<Boolean>(nameof(IsGrant), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            { SetValue<Boolean>(nameof(IsGrant), value); }
        }

        /// <inheritdoc/>
        public Boolean IsDeny
        {
            get
            {
                if (GetValue<Boolean>(nameof(IsDeny), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            { SetValue<Boolean>(nameof(IsDeny), value); }
        }

        /// <summary>
        /// Constructor for SecurablePermissionItem.
        /// </summary>
        protected SecurablePermissionItem() : base()
        { }

        /// <summary>
        /// Constructor for SecurablePermissionItem.
        /// </summary>
        /// <param name="roleKey"></param>
        public SecurablePermissionItem(IRoleKey roleKey) : this()
        { RoleId = roleKey.RoleId; }

        /// <summary>
        /// Constructor for SecurablePermissionItem.
        /// </summary>
        /// <param name="securableKey"></param>
        public SecurablePermissionItem(ISecurableKey securableKey) : this()
        { SecurableId = securableKey.SecurableId; }

        /// <summary>
        /// Constructor for SecurablePermissionItem.
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="securableKey"></param>
        public SecurablePermissionItem(IRoleKey roleKey, ISecurableKey securableKey) : this(roleKey)
        { SecurableId = securableKey.SecurableId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(RoleId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SecurableId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SecurableTitle), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(IsGrant), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsDeny), typeof(Boolean)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for SecurablePermissionItem.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected SecurablePermissionItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return new SecurableKeyName(this).ToString(); }
    }
}
