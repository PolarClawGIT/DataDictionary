using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DomainData.Property;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for Domain Attribute Properties
    /// </summary>
    public interface IDomainAttributePropertyItem : IDomainAttributePropertyKey, IDomainProperty, IScopeKey
    { }

    /// <summary>
    /// Implementation for Domain Attribute Properties
    /// </summary>
    [Serializable]
    public class DomainAttributePropertyItem : BindingTableRow, IDomainAttributePropertyItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get { return GetValue<Guid>(nameof(AttributeId)); } protected set { SetValue(nameof(AttributeId), value); } }

        /// <inheritdoc/>
        public Guid? PropertyId { get { return GetValue<Guid>(nameof(PropertyId)); } set { SetValue(nameof(PropertyId), value); } }

        /// <inheritdoc/>
        public string? PropertyValue { get { return GetValue(nameof(PropertyValue)); } set { SetValue(nameof(PropertyValue), value); } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ModelAttributeProperty;

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
        public DomainAttributePropertyItem(IDomainAttributeKey attributeKey, IDomainPropertyKey propertyKey, IDbExtendedPropertyItem value) : this()
        {
            AttributeId = attributeKey.AttributeId;
            PropertyId = propertyKey.PropertyId;
            PropertyValue = value.PropertyValue;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(AttributeId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyValue), typeof(string)){ AllowDBNull = true},
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
