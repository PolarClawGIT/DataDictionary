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
    /// Interface for the Security Object Owner Item defined for a Principal.
    /// </summary>
    public interface IObjectOwnerItem : IPrincipalKey, IObjectKey, IObjectKeyName, IObjectAuthorization
    { }

    /// <summary>
    /// Implementation of the Security Object Owner Item defined for a Principal.
    /// </summary>
    [Serializable]
    public class ObjectOwnerItem : BindingTableRow, IObjectOwnerItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? PrincipalId { get { return GetValue<Guid>(nameof(PrincipalId)); } protected set { SetValue(nameof(PrincipalId), value); } }

        /// <inheritdoc/>
        public Guid? ObjectId { get { return GetValue<Guid>(nameof(ObjectId)); } protected set { SetValue(nameof(ObjectId), value); } }

        /// <inheritdoc/>
        public String? ObjectTitle { get { return GetValue(nameof(ObjectTitle)); } set { SetValue(nameof(ObjectTitle), value); } }

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
        /// Constructor for ObjectOwnerItem.
        /// </summary>
        protected ObjectOwnerItem() : base()
        { }

        /// <summary>
        /// Constructor for ObjectOwnerItem.
        /// </summary>
        /// <param name="principalKey"></param>
        public ObjectOwnerItem(IPrincipalKey principalKey) : this()
        { PrincipalId = principalKey.PrincipalId; }

        /// <summary>
        /// Constructor for ObjectOwnerItem.
        /// </summary>
        /// <param name="principalKey"></param>
        /// <param name="objectKey"></param>
        public ObjectOwnerItem(IPrincipalKey principalKey, IObjectKey objectKey) : this(principalKey)
        { ObjectId = objectKey.ObjectId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(PrincipalId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(ObjectId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(ObjectTitle), typeof(String)){ AllowDBNull = true},

            new DataColumn(nameof(AlterValue), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(AlterSecurity), typeof(Boolean)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for ObjectOwnerItem.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ObjectOwnerItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return new ObjectKeyName(this).ToString(); }
    }
}
