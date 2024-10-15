using DataDictionary.DataLayer.AppGeneral;
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
    /// Help Subject Security (Permission and Authorization)
    /// </summary>
    public interface IHelpSecurityItem :
        IHelpKey, IHelpKeyName,
        IPrincipleKey, IRoleKey,
        IObjectPermission, IObjectAuthorization
    { }

    /// <summary>
    /// Help Subject Security (Permission and Authorization)
    /// </summary>
    [Serializable]
    public class HelpSecurityItem : BindingTableRow, IHelpSecurityItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? HelpId { get { return GetValue<Guid>(nameof(HelpId)); } protected set { SetValue(nameof(HelpId), value); } }

        /// <inheritdoc/>
        public String? HelpSubject { get { return GetValue(nameof(HelpSubject)); } protected set { SetValue(nameof(HelpSubject), value); } }

        /// <inheritdoc/>
        public Guid? PrincipleId { get { return GetValue<Guid>(nameof(PrincipleId)); } protected set { SetValue(nameof(PrincipleId), value); } }

        /// <inheritdoc/>
        public Guid? RoleId { get { return GetValue<Guid>(nameof(RoleId)); } protected set { SetValue(nameof(RoleId), value); } }

        /// <inheritdoc/>
        public Boolean IsGrant
        {
            get
            {
                if (GetValue<bool>(nameof(IsGrant), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsGrant), value);
                if (value == true) { SetValue<Boolean>(nameof(IsGrant), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsDeny
        {
            get
            {
                if (GetValue<bool>(nameof(IsDeny), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsDeny), value);
                if (value == true) { SetValue<Boolean>(nameof(IsDeny), !value); }
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
        /// Constructor for HelpSecurityItem
        /// </summary>
        public HelpSecurityItem() : base()
        { }

        /// <summary>
        /// Constructor for HelpSecurityItem
        /// </summary>
        /// <param name="helpItem"></param>
        /// <param name="principleKey"></param>
        public HelpSecurityItem(IHelpKeyItem helpItem, IPrincipleKey principleKey) : this()
        {
            HelpId = helpItem.HelpId;
            HelpSubject = helpItem.HelpSubject;
            PrincipleId = principleKey.PrincipleId;
        }

        /// <summary>
        /// Constructor for HelpSecurityItem
        /// </summary>
        /// <param name="helpItem"></param>
        /// <param name="roleKey"></param>
        public HelpSecurityItem(IHelpKeyItem helpItem, IRoleKey roleKey) : this()
        {
            HelpId = helpItem.HelpId;
            HelpSubject = helpItem.HelpSubject;
            RoleId = roleKey.RoleId;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(HelpId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(PrincipleId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(RoleId), typeof(Guid)){ AllowDBNull = true},

            new DataColumn(nameof(IsGrant), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsDeny), typeof(Boolean)){ AllowDBNull = true},

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
        protected HelpSecurityItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}
