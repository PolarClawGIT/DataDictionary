using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the Security Membership Item.
    /// </summary>
    public interface IRoleMembershipItem :
        IPrincipalKey, IRoleKey,
        IObjectAuthorization
    { }

    /// <summary>
    /// Implementation of the Security Membership Item.
    /// </summary>
    [Serializable]
    public class RoleMembershipItem : BindingTableRow, IRoleMembershipItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? PrincipalId { get { return GetValue<Guid>(nameof(PrincipalId)); } set { SetValue(nameof(PrincipalId), value); } }

        /// <inheritdoc/>
        public Guid? RoleId { get { return GetValue<Guid>(nameof(RoleId)); } set { SetValue(nameof(RoleId), value); } }

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
        /// Constructor for RoleMembershipItem.
        /// </summary>
        protected RoleMembershipItem() : base()
        { }

        /// <summary>
        /// Constructor for RoleMembershipItem.
        /// </summary>
        /// <param name="principalKey"></param>
        /// <param name="roleKey"></param>
        public RoleMembershipItem(IPrincipalKey principalKey, IRoleKey roleKey) : this()
        {
            PrincipalId = principalKey.PrincipalId;
            RoleId = roleKey.RoleId;
        }

        /// <summary>
        /// Constructor for RoleMembershipItem.
        /// </summary>
        /// <param name="principalKey"></param>
        public RoleMembershipItem(IPrincipalKey principalKey) : this()
        { PrincipalId = principalKey.PrincipalId; }

        /// <summary>
        /// Constructor for RoleMembershipItem.
        /// </summary>
        /// <param name="roleKey"></param>
        public RoleMembershipItem(IRoleKey roleKey) : this()
        { RoleId = roleKey.RoleId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(PrincipalId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(RoleId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(AlterValue), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(AlterSecurity), typeof(Boolean)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for SecurityMembershipItem.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected RoleMembershipItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}
