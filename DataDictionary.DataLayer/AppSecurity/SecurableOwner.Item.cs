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
    /// Interface for the Securable (Security Object) Owner Item defined for a Principal.
    /// </summary>
    public interface ISecurableOwnerItem : IPrincipalKey, ISecurableKey, ISecurableKeyName, ISecurableAuthorization
    { }

    /// <summary>
    /// Implementation of the Securable (Security Object) Owner Item defined for a Principal.
    /// </summary>
    [Serializable]
    public class SecurableOwnerItem : BindingTableRow, ISecurableOwnerItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? PrincipalId { get { return GetValue<Guid>(nameof(PrincipalId)); } protected set { SetValue(nameof(PrincipalId), value); } }

        /// <inheritdoc/>
        public Guid? SecurableId { get { return GetValue<Guid>(nameof(SecurableId)); } protected set { SetValue(nameof(SecurableId), value); } }

        /// <inheritdoc/>
        public String? SecurableTitle { get { return GetValue(nameof(SecurableTitle)); } set { SetValue(nameof(SecurableTitle), value); } }

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
        /// Constructor for SecurableOwnerItem.
        /// </summary>
        protected SecurableOwnerItem() : base()
        { }

        /// <summary>
        /// Constructor for SecurableOwnerItem.
        /// </summary>
        /// <param name="principalKey"></param>
        public SecurableOwnerItem(IPrincipalKey principalKey) : this()
        { PrincipalId = principalKey.PrincipalId; }

        /// <summary>
        /// Constructor for SecurableOwnerItem.
        /// </summary>
        /// <param name="principalKey"></param>
        /// <param name="objectKey"></param>
        public SecurableOwnerItem(IPrincipalKey principalKey, ISecurableKey objectKey) : this(principalKey)
        { SecurableId = objectKey.SecurableId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(PrincipalId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SecurableId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SecurableTitle), typeof(String)){ AllowDBNull = true},

            new DataColumn(nameof(AlterValue), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(AlterSecurity), typeof(Boolean)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for SecurableOwnerItem.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected SecurableOwnerItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return new SecurableKeyName(this).ToString(); }
    }
}
