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
    /// Interface for a Securable (Security Object).
    /// </summary>
    public interface ISecurableItem : ISecurableKey, ISecurableKeyName
    {
        /// <summary>
        /// Has the Securable (Security Object) been orphaned.
        /// </summary>
        Boolean IsOrphaned { get; }
    }

    /// <summary>
    /// Implementation of a Securable (Security Object).
    /// </summary>
    [Serializable]
    public class SecurableItem : BindingTableRow, ISecurableItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? SecurableId { get { return GetValue<Guid>(nameof(SecurableId)); } private set { SetValue(nameof(SecurableId), value); } }

        /// <inheritdoc/>
        public String? SecurableTitle { get { return GetValue(nameof(SecurableTitle)); } set { SetValue(nameof(SecurableTitle), value); } }

        /// <inheritdoc/>
        public Boolean IsOrphaned
        {
            get
            {
                if (GetValue<bool>(nameof(IsOrphaned), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <summary>
        /// Constructor for SecurableItem.
        /// </summary>
        public SecurableItem() : base()
        { SecurableId = Guid.NewGuid(); }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(SecurableId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(SecurableTitle), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(IsOrphaned), typeof(String)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for SecurableItem.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected SecurableItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return new SecurableKeyName(this).ToString(); }
    }
}
