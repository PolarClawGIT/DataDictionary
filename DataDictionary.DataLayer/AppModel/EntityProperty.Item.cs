using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model Entity Properties
    /// </summary>
    public interface IEntityPropertyItem : IEntityPropertyKey, IProperty,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for Model Entity Properties
    /// </summary>
    [Serializable]
    public class EntityPropertyItem : BindingTableRow, IEntityPropertyItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? EntityId { get { return GetValue<Guid>(nameof(EntityId)); } protected set { SetValue(nameof(EntityId), value); } }

        /// <inheritdoc/>
        public Guid? PropertyId { get { return GetValue<Guid>(nameof(PropertyId)); } set { SetValue(nameof(PropertyId), value); } }

        /// <inheritdoc/>
        public String? PropertyValue { get { return GetValue(nameof(PropertyValue)); } set { SetValue(nameof(PropertyValue), value); } }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Model Entity Properties
        /// </summary>
        public EntityPropertyItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Domain Entity Properties
        /// </summary>
        /// <param name="EntityKey"></param>
        public EntityPropertyItem(IEntityKey EntityKey) : this()
        { EntityId = EntityKey.EntityId; }

        /// <summary>
        /// Constructor for Domain Entity Properties
        /// </summary>
        /// <param name="EntityKey"></param>
        /// <param name="propertyKey"></param>
        public EntityPropertyItem(IEntityKey EntityKey, IPropertyKey propertyKey) : this()
        {
            EntityId = EntityKey.EntityId;
            PropertyId = propertyKey.PropertyId;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(EntityId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyValue), typeof(string)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Entity Properties
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected EntityPropertyItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }
        #endregion
    }
}
