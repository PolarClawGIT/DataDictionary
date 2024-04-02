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
    public interface IDomainAttributeAliasItem : IDomainAttributeKey, IDomainAlias
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
        public string? AliasName { get { return GetValue("AliasName"); } set { SetValue("AliasName", value); } }

        /// <inheritdoc/>
        public ScopeType Scope
        {
            get { return ScopeKey.Parse(GetValue("ScopeName") ?? String.Empty).Scope; }
            set { SetValue("ScopeName", value.ToName()); OnPropertyChanged(nameof(Scope)); }
        }

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
            if (AliasName is String) { return AliasName; }
            else { return String.Empty; }
        }

    }
}
