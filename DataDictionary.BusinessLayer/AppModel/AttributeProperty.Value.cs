using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IAttributePropertyValue : IAttributePropertyItem,
        IPropertyIndex, IAttributeIndex,
        IScopeType
    { }

    /// <inheritdoc/>
    public class AttributePropertyValue : AttributePropertyItem, IAttributePropertyValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelAttributeProperty; } }

        /// <inheritdoc/>
        public AttributePropertyValue() : base() { }

        /// <inheritdoc/>
        public AttributePropertyValue(IAttributeIndex attributeKey) : base(attributeKey) { }

        /// <inheritdoc/>
        public AttributePropertyValue(IAttributeKey attributeKey, DataLayer.AppModel.IPropertyKey propertyKey, DataLayer.AppCatalog.IPropertyItem value) : base(attributeKey, propertyKey, value)
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
