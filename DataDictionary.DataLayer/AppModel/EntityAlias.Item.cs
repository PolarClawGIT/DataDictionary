using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model Entity Alias Items
    /// </summary>
    public interface IEntityAliasItem : IEntityKey, IAliasKey,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for Model Entity Alias Items
    /// </summary>
    [Serializable]
    public class EntityAliasItem : BindingTableRow, IEntityAliasItem
    {
        /// <inheritdoc/>
        public Guid? EntityId
        { get { return GetValue<Guid>(nameof(EntityId)); } protected set { SetValue(nameof(EntityId), value); } }

        /// <inheritdoc/>
        public String? AliasPath { get { return GetValue(nameof(AliasPath)); } set { SetValue(nameof(AliasPath), value); } }

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
        public ITemporal Temporal { get; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(EntityId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(AliasScope), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(AliasPath), typeof(String)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for Domain Entity Alias Items
        /// </summary>
        public EntityAliasItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Domain Entity Alias Items
        /// </summary>
        /// <param name="key"></param>
        public EntityAliasItem(IEntityKey key) : this()
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
        protected EntityAliasItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (AliasPath is String) { return AliasPath; }
            else { return String.Empty; }
        }

    }
}

