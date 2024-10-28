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
    public interface ISecurableOwnerItem : IPrincipalKey, ISecurableKey, ISecurableKeyName
    { }

    /// <summary>
    /// Implementation of the Securable (Security Object) Owner Item defined for a Principal.
    /// </summary>
    [Serializable]
    public class SecurableOwnerItem : BindingTableRow, ISecurableOwnerItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? PrincipalId { get { return GetValue<Guid>(nameof(PrincipalId)); } set { SetValue(nameof(PrincipalId), value); } }

        /// <inheritdoc/>
        public Guid? SecurableId { get { return GetValue<Guid>(nameof(SecurableId)); } set { SetValue(nameof(SecurableId), value); } }

        /// <inheritdoc/>
        public String? SecurableTitle { get { return GetValue(nameof(SecurableTitle)); } set { SetValue(nameof(SecurableTitle), value); } }

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
        /// <param name="securableKey"></param>
        public SecurableOwnerItem(ISecurableKey securableKey) : this()
        { SecurableId = securableKey.SecurableId; }

        /// <summary>
        /// Constructor for SecurableOwnerItem.
        /// </summary>
        /// <param name="principalKey"></param>
        /// <param name="securableKey"></param>
        public SecurableOwnerItem(IPrincipalKey principalKey, ISecurableKey securableKey) : this(principalKey)
        { SecurableId = securableKey.SecurableId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(PrincipalId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SecurableId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SecurableTitle), typeof(String)){ AllowDBNull = true},
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
