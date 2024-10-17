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
    /// Interface for the Security Object Permission Item defined for a Role.
    /// </summary>
    public interface IObjectPermissionItem : IRoleKey, IObjectKey, IObjectKeyName, IObjectAccess, IObjectAuthorization
    { }

    /// <summary>
    /// Implementation of the Security Object Permission Item defined for a Role.
    /// </summary>
    [Serializable]
    public class ObjectPermissionItem : BindingTableRow, IObjectPermissionItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? RoleId { get { return GetValue<Guid>(nameof(RoleId)); } protected set { SetValue(nameof(RoleId), value); } }

        /// <inheritdoc/>
        public Guid? ObjectId { get { return GetValue<Guid>(nameof(ObjectId)); } set { SetValue(nameof(ObjectId), value); } }

        /// <inheritdoc/>
        public String? ObjectTitle { get { return GetValue(nameof(ObjectTitle)); } set { SetValue(nameof(ObjectTitle), value); } }

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
        /// Constructor for ObjectPermissionItem.
        /// </summary>
        protected ObjectPermissionItem() : base()
        { }

        /// <summary>
        /// Constructor for ObjectPermissionItem.
        /// </summary>
        /// <param name="roleKey"></param>
        public ObjectPermissionItem(IRoleKey roleKey) : this()
        { RoleId = roleKey.RoleId; }

        /// <summary>
        /// Constructor for ObjectPermissionItem.
        /// </summary>
        /// <param name="roleKey"></param>
        /// <param name="objectKey"></param>
        public ObjectPermissionItem(IRoleKey roleKey, IObjectKey objectKey) : this(roleKey)
        { ObjectId = objectKey.ObjectId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(RoleId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(ObjectId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(ObjectTitle), typeof(String)){ AllowDBNull = true},
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
        /// Serialization Constructor for ObjectPermissionItem.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ObjectPermissionItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return new ObjectKeyName(this).ToString(); }
    }
}
