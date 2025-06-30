using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IAttributePropertyValue : IAttributePropertyItem,
        IPropertyIndex, IAttributeIndex, IPropertySubType,
        IScopeType
    { }

    /// <inheritdoc/>
    public class AttributePropertyValue : AttributePropertyItem, IAttributePropertyValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelAttributeProperty; } }

        /// <inheritdoc cref="AttributePropertyItem.AttributePropertyItem()"/>
        public AttributePropertyValue() : base() { }

        /// <inheritdoc cref="AttributePropertyItem.AttributePropertyItem(IAttributeKey)"/>
        public AttributePropertyValue(IAttributeIndex attribute) : base(attribute) { }

        /// <inheritdoc cref="AttributePropertyItem.AttributePropertyItem(IAttributeKey, IPropertyKey)"/>
        public AttributePropertyValue(IAttributeIndex attribute, IPropertyIndex property) : base(attribute, property)
        { }


        internal static IReadOnlyList<NodePropertyValue> GetXColumns()
        {
            ScopeType scope = ScopeType.ModelAttributeProperty;
            IAttributePropertyValue attributeNames;
            IPropertyValue propertyNames;
            List<NodePropertyValue> result = new List<NodePropertyValue>()
            {
                new NodePropertyValue() {PropertyName = nameof(propertyNames.PropertyTitle),  DataType = typeof(String), AllowDBNull = false, PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(propertyNames.PropertyType),   DataType = typeof(String), AllowDBNull = true,  PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(propertyNames.PropertyData),   DataType = typeof(String), AllowDBNull = true,  PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(attributeNames.PropertyValue), DataType = typeof(String), AllowDBNull = true,  PropertyScope = scope},
            };

            return result;
        }

    }
}
