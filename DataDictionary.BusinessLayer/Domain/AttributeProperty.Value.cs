using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Resource.Enumerations;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributePropertyValue : IDomainAttributePropertyItem, IPropertyIndex
    { }

    /// <inheritdoc/>
    public class AttributePropertyValue : DomainAttributePropertyItem, IAttributePropertyValue
    {
        /// <inheritdoc/>
        public AttributePropertyValue() : base() { }

        /// <inheritdoc/>
        public AttributePropertyValue(IAttributeIndex attributeKey) : base(attributeKey) { }

        /// <inheritdoc/>
        public AttributePropertyValue(IDomainAttributeKey attributeKey, DataLayer.AppModel.IPropertyKey propertyKey, DataLayer.AppCatalog.IPropertyItem value) : base(attributeKey, propertyKey, value)
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
