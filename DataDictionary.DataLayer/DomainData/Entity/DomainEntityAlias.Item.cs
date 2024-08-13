using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for Domain Entity Alias Items
    /// </summary>
    public interface IDomainEntityAliasItem : IDomainEntityKey, IAliasKey, IScopeType
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

        /// <inheritdoc/>
        public ScopeType AliasScope
        {
            get
            {
                String value = GetValue(nameof(AliasScope)) ?? String.Empty;
                if (ScopeEnumeration.TryParse(value, null, out ScopeEnumeration? result))
                { return result.Value; }
                else { return ScopeType.Null; }
            }
            set
            {
                if (value is ScopeType.Null) { SetValue(nameof(AliasScope), null); }
                else { SetValue(nameof(AliasScope), ScopeEnumeration.Cast(value).Name); }
            }
        }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ModelEntityAlias;

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

