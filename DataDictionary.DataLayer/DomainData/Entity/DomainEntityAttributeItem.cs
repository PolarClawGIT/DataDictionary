using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for DomainEntityAttribute Item
    /// </summary>
    public interface IDomainEntityAttributeItem : IDomainEntityAttributeKey, IScopeType
    {
        /// <summary>
        /// The Position/Order of the Attribute
        /// </summary>
        Nullable<Int32> OrdinalPosition { get; }
    }

    /// <summary>
    /// Implementation for DomainEntityAttribute Item
    /// </summary>
    public class DomainEntityAttributeItem : BindingTableRow, IDomainEntityAttributeItem, ISerializable
    {
        /// <inheritdoc/>
        public Nullable<Guid> EntityId
        {
            get { return GetValue<Guid>(nameof(EntityId)); }
            protected set { SetValue(nameof(EntityId), value); }
        }

        /// <inheritdoc/>
        public Nullable<Guid> AttributeId
        {
            get { return GetValue<Guid>(nameof(AttributeId)); }
            set { SetValue(nameof(AttributeId), value); }
        }

        /// <inheritdoc/>
        public Nullable<Int32> OrdinalPosition
        {
            get { return GetValue<Int32>(nameof(OrdinalPosition)); }
            set { SetValue(nameof(OrdinalPosition), value); }
        }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ModelEntityAttribute;

        /// <summary>
        /// Constructor for DomainEntityAttribute Item
        /// </summary>
        public DomainEntityAttributeItem() : base() { }


        /// <summary>
        /// Constructor for DomainEntityAttribute Item
        /// </summary>
        /// <param name="entity"></param>
        public DomainEntityAttributeItem(IDomainEntityKey entity) : this()
        {
            EntityId = entity.EntityId;
        }


        /// <summary>
        /// Constructor for DomainEntityAttribute Item
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="attribute"></param>
        public DomainEntityAttributeItem(IDomainEntityKey entity, IDomainAttributeKey attribute) : this(entity)
        {
            EntityId = entity.EntityId;
            AttributeId = attribute.AttributeId;
        }


        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(EntityId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(AttributeId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(OrdinalPosition), typeof(Int32)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Entity Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainEntityAttributeItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}
