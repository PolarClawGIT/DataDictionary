// Ignore Spelling: Nullable
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model EntityAttribute Item
    /// </summary>
    public interface IEntityAttributeItem : IEntityKey,
        ITemporalItem
    {
        /// <summary>
        /// The Name of the Attribute as known to the Entity.
        /// </summary>
        String? AttributeTitle { get; }

        /// <summary>
        /// The NameSpace of the Alias of the Attribute
        /// </summary>
        String? AttributePath { get; }

        /// <summary>
        /// Is the Attribute Nullable
        /// </summary>
        Boolean? IsNullable { get; }

        /// <summary>
        /// Is the Attribute a Primary Key element
        /// </summary>
        Boolean? IsPrimaryKey { get; }

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
        public String? AttributeTitle
        {
            get { return GetValue(nameof(AttributeTitle)); }
            set { SetValue(nameof(AttributeTitle), value); }
        }

        /// <inheritdoc/>
        public String? AttributePath
        {
            get { return GetValue(nameof(AttributePath)); }
            set { SetValue(nameof(AttributePath), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsNullable
        {
            get { return GetValue<Boolean>(nameof(IsNullable), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsNullable), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsPrimaryKey
        {
            get { return GetValue<Boolean>(nameof(IsPrimaryKey), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsPrimaryKey), value); }
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
        /// Constructor for EntityAttribute Item
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
        /// Constructor for EntityAttribute Item
        /// </summary>
        /// <param name="entity"></param>
        public EntityAttributeItem(IEntityKey entity) : this()
        { EntityId = entity.EntityId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(EntityId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(AttributeTitle), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(AttributePath), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(OrdinalPosition), typeof(Int32)){ AllowDBNull = true},
            new DataColumn(nameof(IsNullable), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsPrimaryKey), typeof(Boolean)){ AllowDBNull = true},
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
