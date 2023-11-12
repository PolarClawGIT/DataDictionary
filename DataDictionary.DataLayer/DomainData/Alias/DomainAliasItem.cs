using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Alias
{
    /// <summary>
    /// Interface for Domain Alias Item
    /// </summary>
    public interface IDomainAliasItem: IDomainAliasKey, IDomainAliasUniqueKey, IDomainAliasParentKey, IDataItem
    {
        /// <summary>
        /// An element (part) of the Alias Name.
        /// </summary>
        /// <remarks>
        /// Value does not contain square bracket but may contain symbols such as the period.
        /// Because of SQL indexing limits, this value cannot exceed 128 characters. (To be address at later date.)
        /// C# and VB.Net allows up to 1023 for each element of a NameSpace. 
        /// </remarks>
        String? AliasElementName { get; }
    }

    /// <summary>
    /// Implementation for Domain Alias Item
    /// </summary>
    public class DomainAliasItem : BindingTableRow, IDomainAliasItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? AliasId
        { get { return GetValue<Guid>("AliasId"); } protected set { SetValue("AliasId", value); } }

        /// <inheritdoc/>
        public Guid? AliasParentId
        { get { return GetValue<Guid>("AliasParentId"); } protected set { SetValue("AliasParentId", value); } }

        /// <inheritdoc/>
        public string? AliasName
        { get { return GetValue("AliasName"); } set { SetValue("AliasName", value); } }

        /// <inheritdoc/>
        public string? AliasElementName
        { get { return GetValue("AliasElementName"); } set { SetValue("AliasElementName", value); } }

        /// <summary>
        /// Constrictor for Domain Alias Item
        /// </summary>
        public DomainAliasItem () : base ()
        {
            if (AliasId is null) { AliasId = Guid.NewGuid(); }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("AliasId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("AliasParentId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("AliasName", typeof(string)){ AllowDBNull = false},
            new DataColumn("AliasElementName", typeof(string)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Attribute Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainAliasItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

    }
}
