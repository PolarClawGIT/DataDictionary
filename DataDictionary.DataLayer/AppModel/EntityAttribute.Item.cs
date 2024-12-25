// Ignore Spelling: Nullable

using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model EntityAttribute Item
    /// </summary>
    public interface IEntityAttributeItem : IEntityAttributeKey,
        ITemporalItem
    {
        /// <summary>
        /// The Name of the Attribute as known to the Entity.
        /// </summary>
        String? AttributeName { get; }

        /// <summary>
        /// Is the Attribute Nullable
        /// </summary>
        Boolean? IsNullable { get; }

        /// <summary>
        /// The Position/Order of the Attribute
        /// </summary>
        Int32? OrdinalPosition { get; }
    }

    /// <summary>
    /// Implementation for Model EntityAttribute Item
    /// </summary>
    public class EntityAttributeItem : BindingTableRow, IEntityAttributeItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? EntityId
        {
            get { return GetValue<Guid>(nameof(EntityId)); }
            protected set { SetValue(nameof(EntityId), value); }
        }

        /// <inheritdoc/>
        public Guid? AttributeId
        {
            get { return GetValue<Guid>(nameof(AttributeId)); }
            set { SetValue(nameof(AttributeId), value); }
        }

        /// <inheritdoc/>
        public String? AttributeName
        {
            get { return GetValue(nameof(AttributeName)); }
            set { SetValue(nameof(AttributeName), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsNullable
        {
            get { return GetValue<Boolean>(nameof(IsNullable), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsNullable), value); }
        }

        /// <inheritdoc/>
        public Int32? OrdinalPosition
        {
            get { return GetValue<Int32>(nameof(OrdinalPosition)); }
            set { SetValue(nameof(OrdinalPosition), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for DomainEntityAttribute Item
        /// </summary>
        public EntityAttributeItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }


        /// <summary>
        /// Constructor for DomainEntityAttribute Item
        /// </summary>
        /// <param name="entity"></param>
        public EntityAttributeItem(IEntityKey entity) : this()
        { EntityId = entity.EntityId; }


        /// <summary>
        /// Constructor for DomainEntityAttribute Item
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="attribute"></param>
        public EntityAttributeItem(IEntityKey entity, IAttributeKey attribute) : this(entity)
        {
            EntityId = entity.EntityId;
            AttributeId = attribute.AttributeId;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(EntityId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(AttributeId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(AttributeName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(IsNullable), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(OrdinalPosition), typeof(Int32)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Entity Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected EntityAttributeItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
