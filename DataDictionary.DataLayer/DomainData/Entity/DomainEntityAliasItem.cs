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
    public interface IDomainEntityAliasItem : IDomainEntityKey, IDomainAlias
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
        public string? AliasName { get { return GetValue("AliasName"); } set { SetValue("AliasName", value); } }

        /// <inheritdoc/>
        public ScopeType Scope
        {
            get { return ScopeKey.Parse(GetValue("ScopeName") ?? String.Empty).Scope; }
            set {SetValue("ScopeName", value.ToName()); OnPropertyChanged(nameof(Scope)); }
        }

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

