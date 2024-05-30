using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.LibraryData.Member;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for Domain Entity Alias Items
    /// </summary>
    public interface IDomainEntityAliasItem : IDomainEntityKey, IAliasItem, IScopeKey
    { }

    /// <summary>
    /// Implementation for Domain Entity Alias Items
    /// </summary>
    [Serializable]
    public class DomainEntityAliasItem : BindingTableRow, IDomainEntityAliasItem
    {
        /// <inheritdoc/>
        public Guid? EntityId
        { get { return GetValue<Guid>(nameof(EntityId)); } protected set { SetValue(nameof(EntityId), value); } }

        /// <inheritdoc/>
        public String? AliasName { get { return GetValue(nameof(AliasName)); } set { SetValue(nameof(AliasName), value); } }

        /// <summary>
        /// Scope Name for the Alias.
        /// </summary>
        protected String? AliasScope { get { return GetValue(nameof(AliasScope)); } set { SetValue(nameof(AliasScope), value); } }

        /// <inheritdoc/>
        public ScopeType Scope
        {
            get { return ScopeKey.Parse(AliasScope ?? String.Empty).Scope; }
            set { AliasScope = value.ToName(); OnPropertyChanged(nameof(Scope)); }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(EntityId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(AliasName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(AliasScope), typeof(String)){ AllowDBNull = true},
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

