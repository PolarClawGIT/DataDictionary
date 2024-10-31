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
    /// Interface for the Login Authorization Securable.
    /// </summary>
    public interface IAuthorizationSecurableItem : IPrincipalKey,
        ISecurableKey, ISecurableKeyName
    {
        /// <summary>
        /// Is the Securable owned by this Login
        /// </summary>
        Boolean IsOwner { get; }

        /// <summary>
        /// Has Grant permission been given to this Login
        /// </summary>
        Boolean IsGrant { get; }

        /// <summary>
        /// Has a Deny permission been given to this Login
        /// </summary>
        Boolean IsDeny { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class AuthorizationSecurableItem : BindingTableRow, IAuthorizationSecurableItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? PrincipalId { get { return GetValue<Guid>(nameof(PrincipalId)); } }

        /// <inheritdoc/>
        public Guid? SecurableId { get { return GetValue<Guid>(nameof(SecurableId)); } }

        /// <inheritdoc/>
        public String? SecurableTitle { get { return GetValue(nameof(SecurableTitle)); } }

        /// <inheritdoc/>
        public Boolean IsOwner
        {
            get
            {
                if (GetValue<bool>(nameof(IsOwner), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsGrant
        {
            get
            {
                if (GetValue<bool>(nameof(IsGrant), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
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
        }

        /// <summary>
        /// Constructor for AuthorizationSecurableItem.
        /// </summary>
        public AuthorizationSecurableItem() : base()
        { }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(PrincipalId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SecurableId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SecurableTitle), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(IsOwner), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsGrant), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsDeny), typeof(Boolean)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for AuthorizationItem.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected AuthorizationSecurableItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return SecurableTitle ?? String.Empty; }
    }
}
