using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.LibraryData.Member;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for Domain Attribute Alias Items
    /// </summary>
    public interface IDomainAttributeAliasItem : IDomainAttributeKey, IDomainAliasUniqueKey, IScopeUniqueKey, IDataItem
    { }

    /// <summary>
    /// Implementation for Domain Attribute Alias Items
    /// </summary>
    [Serializable]
    public class DomainAttributeAliasItem : BindingTableRow, IDomainAttributeAliasItem
    {

        /// <inheritdoc/>
        public Guid? AttributeId
        { get { return GetValue<Guid>("AttributeId"); } protected set { SetValue("AttributeId", value); } }

        /// <inheritdoc/>
        public string? AliasName { get { return GetValue("AliasName"); } protected set { SetValue("AliasName", value); } }

        /// <inheritdoc/>
        public string? ScopeName { get { return GetValue("ScopeName"); } protected set { SetValue("ScopeName", value); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("AttributeId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("AliasName", typeof(string)){ AllowDBNull = true},
            new DataColumn("ScopeName", typeof(string)){ AllowDBNull = true},
        };

        /// <summary>
        /// Constructor for Domain Attribute Alias Items
        /// </summary>
        public DomainAttributeAliasItem() : base() { }

        /// <summary>
        /// Constructor for Domain Attribute Alias Items
        /// </summary>
        /// <param name="key"></param>
        public DomainAttributeAliasItem(IDomainAttributeKey key) : this()
        { AttributeId = key.AttributeId; }

        /// <summary>
        /// Constructor for Domain Attribute Alias Items
        /// </summary>
        /// <param name="key"></param>
        /// <param name="source"></param>
        public DomainAttributeAliasItem(IDomainAttributeKey key, IDbTableColumnItem source) : this()
        {
            AttributeId = key.AttributeId;
            AliasName = String.Format("[{0}].[{1}].[{2}].[{3}]",source.DatabaseName, source.SchemaName, source.TableName, source.ColumnName);
            ScopeName = source.ToScopeType().ToScopeName();
        }

        /// <summary>
        /// Constructor for Domain Attribute Alias Items
        /// </summary>
        /// <param name="key"></param>
        /// <param name="source"></param>
        public DomainAttributeAliasItem(IDomainAttributeKey key, ILibraryMemberItem source) : this()
        {
            AttributeId = key.AttributeId;
            AliasName = String.Format("{0}.{1}", source.NameSpace, source.MemberName);
            ScopeName = source.ToScopeType().ToScopeName();
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Attribute Alias Items
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainAttributeAliasItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if(AliasName is String) { return AliasName; }
            else { return String.Empty; }
        }

    }
}
