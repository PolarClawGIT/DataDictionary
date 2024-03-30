using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for Domain Attribute Properties
    /// </summary>
    public interface IDomainAttributePropertyItem : IDomainAttributePropertyKey, IDomainProperty
    { }

    /// <summary>
    /// Implementation for Domain Attribute Properties
    /// </summary>
    [Serializable]
    public class DomainAttributePropertyItem : BindingTableRow, IDomainAttributePropertyItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get { return GetValue<Guid>("AttributeId"); } protected set { SetValue("AttributeId", value); } }

        /// <inheritdoc/>
        public Guid? PropertyId { get { return GetValue<Guid>("PropertyId"); } set { SetValue("PropertyId", value); } }

        /// <inheritdoc/>
        public string? PropertyValue { get { return GetValue("PropertyValue"); } set { SetValue("PropertyValue", value); } }

        /// <inheritdoc/>
        public string? DefinitionText { get { return GetValue("DefinitionText"); } set { SetValue("DefinitionText", value); } }

        /// <summary>
        /// Constructor for Domain Attribute Properties
        /// </summary>
        public DomainAttributePropertyItem() : base() { }

        /// <summary>
        /// Constructor for Domain Attribute Properties
        /// </summary>
        /// <param name="attributeKey"></param>
        public DomainAttributePropertyItem(IDomainAttributeKey attributeKey) : this()
        { AttributeId = attributeKey.AttributeId; }

        /// <summary>
        /// Constructor for Domain Attribute Properties
        /// </summary>
        /// <param name="attributeKey"></param>
        /// <param name="propertyKey"></param>
        /// <param name="value"></param>
        public DomainAttributePropertyItem(IDomainAttributeKey attributeKey, IPropertyKey propertyKey, IDbExtendedPropertyItem value) : this()
        {
            AttributeId = attributeKey.AttributeId;
            PropertyId = propertyKey.PropertyId;
            PropertyValue = value.PropertyValue;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("AttributeId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("PropertyId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("PropertyValue", typeof(string)){ AllowDBNull = true},
            new DataColumn("DefinitionText", typeof(string)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Attribute Properties
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainAttributePropertyItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}
