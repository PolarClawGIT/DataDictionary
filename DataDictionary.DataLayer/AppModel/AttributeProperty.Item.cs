using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model Attribute Properties
    /// </summary>
    public interface IAttributePropertyItem : IAttributePropertyKey, IProperty,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for Model Attribute Properties
    /// </summary>
    [Serializable]
    public class AttributePropertyItem : BindingTableRow, IAttributePropertyItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get { return GetValue<Guid>(nameof(AttributeId)); } protected set { SetValue(nameof(AttributeId), value); } }

        /// <inheritdoc/>
        public Guid? PropertyId { get { return GetValue<Guid>(nameof(PropertyId)); } set { SetValue(nameof(PropertyId), value); } }

        /// <inheritdoc/>
        public String? PropertyValue { get { return GetValue(nameof(PropertyValue)); } set { SetValue(nameof(PropertyValue), value); } }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Domain Attribute Properties
        /// </summary>
        public AttributePropertyItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Domain Attribute Properties
        /// </summary>
        /// <param name="attributeKey"></param>
        public AttributePropertyItem(IAttributeKey attributeKey) : this()
        { AttributeId = attributeKey.AttributeId; }

        /// <summary>
        /// Constructor for Domain Attribute Properties
        /// </summary>
        /// <param name="attributeKey"></param>
        /// <param name="propertyKey"></param>
        /// <param name="value"></param>
        public AttributePropertyItem(IAttributeKey attributeKey, IPropertyKey propertyKey, AppCatalog.IPropertyItem value) : this()
        {
            AttributeId = attributeKey.AttributeId;
            PropertyId = propertyKey.PropertyId;
            PropertyValue = value.PropertyValue;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(AttributeId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyValue), typeof(string)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Attribute Properties
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected AttributePropertyItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
