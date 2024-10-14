using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the Security Membership Item.
    /// </summary>
    public interface ISecurityMembershipItem: 
        ISecurityPrincipleKey, ISecurityRoleKey,
        IObjectSecurity
    { }

    /// <summary>
    /// Implementation of the Security Membership Item.
    /// </summary>
    [Serializable]
    public class SecurityMembershipItem : BindingTableRow, ISecurityMembershipItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? PrincipleId { get { return GetValue<Guid>(nameof(PrincipleId)); } protected set { SetValue(nameof(PrincipleId), value); } }

        /// <inheritdoc/>
        public Guid? RoleId { get { return GetValue<Guid>(nameof(RoleId)); } protected set { SetValue(nameof(RoleId), value); } }

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
        /// Constructor for SecurityMembershipItem.
        /// </summary>
        public SecurityMembershipItem() : base()
        { }

        /// <summary>
        /// Constructor for SecurityMembershipItem.
        /// </summary>
        /// <param name="principleKey"></param>
        /// <param name="roleKey"></param>
        public SecurityMembershipItem(ISecurityPrincipleKey principleKey, ISecurityRoleKey roleKey) : this ()
        {
            PrincipleId = principleKey.PrincipleId;
            RoleId = roleKey.RoleId;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(PrincipleId), typeof(Guid)){ AllowDBNull = true},
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
        protected SecurityMembershipItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}
