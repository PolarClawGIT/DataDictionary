using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.LibraryData.Member;
using System.Data;
using System.Runtime.Serialization;
using System.Text;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for Domain Entity Alias Items
    /// </summary>
    public interface IDomainEntityAliasItem : IDomainEntityKey, IDomainAliasUniqueKey, IScopeUniqueKey, IDataItem
    { }

    /// <summary>
    /// Implementation for Domain Entity Alias Items
    /// </summary>
    [Serializable]
    public class DomainEntityAliasItem : BindingTableRow, IDomainEntityAliasItem
    {
        /// <inheritdoc/>
        public Guid? EntityId
        { get { return GetValue<Guid>("EntityId"); } protected set { SetValue("EntityId", value); } }

        /// <inheritdoc/>
        public string? AliasName { get { return GetValue("AliasName"); } protected set { SetValue("AliasName", value); } }

        /// <inheritdoc/>
        public string? ScopeName { get { return GetValue("ScopeName"); } protected set { SetValue("ScopeName", value); } }


        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("EntityId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("AliasName", typeof(string)){ AllowDBNull = true},
            new DataColumn("ScopeName", typeof(string)){ AllowDBNull = true},
        };

        /// <summary>
        /// Constructor for Domain Entity Alias Items
        /// </summary>
        public DomainEntityAliasItem() : base() { }

        /// <summary>
        /// Constructor for Domain Entity Alias Items
        /// </summary>
        /// <param name="key"></param>
        public DomainEntityAliasItem(IDomainEntityKey key) : this()
        { EntityId = key.EntityId; }

        /// <summary>
        /// Constructor for Domain Entity Alias Items
        /// </summary>
        /// <param name="key"></param>
        /// <param name="source"></param>
        public DomainEntityAliasItem(IDomainEntityKey key, IDbTableItem source) : this()
        {
            EntityId = key.EntityId;
            AliasName = String.Format("[{0}].[{1}].[{2}]", source.DatabaseName, source.SchemaName, source.TableName);
            ScopeName = source.ToScopeType().ToScopeName();
        }

        /// <summary>
        /// Constructor for Domain Entity Alias Items
        /// </summary>
        /// <param name="key"></param>
        /// <param name="source"></param>
        public DomainEntityAliasItem(IDomainEntityKey key, ILibraryMemberItem source) : this()
        {
            EntityId = key.EntityId;
            AliasName = String.Format("{0}.{1}", source.NameSpace, source.MemberName);
            ScopeName = source.ToScopeType().ToScopeName();
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Entity Alias Items
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainEntityAliasItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (AliasName is String) { return AliasName; }
            else { return String.Empty; }
        }

    }
}

